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

            //modelBuilder.Entity<Department>().HasRequired(d => d.Users);
            //modelBuilder.Entity<Department>().HasRequired(d => d.DepartmentChief);
            modelBuilder.Entity<User>().HasRequired(u => u.Department).WithOptional(d => d.DepartmentChief);

            modelBuilder.Entity<Absence>().HasRequired(a => a.User).WithMany(u => u.Absences).WillCascadeOnDelete(false);


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
            for (int i = 1; i <= 4; i++)
            {                
                User chief = new User() { Id = i, FirstName = $"Chief{i}", LastName = $"Senpei{i}", UserName = $"Chief{i}", Password = "chiefchief", Email = $"chief{i}@chief.dk", Role = Role.DepartmentChief, Absences = new List<Absence>()};
                Department d = new Department() { Id = i, Name = $"Department {i}",Users = new List<User>(), DepartmentChief = chief };
                chief.Department = d;
                context.Departments.Add(d);
                
                User user = new User();
                



                //context.Departments.AddOrUpdate(d);
                //chief.Department = d;
                //context.Users.AddOrUpdate(chief);
                //d.Users.Add(chief);
                //d.DepartmentChief = chief;

            }
            base.Seed(context);
        }

        //protected override void Seed(FravaerContext context)
        //{
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        User chief = new User()
        //        {
        //            Absences = new List<Absence>(),
        //            Email = $"chief{i}@chief.dk",
        //            FirstName = $"Chief{i}"
        //        };

        //        var usertobesaved = chief;
        //        usertobesaved.Id = i;
        //        Department d = new Department() { Users = new List<User>() { usertobesaved }, DepartmentChief = usertobesaved };
        //        d = context.Departments.Add(d);
        //        d.Id = i;
        //        chief.Department = d;
        //        context.Users.Add(chief);

        //    }
        //    base.Seed(context);
        //}
    }
}
