import { Schema, model } from 'mongoose';

const userSchema = new Schema({
  firstName: {
    type: String,
    required: [true, 'firstname is missing :('],
  },
  lastName: {
    type: String,
    required: [true, 'lastName is required'],
  },
  email: {
    type: String,
    required: true,
    unique: [true, 'email is required'],
  },
  role: {
    type: String,
    default: 'user',
  },
  createdAt: {
    type: Date,
    default: Date.now,
  },
});

export default model('User', userSchema);
