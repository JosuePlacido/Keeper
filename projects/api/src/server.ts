import express from 'express';
import cors from 'cors';
import 'dotenv/config';

import { JSONParseError } from './middlewares';
import './services/TranslationsYup';
import { routes } from './routes';
import { UPLOADS_FOLDER } from './config/upload';


const server = express();


server.use("/images", express.static(UPLOADS_FOLDER));

server.use(cors({
	origin: '*'//process.env.ENABLED_CORS?.split(';') || []
}));

server.use(express.json());

server.use(JSONParseError);

server.use(routes);


export { server };
