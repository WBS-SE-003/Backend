import axios from 'axios';

const OPENAI_API_KEY = process.env.OPENAI_API_KEY;
const OPENAI_API_CHAT = 'https://api.openai.com/v1/chat/completions';
const OPENAI_API_IMAGE = 'https://api.openai.com/v1/images/generations';

export async function generateText(prompt: string): Promise<string> {
  if (!prompt)
    throw new Error('Prompt is required', { cause: { status: 400 } });

  const response = await axios.post(
    OPENAI_API_CHAT,
    {
      model: 'gpt-4o',
      messages: [
        {
          role: 'system',
          content: `You are a creative travel journal blog post writer.
                        Write a detailed, engaging, and informative blog post
                        about the following topic or title.
                        The post should be well-structured, easy to read, and suitable
                        for a general audience.
                        Limit the response to 500 characters. Do not use special characters
                        like /n or line breaks.
                        Write the post as a single paragraph.                        
                        `,
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
  return response.data.choices?.[0]?.message?.content || 'failed';
}

export async function generateImage(prompt: string): Promise<string> {
  if (!prompt)
    throw new Error('Prompt is required', { cause: { status: 400 } });

  const imagePrompt = `Create a high-quality, visually appealing image suitable
    for a travel journal blog post about:
    "${prompt}". The image should be relevant, modern, and engaging.`;

  const response = await axios.post(
    OPENAI_API_IMAGE,
    {
      model: 'dall-e-3',
      prompt: imagePrompt,
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

  return response.data.data?.[0]?.url || '';
}
