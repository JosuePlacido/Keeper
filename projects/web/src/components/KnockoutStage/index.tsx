import React, { useEffect, useRef, useState } from 'react';
import { EClassifieds, EStatus, ETypeStage, IStage, ITeam } from '../../models';
import { Container, StagePanel, StageTitle } from './styles';
import Bracket from '../Bracket';

export function KnockoutStageView() {
	/*
	const { user, userRef } = useAuth();
	const navigate = useNavigate();
	const [championship, setChampionship] = useState<IChampionship>();

	async function loadChampionships() {
		const listResponse = await api.get('championship-rank');
		const { data } = await api.get(
			`championship-rank/${listResponse.data[0].id}`
		);
		setChampionship(data);
	}
	async function handleOpenMatches(id: number) {
		navigate(`result/${id}`);
	}

	useEffect(() => {
		loadChampionships();
	}, []);

	if (!championship) {
		return <Loading />;
	}*/

	return (
		<Container>
			{stage.map((s, indexStage) => (
				<StagePanel key={s.id} stripe={indexStage % 2 === 0}>
					<StageTitle>{s.name}</StageTitle>
					{s.groups.map((g, indexGroup) => (
						<Bracket
							key={g.id}
							group={g}
							prev={indexStage > 0}
							next={
								indexStage < stage.length - 1
									? indexGroup % 2 === 0
										? 'down'
										: 'up'
									: undefined
							}
						/>
					))}
				</StagePanel>
			))}
		</Container>
	);
}

