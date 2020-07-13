import { Action, Reducer } from 'redux';
import { KnownAction } from './EmployeeActions';
import { EmployeeState, LinkState } from './EmployeeState';

export const unloadedState: EmployeeState = {
    id: -1,
    name: '',
    surname: '',
    patronymic: '',
    position: '',
    organization: '',
    birthDate: new Date(),
    tmpLinkId: -1,
    links: []
};

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
            return Object.assign({}, state, { birthDate: action.birthDate });
        case 'SET_EMPLOYEE_ORGANIZATION':
            return Object.assign({}, state, { organization: action.organization });
        case 'SET_EMPLOYEE_POSITION':
            return Object.assign({}, state, { position: action.position });
        case 'CREATE_LINK':
            {
                // используем отрицательные id для новых контактов
                const nextId: number = state.tmpLinkId > 0 || state.tmpLinkId === undefined ? -1 : state.tmpLinkId - 1;
                const newLink: LinkState = { id: nextId, type: action.newType, value: action.newValue };

                return Object.assign({}, state, { links: [...state.links, newLink], tmpLinkId: nextId });
            }
        case 'SET_LINK_VALUE':
            return Object.assign({}, state,
                {
                    links: state.links.map(link => {
                        if (link.id === action.id) {
                            return Object.assign({}, link, { value: action.value });
                        }
                        return link;
                    })
                });
        case 'SET_LINK_TYPE':
            return Object.assign({}, state,
                {
                    links: state.links.map(link => {
                        if (link.id === action.id) {
                            return Object.assign({}, link, { type: action.linkType });
                        }
                        return link;
                    })
                });
        case 'DELETE_LINK':
            return Object.assign({}, state, { links: state.links.filter(link => link.id !== action.link.id) });
        default:
            return state;
    }
};
