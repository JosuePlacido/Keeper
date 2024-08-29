import styled from 'styled-components';

type ContainerProps = {
	reverse?: boolean;
	bold: boolean;
	opaque: boolean;
	justify: 'flex-start' | 'flex-end';
}
export const Container = styled.span<ContainerProps>`
	display: flex;
	flex-direction: ${({ reverse = false }) => reverse ? 'row-reverse' : 'row'};
	gap: 10px;
	align-items: center;
	justify-content: ${({ justify }) => justify};
	font-weight: ${({ bold }) => bold ? 'bold' : 'normal'};
	opacity: ${({ opaque }) => opaque ? 0.5 : 1};
`;
