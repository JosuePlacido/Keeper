import React, { InputHTMLAttributes } from 'react';
import { Container, InputStyled } from './styles';

type Props = InputHTMLAttributes<HTMLInputElement> & {};

const Input: React.FC<Props> = ({ ...rest }: Props) => {
	return (
		<Container>
			<InputStyled {...rest} />
		</Container>
	);
};

export default Input;
