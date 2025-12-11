import { login, logout, me, register } from '#controllers';
import { authenticate } from '#middlewares';
import { Router } from 'express';

const authRouter = Router();

authRouter.post('/register', register);
authRouter.post('/login', login);
authRouter.delete('/logout', logout);
authRouter.get('/me', authenticate, me);

export default authRouter;
