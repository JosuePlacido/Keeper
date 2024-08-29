import { Router } from "express";

import { UserController as controller, } from "../controllers";
import { ensureAuthenticated } from "../middlewares/EnsureAuthenticated";

export const userRoutes = Router();

userRoutes.post("/", controller.signInValidation, controller.signIn);
/*productsRoutes.get("/:id", controller.details);
productsRoutes.post("/", controller.create);
productsRoutes.put("/:id", controller.update);
productsRoutes.delete("/:id", controller.delete);
productsRoutes.get("/availables", controller.index);*/
