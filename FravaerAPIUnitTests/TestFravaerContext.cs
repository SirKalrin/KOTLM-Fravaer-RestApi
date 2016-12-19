using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;

namespace FravaerAPIUnitTests
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
