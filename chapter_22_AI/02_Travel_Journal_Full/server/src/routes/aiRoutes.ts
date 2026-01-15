import { Router } from 'express';
import { genChat, genImage } from '#controllers';

const aiRouter = Router();

aiRouter.post('/text', genChat);
aiRouter.post('/image', genImage);

export default aiRouter;
