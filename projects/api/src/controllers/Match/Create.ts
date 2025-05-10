import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import { MatchRepository } from '../../database';
import { validation } from '../../middlewares';
import * as yup from 'yup';
import { EChampionshipColumnNames, EMatchColumnNames, IChampionship, IMatch } from '../../models';

interface IBodyProps {
	matches: Omit<IMatch, `${EMatchColumnNames.id}`>[]
}
/*
export const createValidation = validation((getSchema) => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		nome: yup.string().required().min(3).max(150),
		logoUrl: yup.string().required().min(3).max(150),
		abrev: yup.string().required().min(3).max(3),
	})),
}));
*/
export const create = async (req: Request<{}, {}, IBodyProps>, res: Response) => {
	const result = await MatchRepository.createMany(req.body.matches);

	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}

	return res.status(StatusCodes.CREATED).json(result);
};
