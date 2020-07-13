export interface LinkState {
    id: number;
    value: string;
    type: string;
}

export interface EmployeeState {
    id: number;
    name: string;
    surname: string;
    birthDate: Date;
    patronymic: string;
    position: string;
    organization: string;

    tmpLinkId: number;

    links: LinkState[];
}