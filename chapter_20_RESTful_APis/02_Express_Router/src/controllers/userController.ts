import type { RequestHandler } from 'express';

export const getAllUsers: RequestHandler = (req, res) => {
  res.json({ message: 'List of users' });
};

export const getUserById: RequestHandler = (req, res) => {
  const { id } = req.params;

  res.json({ message: `fetched user with id: ${id}` });
};

export const createUser: RequestHandler = (req, res) => {
  res.status(201).json({
    message: 'user created successfully',
    user: req.body,
  });
};

export const updateUser: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `updated user with id: ${id}` });
};

export const deleteUser: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `deleted user with id: ${id}` });
};
