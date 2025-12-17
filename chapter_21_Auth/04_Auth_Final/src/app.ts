import express from 'express';
import cookieParser from 'cookie-parser';
import '#db';
import { authRoutes, postRoutes, userRoutes } from '#routes';
import { errorHandler } from '#middlewares';
import { openapiSpec } from '#docs';
import swaggerUI from 'swagger-ui-express';

const app = express();
const port = 3000;

app.use(express.json());
app.use(cookieParser());

// Routes
app.use('/auth', authRoutes);
app.use('/users', userRoutes);
app.use('/posts', postRoutes);

//DOC's
app.use('/docs', swaggerUI.serve, swaggerUI.setup(openapiSpec));

app.use(errorHandler);

app.listen(port, () => {
  console.log(`\x1b[35m📡 Server is running at:http://localhost:${port}\x1b`);
  console.log(
    `\x1d[31m📃 Swagger Docs available:http://localhost:${port}/docs\x1b`
  );
});
