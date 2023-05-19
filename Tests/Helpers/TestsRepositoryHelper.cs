using DevExpress.Blazor.Scheduler.Internal;
using KirillTesting.Models;
using KirillTesting.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Helpers
{
    internal static class TestsRepositoryHelper
    {
        public static  KirillTestContext GetDatabaseContext()
        {
           var databaseContext = new KirillTestContext();
            return databaseContext;
        }

        public static Person GetTestPerson()
        {
            var person = new Person();

            person.Firstname = "Test";
            person.Lastname = "Test";
            person.Patronymic = "Test";
            person.Age = 20;
            person.DateOfBirth = DateTime.Now;

            person.Policy = new Policy()
            {
                Enp = "1234567890123456",
                Type = 1
            };
            return person;
        }
    }
}
