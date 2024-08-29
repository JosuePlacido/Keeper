import styled from 'styled-components';

export const OverLay = styled.div`
	position: fixed;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	background-color: rgba(0, 0, 0, 0.5);
	display: flex;
	justify-content: center;
	align-items: flex-start;
	z-index: 1000;
	padding: 4rem;
`;
export const Content = styled.div`
	background-color: white;
	padding: 20px;
	border-radius: 4px;
	max-width: 100%;
	box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.3);
	position: relative;
	min-width: 500px;
`;

export const CloseButton = styled.button`
	background: none;
	border: none;
	font-size: 1.5rem;
	position: absolute;
	top: 0px;
	right: 10px;
	cursor: pointer;
`;
