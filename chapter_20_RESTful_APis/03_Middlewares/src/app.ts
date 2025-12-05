import express from 'express';
import { articleRoutes, postRoutes, userRoutes } from '#routes';
import {
  errorHandler,
  maintenanceMode,
  methodLogger,
  timeLogger,
} from '#middlewares';

const app = express();
const port = 3000;

// bodyparser
app.use(express.json());

// app.use(timeLogger);
// app.use(methodLogger);
// app.use(maintenanceMode);

// ROUTES
app.use('/users', userRoutes);
app.use('/posts', postRoutes);
app.use('/articles', articleRoutes);

// Error Handler
app.use(errorHandler);

app.listen(port, () =>
  console.log(`\x1b[35m📡 Server is running at http://localhost:${port}\x1b`)
);
