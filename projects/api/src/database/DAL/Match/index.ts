import * as List from './list';
import * as Create from './Create';
import * as Update from './Update';
import * as Get from './GetUniqueTeam';
import * as Transaction from './Transactions';


export const MatchRepository = {
	...List,
	...Create,
	...Update,
	...Get,
	...Transaction
};
