import { Request, Response } from "express";
import { TeamsRepository } from "../../database";
import { validation } from "../../middlewares";
import * as yup from 'yup';


interface IListQueryProps {
	page?: number;
	limit?: number;
}
export const getAllValidation = validation((getSchema) => ({
	query: getSchema<IListQueryProps>(yup.object().shape({
		page: yup.number().optional().moreThan(0),
		limit: yup.number().optional().moreThan(0)
	})),
}));

export const getAll = async (request: Request<{}, {}, {}, IListQueryProps>, response: Response) => {
	const { page, limit } = request.query;

	const data = await TeamsRepository.getAll(page, limit);
	return response.json(data);
}
