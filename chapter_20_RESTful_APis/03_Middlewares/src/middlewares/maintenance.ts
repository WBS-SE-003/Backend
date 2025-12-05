import type { RequestHandler } from 'express';

const maintenanceMode: RequestHandler = (req, res, next) => {
  const isUnderMaintanence = false;

  if (isUnderMaintanence) {
    return res.status(503).json({
      message: 'The site is under maintenance. Please try again later',
    });
  }

  next();
};

export default maintenanceMode;
