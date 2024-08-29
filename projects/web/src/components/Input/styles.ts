import styled from 'styled-components';

export const Container = styled.span`
	flex: 1;
`;
export const InputStyled = styled.input`
	width: 100%;
	height: 100%;
	padding: 10px;
    border: 1px solid ${({ theme }) => theme.COLORS.GRAY_300};
    border-radius: 5px;
`;
