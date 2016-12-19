using System;
using System.Collections.Generic;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Facade;
using KOTLM_Fravaer_DLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FravaerAPIUnitTests
{
    [TestClass]
    public class AbsenceCRUDTests
    {
        private List<Absence> _testAbsences;
        private List<User> _testUsers;
        private List<Department> _testDepartments;
        private IAbsenceRepository _testRepository;
        private IFravaerContext _testContext;

        /*
         * This method tests if we can create a new absences via the repository.
         */
        [TestMethod]
        public void CreateAbsenceTest()
        {
            _testContext.Absences.Local.Clear();
            var testAbsence = _testAbsences[0];
            _testRepository.Create(testAbsence);
            Assert.IsTrue(_testContext.Absences.Local.Count > 0);
            Assert.IsTrue(_testContext.Absences.Local.Contains(testAbsence));
            Assert.AreEqual(testAbsence, _testContext.Absences.Local[0]);
            Assert.IsNotNull(testAbsence);
        }

        /*
         * This method tests if we can read a single absences via the repository.
         */
        [TestMethod]
        public void ReadSingleAbsenceTest()
        {
            InitializeTestData();          
            Assert.AreEqual(_testRepository.Read(_testAbsences[0].Id), _testAbsences[0]);
            Assert.IsNotNull(_testAbsences[0]);
        }

        /*
         * This method tests if we can read all absences via the repository.
         */
        [TestMethod]
        public void ReadAllAbsenceTest()
        {
            InitializeTestData();
            var result = _testRepository.ReadAll();
            Assert.IsTrue(result.Count > 0);           
            int i = 0;
            foreach (var absence in _testAbsences)
            {
                Assert.AreEqual(absence, result[i]);
                i++;
            }
        }

        /*
         * This method tests if we can read a list of absences via the repository.
         * Only absences with a date larger or equal to the first date, and smaller or equal to the second date is returned.
         */
        [TestMethod]
        public void ReadIntervalAbsenceTest()
        {
            InitializeTestData();
            var result = _testRepository.ReadInterval(new DateTime(2016, 12, 2), new DateTime(2016, 12, 3));
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0], _testAbsences[1]);
            Assert.AreEqual(result[1], _testAbsences[2]);
            Assert.IsFalse(result.Contains(_testAbsences[0]));
        }

        /*
         * This method tests if we can update an absences variables via the repository.
         */
        [TestMethod]
        public void UpdateAbsenceTest()
        {
            InitializeTestData();
            var testAbsence = _testAbsences[0];
            testAbsence.User = _testUsers[1];
            testAbsence.Date = new DateTime(2017,5,16);
            testAbsence.Status = Status.S;
            _testRepository.Update(testAbsence);
            Assert.AreEqual(testAbsence, _testContext.Absences.Local[0]);
            Assert.IsNotNull(testAbsence);
        }

        /*
         * This method tests if we can completely delete an absence via the repository.
         */
        [TestMethod]
        public void DeleteAbsenceTest()
        {
            InitializeTestData();
            _testRepository.Delete(_testAbsences[0].Id);
            Assert.IsNull(_testRepository.Read(_testAbsences[0].Id));
            Assert.IsFalse(_testContext.Absences.Local.Contains(_testAbsences[0]));
        }

        /*
        * This method initializes all data needed for testing CRUD functionality via the AbsenceRepository.
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
            _testRepository = new DLLFacade().GetAbsenceRepository(_testContext);

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
