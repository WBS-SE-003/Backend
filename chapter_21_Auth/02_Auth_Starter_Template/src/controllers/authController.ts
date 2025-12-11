import type { RequestHandler, Response } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import { User } from '#models';

/*
    Check if user exist (email) [X]
        - If user exists, return an Error [X]
        - If user does not exist:
            - Secure the password using bcrypt [X]
            - Store the user in DB [X]
            - Sign a token [X]
            - Return the token [X]

*/

export const register: RequestHandler = async (req, res) => {};

export const login: RequestHandler = async (req, res) => {};

export const logout: RequestHandler = async (req, res) => {};

export const me: RequestHandler = async (req, res) => {};
