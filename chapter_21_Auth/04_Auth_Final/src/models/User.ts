import { Schema, model } from 'mongoose';

/**
 * User Mongoose Schema
 * @typedef User
 * @property {string} firstName
 * @property {string} lastName
 * @property {string} email
 * @property {string[]} roles
 * @property {Date} createdAt
 */
const userSchema = new Schema(
  {
    firstName: {
      type: String,
      required: [true, 'sasha is required'],
      trim: true,
    },
    lastName: {
      type: String,
      required: true,
      trim: true,
    },
    email: {
      type: String,
      required: true,
      unique: true,
      lowercase: true,
      trim: true,
    },
    password: {
      type: String,
      required: [true, 'password is required'],
      select: false,
    },
    roles: {
      type: [String],
      default: ['user'],
    },
    tokenVersion: {
      type: Number,
      default: 0,
      select: false,
    },
  },
  { timestamps: { createdAt: true, updatedAt: false } }
);

export default model('User', userSchema);
