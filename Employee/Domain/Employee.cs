using System;

namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; set; }

        public Person Person { get; set; }

        public Name Position { get; set; }

        private Employee()
        {
        }

        public Employee(Person person, Name position)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}