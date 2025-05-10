import * as List from './list';
import * as Create from './Create';
import * as Count from './Count';
import * as Update from './Update';
import * as Delete from './Delete';


export const TeamsRepository = {
	...List,
	...Create,
	...Count,
	...Update,
	...Delete
};
