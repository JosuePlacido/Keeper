import * as List from './list';
import * as Create from './Create';
import * as Count from './Count';
import * as Update from './Update';
import * as Delete from './Delete';
import * as Get from './GetUnique';


export const ChampionshipRepository = {
	...List,
	...Create,
	...Count,
	...Update,
	...Delete,
	...Get
};
