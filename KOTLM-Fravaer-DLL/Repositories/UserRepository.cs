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
    class UserRepository : IRepository<User, int>
    {
        public User Create(User t)
        {
            using (var dbContext = new FravaerContext())
            {
                dbContext.Users.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public User Read(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<User> ReadAll()
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Update(User t)
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
                var toBeDeleted = dbContext.Users.FirstOrDefault(x => x.Id == id);
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
