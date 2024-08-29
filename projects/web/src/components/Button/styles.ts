import styled from 'styled-components';


interface IButtonProps {
	variant: 'primary' | 'default';
}

export const Container = styled.button<IButtonProps>`
	flex: 1;
    padding: 10px;
    background-color: ${({ variant, theme }) => variant === 'primary' ? theme.COLORS.BLUE : theme.COLORS.GRAY_300};
    color: ${({ variant, theme }) => variant === 'primary' ? theme.COLORS.WHITE : theme.COLORS.GRAY_900};
    border: none;
    border-radius: 5px;
    cursor: pointer;
	&:hover {
    	background-color: ${({ variant, theme }) => variant === 'primary' ? theme.COLORS.BLUE_LIGHT : theme.COLORS.GRAY_200};
	}
`;
