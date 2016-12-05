using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Context;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;

namespace KOTLM_Fravaer_DLL.Repositories
{
    class DepartmentRepository : IRepository<Department, int>
    {
        public Department Create(Department t)
        {
            using (var dbContext = new FravaerContext())
            {
                dbContext.Departments.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Department Read(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Departments.Include("Users").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Department> ReadAll()
        {
            using (var dbContext = new FravaerContext())
            {
                List<Department> departments = dbContext.Departments.Include("Users").ToList();
                foreach (var department in departments)
                {
                    foreach (var user in department.Users)
                    {
                        List<Absence> absences = dbContext.Users.Include("Absences").FirstOrDefault(model => model.Id == user.Id).Absences;
                        if (absences != null)
                        {
                            user.Absences = absences;
                        }
                        else
                        {
                            user.Absences = new List<Absence>();
                        }
                    }
                }
                return departments;
            }
        }

        public Department Update(Department t)
        {
            using (var dbContext = new FravaerContext())
            {
                dbContext.Entry(t).State = EntityState.Modified;
                dbContext.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                var toBeDeleted = dbContext.Departments.Include("Users").FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null && toBeDeleted.Id != 1)
                {
                    foreach (var user in toBeDeleted.Users)
                    {
                        user.Department = dbContext.Departments.FirstOrDefault(x => x.Id == 1);
                    }
                    dbContext.Departments.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
