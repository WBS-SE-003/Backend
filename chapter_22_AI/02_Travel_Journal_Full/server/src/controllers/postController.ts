import type { RequestHandler } from 'express';
import { Post } from '#models';
import { generateImage, generateText } from '#utils';
import { v2 as cloudinary } from 'cloudinary';

export const getAllPosts: RequestHandler = async (req, res) => {
  const posts = await Post.find()
    .sort({ createdAt: -1 })
    .lean()
    .populate('author');

  if (posts.length === 0)
    throw new Error('No post found', { cause: { status: 404 } });

  res.json(posts);
};

export const createPost: RequestHandler = async (req, res) => {
  const { title, content } = req.body;

  if (!title) {
    throw new Error('Title is required', { cause: { status: 400 } });
  }

  let finalContent = content;
  let finalImage = undefined;

  // if file uploaded, use its Cloudinary URL
  if (req.file && (req.file as any).path) {
    finalImage = (req.file as any).path;
  } else if (req.body.image) {
    // fallback if image string is still sent because why not?
    finalImage = req.body.image;
  }

  if (!finalContent) {
    try {
      finalContent = await generateText(title);
    } catch (error) {
      console.log(error);
      throw new Error('Failed to generate content', { cause: { status: 500 } });
    }
  }

  if (!finalImage) {
    try {
      // generate image with OpenAI
      const openAIImageUrl = await generateImage(title);

      // upload to Cloudinary
      const result = await cloudinary.uploader.upload(openAIImageUrl, {
        folder: 'posts',
        format: 'webp',
        resource_type: 'image',
        transformation: [{ width: 1920, height: 1080, crop: 'limit' }],
      });

      // save the uploaded image in DB
      finalImage = result.secure_url;
    } catch (error) {
      throw new Error('Failed to generate image', { cause: { status: 500 } });
    }
  }

  const post = await Post.create({
    title,
    image: finalImage,
    content: finalContent,
    author: req.user?.id,
  });

  res.status(201).json({ message: 'post created', post });
};

export const getSinglePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const post = await Post.findById(id).lean().populate('author');
  if (!post) throw new Error('Post not found', { cause: { status: 404 } });

  res.json(post);
};

export const updatePost: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const { title, image, content } = req.body;

  const post = await Post.findByIdAndUpdate(
    id,
    { title, image, content },
    { new: true }
  );

  if (!post) throw new Error('Post not found', { cause: { status: 404 } });
  res.json({ message: 'post updated', post });
};

export const deletePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  await Post.findByIdAndDelete(id);

  res.json({ message: `Post with ${id} was deleted` });
};
