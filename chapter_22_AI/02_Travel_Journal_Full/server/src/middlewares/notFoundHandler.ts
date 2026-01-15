import type { RequestHandler } from 'express';

const notFoundHanlder: RequestHandler = (req, res, next) => {
  next(new Error('Not Found', { cause: { status: 404 } }));
};

export default notFoundHanlder;
