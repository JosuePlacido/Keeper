import React, { ButtonHTMLAttributes } from 'react';
import { Container } from './styles';

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
	variant?: 'primary' | 'default';
};
export default function Button({
	children,
	variant = 'primary',
	...rest
}: ButtonProps) {
	return (
		<Container variant={variant} {...rest}>
			{children}
		</Container>
	);
}
