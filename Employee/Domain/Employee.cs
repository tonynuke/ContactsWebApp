using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Domain.Contacts;

namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; }

        public String Position { get; set; }

        public String Name { get; set; }

        public String Surname { get; set; }

        public String Patronymic { get; set; }

        public String Organization { get; set; }

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

        public Employee(String name, String position)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}