using KirillTesting.Models;
using KirillTesting.Models.Contexts;
using KirillTesting.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using Tests.Helpers;

namespace Tests
{
    public class TestPersonRepository
    {
        private KirillTestContext _context;
        public TestPersonRepository()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _context = TestsRepositoryHelper.GetDatabaseContext();
        }

        [Fact]
        public async Task TestAdding()
        {
            // Arrange

            var repository = new PersonsRepository(_context);
            //Act
            var person = TestsRepositoryHelper.GetTestPerson();

            await repository.AddPersonAsync(person);

            //Assert
            Assert.True(_context.Persons.Any(x => x == person));

            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task TestUpdating()
        {
            // Arrange

            var repository = new PersonsRepository(_context);

            var person = TestsRepositoryHelper.GetTestPerson();

            await repository.AddPersonAsync(person);

            var updatingPerson = person;
            updatingPerson.Lastname = "Xyq";
            //Act
            await repository.UpdatePersonAsync(updatingPerson.Id, updatingPerson);
            var updatedPerson = await _context.Persons.FirstOrDefaultAsync(x => x.Id == updatingPerson.Id);

            //Assert
            Assert.Equal("Xyq", updatedPerson?.Lastname);

            _context.Remove(person);
            await _context.SaveChangesAsync();
        }
        [Fact]
        public async Task TestSelecting()
        {
            // Arrange
            var repository = new PersonsRepository(_context);

            var person = TestsRepositoryHelper.GetTestPerson();

            await repository.AddPersonAsync(person);
            //Act
            var persons = await repository.GetAllPersonsAsync();
            var selectedPerson = await repository.GetPersonAsync(person.Id);
            //Assert
            Assert.NotEmpty(persons);
            Assert.Equal(person.Id, selectedPerson?.Id);

            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task TestDeleting()
        {
            // Arrange
            var repository = new PersonsRepository(_context);

            var person = TestsRepositoryHelper.GetTestPerson();
            await repository.AddPersonAsync(person);
            //Act
            await repository.DeletePersonAsync(person.Id);

            //Assert
            Assert.False(_context.Persons.Any(x => x.Id == person.Id));
        }

        

    }
}