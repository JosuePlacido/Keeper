import { EMatchColumnNames, EStatus, IMatch, ITeamRegister, IVacancy } from "../models";

interface IRoundRobinConfigs {
	groupId: number;
	duplicateTurn?: boolean;
	mirrorTurn?: boolean;
	isKcnockout?: boolean;
}

export interface ITeamsOrPlaces {
	id: number;
	isTeamPlace: boolean;
}

const generateRoundRobin = (teams: ITeamsOrPlaces[], { groupId, duplicateTurn = false, mirrorTurn = false, isKcnockout = false }: IRoundRobinConfigs): IMatch[] => {
	const matches: IMatch[] = [];
	const totalTeams = teams.length;
	const totalRound = totalTeams - 1 + totalTeams % 2;
	const gamesPerRound = totalTeams / 2;
	const roundReturn = duplicateTurn ? mirrorTurn ? -totalRound * 2 - 1 : totalRound : 0;
	const teamsA = teams.slice(0, gamesPerRound);
	const teamsB = teams.slice(gamesPerRound, teams.length - gamesPerRound).reverse();
	if (teams.length % 2 != 0) {
		teamsA.push({ id: 0, isTeamPlace: true });
	}

	for (let round = 1; round <= totalRound; round++) {
		let matchsCreated = 0;
		for (let game = 0; matchsCreated < gamesPerRound; game++) {
			if (teamsA[0] != null || game != 0) {
				let homeTeam = teamsB[game];
				let awayTeam = teamsA[game];
				if (game % 2 == 1 || (game == 0 && round % 2 == 1)) {
					homeTeam = teamsA[game];
					awayTeam = teamsB[game];
				}
				matches.push({
					id: 0,
					round,
					status: EStatus.Created,
					name: `Rodada ${round} - Jogo ${game + 1}`,
					homeId: homeTeam.isTeamPlace ? undefined : homeTeam.id,
					hookHomeId: homeTeam.isTeamPlace ? homeTeam.id : undefined,
					awayId: awayTeam.isTeamPlace ? undefined : awayTeam.id,
					hookAwayId: awayTeam.isTeamPlace ? awayTeam.id : undefined,
					finalGame: false,
					groupId,
					aggregateGame: false,
					penalty: false
				});
				if (roundReturn != 0) {
					matches.push({
						id: 0,
						round: Math.abs(round + roundReturn),
						status: EStatus.Created,
						name: `Rodada ${Math.abs(round + roundReturn)} - Jogo ${game + 1}`,
						awayId: teamsB[game].isTeamPlace ? undefined : teamsB[game].id,
						hookAwayId: teamsB[game].isTeamPlace ? teamsB[game].id : undefined,
						homeId: teamsA[game].isTeamPlace ? undefined : teamsA[game].id,
						hookHomeId: teamsA[game].isTeamPlace ? teamsA[game].id : undefined,
						finalGame: isKcnockout,
						groupId,
						aggregateGame: isKcnockout,
						penalty: isKcnockout
					});
				}
				matchsCreated++;
			}
		}
		teamsA.unshift(teamsB.splice(0, 1)[0]);
		teamsB.push(teamsA.splice(teamsA.length - 1, 1)[0]);
	}
	return matches;
};

export const ScheduleGenerator = {
	generateRoundRobin
};
