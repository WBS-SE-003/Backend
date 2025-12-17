import { Schema, model } from 'mongoose';

/**
 * Post Mongoose Schema
 * @typedef Post
 * @property {string} title
 * @property {string} content
 * @property {string} author
 * @property {string[]} image_url
 * @property {Date} createdAt
 * @property {Date} updatedAt
 */
const postSchema = new Schema(
  {
    title: {
      type: String,
      required: true,
      trim: true,
    },
    content: {
      type: String,
      required: true,
    },
    author: {
      type: Schema.Types.ObjectId,
      ref: 'User',
      required: true,
    },
    image_url: {
      type: [String],
    },
  },
  { timestamps: true }
);

export default model('Post', postSchema);
