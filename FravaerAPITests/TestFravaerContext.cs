using System.Data.Entity;
using FravaerAPIUnitTests;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;

namespace FravaerAPITests
{
    class TestFravaerContext : IFravaerContext
    {
        public TestFravaerContext()
        {
            this.EndUsers = new TestUserDbSet();
            this.Absences = new TestAbsenceDbSet();
            this.Departments = new TestDepartmentDbSet();
        }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<User> EndUsers { get; set; }
        public DbSet<Absence> Absences { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkUserAsModified(User user, User oldUser)
        {
        }

        public void MarkAbsenceAsModified(Absence absence, Absence oldAbsence)
        {
        }

        public void MarkDepartmentAsModified(Department departmentt)
        {
        }

        public void Dispose()
        {
        }
    }
}
