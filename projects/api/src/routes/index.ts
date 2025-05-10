import swaggerUI from 'swagger-ui-express';
import express, { Router } from "express";

import { teamRoutes } from "./teams.routes";
import { playerRoutes } from "./player.routes";
import { userRoutes } from "./user.routes";
import { championshipRoutes, publicChampionshipRoutes } from "./championship.routes";
import { matchRoutes } from "./match.routes";
import { PasswordCrypto } from '../services';
import { UPLOADS_FOLDER } from '../config/upload';
/*import { usersRoutes } from "./users.routes";
import { productsRoutes } from "./products.routes";
import { productImagesRoutes } from "./productsImages.routes";
*/
export const routes = Router();
const swaggerFile = require('../swagger_documentation.json')

routes.get('/', async (_, res) => {

	return res.send(await PasswordCrypto.hashPassword('admin'));
});
routes.use('/docs', swaggerUI.serve, swaggerUI.setup(swaggerFile));
routes.use("/user", userRoutes);
routes.use("/teams", teamRoutes);
routes.use("/player", playerRoutes);
routes.use("/championship", championshipRoutes);
routes.use("/championship-rank", publicChampionshipRoutes);
routes.use("/match", matchRoutes);
/*
routes.use("/users", usersRoutes);
routes.use("/sessions", sessionsRoutes);

routes.use("/products/images", productImagesRoutes);
routes.use("/products", productsRoutes);*/
