import styled from 'styled-components';

interface IButtonIconProps {
	variant: 'default' | 'outline' | 'fill';
	color: string;
}

export const Container = styled.button<IButtonIconProps>`
	background-color: ${({ variant, color }) => variant === 'fill' ? color : 'transparent'};
	border-color: ${({ variant, color }) => variant === 'outline' ? color : 'transparent'};
	color: ${({ variant, color }) => variant !== 'fill' ? 'white' : color};
	justify-content: center;
	align-items: center;
	border-radius: 4px;
	cursor: pointer;

`;
