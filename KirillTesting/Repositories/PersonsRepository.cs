using KirillTesting.Models;
using KirillTesting.Models.Contexts;
using KirillTesting.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace KirillTesting.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly KirillTestContext _context;

        public PersonsRepository(KirillTestContext context)
        {
            _context = context;
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = _context.Persons.FirstOrDefault(x => x.Id == id);

            if(person == null)
            {
                throw new InvalidOperationException($"Персона с ИД:{id} не найдена!");
            }

            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.Include(x => x.Policy).ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if( person == null )
            {
                throw new InvalidOperationException($"Персона с ИД:{id} не найдена!");
            }

            return person;
        }

        public async Task UpdatePersonAsync(int id, Person person)
        {
            bool tracking = _context.ChangeTracker.Entries<Person>().Any(x => x.Entity.Id == id);
            if (!tracking)
            {
                _context.Persons.Update(person);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
