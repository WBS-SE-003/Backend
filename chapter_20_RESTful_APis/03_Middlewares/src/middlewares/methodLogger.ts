import type { RequestHandler } from 'express';

const methodLogger: RequestHandler = (req, res, next) => {
  console.log(`${req.method} ${req.url}`);
  next();
};

export default methodLogger;
