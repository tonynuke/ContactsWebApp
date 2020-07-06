using System;
using System.Collections.ObjectModel;

namespace ContactsApp.Domain
{
    public class Employee
    {
        public Person Person { get; set; }

        public Position Position { get; set; }
    }

    public class Organisation
    {
        public string Name { get; set; }

        public readonly ReadOnlyCollection<Employee> Employees = new ReadOnlyCollection<Employee>();
    }

    public class Position
    {
        public string Name { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public enum LinkType
    {
        Skype,
        Email
    }

    public class Link
    {
        public LinkType Type { get; set; }
        public string Name { get; set; }
    }
}