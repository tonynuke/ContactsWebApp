import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Employees from './containers/EmployeesContainer';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Employees} />
    </Layout>
);
