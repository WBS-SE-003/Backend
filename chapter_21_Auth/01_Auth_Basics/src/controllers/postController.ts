import type { RequestHandler } from 'express';
import { Post } from '#models';

export const getAllPosts: RequestHandler = async (req, res) => {
  const posts = await Post.find()
    .lean()
    .populate('author', 'firstName lastName');

  if (!posts.length) {
    throw new Error('No Post was found', { cause: { status: 404 } });
  }
  res.json(posts);
};

export const createPost: RequestHandler = async (req, res) => {
  const { title, image, content } = req.body;

  if (!req.user || !req.user.id) {
    throw new Error('Not Authenticated', { cause: { status: 401 } });
  }

  const post = await Post.create({
    title,
    image,
    content,
    author: req.user?.id,
  });

  res.status(201).json({ message: 'Post created', post });
};

export const getSinglePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const post = await Post.findById(id)
    .lean()
    .populate('author', 'firstName, lastName');

  if (!post) {
    throw new Error('No Post was found', { cause: { status: 404 } });
  }

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

  if (!post) {
    throw new Error('No Post was found', { cause: { status: 404 } });
  }

  res.json({ message: 'post updated', post });
};

export const deletePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  await Post.findByIdAndDelete(id);

  res.json({ message: `Post with id ${id} was deleted` });
};
