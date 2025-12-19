import type { RequestHandler } from 'express';
import OpenAI from 'openai';

const client = new OpenAI({
  apiKey: process.env.OPENAI_API_KEY,
});

export const openAInewChat: RequestHandler = async (req, res) => {
  const { prompt } = req.body;

  if (!prompt) {
    throw new Error('prompt is required', { cause: { status: 400 } });
  }

  const response = await client.responses.create({
    model: 'gpt-5.1',
    // input: prompt,
    input: [
      {
        role: 'system',
        content: 'You are a helpful assistant',
      },
      {
        role: 'user',
        content: prompt,
      },
    ],
  });

  res.json(response);
};

export const newGenerateImage: RequestHandler = async (req, res) => {
  const { prompt, size = '1024x1024' } = req.body;

  if (!prompt) {
    throw new Error('prompt is required', { cause: { status: 400 } });
  }

  const response = await client.images.generate({
    model: 'dall-e-3',
    prompt,
    size,
  });

  res.json(response);
};
