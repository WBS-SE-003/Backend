import type { Request } from 'express';

// Extend the Express Request type to include a custom 'user' property.
// This allows TypeScript to recognize 'req.user' as a valid field,
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

// This section extends the Express Request type with
// a custom 'user' property.

// Normally, types like 'Request' and 'RequestHandler'
// do not include a 'user' field (e.g., only 'req.id', 'req.body', etc.).

// By adding this, we ensure 'req.user' and 'req.user.roles'
// are available throughout the request lifecycle.

// .d.ts files only provide type definitions
// and are not compiled to JavaScript.

// We use types.d.ts to define global or project-specific types,
//  grouping them with namespaces and describing object
// structures with interfaces.
