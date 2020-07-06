namespace Employee.Domain
{
    public class Employee
    {
        public long Id { get; set; }

        public Person Person { get; set; }

        public string Position { get; set; }
    }
}