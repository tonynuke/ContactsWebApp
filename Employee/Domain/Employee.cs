using System;
using System.Collections.Generic;
using Employee.Domain.Contacts;

namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; }

        public Name Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Organization { get; set; }
        public string Position { get; set; }

        public DateTime BirthDate { get; set; }

        private readonly List<Contact> contacts = new List<Contact>();

        public IReadOnlyCollection<Contact> Contacts => this.contacts;

        public void AddContacts(IEnumerable<Contact> contacts)
        {
            this.contacts.AddRange(contacts);
        }

        public Contact AddContact(Contact contact)
        {
            this.contacts.Add(contact);
            return contact;
        }

        public void ClearContacts()
        {
            this.contacts.Clear();
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