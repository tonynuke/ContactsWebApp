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

        private readonly List<Contact> contacts = new List<Contact>();

        public IReadOnlyCollection<Contact> Contacts => this.contacts;

        public Contact AddContact(string value, ContactType type)
        {
            var contact = new Contact { Value = value, Type = type };
            this.contacts.Add(contact);
            return contact;
        }

        public void RemoveContact(long id)
        {
            var contact = this.contacts.SingleOrDefault(e => e.Id == id);
            if (contact != null)
                this.RemoveContact(contact);
        }

        public void RemoveContact(Contact contact)
        {
            this.contacts.Remove(contact);
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