using System;
using System.Collections.Generic;

namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; private set; }

        public Name Position { get; set; }

        public Name Name { get; set; }

        public Name Surname { get; set; }

        public Name Patronymic { get; set; }

        public DateTime BirthDate { get; set; }

        private readonly List<Link> links = new List<Link>();

        public IReadOnlyCollection<Link> Links => this.links;

        public Link AddLink(string value, LinkType type)
        {
            var link = new Link { Value = value, Type = type };
            this.links.Add(link);
            return link;
        }

        private Employee()
        {
        }

        public Employee(Name position, Name name)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}