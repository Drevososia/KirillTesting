using KirillTesting.Models.Contexts;
using KirillTesting.Models.Sprs;
using KirillTesting.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KirillTesting.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly KirillTestContext _context;

        public GenderRepository(KirillTestContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }
    }
}
