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
            modelBuilder.Entity<User>().HasOptional(u => u.Department).WithMany(d => d.Users);
            //.WillCascadeOnDelete(false);

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
            Department readOnlyDepartment = new Department()
            {
                Id = 1,
                Name = $"Utildelte Medarbejdere",
                Users = new List<User>()
            };
            context.Departments.Add(readOnlyDepartment);

            for (int i = 1; i <= 4; i++)
            {
                User chief = new User()
                {
                    FirstName = $"Afdelingsleder{i}",
                    LastName = $"Senpei{i}",
                    UserName = $"Afdelingsleder{i}",
                    Email = $"afdelingsleder{i}@afdelingsleder.dk",
                    Role = Role.Afdelingsleder,
                    Absences = new List<Absence>()
                };
                User employee = new User()
                {
                    FirstName = $"Medarbejder{i}",
                    LastName = $"Padawan{i}",
                    UserName = $"Medarbejder{i}",
                    Email = $"medarbejder{i}@medarbejder.dk",
                    Role = Role.Medarbejder,
                    Absences = new List<Absence>()
                };
                User admin = new User()
                {
                    FirstName = $"Administrator{i}",
                    LastName = $"Master{i}",
                    UserName = $"Administrator{i}",
                    Email = $"administrator{i}@administrator.dk",
                    Role = Role.Administrator,
                    Absences = new List<Absence>()
                };
                Absence a = new Absence() { User = employee, Date = DateTime.Today.AddDays(i), Status = Status.FF };
                Absence ab = new Absence() { User = employee, Date = DateTime.Today.AddDays(i + 1), Status = Status.FF };
                Absence ac = new Absence() { User = employee, Date = DateTime.Today.AddDays(i + 2), Status = Status.FF };
                Absence ad = new Absence() { User = employee, Date = DateTime.Today.AddDays(i + 3), Status = Status.FF };
                Department d = new Department() { Id = i + 1, Name = $"Afdeling {i}", Users = new List<User>() };

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
            //Department fælles = new Department() { Id = 2, Name = "Fælles", Users = new List<User>() };
            //Department erhvervs = new Department() { Id = 3, Name = "Erhverv", Users = new List<User>() };
            //Department markerting = new Department() { Id = 4, Name = "Marketing", Users = new List<User>() };
            //Department ribeturist = new Department() { Id = 5, Name = "Ribe Turistbureau", Users = new List<User>() };
            //Department esbjergturist = new Department()
            //{
            //    Id = 6,
            //    Name = "Esbjerg Turistbureau",
            //    Users = new List<User>()
            //};
            //Department fanøturist = new Department() { Id = 7, Name = "Fanø Turistbureau", Users = new List<User>() };
            //Department turist = new Department() { Id = 8, Name = "Turist", Users = new List<User>() };

            //User admin = new User()
            //{
            //    FirstName = "Niels",
            //    LastName = "Bock",
            //    UserName = "nbo",
            //    Email = "nbo@eeu.dk",
            //    Role = Role.Administrator,
            //    Absences = new List<Absence>(),
            //};
            //User chief1 = new User()
            //{
            //    FirstName = "Tom",
            //    LastName = "Lykkegaard Nielsen",
            //    UserName = "tln",
            //    Email = "tln@eeu.dk",
            //    Role = Role.Afdelingsleder,
            //    Absences = new List<Absence>(),
            //};
            //User chief2 = new User()
            //{
            //    FirstName = "Birgit",
            //    LastName = "Bech Jensen",
            //    UserName = "bbj",
            //    Email = "bbj@eeu.dk",
            //    Role = Role.Afdelingsleder,
            //    Absences = new List<Absence>(),
            //};
            //User chief3 = new User()
            //{
            //    FirstName = "Karsten",
            //    LastName = "Rieder",
            //    UserName = "kar",
            //    Email = "kar@eeu.dk",
            //    Role = Role.Afdelingsleder,
            //    Absences = new List<Absence>(),
            //};
            //User employee = new User()
            //{
            //    FirstName = "Noah",
            //    LastName = "Bock",
            //    UserName = "nob",
            //    Email = "nob@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee2 = new User()
            //{
            //    FirstName = "Søs",
            //    LastName = "Knudsen",
            //    UserName = "skn",
            //    Email = "mks@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee3 = new User()
            //{
            //    FirstName = "Mikael",
            //    LastName = "Simonsen",
            //    UserName = "mks",
            //    Email = "mks@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee4 = new User()
            //{
            //    FirstName = "Gitte",
            //    LastName = "Sydbøge",
            //    UserName = "gsy",
            //    Email = "gsy@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee5 = new User()
            //{
            //    FirstName = "Peter",
            //    LastName = "Hegelund",
            //    UserName = "phe",
            //    Email = "phe@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee6 = new User()
            //{
            //    FirstName = "Gert",
            //    LastName = "Laustsen",
            //    UserName = "gla",
            //    Email = "gla@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee7 = new User()
            //{
            //    FirstName = "Lasse",
            //    LastName = "Jensen",
            //    UserName = "laj",
            //    Email = "laj@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee8 = new User()
            //{
            //    FirstName = "Uffe",
            //    LastName = "Lundgaard",
            //    UserName = "ufl",
            //    Email = "ufl@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee9 = new User()
            //{
            //    FirstName = "Randi",
            //    LastName = "Høxbro",
            //    UserName = "rah",
            //    Email = "rah@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee10 = new User()
            //{
            //    FirstName = "Sofie",
            //    LastName = "Brandt",
            //    UserName = "sbr",
            //    Email = "sbr@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};
            //User employee11 = new User()
            //{
            //    FirstName = "Sylvia",
            //    LastName = "Steinberg",
            //    UserName = "ses",
            //    Email = "ses@eeu.dk",
            //    Role = Role.Medarbejder,
            //    Absences = new List<Absence>(),
            //};

            //Absence a = new Absence() { User = employee, Date = DateTime.Today.AddDays(10), Status = Status.FF };
            //Absence a2 = new Absence() { User = employee, Date = DateTime.Today.AddDays(5), Status = Status.FF };
            //Absence a3 = new Absence() { User = employee, Date = DateTime.Today.AddDays(2), Status = Status.B };
            //Absence a4 = new Absence() { User = employee, Date = DateTime.Today.AddDays(6), Status = Status.SN };

            //Absence a5 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(4), Status = Status.S };
            //Absence a6 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(11), Status = Status.S };
            //Absence a7 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(9), Status = Status.HA };
            //Absence a8 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(3), Status = Status.HA };

            //Absence a9 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(12), Status = Status.A };
            //Absence a10 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(5), Status = Status.A };
            //Absence a11 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(9), Status = Status.F };
            //Absence a12 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(0), Status = Status.F };

            //Absence a13 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(7), Status = Status.F };
            //Absence a14 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(2), Status = Status.F };
            //Absence a15 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(12), Status = Status.S };
            //Absence a16 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(8), Status = Status.S };

            //Absence a17 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(1), Status = Status.A };
            //Absence a18 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(3), Status = Status.A };
            //Absence a19 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(7), Status = Status.SN };
            //Absence a20 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(4), Status = Status.HA };

            //Absence a21 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(2), Status = Status.FF };
            //Absence a22 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(8), Status = Status.FF };
            //Absence a23 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(3), Status = Status.FF };
            //Absence a24 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(6), Status = Status.FF };

            //Absence a25 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(1), Status = Status.F };
            //Absence a26 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(2), Status = Status.HFF };
            //Absence a27 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(8), Status = Status.AF };
            //Absence a28 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(4), Status = Status.F };

            //Absence a29 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(1), Status = Status.FF };
            //Absence a30 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(3), Status = Status.S };
            //Absence a31 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(8), Status = Status.HFF };
            //Absence a32 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(4), Status = Status.F };

            //Absence a33 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(1), Status = Status.B };
            //Absence a34 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(9), Status = Status.SN };
            //Absence a35 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(3), Status = Status.HA };
            //Absence a36 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(4), Status = Status.K };

            //admin.Department = fælles;
            //chief1.Department = erhvervs;
            //chief2.Department = fælles;
            //chief3.Department = markerting;

            //employee.Department = erhvervs;
            //employee.Department = erhvervs;
            //employee.Department = erhvervs;
            //employee.Department = erhvervs;

            //employee.Department = fælles;
            //employee.Department = fælles;
            //employee.Department = fælles;
            //employee.Department = fælles;

            //employee.Department = markerting;
            //employee.Department = markerting;
            //employee.Department = markerting;
            //employee.Department = markerting;

            //employee.Absences.Add(a);
            //employee.Absences.Add(a2);
            //employee.Absences.Add(a3);
            //employee.Absences.Add(a4);

            //employee2.Absences.Add(a5);
            //employee2.Absences.Add(a6);
            //employee2.Absences.Add(a7);
            //employee2.Absences.Add(a8);

            //employee3.Absences.Add(a9);
            //employee3.Absences.Add(a10);
            //employee3.Absences.Add(a11);
            //employee3.Absences.Add(a12);

            //employee4.Absences.Add(a13);
            //employee4.Absences.Add(a14);
            //employee4.Absences.Add(a15);
            //employee4.Absences.Add(a16);

            //employee5.Absences.Add(a17);
            //employee5.Absences.Add(a18);
            //employee5.Absences.Add(a19);
            //employee5.Absences.Add(a20);

            //employee6.Absences.Add(a21);
            //employee6.Absences.Add(a22);
            //employee6.Absences.Add(a23);
            //employee6.Absences.Add(a24);

            //employee7.Absences.Add(a25);
            //employee7.Absences.Add(a26);
            //employee7.Absences.Add(a27);
            //employee7.Absences.Add(a28);

            //employee8.Absences.Add(a29);
            //employee8.Absences.Add(a30);
            //employee8.Absences.Add(a31);
            //employee8.Absences.Add(a32);

            //employee9.Absences.Add(a33);
            //employee9.Absences.Add(a34);
            //employee9.Absences.Add(a35);
            //employee9.Absences.Add(a36);



            //context.Users.Add(admin);
            //context.Users.Add(chief1);
            //context.Users.Add(chief2);
            //context.Users.Add(chief3);
            //context.Users.Add(employee);
            //context.Users.Add(employee2);
            //context.Users.Add(employee3);
            //context.Users.Add(employee4);
            //context.Users.Add(employee5);
            //context.Users.Add(employee6);
            //context.Users.Add(employee7);
            //context.Users.Add(employee8);
            //context.Users.Add(employee9);
            //context.Users.Add(employee10);
            //context.Users.Add(employee11);

            base.Seed(context);
        }
    }
}
