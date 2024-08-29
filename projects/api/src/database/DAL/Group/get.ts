import { EGroupColumnNames, EStageColumnNames, EStatisticsColumnNames, IChampionship, IStatistics } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';

export const getRank = async (groupId: number, transaction: any): Promise<{ rank: IStatistics[], criterias: string }> => {
	try {
		const result = await transaction(ETableNames.statistics)
			.select('*').where(EStatisticsColumnNames.groupId, '=', groupId);
		const { criterias } = await transaction(ETableNames.stage)
			.join(ETableNames.group, `${ETableNames.group}.${EGroupColumnNames.stageId}`, '=', `${ETableNames.stage}.${EStageColumnNames.id}`)
			.select(EStageColumnNames.criterias).where(`${ETableNames.group}.${EGroupColumnNames.id}`, '=', groupId).first();
		return { rank: result, criterias };
	} catch (error) {
		console.log(error);
		throw Error('Erro ao consultar os registros');
	}
};
