import { Request, Response } from "express";
import { validation } from "../../middlewares";
import * as yup from 'yup';
import { PlayerRepository } from "../../database";
import { StatusCodes } from "http-status-codes";


interface IQueryProps {
	id?: number;
}
export const getByIdValidation = validation((getSchema) => ({
	query: getSchema<IQueryProps>(yup.object().shape({
		id: yup.number().optional().moreThan(0)
	})),
}));

export const getById = async (request: Request<{}, {}, {}, IQueryProps>, response: Response) => {
	const { id } = request.query;

	if (!id) {
		return response.status(StatusCodes.BAD_REQUEST).json({
			errors: {
				default: 'O parâmetro "id" precisa ser informado.'
			}
		});
	}
	const data = await PlayerRepository.getById(id);
	return response.json(data);
}
