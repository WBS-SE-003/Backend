import { Types } from 'mongoose';
import { z } from 'zod';

export const userInputSchema = z
  .object({
    firstName: z.string({ error: 'firstname must be string' }).min(2, {
      message: 'firstName is required and must be at least 2 characters long',
    }),
    lastName: z
      .string({ error: 'lastName must be a string' })
      .min(1, { message: 'lastName must be at least 2 characters long' }),
    email: z
      .string({ error: 'email must be a string' })
      .email({ message: 'email bust be a valid email address' }),
    //   email: z
    //     .string({ error: 'email must be a string' })
    //     .regex(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, {
    //       message: 'email must be a valid email address',
    //     }),
  })
  .strict();

const objectIdRefined = z
  .string()
  .refine((v) => Types.ObjectId.isValid(v), { message: 'invalid ObjectId' });

export const postInputSchema = z.object({
  title: z
    .string({ error: 'title must be a string' })
    .min(5, { message: 'content must be at least 5 characters long' }),
  content: z
    .string({ error: 'content must be a string' })
    .min(5, { message: 'content must be at least 5 characters long' }),
  author: z
    .string({ error: 'author must be a string' })
    .min(24, { message: 'author (user id) is required' }),
  //   author: objectIdRefined,

  createdAt: z.date().optional(),
  updatedAt: z.date().optional(),
});
