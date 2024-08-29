import styled from 'styled-components';

export const Container = styled.div`
	display: flex;
	flex: 1;
	position: relative;
    transition: transform 0.5s ease-in-out;
`;
export const Title = styled.h3`

`;
export const Article = styled.article`
    min-width: 100%;
    padding: 20px;
    box-sizing: border-box;
	background-color: ${({ theme }) => theme.COLORS.WHITE};
	border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
`;
export const Header = styled.header`
	display: flex;
	justify-content: space-between;
	align-items: baseline;
	width: 100%;
	border-bottom: 1px solid ${({ theme }) => theme.COLORS.GRAY_200};
`;
