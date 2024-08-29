import styled from 'styled-components';

export const Container = styled.div`
    display: flex;
    justify-content: center;
    align-items: flex-start;
    padding: 20px;
`;
export const Panel = styled.div`
	display: flex;
	flex-direction: column;
	background-color: ${({ theme }) => theme.COLORS.WHITE};
    padding: 40px;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    width: 100%;
    max-width: 480px;
    text-align: center;
	align-items: stretch;
	& fieldset {
		display: flex;
		flex-direction: column;
		gap: 5px;
		border: none;
		align-items: flex-start;
		margin-bottom: 15px;
	}
`;
export const Title = styled.h2`
	color: ${({ theme }) => theme.COLORS.BLUE};
	margin-bottom: 20px;
`;
