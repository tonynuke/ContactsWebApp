namespace ContactsApp.DTO
{
    public class PatchEmployeeDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public long EmployeeId { get; set; }
        public long OrganizationId { get; set; }
    }
}