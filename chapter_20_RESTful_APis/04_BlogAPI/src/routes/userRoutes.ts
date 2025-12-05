import {
  deleteUser,
  getAllUsers,
  getUserById,
  registerUser,
  updateUser,
} from '#controllers';
import { Router } from 'express';

const userRoutes = Router();

userRoutes.get('/', getAllUsers);
userRoutes.post('/', registerUser);

userRoutes.get('/:id', getUserById);
userRoutes.put('/:id', updateUser);
userRoutes.delete('/:id', deleteUser);

export default userRoutes;
