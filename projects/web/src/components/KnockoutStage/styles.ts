import styled from 'styled-components';

export const Container = styled.div`
	display: flex;
   	flex: 1;
	flex-direction: row;
	overflow: auto;
`;

interface IStageProps {
	stripe: boolean;
}
export const StagePanel = styled.div<IStageProps>`
	display: flex;
	flex-direction: column;
	justify-content: center;
	background-color: ${({ theme, stripe }) => stripe ? theme.COLORS.GRAY_100 : theme.COLORS.GRAY_200};
	padding-top: 15px;
`;
export const StageTitle = styled.h3`
	text-align: center;
	text-transform: uppercase;
	margin-bottom: 15px;
`;
