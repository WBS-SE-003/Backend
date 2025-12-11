import type { RequestHandler, Response } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import { User } from '#models';

const setAuthCookies = (res: Response, accessToken: string) => {
  res.cookie('accessToken', accessToken, {
    httpOnly: true,
    secure: process.env.NODE_ENV === 'production',
    maxAge: Number(process.env.ACCESS_TOKEN_TTL) * 1000,
  });
};

/*
    Check if user exist (email) [X]
        - If user exists, return an Error [X]
        - If user does not exist:
            - Secure the password using bcrypt [X]
            - Store the user in DB [X]
            - Sign a token [X]
            - Return the token [X]

*/

export const register: RequestHandler = async (req, res) => {
  const { email, password, firstName, lastName } = req.body;

  const userExists = await User.exists({ email });

  if (userExists) {
    throw new Error('Registration failed', { cause: { status: 400 } });
  }

  const hash = await bcrypt.hash(password, 10);
  // Salt rounds indicate how many times the password is hashed internally
  // The higher the value, the more secure but also slower. Max recommended: 14

  const user = await User.create({
    email,
    password: hash,
    firstName,
    lastName,
  });

  // JWT
  const token = jwt.sign({ jti: user._id }, process.env.ACCESS_JWT_SECRET!, {
    expiresIn: '1h',
    issuer: process.env.JWT_ISSUER,
  });

  //   res
  //     .status(201)
  //     .json({ message: 'registered successfully', user: user, token: token });

  // COOKIE SETTINGS
  res
    .cookie('accessToken', token, {
      httpOnly: true,
      maxAge: Number(process.env.ACCESS_TOKEN_TTL) * 1000,
    })
    .status(201)
    .json({
      messsage: 'Registered successfully',
      user,
      token,
    });

  // Easier alternative:
  //   setAuthCookies(res, token);
  //   res
  //     .status(201)
  //     .json({ message: 'Registered successfully', user: user, token: token });
};

export const login: RequestHandler = async (req, res) => {
  const { email, password } = req.body;

  const user = await User.findOne({ email }).select('+password');
  if (!user) throw new Error('Invalid credentials', { cause: { status: 401 } });

  const isMatch = await bcrypt.compare(password, user.password);
  if (!isMatch)
    throw new Error('Invalid credentials', { cause: { status: 401 } });

  const token = jwt.sign(
    { jti: user._id, roles: user.roles },
    process.env.ACCESS_JWT_SECRET!,
    {
      expiresIn: '1h',
      issuer: process.env.JWT_ISSUER,
    }
  );

  setAuthCookies(res, token);
  res.status(201).json({
    messsage: 'logged in successfully',
    user,
    token,
  });
};

// FIRST MIDDLEWARE!
export const logout: RequestHandler = async (req, res) => {
  res.clearCookie('accessToken').json({ message: 'Successfully logged out' });
};

// EksBlowfish
export const me: RequestHandler = async (req, res) => {
  const user = await User.findById(req.user?.id).select('-password');

  if (!user) {
    throw new Error('User not found', { cause: { status: 404 } });
  }

  res.json({ user });
};
