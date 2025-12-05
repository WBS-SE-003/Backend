import { createPost, getAllPosts, getPostById } from '#controllers';
import { methodLogger, timeLogger } from '#middlewares';
import { Router } from 'express';

const postRoutes = Router();

// postRoutes.use(timeLogger);

postRoutes.get('/', getAllPosts);
postRoutes.get('/:id', timeLogger, getPostById);
postRoutes.post('/', timeLogger, methodLogger, createPost);

export default postRoutes;
