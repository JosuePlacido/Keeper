import { Router } from "express";

import { PlayerController as controller, } from "../controllers";
import { ensureAuthenticated } from "../middlewares/EnsureAuthenticated";
import { upload } from "../middlewares/Uploads";

export const playerRoutes = Router();

playerRoutes.use(ensureAuthenticated);

playerRoutes.get("/", controller.getAll, controller.getAll);
playerRoutes.post("/", upload.single('picture'), controller.create);
playerRoutes.put("/:id", controller.updateById);
playerRoutes.delete("/:id", controller.deleteByIdValidation, controller.deleteById);
playerRoutes.get("/:id", controller.getById, controller.getById);
playerRoutes.get("/squads", controller.getSquads);
//productsRoutes.get("/availables", controller.index);
