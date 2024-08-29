import React from 'react';
import { Container } from './styles';

export function Before() {
	return (
		<svg
			width="20"
			height="100%"
			viewBox="0 0 20 100"
			xmlns="http://www.w3.org/2000/svg"
		>
			<path
				d="M 0 50 L 100 50"
				stroke="#000"
				fill="transparent"
				stroke-width="2"
			/>
		</svg>
	);
}
interface IDrawBracketProps {
	additionalHeight: number;
}
export function GoDown({ additionalHeight }: IDrawBracketProps) {
	const finalCordinate = 100 + (additionalHeight - 100) / 2;
	return (
		<svg
			width="20"
			height="100%"
			viewBox="0 0 20 100"
			xmlns="http://www.w3.org/2000/svg"
		>
			<path
				d={`M 0 50 L 8 50 Q 19 50 19 60 L 19 ${finalCordinate}`}
				stroke="#000"
				fill="transparent"
				stroke-width="2"
			/>
		</svg>
	);
}
export function GoUp({ additionalHeight }: IDrawBracketProps) {
	const finalCordinate = 0 - (additionalHeight - 100) / 2;
	return (
		<svg
			width="20"
			height="100%"
			viewBox="0 0 20 100"
			xmlns="http://www.w3.org/2000/svg"
		>
			<path
				d={`M 0 50 L 8 50 Q 19 50 19 40 L 19 ${finalCordinate}`}
				stroke="#000"
				fill="transparent"
				stroke-width="2"
			/>
		</svg>
	);
}
