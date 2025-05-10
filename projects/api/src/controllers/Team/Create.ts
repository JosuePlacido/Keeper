import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import { TeamsRepository } from '../../database';
import { validation } from '../../middlewares';
import { ImageService } from '../../services';
import { ETeamColumnNames, ITeam } from '../../models';
import fs from 'fs';
import * as yup from 'yup';


interface IBodyProps extends Omit<ITeam, ETeamColumnNames.id> { }
/*
export const createValidation = validation((getSchema) => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		nome: yup.string().required().min(3).max(150),
		logoUrl: yup.string().required().min(3).max(150),
		abrev: yup.string().required().min(3).max(3),
	})),
}));
*/
export const create = async (req: Request<{}, {}, ITeam>, res: Response) => {
	try {
		const result = await TeamsRepository.create(req.body);

		if (req.file) {
			const url = `/images/t${result}.${req.file.originalname.split('.').reverse()[0]}`
			await TeamsRepository.updateById(result, { logoURL: url });
			const newFilePath = `${req.file.destination}/t${result}.${req.file.originalname.split('.').reverse()[0]}`;
			await ImageService.resizeImage(req.file.path, newFilePath, 50, 50);
			fs.unlinkSync(req.file.path);
		}
		return res.status(StatusCodes.CREATED).json(result);
	} catch (error: any) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: error.message
			}
		});
	}
};
