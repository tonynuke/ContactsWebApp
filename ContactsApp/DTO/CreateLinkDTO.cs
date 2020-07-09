﻿using Employee.Domain;

namespace ContactsApp.DTO
{
    public class CreateLinkDTO
    {
        public string Value { get; set; }
        public LinkType LinkType { get; set; }
        public long OrganisationId { get; set; }

        public long EmployeeId { get; set; }
    }
}