export enum State {
    Saved = 0,
    New = 1,
    Changed,
    Deleted,
}

export interface LinkState {
    id: number;
    value: string;
    type: string;

    state: State;
}

export interface EmployeeState {
    id: number;
    name: string;
    surname: string;
    patronymic: string;
    position: string;
    organization: string;

    links: LinkState[];

    state: State;
}