import { Router } from "express";

import { TeamController as controller, } from "../controllers";
import { ensureAuthenticated } from "../middlewares/EnsureAuthenticated";
import { upload } from "../middlewares/Uploads";

export const teamRoutes = Router();

teamRoutes.use(ensureAuthenticated);

teamRoutes.get("/", controller.getAll, controller.getAll);
teamRoutes.post("/", upload.single('picture'), controller.create);
teamRoutes.put("/:id", controller.updateById);
teamRoutes.get("/:id", controller.getByIdValidation, controller.getById);
teamRoutes.delete("/:id", controller.deleteByIdValidation, controller.deleteById);
//productsRoutes.get("/availables", controller.index);*/
