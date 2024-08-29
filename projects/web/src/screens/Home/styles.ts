import styled from 'styled-components';

export const Container = styled.div`
	display: flex;
   	flex: 1;
	flex-wrap: wrap;
	justify-content: center;
	gap: 20px;
	padding: 20px;
`;

export const Panel = styled.section`
	display: flex;
	flex-direction: column;
	background-color: ${({ theme }) => theme.COLORS.WHITE};
	border-radius: 8px;
	box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
	gap: 15px;
	padding: 20px;
`;
export const Title = styled.h2`
`;
