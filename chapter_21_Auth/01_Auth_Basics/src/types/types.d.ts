import type { Request } from 'express';

// extend the Express Request type to include a custom 'user' property.
// this allows TypeScript to recognize 'req.user' as a valid field,
// avoiding the "Property 'user' does not exist on type 'Request'" error.

declare global {
  namespace Express {
    interface Request {
      user?: {
        id: string;
        roles: string[];
      };
    }
  }
}
