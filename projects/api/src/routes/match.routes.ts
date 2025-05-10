import { Router } from "express";

import { MatchController as controller, } from "../controllers";
import { ensureAuthenticated } from "../middlewares/EnsureAuthenticated";

export const matchRoutes = Router();

matchRoutes.use(ensureAuthenticated);

matchRoutes.get("/", controller.getAll, controller.getAll);
matchRoutes.post("/", controller.create);
matchRoutes.get("/:id", controller.getById, controller.getById);
matchRoutes.post("/:id", controller.registerResult);
//productsRoutes.get("/availables", controller.index);
