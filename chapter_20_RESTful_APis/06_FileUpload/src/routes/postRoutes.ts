import {
  createPost,
  createPost2,
  deletePost,
  getAllPosts,
  getPostById,
  updatePost,
} from '#controllers';
import { upload, validateBodyZod } from '#middlewares';
import { postInputSchema } from '#schemas';
import { Router } from 'express';

const postRoutes = Router();

postRoutes.get('/', getAllPosts);
// postRoutes.post(
//   '/',
//   upload.single('image'),
//   validateBodyZod(postInputSchema),
//   createPost
// );

postRoutes.post(
  '/',
  upload.array('image', 4),
  validateBodyZod(postInputSchema),
  createPost2
);

postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', validateBodyZod(postInputSchema), updatePost);
postRoutes.delete('/:id', deletePost);

export default postRoutes;
