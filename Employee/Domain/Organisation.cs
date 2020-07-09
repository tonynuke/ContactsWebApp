using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Domain
{
    public class Organization
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

        public void RemoveEmployee(long id)
        {
            var employee = this.employees.SingleOrDefault(e => e.Id == id);
            if (employee != null)
                this.RemoveEmployee(employee);
        }

        private Organization()
        {
        }

        public Organization(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}