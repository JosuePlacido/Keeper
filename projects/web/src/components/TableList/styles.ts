import styled from 'styled-components';

export const Container = styled.div`
	& table {
		width: 100%;
		& thead {
			background-color: ${({ theme }) => theme.COLORS.GRAY_400};
			font-weight: bold;
			text-transform: uppercase;

			& th {
				padding: 5px 10px;
				vertical-align: middle;
			}
		}
		& tbody {
		overflow-y: auto;
		max-height: 500;
		& tr {
			border-bottom: 1px solid ${({ theme }) => theme.COLORS.GRAY_400};
			&:nth-child(even) {
				background-color: ${({ theme }) => theme.COLORS.GRAY_200};
			}
			& td {
				padding: 5px 10px;
			}
		}
	}
	}
`;
