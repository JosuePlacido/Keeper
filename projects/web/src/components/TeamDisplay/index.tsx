import React, { HTMLAttributes, ReactNode } from 'react';
import { Container } from './styles';
import { ITeam } from '../../models';

interface ITeamDisplayProps {
	team: ITeam;
	reverse?: boolean;
	size?: 'sm' | 'md' | 'lg';
	children?: ReactNode;
	variant?: 'bold' | 'opaque';
	matchLayout?: boolean;
}

const SIZES_IMG: { [key: string]: number } = {
	sm: 20,
	md: 30,
	lg: 50
};
export default function TeamDisplay({
	team,
	reverse,
	variant,
	size = 'md',
	children,
	matchLayout = false
}: ITeamDisplayProps) {
	const teamName = size === 'lg' ? <h2>{team.name}</h2> : team.name;
	return (
		<Container
			bold={variant === 'bold'}
			opaque={variant === 'opaque'}
			reverse={reverse}
			justify={matchLayout ? 'flex-end' : 'flex-start'}
		>
			{matchLayout && teamName}
			<img
				width={SIZES_IMG[size]}
				height={SIZES_IMG[size]}
				src={team.logoURL ? team.logoURL : 'team-default.png'}
				alt={`${team.name} símbolo`}
			/>
			{!matchLayout && teamName}
			{children}
		</Container>
	);
}
