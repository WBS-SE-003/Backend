import { Router } from 'express';
import { authenticate, authorize } from '#middlewares';
import {
  getAllPosts,
  createPost,
  getSinglePost,
  updatePost,
  deletePost,
} from '#controllers';

const postRouter = Router();

// prettier-ignore
postRouter
    .route('/')
    .get(getAllPosts)
    .post(authenticate, createPost);

// prettier-ignore
postRouter
  .route('/:id')
  .get(getSinglePost)
  .put(authenticate, authorize, updatePost)
  .delete(authenticate, authorize, deletePost)

export default postRouter;
