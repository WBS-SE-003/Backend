import { Post } from '#models';
import type { RequestHandler } from 'express';

export const createPost: RequestHandler = async (req, res) => {
  const { title, content, author } = req.body;
  const image = req.file;

  if (!title || !content || !author) {
    return res.status(400).json({ message: 'Missing required fields' });
  }

  const newPost = await Post.create({
    title,
    content,
    author,
    image_url: image?.path,
  });

  console.log('cloudinary upload result', image);
  res.json(newPost);
};

export const createPost2: RequestHandler = async (req, res) => {
  const { title, content, author } = req.body;
  const files = (req.files as Express.Multer.File[]) || [];

  const imageUrls = files.map((file) => file.path);

  const newPost = await Post.create({
    title,
    content,
    author,
    image_url: imageUrls,
  });

  console.log('cloudinary multiple upload results', files);
  res.status(201).json(newPost);
};

export const getAllPosts: RequestHandler = async (req, res) => {
  const posts = await Post.find().populate('author', 'firstName lastName');

  // if (!posts.length) return res.status(404).json({ message: 'No post found' });

  if (!posts.length) throw new Error('post not found', { cause: 404 });
  // if (!posts.length)
  //   throw new Error('post not found', { cause: { status: 404 } });

  res.json(posts);
};

export const getPostById: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const post = await Post.findById(id).populate('author', 'firstName lastName');

  if (!post) return res.status(404).json({ message: 'Post not found' });

  res.json(post);
};

export const updatePost: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const { title, content, author } = req.body;

  const updatedPost = await Post.findByIdAndUpdate(
    id,
    { title, content, author },
    { new: true, runValidators: true }
  ).populate('author', 'firstName lastName');

  res
    .status(200)
    .json({ message: 'post updated successfully', post: updatedPost });
};

export const deletePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const deletedPost = await Post.findByIdAndDelete(id);

  if (!deletedPost) return res.status(404).json({ message: 'Post not found' });

  res.json({ message: `Post with id: ${id} was deleted` });
};
