import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import * as yup from 'yup';

import { TeamsRepository } from '../../database';
import { validation } from '../../middlewares';
import { ETeamColumnNames, ITeam } from '../../models';


interface IParamProps {
	id?: number;
}

interface IBodyProps extends Omit<ITeam, ETeamColumnNames.id> { }
/*
export const updateByIdValidation = validation(getSchema => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		nome: yup.string().required().min(3),
		logoURL: yup.string().required().min(3),
		abrev: yup.string().required().min(3),
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

	const result = await TeamsRepository.updateById(req.params.id, req.body);
	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}

	return res.status(StatusCodes.NO_CONTENT).json(result);
};
