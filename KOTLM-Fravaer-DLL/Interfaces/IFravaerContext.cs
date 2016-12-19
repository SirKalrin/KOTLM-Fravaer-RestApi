using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;

namespace KOTLM_Fravaer_DLL.Interfaces
{
    public interface IFravaerContext : IDisposable
    {
        DbSet<User> EndUsers { get; set; }
        int SaveChanges();
        void MarkUserAsModified(User user, User oldUser);
        DbSet<Absence> Absences { get; set; }
        void MarkAbsenceAsModified(Absence absence, Absence oldAbsence);
        DbSet<Department> Departments { get; set; }
        void MarkDepartmentAsModified(Department department);
    }
}
