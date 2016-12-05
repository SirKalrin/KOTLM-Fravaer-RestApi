﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;

namespace KOTLM_Fravaer_DLL.Context
{
    public class FravaerContext : DbContext
    {
        public FravaerContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new DBInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOptional(u => u.Department).WithMany(d => d.Users);//.WillCascadeOnDelete(false);

            //This line is HOLY!
            modelBuilder.Entity<Absence>().HasRequired(a => a.User).WithMany(u => u.Absences);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

    }

    public class DBInitializer : DropCreateDatabaseAlways<FravaerContext>
    {
        protected override void Seed(FravaerContext context)
        {
            int userId = 0;
            int absenceId = 0;
            for (int i = 1; i <= 4; i++)
            {
                User chief = new User() { Id = ++userId, FirstName = $"Afdelingsleder{i}", LastName = $"Senpei{i}", UserName = $"Afdelingsleder{i}", Password = "afdelingslederpassword", Email = $"afdelingsleder{i}@afdelingsleder.dk", Role = Role.Afdelingsleder, Absences = new List<Absence>() };
                User employee = new User() { Id = ++userId, FirstName = $"Medarbejder{i}", LastName = $"Padawan{i}", UserName = $"Medarbejder{i}", Password = "medarbejderpassword", Email = $"medarbejder{i}@medarbejder.dk", Role = Role.Medarbejder, Absences = new List<Absence>() };
                User admin = new User() { Id = ++userId, FirstName = $"Administrator{i}", LastName = $"Master{i}", UserName = $"Administrator{i}", Password = "adminpassword", Email = $"administrator{i}@administrator.dk", Role = Role.Administrator, Absences = new List<Absence>() };
                Absence a = new Absence() { Id = ++absenceId, User = employee, Date = DateTime.Today.AddDays(i), Status = Status.FF };
                Absence ab = new Absence() { Id = ++absenceId, User = employee, Date = DateTime.Today.AddDays(i+1), Status = Status.FF };
                Absence ac = new Absence() { Id = ++absenceId, User = employee, Date = DateTime.Today.AddDays(i+2), Status = Status.FF };
                Absence ad = new Absence() { Id = ++absenceId, User = employee, Date = DateTime.Today.AddDays(i+3), Status = Status.FF };
                Department d = new Department() { Id = i, Name = $"Afdeling {i}", Users = new List<User>() };

                employee.Absences.Add(a);
                employee.Absences.Add(ab);
                employee.Absences.Add(ac);
                employee.Absences.Add(ad);
                chief.Department = d;
                admin.Department = d;
                employee.Department = d;
                context.Users.Add(chief);
                context.Users.Add(admin);
                context.Users.Add(employee);
            }
            base.Seed(context);
        }
    }
}
