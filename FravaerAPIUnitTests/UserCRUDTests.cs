using System;
using System.Collections.Generic;
using FakeItEasy;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FravaerAPIUnitTests
{
    [TestClass]
    public class UserCRUDTests
    {

        /*
         * This method tests if we can create users in database.
         * We ensure that the user is given an id when created in the database.
         * We ensure that the user created is actually in the database by reading and compairing.
         */
        [TestMethod]
        public void Create()
        {
            var repository = new DLLFacade().GetUserRepository();

            var user = new User()
            {
                Department = new Department()
                {
                    Id = 1,
                    Users = new List<User>()
                },
                Absences = new List<Absence>(),
                Email = "test@mail.dk",
                FirstName = "Bo",
                LastName = "Larsen",
                UserName = "Bobo",
                Password = "!Bo1Larsen",
                Role = Role.Medarbejder,
            };

            user = repository.Create(user);
            Assert.Equals(user.Id, null);
            Assert.Equals(repository.Read(user.Id), user);
        }
    }
}
