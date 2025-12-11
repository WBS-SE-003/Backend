import type { ErrorRequestHandler } from 'express';

// 🧩 Central error-handling middleware for Express
// Called whenever an error is thrown anywhere in the code (throw new Error(...))
const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  // 🐞 If not in production mode, print the stack trace in red
  process.env.NODE_ENV !== 'production' &&
    console.error(`\x1b[31m${err.stack}\x1b[0m`);

  // 🔍 Checks if the error is a real Error object
  if (err instanceof Error) {
    // 📦 If the error has a "cause" with a status (e.g., { cause: { status: 404 } })
    if (err.cause) {
      const cause = err.cause as { status: number };

      // 💬 Sends the error status and message as JSON to the client
      res.status(cause.status).json({ message: err.message });
      return;
    }

    // ⚙️ If no custom status is provided → default to 500 (Internal Server Error)
    res.status(500).json({ message: err.message });
    return;
  }

  // 🚨 If the error is not an Error object → return a generic error message
  res.status(500).json({ message: 'Internal server error' });
  return;
};

export default errorHandler;
