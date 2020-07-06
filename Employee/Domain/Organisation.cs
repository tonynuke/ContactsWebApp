using System.Collections.ObjectModel;

namespace Employee.Domain
{
    public class Organisation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public readonly ReadOnlyCollection<Employee> Employees;
    }
}