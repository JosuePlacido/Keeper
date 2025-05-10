import React, { ReactNode } from 'react';
import { Container } from './styles';

type Headers = {
	key: string;
	label: string;
};
type Props = {
	/*
	headers: Headers;
	data: any;*/ children: ReactNode;
};

export default function TableList({ children }: Props) {
	return (
		<Container>
			<table>{children}</table>
		</Container>
	);
}
