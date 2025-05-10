import * as getAll from './List';
import * as Create from './Create';
import * as Update from './Update';
import * as Get from './Get';


export const MatchController = {
	...getAll,
	...Create,
	...Update,
	...Get
};
