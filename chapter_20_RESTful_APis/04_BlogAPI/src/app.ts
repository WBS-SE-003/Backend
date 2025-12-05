import express from 'express';
import '#db';
import { postRoutes, userRoutes } from '#routes';

const app = express();
const port = 3000;

// body-parser
app.use(express.json());

// ROUTES
app.use('/users', userRoutes);
app.use('/posts', postRoutes);

app.listen(port, () =>
  console.log(`\x1b[35m📡 Server is running at http://localhost:${port}\x1b`)
);
