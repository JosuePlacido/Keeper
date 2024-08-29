import { IStatistics } from "../models";


function updatePositionField(teams: IStatistics[], field: keyof IStatistics) {
	let position = 1;
	let teamsDrowed = 1;
	let Drowed = true;
	let hasDrowed = false;
	for (let x = 1; x < teams.length; x++) {
		Drowed = teams[x][field] == teams[x - 1][field]
			&& (field === 'points' || teams[x].position == teams[x - 1].position);
		if (Drowed) {
			teamsDrowed++;
			hasDrowed = true;
		}
		else {
			position += teamsDrowed;
			teamsDrowed = 1;
		}
		teams[x].position = position;
	}
	return { teams, hasDrowed };
}

const CRITERIASFUNCTION: { [key: string]: (teams: IStatistics[]) => IStatistics[] } = {
	'points': (teams) => {
		return teams.sort((a, b) => b.points - a.points);
	},
	'won': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : b.won - a.won);
	},
	'goals': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : b.goals - a.goals);
	},
	'goalsDifference': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : b.goalsDifference - a.goalsDifference);
	},
	'goalsAgainst': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : a.goalsAgainst - b.goalsAgainst);
	},
	'reds': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : a.reds - b.reds);
	},
	'yellows': (teams) => {
		return teams.sort((a, b) => a.position !== b.position ? a.position - b.position : a.yellows - b.yellows);
	},
};

export function ranking(teams: IStatistics[], criterias: string) {
	const criterionList: string[] = criterias.split(',');
	for (let c = 0; c < criterionList.length; c++) {
		teams = CRITERIASFUNCTION[criterionList[c]](teams);
		const result = updatePositionField(teams, criterionList[c] as keyof IStatistics);
		teams = result.teams;
		if (!result.hasDrowed)
			break;
	}
	return teams;
}
