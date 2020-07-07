using System;
using System.Collections.Generic;

namespace Employee.Domain
{
    public class Person
    {
        public long Id { get; set; }
        public Name Name { get; set; }
        public Name Surname { get; set; }
        public Name Patronymic { get; set; }
        public DateTime BirthDate { get; set; }

        private readonly List<Link> links = new List<Link>();

        public IReadOnlyCollection<Link> Links => this.links;

        public Link AddLink(string value, LinkType linkType)
        {
            var link = new Link { Value = value, Type = linkType };
            this.links.Add(link);
            return link;
        }

        private Person()
        {
        }

        public Person(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}