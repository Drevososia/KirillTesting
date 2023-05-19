using KirillTesting.Models;

namespace KirillTesting.Repositories.Interfaces
{
    public interface IPoliciesRepository
    {
        Task<Policy> GetPolicy(int id);
        Task<Policy> GetPolicyByPersonId(int personId);
        Task DeletePolicyAsync(int id);
        Task UpdatePolicyAsync(int id, Policy policy);
        Task AddPolicyAsync(Policy policy);
    }
}
