using KirillTesting.Models.Sprs;

namespace KirillTesting.Repositories.Interfaces
{
    public interface IPolicyTypesRepository
    {
        Task<List<PolicyType>> GetAllPolicyTypes();
    }
}
