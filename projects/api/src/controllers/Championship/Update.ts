import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import * as yup from 'yup';
import { validation } from '../../middlewares';
import { ChampionshipRepository } from '../../database';
import { EStatus } from '../../models';


interface IParamProps {
	id?: number;
}

interface IBodyProps {
	name: string;
	category: string;
	season: string;
	isSketch: boolean;
	status: EStatus;
}
/*
export const updateByIdValidation = validation(getSchema => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		name: yup.string().required(),
		category: yup.string().required(),
		season: yup.string().required(),
		status: yup.
	})),
	params: getSchema<IParamProps>(yup.object().shape({
		id: yup.number().integer().required().moreThan(0),
	})),
}));*/

export const updateById = async (req: Request<IParamProps, {}, IBodyProps>, res: Response) => {
	if (!req.params.id) {
		return res.status(StatusCodes.BAD_REQUEST).json({
			errors: {
				default: 'O parâmetro "id" precisa ser informado.'
			}
		});
	}
	const result = await ChampionshipRepository.updateById(req.params.id, req.body);
	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}
	return res.status(StatusCodes.NO_CONTENT).json(result);
};
