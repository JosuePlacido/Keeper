import styled from 'styled-components';

export const Container = styled.span`
	display: flex;
	width: 100%;
	flex-direction: column;
	align-items: center;
	position: relative;
`;
export const InputStyled = styled.input`
	display: none;
`;
export const ButtonRemove = styled.button`
	position: absolute;
	bottom: 0;
	right: 39%;
	border-radius: 50%;
	padding: 4px 8px;
	background-color: ${({ theme }) => theme.COLORS.BLUE_LIGHT};
	border: none;
	color: ${({ theme }) => theme.COLORS.WHITE};
	cursor: pointer;
`;

export const Preview = styled.img`
	width: 100px;
	height: 100px;
	border: 2px solid ${({ theme }) => theme.COLORS.BLUE};
`;
