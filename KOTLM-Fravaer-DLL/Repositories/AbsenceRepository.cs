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
    class AbsenceRepository : IRepository<Absence, int>
    {
        private FravaerContext dbContext = new FravaerContext();
        public Absence Create(Absence t)
        {
            using (dbContext)
            {
                dbContext.Absences.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Absence Read(int id)
        {
            using (dbContext)
            {
                return dbContext.Absences.Include("User").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Absence> ReadAll()
        {
            using (dbContext)
            {
                return dbContext.Absences.Include("User").ToList();
            }
        }

        public Absence Update(Absence t)
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
            var toBeDeleted = dbContext.Absences.FirstOrDefault(x => x.Id == id);
            if (toBeDeleted != null)
            {
                dbContext.Absences.Remove(toBeDeleted);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
