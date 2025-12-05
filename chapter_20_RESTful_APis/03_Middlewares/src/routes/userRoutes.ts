import { Router } from 'express';
import { getAllUsers, getUserById, createUser } from '#controllers';
import { maintenanceMode } from '#middlewares';

const userRoutes = Router();

userRoutes.get('/', getAllUsers);
userRoutes.get('/:id', getUserById);
userRoutes.post('/', maintenanceMode, createUser);

export default userRoutes;
