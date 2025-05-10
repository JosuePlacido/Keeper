import { Router } from "express";

import { ChampionshipController as controller, } from "../controllers";
import { ensureAuthenticated } from "../middlewares/EnsureAuthenticated";

export const championshipRoutes = Router();

championshipRoutes.use(ensureAuthenticated);

championshipRoutes.post("/", controller.createScope);
championshipRoutes.put("/", controller.create);
championshipRoutes.put("/:id", controller.updateById);
championshipRoutes.delete("/:id", controller.deleteByIdValidation, controller.deleteById);

export const publicChampionshipRoutes = Router();
publicChampionshipRoutes.get("/", controller.getAll, controller.getAll);
publicChampionshipRoutes.get("/:id", controller.getById);
//productsRoutes.get("/availables", controller.index);
