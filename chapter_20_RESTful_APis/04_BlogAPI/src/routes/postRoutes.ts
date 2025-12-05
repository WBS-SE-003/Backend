import {
  createPost,
  deletePost,
  getAllPosts,
  getPostById,
  updatePost,
} from '#controllers';
import { Router } from 'express';

const postRoutes = Router();

postRoutes.get('/', getAllPosts);
postRoutes.post('/', createPost);

postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', updatePost);
postRoutes.delete('/:id', deletePost);

export default postRoutes;
