using KirillTesting.Models.Contexts;
using KirillTesting.Models.Sprs;
using KirillTesting.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KirillTesting.Repositories
{
    public class PolicyTypesRepository : IPolicyTypesRepository
    {
        private readonly KirillTestContext _context;

        public PolicyTypesRepository(KirillTestContext context)
        {
            _context = context;
        }
        public async Task<List<PolicyType>> GetAllPolicyTypes()
        {
            return await _context.PolicyTypes.ToListAsync();
        }
    }
}
