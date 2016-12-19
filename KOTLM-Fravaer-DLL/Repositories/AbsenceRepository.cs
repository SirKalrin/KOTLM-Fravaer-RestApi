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
     * This class implements the final phase of CRUD functionality for the Absence entity, from the application to the database
     */
    class AbsenceRepository : IAbsenceRepository
    {
        private IFravaerContext context;
        public AbsenceRepository(IFravaerContext context)
        {
            this.context = context;
        }
        /*
         * Writes the given Absence to the database, returns it with an Id.
         */
        public Absence Create(Absence t)
        {
            using (var dbContext = GetContext())
            {
                t.User = dbContext.EndUsers.FirstOrDefault(x => x.Id == t.User.Id);
                dbContext.Absences.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Returns the Absence with Id equal to id if it exists. Else returns null.
         */
        public Absence Read(int id)
        {
            using (var dbContext = GetContext())
            {
                return dbContext.Absences.Include("User").FirstOrDefault(x => x.Id == id);
            }
        }

        /*
         * Returns a list<Absence> with date equal or smaller to firstDate, and equal or larger to lastDate
         */
        public List<Absence> ReadInterval(DateTime firstDate, DateTime lastDate)
        {
            using (var dbContext = GetContext())
            {
                var absencesInRange = (IQueryable<Absence>)from a in dbContext.Absences.Include("User") where a.Date >= firstDate && a.Date <= lastDate select a;
                return absencesInRange.ToList();
            }
        }
        /*
         * Returns a List<Absense> of all Absense in the database
         */
        public List<Absence> ReadAll()
        {
            using (var dbContext = GetContext())
            {
                return dbContext.Absences.Include("User").ToList();
            }
        }

        /*
         * Overwrites the Absence in the database with equal Id to the given Absence. 
         * Returns the given absence.
         */
        public Absence Update(Absence t)
        {
            using (var dbContext = GetContext())
            {
                t.User = dbContext.EndUsers.Include("Absences").FirstOrDefault(x => x.Id == t.User.Id);
                var oldAbsence = dbContext.Absences.FirstOrDefault(x => x.Id == t.Id);
                oldAbsence.User = t.User;
                dbContext.MarkAbsenceAsModified(t, oldAbsence);
                dbContext.SaveChanges();
                return t;
            }
        }

        /*
         * Deletes the Absence with Id matching to the given id on the database.
         * Returns true if succes, and false if it fails.
         */
        public bool Delete(int id)
        {
            using (var dbContext = GetContext())
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
