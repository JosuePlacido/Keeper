import styled from 'styled-components';

export const Container = styled.div`
	position: relative;
	min-width: 150px;
	flex: 1;
	& select {
		height: 40px;
		padding: 5px;
		border-radius: 8px;
		width: 100%;
	}
`;

export const SelectedOption = styled.div`
	display: flex;
	align-items: center;
	gap: 8px;
	padding: 5px;
	border: 1px solid ${({ theme }) => theme.COLORS.GRAY_400};
	border-radius: 4px;
`;

export const Dropdown = styled.ul`
	margin: 0;
	padding: 0;
	list-style: none;
	position: absolute;
	top: 100%;
	left: 0;
	width: 100%;
	border: 1px solid ${({ theme }) => theme.COLORS.GRAY_400};
	border-radius: 4px;
	background-color: ${({ theme }) => theme.COLORS.WHITE};
	z-index: 1000;
	max-height: 350px;
	overflow-y: auto;
	overflow-wrap: anywhere;

	& li {
		display: flex;
		align-items: center;
		gap: 8px;
		padding: 8px;
		cursor: pointer;
		transition: background-color 0.2s ease;
	}
	& li.active {
		background-color: ${({ theme }) => theme.COLORS.BLUE_LIGHT};
		color: ${({ theme }) => theme.COLORS.WHITE};
	}
	& li:hover {
		background-color: ${({ theme }) => theme.COLORS.GRAY_300};
	}`;

interface ImgProps {
	rounded: boolean;
}
export const ImgIcon = styled.img<ImgProps>`
		width: 30px;
		height: 30px;
		border-radius: ${({ rounded }) => rounded ? '50%' : '0'};
`;

export const Overlay = styled.div`
	position: fixed;
	top: 0;
	left: 0;
	width: 100vw;
	height: 100vh;
	z-index: 10;
`;
