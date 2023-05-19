using KirillTesting.Models.Sprs;

namespace KirillTesting.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<List<Gender>> GetAllGendersAsync();
    }
}
