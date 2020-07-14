import * as EmployeesStore from '../store/EmployeesContainer';

export type EmployeesProps =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators