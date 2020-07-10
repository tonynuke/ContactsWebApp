namespace ContactsApp.DTO.Employee
{
    public class PutEmployeeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
    }
}