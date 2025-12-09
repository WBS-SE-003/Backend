import {
  createPost,
  deletePost,
  getAllPosts,
  getPostById,
  updatePost,
} from '#controllers';
import { validateBodyZod } from '#middlewares';
import { postInputSchema } from '#schemas';
import { Router } from 'express';

const postRoutes = Router();

postRoutes.get('/', getAllPosts);
postRoutes.post('/', validateBodyZod(postInputSchema), createPost);

postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', validateBodyZod(postInputSchema), updatePost);
postRoutes.delete('/:id', deletePost);

export default postRoutes;
