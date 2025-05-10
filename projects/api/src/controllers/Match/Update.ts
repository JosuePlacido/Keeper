import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import * as yup from 'yup';
import { validation } from '../../middlewares';
import { EMatchColumnNames, EPlayerColumnNames, EStatus, ETypeEvent, IEventGame, IMatch, IPlayer, IStatistics } from '../../models';
import { MatchRepository, PlayerRepository, TeamsRepository } from '../../database';
import { GroupRepository } from '../../database/DAL/Group';
import { ranking } from '../../services';
import { EventGameRepository } from '../../database/DAL/EventGame';


interface IParamProps {
	id?: number;
}

interface IBodyProps extends Omit<IMatch, `${EMatchColumnNames.id}`> { }
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
	const result = await MatchRepository.updateById(req.params.id, req.body);
	if (result instanceof Error) {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: result.message
			}
		});
	}
	return res.status(StatusCodes.NO_CONTENT).json(result);
};

interface IregisterResult {
	homeGoals: number;
	awayGoals: number;
	homePenalties?: number;
	awayPenalties?: number;
	events: IEventGame[]
}


export const registerResult = async (req: Request<IParamProps, {}, IregisterResult>, res: Response) => {
	if (!req.params.id) {
		return res.status(StatusCodes.BAD_REQUEST).json({
			errors: {
				default: 'O parâmetro "id" precisa ser informado.'
			}
		});
	}
	const { events, ...result } = req.body;
	const transaction = await MatchRepository.getTransaction();
	try {
		const [groupId, homeTeamId, awayTeamId] = await MatchRepository.registerResult({ id: req.params.id, ...req.body }, transaction);
		await TeamsRepository.registerResultStatistics(homeTeamId, result.homeGoals, result.awayGoals, groupId, events, transaction);
		await TeamsRepository.registerResultStatistics(awayTeamId, result.awayGoals, result.homeGoals, groupId, events, transaction);
		const { rank, criterias } = await GroupRepository.getRank(groupId, transaction);
		const newRank = ranking(rank, criterias);
		await GroupRepository.updateRank(newRank, transaction);
		const playersStats: { id: number; goals: number; mvps: number; yellows: number; reds: number }[] = [];
		events.forEach(event => {
			let indexPlayer = playersStats.findIndex(ps => ps.id === event.registerPlayerId)
			if (indexPlayer < 0) {
				indexPlayer = playersStats.length;
				playersStats.push({
					id: event.registerPlayerId,
					goals: 0,
					mvps: 0,
					yellows: 0,
					reds: 0
				});
			}
			if (event.type === ETypeEvent.Goal)
				playersStats[indexPlayer].goals++;
			else if (event.type === ETypeEvent.MVP)
				playersStats[indexPlayer].mvps++;
			else if (event.type === ETypeEvent.YellowCard)
				playersStats[indexPlayer].yellows++;
			else if (event.type === ETypeEvent.RedCard)
				playersStats[indexPlayer].reds++;
		});
		await EventGameRepository.createMany(events, transaction);
		await PlayerRepository.updateRegister(playersStats, transaction);
		await MatchRepository.commit(transaction);

	} catch (e: any) {
		transaction.rollback();
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
			errors: {
				default: e.message
			}
		});
	}
	return res.status(StatusCodes.NO_CONTENT).json(result);
};
