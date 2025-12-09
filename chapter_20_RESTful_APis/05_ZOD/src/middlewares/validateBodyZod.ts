import type { RequestHandler } from 'express';
import { ZodObject } from 'zod/v4';

const validateBodyZod =
  (ZodSchema: ZodObject): RequestHandler =>
  (req, res, next) => {
    const parsed = ZodSchema.safeParse(req.body);
    console.log(parsed?.error?.issues);

    if (!parsed.success) {
      const issues = parsed.error.issues.map((issue) => ({
        path: issue.path.join(),
        message: issue.message,
      }));

      return res.status(400).json({
        validationError: 'Validation failed',
        issues,
      });
    }

    req.body = parsed.data;
    next();
  };

export default validateBodyZod;