const stage: IStage[] = [
	{
		id: 1,
		criterias: 'points,goalsDifference,penalty',
		championshipId: 1,
		isDoubleTurn: true,
		name: 'nome da fase',
		order: 2,
		regulation: EClassifieds.Configured,
		typeStage: ETypeStage.Knockout,
		groups: [
			{
				id: 1,
				name: 'Quartas de final 1',
				stageId: 1,
				teams: [
					{
						id: 1,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 1,
						won: 0,
						team: {
							id: 1,
							abrev: 'SAO',
							name: 'São Paulo',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/03/11/sao-paulo.svg'
						}
					},
					{
						id: 2,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 2,
						team: {
							id: 2,
							abrev: 'CRI',
							name: 'Criciúma',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2024/03/28/Criciuma-2024.svg'
						}
					}
				],
				vacancy: [],
				statistics: [],
				matchs: [
					{
						id: 1,
						homeId: 1,
						awayId: 2,
						round: 1,
						penalty: false,
						status: EStatus.Finish,
						name: 'Ida',
						groupId: 1,
						finalGame: false,
						aggregateGame: true,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 2,
						goalsAway: 0
					},
					{
						id: 2,
						homeId: 2,
						awayId: 1,
						round: 2,
						penalty: true,
						status: EStatus.Finish,
						name: 'Volta',
						groupId: 1,
						finalGame: true,
						aggregateGame: true,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 1,
						goalsAway: 1
					}
				]
			},
			{
				id: 2,
				name: 'Quartas de final 2',
				stageId: 1,
				teams: [
					{
						id: 3,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 3,
						won: 0,
						team: {
							id: 3,
							abrev: 'BOT',
							name: 'Botafogo',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2019/02/04/botafogo-svg.svg'
						}
					},
					{
						id: 4,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 4,
						team: {
							id: 4,
							abrev: 'CRU',
							name: 'Cruzeiro',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2021/02/13/cruzeiro_2021.svg'
						}
					}
				],
				vacancy: [],
				statistics: [],
				matchs: [
					{
						id: 3,
						homeId: 3,
						awayId: 4,
						round: 1,
						penalty: false,
						status: EStatus.Finish,
						name: 'Ida',
						groupId: 2,
						finalGame: false,
						aggregateGame: true,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 2,
						goalsAway: 0
					},
					{
						id: 4,
						homeId: 4,
						awayId: 3,
						round: 2,
						penalty: true,
						status: EStatus.Finish,
						name: 'Volta',
						groupId: 2,
						finalGame: true,
						aggregateGame: true,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 4,
						goalsAway: 2,
						goalsPenaltyAway: 5,
						goalsPenaltyHome: 6
					}
				]
			},
			{
				id: 3,
				name: 'Quartas de final 3',
				stageId: 1,
				teams: [
					{
						id: 5,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 5,
						won: 0,
						team: {
							id: 5,
							abrev: 'INT',
							name: 'Internacional',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/03/11/internacional.svg'
						}
					},
					{
						id: 6,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 6,
						team: {
							id: 6,
							abrev: 'FLA',
							name: 'Flamengo',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/04/10/Flamengo-2018.svg'
						}
					}
				],
				vacancy: [],
				statistics: [],
				matchs: [
					{
						id: 5,
						homeId: 5,
						awayId: 6,
						round: 1,
						penalty: true,
						status: EStatus.Finish,
						name: 'Ida',
						groupId: 3,
						finalGame: true,
						aggregateGame: false,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 2,
						goalsAway: 1
					}
				]
			},
			{
				id: 4,
				name: 'Quartas de final 4',
				stageId: 1,
				teams: [
					{
						id: 7,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 7,
						won: 0,
						team: {
							id: 7,
							abrev: 'GRE',
							name: 'Gremio',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/03/12/gremio.svg'
						}
					},
					{
						id: 8,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 8,
						team: {
							id: 8,
							abrev: 'VAS',
							name: 'Vasco',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2021/09/04/vasco_SVG.svg'
						}
					}
				],
				vacancy: [],
				statistics: [],
				matchs: [
					{
						id: 6,
						homeId: 7,
						awayId: 8,
						round: 1,
						penalty: true,
						status: EStatus.Finish,
						name: 'Ida',
						groupId: 4,
						finalGame: true,
						aggregateGame: false,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 2,
						goalsAway: 2,
						goalsPenaltyHome: 3,
						goalsPenaltyAway: 4
					}
				]
			}
		]
	},
	{
		id: 2,
		criterias: 'points,goalsDifference,penalty',
		championshipId: 1,
		isDoubleTurn: true,
		name: 'nome da fase',
		order: 3,
		regulation: EClassifieds.Configured,
		typeStage: ETypeStage.Knockout,
		groups: [
			{
				id: 5,
				name: 'Semi-final 1',
				stageId: 1,
				teams: [
					{
						id: 1,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 1,
						won: 0,
						team: {
							id: 1,
							abrev: 'SAO',
							name: 'São Paulo',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/03/11/sao-paulo.svg'
						}
					},
					{
						id: 4,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 4,
						team: {
							id: 4,
							abrev: 'CRU',
							name: 'Cruzeiro',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2021/02/13/cruzeiro_2021.svg'
						}
					}
				],
				vacancy: [
					{
						id: 1,
						fromGroupId: 1,
						position: 1,
						description: 'Vencedor Quartas 1',
						ownGroupId: 5,
						regulation: EClassifieds.Configured
					},
					{
						id: 2,
						fromGroupId: 2,
						position: 1,
						description: 'Vencedor Quartas 2',
						ownGroupId: 5,
						regulation: EClassifieds.Configured
					}
				],
				statistics: [],
				matchs: [
					{
						id: 8,
						homeId: 1,
						awayId: 4,
						round: 1,
						penalty: true,
						status: EStatus.Finish,
						name: 'Ida',
						groupId: 5,
						finalGame: true,
						aggregateGame: false,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam,
						goalsHome: 1,
						goalsAway: 0
					},
					{
						id: 6,
						homeId: 4,
						awayId: 1,
						round: 2,
						penalty: true,
						status: EStatus.Scheduled,
						name: 'Volta',
						groupId: 5,
						finalGame: true,
						aggregateGame: true,
						awayTeam: {} as ITeam,
						homeTeam: {} as ITeam
					}
				]
			},
			{
				id: 6,
				name: 'Semi-final 2',
				stageId: 103,
				teams: [
					{
						id: 5,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						teamId: 5,
						won: 0,
						team: {
							id: 5,
							abrev: 'INT',
							name: 'Internacional',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2018/03/11/internacional.svg'
						}
					},
					{
						id: 8,
						championshipId: 1,
						drowns: 0,
						games: 0,
						goals: 0,
						goalsAgainst: 0,
						goalsDifference: 0,
						lost: 0,
						yellows: 0,
						red: 0,
						status: EStatus.Created,
						won: 0,
						teamId: 8,
						team: {
							id: 8,
							abrev: 'VAS',
							name: 'Vasco',
							logoURL:
								'https://s.sde.globo.com/media/organizations/2021/09/04/vasco_SVG.svg'
						}
					}
				],
				vacancy: [
					{
						id: 3,
						fromGroupId: 3,
						position: 1,
						description: 'Vencedor Quartas 3',
						ownGroupId: 6,
						regulation: EClassifieds.Configured
					},
					{
						id: 4,
						fromGroupId: 4,
						position: 1,
						description: 'Vencedor Quartas 4',
						ownGroupId: 6,
						regulation: EClassifieds.Configured
					}
				],
				statistics: [],
				matchs: []
			}
		]
	},
	{
		id: 3,
		criterias: 'points,goalsDifference,penalty',
		championshipId: 1,
		isDoubleTurn: true,
		name: 'nome da fase',
		order: 4,
		regulation: EClassifieds.Configured,
		typeStage: ETypeStage.Knockout,
		groups: [
			{
				id: 7,
				name: 'Final',
				stageId: 1,
				teams: [],
				vacancy: [
					{
						id: 5,
						fromGroupId: 5,
						position: 1,
						description: 'Vencedor Semi-final 1',
						ownGroupId: 7,
						regulation: EClassifieds.Configured
					},
					{
						id: 6,
						fromGroupId: 6,
						position: 1,
						description: 'Vencedor Semi-final 2',
						ownGroupId: 7,
						regulation: EClassifieds.Configured
					}
				],
				statistics: [],
				matchs: []
			}
		]
	}
];
