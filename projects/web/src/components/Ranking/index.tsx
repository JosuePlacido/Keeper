import React, { InputHTMLAttributes, useEffect, useRef, useState } from 'react';
import {
	Cell,
	CellHeader,
	Container,
	Line,
	TeamDisplayFull,
	Title
} from './styles';
import { PiArrowLeft, PiArrowRight } from 'react-icons/pi';
import { IGroup, IStage, IStatistics } from '../../models';
import ButtonIcon from '../ButtonIcon';
import { api } from '../../services/api';
import TeamDisplay from '../TeamDisplay';

interface ISliderStageAttributes {
	data: IStatistics[];
}
const Ranking = ({ data }: ISliderStageAttributes) => {
	return (
		<Container>
			<table>
				<thead>
					<Line>
						<CellHeader>#</CellHeader>
						<CellHeader>CLASSIFICAÇÃO</CellHeader>
						<CellHeader>P</CellHeader>
						<CellHeader>J</CellHeader>
						<CellHeader>V</CellHeader>
						<CellHeader>E</CellHeader>
						<CellHeader>D</CellHeader>
						<CellHeader>GP</CellHeader>
						<CellHeader>GC</CellHeader>
						<CellHeader>SG</CellHeader>
					</Line>
				</thead>
				<tbody>
					{data.map((s, i) => (
						<Line key={s.id} stripe={i % 2 === 0}>
							<Cell>{s.position}</Cell>
							<Cell>
								<TeamDisplay
									team={s.teamRegister.team}
									size="sm"
								/>
							</Cell>
							<Cell>{s.points}</Cell>
							<Cell>{s.games}</Cell>
							<Cell>{s.won}</Cell>
							<Cell>{s.drowns}</Cell>
							<Cell>{s.lost}</Cell>
							<Cell>{s.goals}</Cell>
							<Cell>{s.goalsAgainst}</Cell>
							<Cell>{s.goalsDifference}</Cell>
						</Line>
					))}
				</tbody>
			</table>
		</Container>
	);
};

export default Ranking;
