import type { RequestHandler } from 'express';
import { GoogleGenAI } from '@google/genai';

const ai = new GoogleGenAI({});

export const geminiAdvChat: RequestHandler = async (req, res) => {
  const { prompt } = req.body;

  if (!prompt) {
    throw new Error('prompt is required', { cause: { status: 400 } });
  }

  const response = await ai.models.generateContent({
    model: 'gemini-2.5-flash',
    contents: [
      {
        parts: [{ text: prompt }],
      },
    ],
  });

  res.status(200).json(response);
};
