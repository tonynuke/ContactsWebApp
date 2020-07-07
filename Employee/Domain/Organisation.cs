using System;
using System.Collections.Generic;

namespace Employee.Domain
{
    public class Organisation
    {
        public long Id { get; set; }

        public Name Name { get; set; }

        private readonly List<Employee> employees = new List<Employee>();

        public IReadOnlyCollection<Employee> Employees => this.employees;

        public Employee AddEmployee(Person person, string position)
        {
            var employee = new Employee(person, position);
            this.employees.Add(employee);
            return employee;
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