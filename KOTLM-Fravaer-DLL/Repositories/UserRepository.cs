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

namespace KOTLM_Fravaer_DLL.Repositories
{
    /*
     * This class implements the final phase of CRUD functionality for the User entity, from the application to the database
     */
    class UserRepository : IRepository<User, int>
    {
        /*
         * Writes the given User to the database, returns it with an Id.
         */
        public User Create(User t)
        {
            using (var dbContext = new FravaerContext())
            {
                t.Department = dbContext.Departments.FirstOrDefault(x => x.Id == t.Department.Id);
                dbContext.Users.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Returns the User with Id equal to id if it exists. Else returns null.
         */
        public User Read(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Users.Include("Department").Include("Absences").FirstOrDefault(x => x.Id == id);
            }
        }

        /*
         * Returns a List<User> of all Users in the database
         */
        public List<User> ReadAll()
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Users.Include("Department").Include("Absences").ToList();
            }
        }

        /*
         * Overwrites the User in the database with equal Id to the given User. 
         * Returns the given User.
         */
        public User Update(User t)
        {
            using (var dbContext = new FravaerContext())
            {
                t.Department = dbContext.Departments.Include("Users").FirstOrDefault(x => x.Id == t.Department.Id);
                var oldUser = dbContext.Users.FirstOrDefault(x => x.Id == t.Id);
                oldUser.Department = t.Department;
                dbContext.Entry(oldUser).CurrentValues.SetValues(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Deletes the User with Id matching to the given id on the database.
         * Returns true if succes, and false if it fails.
         */
        public bool Delete(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                var toBeDeleted = dbContext.Users.Include("Absences").FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null)
                {
                    dbContext.Users.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
