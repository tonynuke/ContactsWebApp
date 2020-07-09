using System;
using Employee.Domain;

namespace Employee.DTO
{
    public class PlainEmployeeDTO
    {
        public Guid Id { get; private set; }

        public string OrganisationName { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDate { get; set; }

        public string LinkValue { get; set; }

        public LinkType? LinkType { get; set; }
    }
}