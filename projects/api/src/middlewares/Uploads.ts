import { NextFunction, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import multer from 'multer';
import { MULTER } from '../config/upload';

export const upload = multer(MULTER);
