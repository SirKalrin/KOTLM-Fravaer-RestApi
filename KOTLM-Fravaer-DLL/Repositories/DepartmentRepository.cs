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
                return dbContext.Departments.Include("Users").ToList();
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
                var toBeDeleted = dbContext.Departments.FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null)
                {
                    dbContext.Departments.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
