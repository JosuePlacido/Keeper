import styled from 'styled-components';

export const Container = styled.div`
	min-height: 350px;
	& header {
		display: flex;
		justify-content: center;
		align-items: center;
		gap: 10px;
		padding: 10px;
		& span {
			& input {
				width: 50px;
			}
		}
	}
	& main {
		display: flex;
		flex-direction: column;
		align-items: center;
		gap: 15px;
	}
	& footer {
		display: flex;
		justify-content: flex-end;
		align-items: center;
		gap: 15px;
		padding: 10px 0;
	}
`;

export const Form = styled.span`
	display: flex;
	gap: 10px;
	align-items: flex-end;
	flex-wrap: wrap;
	& fieldset {
		border: none;
		display: flex;
		align-self: stretch;
		flex-direction: column;
		flex: 1;
		& label {
			font-weight: bold;
			font-size: 90%;
		}
	}
`;

export const ListEvent = styled.ul`
	display: flex;
	align-self: stretch;
	flex-direction: column;
	overflow-y: auto;
	min-height: 300px;
	max-height: 400px;
	& li {
		display: grid;
		grid-template-columns: 1fr 5px 1fr 30px;
		justify-content: space-evenly;
		& span {
			display: flex;
			align-items: center;
			gap: 15px;
			justify-content: flex-start;
			border-bottom: 1px solid ${({ theme }) => theme.COLORS.GRAY_300};
			padding: 5px 15px;
			& button:first-of-type {
				margin-left: auto;
			}
		}
		& span:first-of-type {
			flex-direction: row-reverse;
			& button:first-of-type {
				margin-right: auto;
				margin-left: 0;
			}
		}
	}
`;

export const Separator = styled.span`
	height: 100%;
	width: 5px;
	background-color: ${({ theme }) => theme.COLORS.GRAY_900};
	padding: 0px !important;
	border-color: ${({ theme }) => theme.COLORS.GRAY_900} !important;
`;

export const PenaltisPanel = styled.span`
	display: flex;
	align-items: center;
	gap: 10px;
`;
