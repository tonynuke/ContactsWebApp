namespace ContactsApp.DTO
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public long OrganisationId { get; set; }
    }
}