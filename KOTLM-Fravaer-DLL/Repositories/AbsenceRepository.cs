﻿using System;
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
        public Absence Create(Absence t)
        {
            using (var dbContext = new FravaerContext())
            {
                t.User = dbContext.Users.FirstOrDefault(x => x.Id == t.User.Id);
                dbContext.Absences.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Absence Read(int id)
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Absences.Include("User").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Absence> ReadAll()
        {
            using (var dbContext = new FravaerContext())
            {
                return dbContext.Absences.Include("User").ToList();
            }
        }

        public Absence Update(Absence t)
        {
            using (var dbContext = new FravaerContext())
            {
                t.User = dbContext.Users.Include("Absences").FirstOrDefault(x => x.Id == t.User.Id);
                var oldAbsence = dbContext.Absences.FirstOrDefault(x => x.Id == t.Id);
                oldAbsence.User = t.User;
                dbContext.Entry(oldAbsence).CurrentValues.SetValues(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id)
        {
            using (var dbContext = new FravaerContext())
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
}
