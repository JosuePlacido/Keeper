import { Request, Response } from "express";
import { validation } from "../../middlewares";
import * as yup from 'yup';
import { StatusCodes } from "http-status-codes";
import { ChampionshipRepository } from "../../database";


interface IParamsProps {
	id?: number;
}
export const getByIdValidation = validation((getSchema) => ({
	params: getSchema<IParamsProps>(yup.object().shape({
		id: yup.number().optional().moreThan(0)
	})),
}));

export const getById = async (request: Request<IParamsProps>, response: Response) => {
	const { id } = request.params;

	if (!id) {
		return response.status(StatusCodes.BAD_REQUEST).json({
			errors: {
				default: 'O parâmetro "id" precisa ser informado.'
			}
		});
	}
	const championship = await ChampionshipRepository.getByIdWithMatchesAndRanking(id);
	return response.json(championship);
}
