import React, { InputHTMLAttributes, useEffect, useRef, useState } from 'react';
import {
	Cell,
	CellHeader,
	Container,
	Line,
	PlayerPhoto,
	TeamDisplayFull,
	TeamLabel,
	Title
} from './styles';
import { PiArrowLeft, PiArrowRight } from 'react-icons/pi';
import {
	IGroup,
	IMatch,
	IRegisterPlayer,
	IScorers,
	IStage,
	IStatistics
} from '../../models';
import ButtonIcon from '../ButtonIcon';
import { api } from '../../services/api';
import ImgUserDefault from '../../../public/user-default.png';

interface IScorersProps {
	data: IRegisterPlayer[];
}
const Scorers = ({ data }: IScorersProps) => {
	return (
		<Container>
			<table>
				<thead>
					<Line>
						<CellHeader>JOGADOR</CellHeader>
						<CellHeader>GOLS</CellHeader>
					</Line>
				</thead>
				<tbody>
					{data.map((d, index) => (
						<Line key={d.player!.id}>
							<Cell>
								<TeamDisplayFull>
									{index + 1}
									<PlayerPhoto
										width="40"
										height="40"
										src={
											d.player && d.player.picture
												? 'https://github.com/JosuePlacido.png'
												: '/user-default.png'
										}
										alt={`foto de ${d.player!.name}`}
									/>
									<span>
										{d.player!.nickname} <br />
										<TeamLabel>{d.team.name}</TeamLabel>
									</span>
								</TeamDisplayFull>
							</Cell>
							<Cell>{d.goals}</Cell>
						</Line>
					))}
				</tbody>
			</table>
		</Container>
	);
};

export default Scorers;
