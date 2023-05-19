using KirillTesting.Models;

namespace KirillTesting.Repositories.Interfaces
{
    public interface IPersonsRepository
    {
        Task<List<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonAsync(int id);
        Task DeletePersonAsync(int id);
        Task UpdatePersonAsync(int id,Person person);
        Task AddPersonAsync(Person person);
    }
}
