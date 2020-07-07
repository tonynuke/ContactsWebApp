﻿using System;
using System.Collections.Generic;

namespace Employee.Domain
{
    public class Organisation
    {
        public long Id { get; private set; }

        public Name Name { get; set; }

        private readonly List<Employee> employees = new List<Employee>();

        public IReadOnlyCollection<Employee> Employees => this.employees;

        public Employee CreateEmployee(Name name, Name position)
        {
            var employee = new Employee(name, position);
            this.employees.Add(employee);
            return employee;
        }

        public void RemoveEmployee(Employee employee)
        {
            this.employees.Remove(employee);
        }

        private Organisation()
        {
        }

        public Organisation(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}