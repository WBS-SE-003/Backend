import type { RequestHandler } from 'express';

export const getAllArticles: RequestHandler = (req, res) => {
  res.json({ message: 'List of articles' });
};

export const getArticleById: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Fetched article with ID: ${id}` });
};

export const createArticle: RequestHandler = (req, res) => {
  res.status(201).json({
    message: 'article created successfully',
    article: req.body,
  });
};
