import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import * as yup from 'yup';
import { validation } from '../../middlewares';
import { ETeamColumnNames, IPlayer } from '../../models';
import { PlayerRepository } from '../../database/DAL/Player';


interface IParamProps {
	id?: number;
}

interface IBodyProps extends Omit<IPlayer, ETeamColumnNames.id> { }
/*
export const updateByIdValidation = validation(getSchema => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		nome: yup.string().required().min(3),
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
	try {
		const result = await PlayerRepository.updateById(req.params.id, req.body);
		return res.status(StatusCodes.NO_CONTENT).json(result);
	} catch (error: any) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: error.message
			}
		});
	}


};
