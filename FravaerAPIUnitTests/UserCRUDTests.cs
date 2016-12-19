using System;
using System.Collections.Generic;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FravaerAPIUnitTests
{
    [TestClass]
    public class UserCRUDTests
    {
        private Department _testDepartment;
        private TestFravaerContext _testContext;
        private IRepository<User, int> _repository;
        private List<User> _testUsers;
        
        /*
         * This method tests if we can create and read users via the repository.
         * We ensure that the user created is actually passed on by the repository, and is readable, by reading and compairing.
         */
        [TestMethod]
        public void CreateAndReadUserTest()
        {
            Initialize();
            var testUser = _testUsers[0];    
            testUser = _repository.Create(testUser);           
            Assert.AreEqual(_repository.Read(testUser.Id), testUser);
        }

        /*
         * This method tests if we can read all users via the repository.
         */
        [TestMethod]
        public void ReadAllUserstest()
        {
            Initialize();
            int i = 0;
            Assert.IsTrue(_repository.ReadAll().Count > 0);
            foreach (var user in _repository.ReadAll())
            {
                    Assert.AreEqual(user, _testUsers[i]);
                i++;
            }
        }

        /*
         * This method tests if we can update variables on a single user via the repository.
         */
        [TestMethod]
        public void UpdateUserTest()

        {
            Initialize();
            var testUser = _repository.Read(1);
             var testDepartment = new Department() { Id = 2, Name = "testdepartment2", Users = new List<User>() { testUser } };
            testUser.Department = testDepartment;
            _testContext.Departments.Add(testDepartment);
            testUser.Absences = new List<Absence>() {new Absence() {User = testUser, Date = new DateTime(2016,12,15),Id = 2,Status = Status.B} };
            testUser.Email = "bingo@bango.dk";
            testUser.FirstName = "Poul";
            testUser.LastName = "Hansen";
            testUser.Password = "11!!qqQQ";
            testUser.Role = Role.Afdelingsleder;
            testUser.UserName = "bananprofil";
            _repository.Update(testUser);
            Assert.AreEqual(testUser, _repository.Read(1));
            Assert.IsNotNull(testUser.Department);
        }

        /*
         * This method tests if we can delete a single user via the repository.
         */
        [TestMethod]
        public void DeleteUserTest()
        {
            Initialize();
            var testUser = _repository.Read(1);
            Assert.IsTrue(_repository.Delete(testUser.Id));
            Assert.IsNull(_repository.Read(testUser.Id));
        }

        /*
        * This method initializes all data needed for testing CRUD functionality via the UserRepository.
        */
        [TestInitialize]
        public void Initialize()
        {
            _testDepartment = new Department()
            {
                Id = 1,
                Name = "Testdepartment1",
                Users = new List<User>()
            };
            _testUsers = new List<User>() {new User()
            {
                Department = _testDepartment
                ,
                Absences = new List<Absence>(),
                Email = "test1@mail.dk",
                FirstName = "Bo",
                LastName = "Larsen",
                UserName = "Bobo",
                Password = "!Bo1Larsen",
                Role = Role.Medarbejder,
                Id = 1
            }, new User()
            {
                Department = _testDepartment
                ,
                Absences = new List<Absence>(),
                Email = "test2@mail.dk",
                FirstName = "Bob",
                LastName = "Lassen",
                UserName = "Bobobo",
                Password = "!Bo1Larsen",
                Role = Role.Afdelingsleder,
                Id = 2
            },
            new User()
            {
                Department = _testDepartment
                ,
                Absences = new List<Absence>(),
                Email = "test3@mail.dk",
                FirstName = "Bobo",
                LastName = "Farsen",
                UserName = "Bobobobo",
                Password = "!Bo1Larsen",
                Role = Role.Administrator,
                Id = 3
            }};
            _testContext = new TestFravaerContext();
            foreach (var user in _testUsers)
            {
                _testDepartment.Users.Add(user);
                _testContext.EndUsers.Add(user);
            }
            _testContext.Departments.Add(_testDepartment);
            _repository = new DLLFacade().GetUserRepository(_testContext);
        }
    }
}
