import '#db';
import express from 'express';
import cookieParser from 'cookie-parser';
import { aiRouter, authRouter, postRouter } from '#routes';
import { errorHandler, notFoundHanlder } from '#middlewares';
import cors from 'cors';

const app = express();
const port = process.env.PORT || 3000;

app.use(
  cors({
    origin: process.env.CLIENT_BASE_URL,
    credentials: true,
    exposedHeaders: ['WWW-Authenticate'],
  })
);

app.use(express.json(), cookieParser());

app.use('/auth', authRouter);
app.use('/posts', postRouter);
app.use('/ai', aiRouter);

app.use('*splat', notFoundHanlder);

app.use(errorHandler);

app.listen(port, () =>
  console.log(`\x1b[31mServer listening at http://localhost:${port}\x1b[0m`)
);
