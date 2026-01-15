import { geminiAdvChat } from '#controllers';
import { Router } from 'express';

const geminiRouter = Router();

geminiRouter.post('/text', geminiAdvChat);

export default geminiRouter;
