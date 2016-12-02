using System;
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
            modelBuilder.Entity<User>().HasRequired(u => u.Department).WithMany(d => d.Users).WillCascadeOnDelete(false);

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
            int id = 0;
            for (int i = 1; i <= 4; i++)
            {
                User chief = new User() { Id = ++id, FirstName = $"Chief{i}", LastName = $"Senpei{i}", UserName = $"Chief{i}", Password = "chiefchief", Email = $"chief{i}@chief.dk", Role = Role.DepartmentChief, Absences = new List<Absence>() };
                User employee = new User() { Id = ++id, FirstName = $"Employee{i}", LastName = $"Padawan{i}", UserName = $"Employee{i}", Password = "employee", Email = $"employee{i}@employee.dk", Role = Role.Employee, Absences = new List<Absence>() };
                User admin = new User() { Id = ++id, FirstName = $"Admin{i}", LastName = $"Master{i}", UserName = $"Admin{i}", Password = "adminadmin", Email = $"admin{i}@admin.dk", Role = Role.Admin, Absences = new List<Absence>() };
                Absence a = new Absence() { Id = i, User = employee, Date = DateTime.Today.AddDays(i), Status = Status.FF };
                Department d = new Department() { Id = i, Name = $"Department {i}", Users = new List<User>() };

                employee.Absences.Add(a);
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
