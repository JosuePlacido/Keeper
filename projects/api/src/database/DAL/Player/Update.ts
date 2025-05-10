import { ETableNames } from '../../ETableNames';
import { EPlayerColumnNames, ERegisterPlayer, IPlayer, IRegisterPlayer } from '../../../models';
import { Knex } from '../../knex';


export const updateById = async (id: number, player: Partial<IPlayer>): Promise<void> => {
	try {
		const result = await Knex(ETableNames.player)
			.update(player)
			.where('id', '=', id);

		if (result > 0) return;
	} catch (error) {
		console.log(error);
		throw Error('Erro ao atualizar o registro');
	}
};

interface IUpdateRegisterItems { id: number; goals: number; mvps: number; yellows: number; reds: number }

export const updateRegister = async (playerStats: IUpdateRegisterItems[], transaction: any): Promise<void> => {
	try {
		for (let i = 0; i < playerStats.length; i++) {
			const currentStats: IRegisterPlayer = await transaction(ETableNames.playerRegister)
				.select('*')
				.where(ERegisterPlayer.id, '=', playerStats[i].id).first();
			await transaction(ETableNames.playerRegister)
				.update({
					goals: currentStats.goals + playerStats[i].goals,
					mvps: currentStats.mvps + playerStats[i].mvps,
					yellowCard: currentStats.yellowCard + playerStats[i].yellows,
					redCard: currentStats.redCard + playerStats[i].reds
				} as IRegisterPlayer)
				.where(ERegisterPlayer.id, '=', playerStats[i].id)
		}

	} catch (error) {
		console.log(error);
		throw new Error('Erro ao atualizar o registro');
	}
};
