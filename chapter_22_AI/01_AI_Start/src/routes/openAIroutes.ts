import { Router } from 'express';
import {
  openAIchat,
  generateImage,
  openAInewChat,
  newGenerateImage,
} from '#controllers';

const openAIrouter = Router();

openAIrouter.post('/text', openAIchat);
openAIrouter.post('/image', generateImage);

// SDK
openAIrouter.post('/responses', openAInewChat);
openAIrouter.post('/newimage', newGenerateImage);

export default openAIrouter;
