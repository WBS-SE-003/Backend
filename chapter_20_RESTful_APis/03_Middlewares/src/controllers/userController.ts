import type { RequestHandler } from 'express';

export const getAllUsers: RequestHandler = (req, res) => {
  // throw new Error();
  throw new Error('Error getting user', { cause: 418 });
  res.json({ message: 'List of users' });
};

export const getUserById: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Fetched user with ID: ${id}` });
};

export const createUser: RequestHandler = (req, res) => {
  res.status(201).json({
    message: 'user created successfully',
    user: req.body,
  });
};
