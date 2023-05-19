using KirillTesting.Models;
using KirillTesting.Models.Contexts;
using KirillTesting.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KirillTesting.Repositories
{
    public class PoliciesRepository : IPoliciesRepository
    {
        private readonly KirillTestContext _context;

        public PoliciesRepository(KirillTestContext context)
        {
            _context = context;
        }

        public async Task AddPolicyAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePolicyAsync(int id)
        {
            var policy = _context.Policies.FirstOrDefault(x => x.Id == id);

            if (policy == null)
            {
                throw new InvalidOperationException($"Полис с ИД:{id} не найден!");
            }
            _context.Remove(policy);
            await _context.SaveChangesAsync();
        }

        public async Task<Policy> GetPolicy(int id)
        {
            var policy = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);

            if (policy == null)
            {
                throw new InvalidOperationException($"Полис с ИД:{id} не найден!");
            }

            return policy;
        }

        public async Task<Policy> GetPolicyByPersonId(int personId)
        {
            var person = await _context.Persons
                .Include(x => x.Policy)
                .FirstOrDefaultAsync(x => x.Id == personId);

            if (person == null)
            {
                throw new InvalidOperationException($"Полис с ИД персоны:{personId} не найден!");
            }
            return person.Policy;
        }

        public async Task UpdatePolicyAsync(int id, Policy policy)
        {
            bool isExist = await _context.Policies.AnyAsync(x => x.Id == id);

            if (!isExist)
            {
                throw new InvalidOperationException($"Персона с ИД:{id} не найдена!");
            }

            _context.Policies.Update(policy);
        }
    }
}
