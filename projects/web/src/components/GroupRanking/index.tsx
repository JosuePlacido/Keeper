import React, { InputHTMLAttributes, useEffect, useRef, useState } from 'react';
import { Container, Title } from './styles';
import { PiArrowLeft, PiArrowRight } from 'react-icons/pi';
import { IGroup, IStage } from '../../models';
import ButtonIcon from '../ButtonIcon';
import { api } from '../../services/api';
import Ranking from '../Ranking';

interface ISliderStageAttributes {
	group: IGroup;
	editMode?: boolean;
}
const SliderStage = ({ group, editMode = false }: ISliderStageAttributes) => {
	if (group.statistics)
		group.statistics = group.statistics.map(s => {
			if (!s.teamRegister)
				s.teamRegister = JSON.parse(s.teamRegisterString);
			return s;
		});

	console.log(group);
	return (
		<Container>
			<Ranking data={group.statistics} />
		</Container>
	);
};

export default SliderStage;
