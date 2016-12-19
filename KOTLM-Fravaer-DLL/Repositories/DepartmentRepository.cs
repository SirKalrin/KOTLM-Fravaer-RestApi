using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Context;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;
using KOTLM_Fravaer_DLL.Models;

namespace KOTLM_Fravaer_DLL.Repositories
{
    /*
     * This class implements the final phase of CRUD functionality for the Department entity, from the application to the database
     */
    class DepartmentRepository : IRepository<Department, int>
    {
        private IFravaerContext context;
        public DepartmentRepository(IFravaerContext context)
        {
            this.context = context;
        }
        /*
         * Writes the given Department to the database, returns it with an Id.
         */
        public Department Create(Department t)
        {
            using (var dbContext = GetContext())
            {
                dbContext.Departments.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Returns the Department with Id equal to id if it exists. Else returns null.
         */
        public Department Read(int id)
        {
            using (var dbContext = GetContext())
            {
                return dbContext.Departments.Include("Users").FirstOrDefault(x => x.Id == id);
            }
        }

        /*
         * Returns a List<Department> of all Departments in the database
         */
        public List<Department> ReadAll()
        {
            using (var dbContext = GetContext())
            {
                List<Department> departments = dbContext.Departments.Include("Users").ToList();
                foreach (var department in departments)
                {
                    foreach (var user in department.Users)
                    {
                        List<Absence> absences = dbContext.EndUsers.Include("Absences").FirstOrDefault(model => model.Id == user.Id).Absences;
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

        /*
         * Overwrites the Department in the database with equal Id to the given Department. 
         * Returns the given Department.
         */
        public Department Update(Department t)
        {
            using (var dbContext = GetContext())
            {
                dbContext.MarkDepartmentAsModified(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Deletes the Department with Id matching to the given id on the database.
         * Returns true if succes, and false if it fails.
         */
        public bool Delete(int id)
        {
            using (var dbContext = GetContext())
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
        private IFravaerContext GetContext()
        {
            if (context.GetType().FullName.Equals("KOTLM_Fravaer_DLL.Models.ApplicationDbContext"))
            {
                return new ApplicationDbContext();
            }
            return context;
        }
    }
}
