import styled from 'styled-components';

export const Container = styled.div`
	display: grid;
	grid-template-columns: auto 1fr;
	grid-template-rows: auto 1fr;
	grid-template-areas: "header header" "menu body";
`;

export const Body = styled.main`
	grid-area: body;
	display: flex;
   	flex: 1;
	flex-wrap: wrap;
	justify-content: center;
	gap: 20px;
	padding: 20px;
	overflow: auto;
`;
