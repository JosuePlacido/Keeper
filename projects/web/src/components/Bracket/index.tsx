import React, { useRef } from 'react';
import { BracketPanel, Container, DisplayTeam } from './styles';
import { Before, GoDown, GoUp } from './BracketDraw';
import { EStatus, IGroup, IMatch, ITeam } from '../../models';
import TeamDisplay from '../TeamDisplay';

interface IBracketGroupProps {
	prev?: boolean;
	next?: 'down' | 'up';
	group: IGroup;
}

export default function Bracket({
	group,
	next,
	prev = false
}: IBracketGroupProps) {
	const refContainer = useRef<HTMLDivElement>(null);
	const teamA = {
		id: group.teams.length > 0 ? group.teams[0].id : undefined,
		name:
			group.teams.length > 0
				? group.teams[0].team.name
				: group.vacancy[0].description,
		abrev:
			group.teams.length > 0
				? group.teams[0].team.abrev
				: group.vacancy[0].description,
		logoURL:
			group.teams.length > 0
				? group.teams[0].team.logoURL || 'team-default.png'
				: 'team-default.png',
		score: [] as string[],
		totalScores: 0,
		status: undefined as undefined | 'winner' | 'loser'
	};
	const teamB = {
		id: group.teams.length > 0 ? group.teams[1].id : undefined,
		name:
			group.teams.length > 0
				? group.teams[1].team.name
				: group.vacancy[1].description,
		abrev:
			group.teams.length > 0
				? group.teams[1].team.abrev
				: group.vacancy[1].description,
		logoURL:
			group.teams.length > 0
				? group.teams[1].team.logoURL || 'team-default.png'
				: 'team-default.png',
		score: [] as string[],
		totalScores: 0,
		status: undefined as undefined | 'winner' | 'loser'
	};
	let bracketFinished = true;
	group.matchs.forEach((m, i) => {
		if (
			m.status === EStatus.Finish &&
			m.goalsHome !== undefined &&
			m.goalsAway !== undefined
		) {
			if (m.homeId === teamA.id) {
				teamA.score.push(' ' + m.goalsHome);
				teamA.totalScores += m.goalsHome;
				teamB.score.push(' ' + m.goalsAway);
				teamB.totalScores += m.goalsAway;
				if (
					m.finalGame &&
					m.goalsPenaltyHome !== undefined &&
					m.goalsPenaltyAway !== undefined
				) {
					teamA.score.push(` (${m.goalsPenaltyHome})`);
					teamA.totalScores += m.goalsPenaltyHome;
					teamB.score.push(` (${m.goalsPenaltyAway})`);
					teamB.totalScores += m.goalsPenaltyAway;
				}
			} else {
				teamB.score.push(' ' + m.goalsHome);
				teamB.totalScores += m.goalsHome;
				teamA.score.push(' ' + m.goalsAway);
				teamA.totalScores += m.goalsAway;
				if (
					m.finalGame &&
					m.goalsPenaltyHome !== undefined &&
					m.goalsPenaltyAway !== undefined
				) {
					teamB.score.push(` (${m.goalsPenaltyHome})`);
					teamB.totalScores += m.goalsPenaltyHome;
					teamA.score.push(` (${m.goalsPenaltyAway})`);
					teamA.totalScores += m.goalsPenaltyAway;
				}
			}
		} else {
			bracketFinished = false;
		}
	});
	if (teamA.totalScores !== teamB.totalScores && bracketFinished) {
		teamA.status =
			teamA.totalScores > teamB.totalScores ? 'winner' : 'loser';
		teamB.status =
			teamA.totalScores < teamB.totalScores ? 'winner' : 'loser';
	}

	return (
		<Container ref={refContainer}>
			{prev && <Before />}
			<BracketPanel prev={prev} next={!!next}>
				<TeamDisplay
					team={teamA as ITeam}
					variant={
						teamA.status === 'winner'
							? 'bold'
							: teamA.status === 'loser'
							? 'opaque'
							: undefined
					}
				>
					{teamA.score.length > 0 && (
						<span className="ml-a">{teamA.score.join('')}</span>
					)}
				</TeamDisplay>
				<TeamDisplay
					team={teamB as ITeam}
					variant={
						teamB.status === 'winner'
							? 'bold'
							: teamB.status === 'loser'
							? 'opaque'
							: undefined
					}
				>
					{teamB.score.length > 0 && (
						<span className="ml-a">{teamB.score.join('')}</span>
					)}
				</TeamDisplay>
			</BracketPanel>
			{next === 'down' && (
				<GoDown
					additionalHeight={refContainer.current?.clientHeight || 0}
				/>
			)}
			{next === 'up' && (
				<GoUp
					additionalHeight={refContainer.current?.clientHeight || 0}
				/>
			)}
		</Container>
	);
}
