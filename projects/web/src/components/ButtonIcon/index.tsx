import React, { ButtonHTMLAttributes, InputHTMLAttributes } from 'react';
import { Container } from './styles';
import { useTheme } from 'styled-components';

type Props = ButtonHTMLAttributes<HTMLButtonElement> & {
	icon: React.ReactNode;
	variant?: 'default' | 'fill' | 'outline';
};

const Input: React.FC<Props> = ({
	icon,
	color,
	variant = 'default',
	...rest
}: Props) => {
	const { COLORS } = useTheme();
	return (
		<Container
			variant={variant}
			color={color ? color : COLORS.BLUE_LIGHT}
			{...rest}
		>
			{icon}
		</Container>
	);
};

export default Input;
