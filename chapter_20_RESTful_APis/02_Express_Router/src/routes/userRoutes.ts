import {
  createUser,
  deleteUser,
  getAllUsers,
  getUserById,
  updateUser,
} from '#controllers';
import { Router } from 'express';

const userRoutes = Router();

userRoutes.get('/', getAllUsers);
userRoutes.post('/', createUser);

userRoutes.get('/:id', getUserById);
userRoutes.put('/:id', updateUser);
userRoutes.delete('/:id', deleteUser);

export default userRoutes;
