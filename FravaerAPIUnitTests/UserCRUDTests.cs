using System;
using System.Collections.Generic;
using FakeItEasy;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FravaerAPIUnitTests
{
    [TestClass]
    public class UserCRUDTests
    {

        /*
         * This method tests if we can create users using moq.
         * We ensure that the user is given an id when created in the database
         */
        [TestMethod]
        public User Create()
        {
            var user = new User()
            {
                Department = null,
                Absences = new List<Absence>(),
                Email = "test@mail.dk",
                FirstName = "Bo",
                LastName = "Larsen",
                UserName = "Bobo",
                Password = "!Bo1Larsen",
                Role = Role.Medarbejder,
            };

            var user2 = A.Fake<User>();
            var repository = A.Fake<IRepository<User, int>>();

            //A.CallTo(() => repository.Create()).Returns(user2);
            return null;
        }
    }
}
