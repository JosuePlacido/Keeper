import styled from 'styled-components';

export const Container = styled.article`
	display: flex;
	flex-direction: column;
	gap: 10px;
	padding: 10px;
	justify-content: stretch;
	& form fieldset {
		display: flex;
		flex-direction: column;
		gap: 5px;
		border: none;
		align-items: flex-start;
		margin-bottom: 15px;
	}
`;

export const Footer = styled.footer`
	display: flex;
	gap: 10px;
	padding: 10px;
	justify-content: stretch;
`;
