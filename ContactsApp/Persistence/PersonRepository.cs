using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApp.Domain;

namespace ContactsApp.Infrasructure
{
    public interface PersonRepository
    {
        Task Add(Person person);
        Task<IReadOnlyList<Person>> GetAll();


    }
}