import type { RequestHandler } from 'express';
import axios from 'axios';

const OPENAI_API_KEY = process.env.OPENAI_API_KEY;
const OPENAI_API_CHAT = 'https://api.openai.com/v1/chat/completions';
const OPENAI_API_IMAGE = 'https://api.openai.com/v1/images/generations';

export const openAIchat: RequestHandler = async (req, res, next) => {
  const { prompt } = req.body;

  if (!prompt) {
    throw new Error('Prompt is requred', { cause: { status: 403 } });
  }

  const response = await axios.post(
    OPENAI_API_CHAT,
    {
      model: 'gpt-5.2',
      messages: [
        {
          role: 'system',
          content: 'You are a helpful assistant',
        },
        {
          role: 'user',
          content: prompt,
        },
      ],
    },
    {
      headers: {
        Authorization: `Bearer ${OPENAI_API_KEY}`,
        'Content-Type': 'application/json',
      },
    }
  );
  res.status(200).json(response.data);
};

export const generateImage: RequestHandler = async (req, res, next) => {
  const { prompt, model = 'dall-e-3', n = 1, size = '1024x1024' } = req.body;

  if (!prompt) {
    throw new Error('Prompt is requred', { cause: { status: 403 } });
  }

  const response = await axios.post(
    OPENAI_API_IMAGE,
    {
      model,
      prompt,
      n,
      size,
    },
    {
      headers: {
        Authorization: `Bearer ${OPENAI_API_KEY}`,
        'Content-Type': 'application/json',
      },
    }
  );
  res.status(200).json(response.data);
};
