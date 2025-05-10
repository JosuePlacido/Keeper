import { Knex } from '../../knex';
import { EPlayerColumnNames, ERegisterPlayer, IPlayer, IRegisterPlayer } from '../../../models';
import { ETableNames } from '../..';


export const getById = async (id: number): Promise<IPlayer | Error> => {
	try {
		const result = await Knex(ETableNames.player)
			.select('*')
			.where(EPlayerColumnNames.id, '=', id)
			.first();

		if (result) return result;

		return new Error('Registro não encontrado');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar o registro');
	}
};

export const getSquads = async (championshipId: number, teamSubscribes: number[]): Promise<IRegisterPlayer[]> => {
	try {
		const result: IRegisterPlayer[] = await Knex(`${ETableNames.playerRegister} pr`)
			.select('pr.*')
			.select(Knex.raw(`(?) as playerString`,
				Knex(`${ETableNames.player} as p`)
					.select(Knex.raw("json_object('id', p.id,'name',p.name,'mainPosition',p.mainPosition,'nickname',p.nickname)"))
					.where(EPlayerColumnNames.id, '=', Knex.ref(`pr.${ERegisterPlayer.playerId}`))))
			.whereIn(ERegisterPlayer.registerId, teamSubscribes)
			.andWhere(ERegisterPlayer.championshipId, '=', championshipId)
			.orderBy(ERegisterPlayer.registerId);

		result.forEach(pr => {
			pr.player = JSON.parse(pr.playerString!);
		});
		return result;
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao consultar o registro');
	}
};
