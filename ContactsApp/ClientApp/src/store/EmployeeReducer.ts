import { Action, Reducer } from 'redux';
import { KnownAction } from './EmployeeActions';
import { EmployeeState, LinkState, State } from './EmployeeState';

const unloadedState: EmployeeState = {
    id: -1,
    name: '',
    surname: '',
    patronymic: '',
    position: '',
    organization: '',
    state: State.New,
    links: []
} as EmployeeState;

export const reducer: Reducer<EmployeeState> = (state: EmployeeState | undefined, incomingAction: Action): EmployeeState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_EMPLOYEE_NAME':
            return Object.assign({}, state, { name: action.name });
        case 'SET_EMPLOYEE_SURNAME':
            return Object.assign({}, state, { surname: action.surname });
        case 'SET_EMPLOYEE_PATRONYMIC':
            return Object.assign({}, state, { patronymic: action.patronymic });
        case 'SET_EMPLOYEE_BIRTHDATE':
            return Object.assign({}, state, { date: action.date });
        case 'SET_EMPLOYEE_ORGANIZATION':
            return Object.assign({}, state, { organization: action.organization });
        case 'SET_EMPLOYEE_POSITION':
            return Object.assign({}, state, { position: action.position });
        case 'CREATE_LINK':
            {
                const ids: number[] = state.links.map(link => link.id);
                const nextId: number = ids.length === 0 ? 0 : Math.max(...ids) + 1;
                const newLink: LinkState = { id: nextId, type: action.newType, value: action.newValue, state: State.New };

                return Object.assign({}, state, { links: [...state.links, newLink], state: State.Changed });
            }
        case 'UPDATE_LINK':
            {
                const newState = action.link.state === State.Saved ? State.Changed : State.New;

                const index: number = state.links.indexOf(action.link);
                const newLink: LinkState = {
                    id: action.link.id,
                    value: action.newValue,
                    type: action.newType,
                    state: newState
                };
                state.links[index] = newLink;

                return Object.assign({}, state, { links: state.links, state: State.Changed });
            }
        case 'DELETE_LINK':
            {
                if (action.link.state === State.New) {
                    return Object.assign({}, state, { links: state.links.filter(link => link.id !== action.link.id), state: State.Changed });
                }

                const index: number = state.links.findIndex(link => link.id === action.link.id);
                const newLink: LinkState = {
                    id: action.link.id,
                    value: action.link.value,
                    type: action.link.type,
                    state: State.Deleted
                };
                state.links[index] = newLink;

                return Object.assign({}, state, { links: state.links, state: State.Changed });
            }
        case 'SAVE_EMPLOYEE':
            {
                const newLinks: LinkState[] = state.links
                    .filter(link => link.state !== State.Deleted)
                    .map(link => Object.assign({}, link, { state: State.Saved }));

                return Object.assign({}, state, { links: newLinks, state: State.Saved });
            }
        default:
            return state;
    }
};
