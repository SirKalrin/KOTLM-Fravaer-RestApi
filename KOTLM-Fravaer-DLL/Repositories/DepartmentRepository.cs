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
        private FravaerContext dbContext = new FravaerContext();
        public Department Create(Department t)
        {
            using (dbContext)
            {
                dbContext.Departments.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Department Read(int id)
        {
            using (dbContext)
            {
                return dbContext.Departments.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Department> ReadAll()
        {
            using (dbContext)
            {
                return dbContext.Departments.ToList();
            }
        }

        public Department Update(Department t)
        {
            using (dbContext)
            {
                dbContext.Entry(t).State = EntityState.Modified;
                dbContext.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id)
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
