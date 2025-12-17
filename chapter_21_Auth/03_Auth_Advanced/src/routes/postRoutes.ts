import { Router } from 'express';
import {
  createPost,
  deletePost,
  getAllPosts,
  getPostById,
  updatePost,
} from '#controllers';
import { authenticate, authorize, upload, validateBodyZod } from '#middlewares';
import { postInputSchema } from '#schemas';
import { Post } from '#models';

const postRoutes = Router();

/* ---------- PUBLIC ---------- */
postRoutes.get('/', getAllPosts);
postRoutes.get('/:id', getPostById);

/* ---------- CREATE---------- */
postRoutes.post(
  '/',
  authenticate,
  upload.array('image', 5),
  validateBodyZod(postInputSchema),
  createPost
);

/* ---------- UPDATE---------- */
postRoutes.put(
  '/:id',
  authenticate,
  authorize(Post),
  upload.array('image', 5),
  validateBodyZod(postInputSchema),
  updatePost
);

/* ---------- DELETE---------- */
postRoutes.delete('/:id', authenticate, authorize(Post), deletePost);

export default postRoutes;
