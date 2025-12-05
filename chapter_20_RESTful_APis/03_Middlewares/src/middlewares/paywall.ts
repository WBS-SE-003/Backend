import type { RequestHandler } from 'express';

const payWall: RequestHandler = (req, res, next) => {
  const { userRole } = req.body;

  if (userRole === 'subscriber') {
    return next();
  }

  return res.status(402).json({
    message: 'This article is available to subscribers only :/',
  });
};

export default payWall;
