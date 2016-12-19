using System;
using System.Collections.Generic;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FravaerAPITests
{
    [TestClass]
    public class DepartmentCRUDTests
    {
        private List<Absence> _testAbsences;
        private List<User> _testUsers;
        private List<Department> _testDepartments;
        private IRepository<Department, int> _testRepository;
        private IFravaerContext _testContext;

        /*
         * This method tests if we can create new departments via the repository.
         */
        [TestMethod]
        public void CreateDepartmentTest()
        {
            _testContext.Departments.Local.Clear();
            var testDepartment = _testDepartments[0];
            _testRepository.Create(testDepartment);
            Assert.IsTrue(_testContext.Departments.Local.Count > 0);
            Assert.IsTrue(_testContext.Departments.Local.Contains(testDepartment));
            Assert.AreEqual(testDepartment, _testContext.Departments.Local[0]);
            Assert.IsNotNull(testDepartment);
        }

        /*
         * This method tests if we can read a single department via the repository.
         */
        [TestMethod]
        public void ReadSingleDepartmentTest()
        {
            InitializeTestData();
            Assert.AreEqual(_testRepository.Read(_testDepartments[0].Id), _testDepartments[0]);
            Assert.IsNotNull(_testDepartments[0]);
        }

        /*
         * This method tests if we can read all departments via the repository.
         */
        [TestMethod]
        public void ReadAllDepartmentTest()
        {
            InitializeTestData();
            var result = _testRepository.ReadAll();
            Assert.IsTrue(result.Count > 0);
            int i = 0;
            foreach (var department in _testDepartments)
            {
                Assert.AreEqual(department, result[i]);
                i++;
            }
        }

        /*
         * This method tests if we can update a departments variables via the repository.
         */
        [TestMethod]
        public void UpdateDepartmentTest()
        {
            InitializeTestData();
            var testDepartment = _testDepartments[0];
            testDepartment.Name = "new department";
            _testRepository.Update(testDepartment);
            Assert.AreEqual(testDepartment, _testContext.Departments.Local[0]);
            Assert.IsNotNull(testDepartment);
        }

        /*
         * This method tests if we can completely delete a department via the repository.
         */
        [TestMethod]
        public void DeleteDepartmentTest()
        {
            InitializeTestData();
            _testRepository.Delete(_testDepartments[1].Id);
            Assert.IsNull(_testRepository.Read(_testDepartments[1].Id));
            Assert.IsFalse(_testContext.Departments.Local.Contains(_testDepartments[1]));
        }

        /*
        * This method initializes all data needed for testing CRUD functionality via the DepartmentRepository.
        */
        [TestInitialize]
        public void InitializeTestData()
        {
            _testDepartments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "Testdepartment1",
                    Users = new List<User>()
                },
                new Department()
                {
                    Id = 2,
                    Name = "Testdepartment2",
                    Users = new List<User>()
                },
                new Department()
                {
                    Id = 3,
                    Name = "Testdepartment3",
                    Users = new List<User>()
                }
            };
            _testUsers = new List<User>() {new User()
            {
                Department = _testDepartments[0]
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
                Department = _testDepartments[1]
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
                Department = _testDepartments[2]
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
            _testAbsences = new List<Absence>()
            {
                new Absence() {User = _testUsers[0], Id = 1, Date = new DateTime(2016,12,1),Status = Status.A},
                new Absence() {User = _testUsers[1], Id = 2, Date = new DateTime(2016,12,2),Status = Status.A},
                new Absence() {User = _testUsers[2], Id = 3, Date = new DateTime(2016,12,3),Status = Status.B}
            };
            _testDepartments[0].Users.Add(_testUsers[0]);
            _testDepartments[1].Users.Add(_testUsers[1]);
            _testDepartments[2].Users.Add(_testUsers[2]);
            _testUsers[0].Absences.Add(_testAbsences[0]);
            _testUsers[1].Absences.Add(_testAbsences[1]);
            _testUsers[2].Absences.Add(_testAbsences[2]);

            _testContext = new TestFravaerContext();
            _testRepository = new DLLFacade().GetDepartmentRepository(_testContext);

            foreach (var department in _testDepartments)
            {
                _testContext.Departments.Add(department);
            }
            foreach (var absence in _testAbsences)
            {
                _testContext.Absences.Add(absence);
            }
            foreach (var user in _testUsers)
            {
                _testContext.EndUsers.Add(user);
            }
        }
    }
}
