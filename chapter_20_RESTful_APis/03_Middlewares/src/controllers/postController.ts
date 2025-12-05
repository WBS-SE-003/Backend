import type { RequestHandler } from 'express';

export const getAllPosts: RequestHandler = (req, res) => {
  res.json({ message: 'List of posts' });
};

export const getPostById: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Fetched Post with ID: ${id}` });
};

export const createPost: RequestHandler = (req, res) => {
  res.status(201).json({
    message: 'Post created successfully',
    post: req.body,
  });
};
