using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; private set; }

        public Name Position { get; set; }

        public Name Name { get; set; }

        public Name Surname { get; set; }

        public Name Patronymic { get; set; }

        public Name Organization { get; set; }

        public DateTime BirthDate { get; set; }

        private readonly List<Link> links = new List<Link>();

        public IReadOnlyCollection<Link> Links => this.links;

        public Link AddLink(string value, LinkType type)
        {
            var link = new Link { Value = value, Type = type };
            this.links.Add(link);
            return link;
        }

        public void RemoveLink(long id)
        {
            var link = this.links.SingleOrDefault(e => e.Id == id);
            if (link != null)
                this.RemoveLink(link);
        }

        public void RemoveLink(Link link)
        {
            this.links.Remove(link);
        }

        private Employee()
        {
        }

        public Employee(Name name, Name position)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}