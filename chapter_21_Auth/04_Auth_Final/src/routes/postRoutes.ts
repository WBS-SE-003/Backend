/**
 * Post Routes
 * @module routes/postRoutes
 *
 * @route GET    /posts           - Get all posts
 * @route GET    /posts/:id       - Get post by ID
 * @route POST   /posts           - Create a new post (auth required)
 * @route PUT    /posts/:id       - Update post by ID (auth & owner required)
 * @route DELETE /posts/:id       - Delete post by ID (auth & owner required)
 */
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
/**
 * Get all posts
 * @route GET /posts
 */
postRoutes.get('/', getAllPosts);
/**
 * Get post by ID
 * @route GET /posts/:id
 */
postRoutes.get('/:id', getPostById);

/* ---------- CREATE---------- */
/**
 * Create a new post
 * @route POST /posts
 * @middleware authenticate, upload, validateBodyZod
 */
postRoutes.post(
  '/',
  authenticate,
  upload.array('image', 5),
  validateBodyZod(postInputSchema),
  createPost
);

/* ---------- UPDATE---------- */
/**
 * Update post by ID
 * @route PUT /posts/:id
 * @middleware authenticate, authorize, upload, validateBodyZod
 */
postRoutes.put(
  '/:id',
  authenticate,
  authorize(Post),
  upload.array('image', 5),
  validateBodyZod(postInputSchema),
  updatePost
);

/* ---------- DELETE---------- */
/**
 * Delete post by ID
 * @route DELETE /posts/:id
 * @middleware authenticate, authorize
 */
postRoutes.delete('/:id', authenticate, authorize(Post), deletePost);

export default postRoutes;
