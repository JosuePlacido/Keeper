import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import { ChampionshipRepository, TeamsRepository } from '../../database';
import { validation } from '../../middlewares';
import * as yup from 'yup';
import { EChampionshipColumnNames, ETypeStage, IChampionship } from '../../models';
import { IChampionshipCreateScope } from '../../database/DAL/Championship/Create';
import { ITeamsOrPlaces, ScheduleGenerator } from '../../services';

interface IBodyProps extends IChampionship {

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
	const result = await ChampionshipRepository.createChampionshipMode(req.body);

	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}

	return res.status(StatusCodes.CREATED).json(result);
};

export const createScope = async (req: Request<{}, {}, IChampionshipCreateScope>, res: Response) => {
	const result = await ChampionshipRepository.createScope(req.body);

	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}

	for (let s = 0; s < result.stages!.length; s++) {
		for (let g = 0; g < result.stages![s].groups!.length; g++) {
			const teams: ITeamsOrPlaces[] = result.stages![s].groups![g].teams.map(t => {
				return {
					id: t.id,
					isTeamPlace: false
				}
			});
			teams.push(...result.stages![s].groups![g].vacancy.map(v => {
				return {
					id: v.id,
					isTeamPlace: false
				}
			}));
			result.stages![s].groups[g].matchs =
				ScheduleGenerator.generateRoundRobin(teams, {
					groupId: result.stages![s].groups[g].id,
					duplicateTurn: result.stages![s].isDoubleTurn,
					isKcnockout: result.stages![s].typeStage === ETypeStage.Knockout,
					mirrorTurn: result.stages![s].typeStage === ETypeStage.Knockout
				})
		}
	}

	return res.status(StatusCodes.CREATED).json(result);
};
