import type { RequestHandler } from 'express';
import axios from 'axios';
import { Post } from '#models';

const OPENAI_API_KEY = process.env.OPENAI_API_KEY;
const OPENAI_API_CHAT = 'https://api.openai.com/v1/chat/completions';
const OPENAI_API_IMAGE = 'https://api.openai.com/v1/images/generations';

export const genChat: RequestHandler = async (req, res) => {
  const { prompt, postId } = req.body;

  if (!prompt) {
    throw new Error('Prompt is required', { cause: { status: 403 } });
  }

  let systemPrompt = `You are a helpful assistant for a travel journal website. 
                      Do not use line breaks or /n in your answers. Always answer in a single paragraph.`;

  if (postId) {
    const post = await Post.findById(postId).populate('author').lean();
    if (post) {
      const { title, content, author } = post;
      systemPrompt += `Here is the post context you should use to answer questions:`;
      if (title) systemPrompt += `Title: ${title}\n`;
      if (content) systemPrompt += `Content: ${content}\n`;
      if (author) {
        if (
          author &&
          typeof author === 'object' &&
          ('firstName' in author || 'lastName' in author)
        ) {
          const a = author as { firstName?: string; lastName?: string };
          systemPrompt += `Author: ${a.firstName || ''} ${a.lastName || ''}\n`;
        }
      }
      systemPrompt += `Answer as if you are an expert on this post.`;
    }
  }

  const response = await axios.post(
    OPENAI_API_CHAT,
    {
      model: 'gpt-4o',
      messages: [
        {
          role: 'system',
          content: systemPrompt,
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
  res.json(response.data);
};

export const genImage: RequestHandler = async (req, res) => {
  const { prompt } = req.body;

  if (!prompt) {
    throw new Error('Prompt is required', { cause: { status: 403 } });
  }

  const response = await axios.post(
    OPENAI_API_IMAGE,
    {
      model: 'dall-e-3',
      prompt,
      n: 1,
      size: '1024x1024',
    },
    {
      headers: {
        Authorization: `Bearer ${OPENAI_API_KEY}`,
        'Content-Type': 'application/json',
      },
    }
  );
  res.json(response.data);
};
