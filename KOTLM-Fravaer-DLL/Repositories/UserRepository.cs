using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Context;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;
using KOTLM_Fravaer_DLL.Models;

namespace KOTLM_Fravaer_DLL.Repositories
{
    class UserRepository : IRepository<User, int>
    {
        public User Create(User t)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                t.Department = dbContext.Departments.FirstOrDefault(x => x.Id == t.Department.Id);
                dbContext.EndUsers.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public User Read(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.EndUsers.Include("Department").Include("Absences").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<User> ReadAll()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.EndUsers.Include("Department").Include("Absences").ToList();
            }
        }

        public User Update(User t)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                t.Department = dbContext.Departments.Include("Users").FirstOrDefault(x => x.Id == t.Department.Id);
                var oldUser = dbContext.EndUsers.FirstOrDefault(x => x.Id == t.Id);
                oldUser.Department = t.Department;
                dbContext.Entry(oldUser).CurrentValues.SetValues(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var toBeDeleted = dbContext.EndUsers.Include("Absences").FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null)
                {
                    dbContext.EndUsers.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
