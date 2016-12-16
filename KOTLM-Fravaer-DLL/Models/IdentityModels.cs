
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Context;
using KOTLM_Fravaer_DLL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace KOTLM_Fravaer_DLL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDBInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
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
        public DbSet<User> EndUsers { get; set; }


    }

    public class ApplicationDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role1 = new IdentityRole()
                {
                    Name = "Medarbejder"
                };
                var role2 = new IdentityRole()
                {
                    Name = "Afdelingsleder"
                };
                var role3 = new IdentityRole
                {
                    Name = "Administrator"
                };

                roleManager.Create(role1);
                roleManager.Create(role2);
                roleManager.Create(role3);
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);

                var user = new ApplicationUser
                {
                    Email = "nbo@eeu.dk",
                    UserName = "Admin"
                };
                userManager.Create(user, "!Administrator1");
                userManager.AddToRole(user.Id, "Administrator");
            }

            Department readOnlyDepartment = new Department()
            {
                Id = 1,
                Name = $"Utildelte Medarbejdere",
                Users = new List<User>()
            };
            context.Departments.Add(readOnlyDepartment);

            Department fælles = new Department() { Id = 2, Name = "Fælles", Users = new List<User>() };
            Department erhvervs = new Department() { Id = 3, Name = "Erhverv", Users = new List<User>() };
            Department markerting = new Department() { Id = 4, Name = "Marketing", Users = new List<User>() };

            User admin = new User()
            {
                FirstName = "Niels",
                LastName = "Bock",
                UserName = "Admin",
                Email = "nbo@eeu.dk",
                Role = Role.Administrator,
                Absences = new List<Absence>(),
                Password = "!Administrator1"
               , EditFromDate = DateTime.Now
            };
            User chief1 = new User()
            {
                FirstName = "Tom",
                LastName = "Lykkegaard Nielsen",
                UserName = "tln",
                Email = "tln@eeu.dk",
                Role = Role.Afdelingsleder,
                Absences = new List<Absence>()
                ,
                EditFromDate = DateTime.Now
            };
            User chief2 = new User()
            {
                FirstName = "Birgit",
                LastName = "Bech Jensen",
                UserName = "bbj",
                Email = "bbj@eeu.dk",
                Role = Role.Afdelingsleder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User chief3 = new User()
            {
                FirstName = "Karsten",
                LastName = "Rieder",
                UserName = "kar",
                Email = "kar@eeu.dk",
                Role = Role.Afdelingsleder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee = new User()
            {
                FirstName = "Noah",
                LastName = "Bock",
                UserName = "nob",
                Email = "nob@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee2 = new User()
            {
                FirstName = "Søs",
                LastName = "Knudsen",
                UserName = "skn",
                Email = "mks@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee3 = new User()
            {
                FirstName = "Mikael",
                LastName = "Simonsen",
                UserName = "mks",
                Email = "mks@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee4 = new User()
            {
                FirstName = "Gitte",
                LastName = "Sydbøge",
                UserName = "gsy",
                Email = "gsy@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee5 = new User()
            {
                FirstName = "Peter",
                LastName = "Hegelund",
                UserName = "phe",
                Email = "phe@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee6 = new User()
            {
                FirstName = "Gert",
                LastName = "Laustsen",
                UserName = "gla",
                Email = "gla@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee7 = new User()
            {
                FirstName = "Lasse",
                LastName = "Jensen",
                UserName = "laj",
                Email = "laj@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee8 = new User()
            {
                FirstName = "Uffe",
                LastName = "Lundgaard",
                UserName = "ufl",
                Email = "ufl@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee9 = new User()
            {
                FirstName = "Randi",
                LastName = "Høxbro",
                UserName = "rah",
                Email = "rah@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee10 = new User()
            {
                FirstName = "Sofie",
                LastName = "Brandt",
                UserName = "sbr",
                Email = "sbr@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee11 = new User()
            {
                FirstName = "Sylvia",
                LastName = "Steinberg",
                UserName = "ses",
                Email = "ses@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };
            User employee12 = new User()
            {
                FirstName = "Kirsten",
                LastName = "Rasmussen",
                UserName = "kra",
                Email = "kra@eeu.dk",
                Role = Role.Medarbejder,
                Absences = new List<Absence>(),
                EditFromDate = DateTime.Now
            };

            Absence a = new Absence() { User = employee, Date = DateTime.Today.AddDays(10), Status = Status.FF };
            Absence a2 = new Absence() { User = employee, Date = DateTime.Today.AddDays(5), Status = Status.FF };
            Absence a3 = new Absence() { User = employee, Date = DateTime.Today.AddDays(2), Status = Status.B };
            Absence a4 = new Absence() { User = employee, Date = DateTime.Today.AddDays(6), Status = Status.SN };

            Absence a5 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(4), Status = Status.S };
            Absence a6 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(11), Status = Status.S };
            Absence a7 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(9), Status = Status.HA };
            Absence a8 = new Absence() { User = employee2, Date = DateTime.Today.AddDays(3), Status = Status.HA };

            Absence a9 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(12), Status = Status.A };
            Absence a10 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(5), Status = Status.A };
            Absence a11 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(9), Status = Status.F };
            Absence a12 = new Absence() { User = employee3, Date = DateTime.Today.AddDays(0), Status = Status.F };

            Absence a13 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(7), Status = Status.F };
            Absence a14 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(2), Status = Status.F };
            Absence a15 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(12), Status = Status.S };
            Absence a16 = new Absence() { User = employee4, Date = DateTime.Today.AddDays(8), Status = Status.S };

            Absence a17 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(1), Status = Status.A };
            Absence a18 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(3), Status = Status.A };
            Absence a19 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(7), Status = Status.SN };
            Absence a20 = new Absence() { User = employee5, Date = DateTime.Today.AddDays(4), Status = Status.HA };

            Absence a21 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(2), Status = Status.FF };
            Absence a22 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(8), Status = Status.FF };
            Absence a23 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(3), Status = Status.FF };
            Absence a24 = new Absence() { User = employee6, Date = DateTime.Today.AddDays(6), Status = Status.FF };

            Absence a25 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(1), Status = Status.F };
            Absence a26 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(2), Status = Status.HFF };
            Absence a27 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(8), Status = Status.AF };
            Absence a28 = new Absence() { User = employee7, Date = DateTime.Today.AddDays(4), Status = Status.F };

            Absence a29 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(1), Status = Status.FF };
            Absence a30 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(3), Status = Status.S };
            Absence a31 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(8), Status = Status.HFF };
            Absence a32 = new Absence() { User = employee8, Date = DateTime.Today.AddDays(4), Status = Status.F };

            Absence a33 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(1), Status = Status.B };
            Absence a34 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(9), Status = Status.SN };
            Absence a35 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(3), Status = Status.HA };
            Absence a36 = new Absence() { User = employee9, Date = DateTime.Today.AddDays(4), Status = Status.K };

            admin.Department = fælles;
            chief1.Department = erhvervs;
            chief2.Department = fælles;
            chief3.Department = markerting;

            employee.Department = erhvervs;
            employee2.Department = erhvervs;
            employee3.Department = erhvervs;
            employee4.Department = erhvervs;

            employee5.Department = fælles;
            employee6.Department = fælles;
            employee7.Department = fælles;
            employee8.Department = fælles;

            employee9.Department = markerting;
            employee10.Department = markerting;
            employee11.Department = markerting;
            employee12.Department = markerting;

            employee.Absences.Add(a);
            employee.Absences.Add(a2);
            employee.Absences.Add(a3);
            employee.Absences.Add(a4);

            employee2.Absences.Add(a5);
            employee2.Absences.Add(a6);
            employee2.Absences.Add(a7);
            employee2.Absences.Add(a8);

            employee3.Absences.Add(a9);
            employee3.Absences.Add(a10);
            employee3.Absences.Add(a11);
            employee3.Absences.Add(a12);

            employee4.Absences.Add(a13);
            employee4.Absences.Add(a14);
            employee4.Absences.Add(a15);
            employee4.Absences.Add(a16);

            employee5.Absences.Add(a17);
            employee5.Absences.Add(a18);
            employee5.Absences.Add(a19);
            employee5.Absences.Add(a20);

            employee6.Absences.Add(a21);
            employee6.Absences.Add(a22);
            employee6.Absences.Add(a23);
            employee6.Absences.Add(a24);

            employee7.Absences.Add(a25);
            employee7.Absences.Add(a26);
            employee7.Absences.Add(a27);
            employee7.Absences.Add(a28);

            employee8.Absences.Add(a29);
            employee8.Absences.Add(a30);
            employee8.Absences.Add(a31);
            employee8.Absences.Add(a32);

            employee9.Absences.Add(a33);
            employee9.Absences.Add(a34);
            employee9.Absences.Add(a35);
            employee9.Absences.Add(a36);

            context.EndUsers.Add(admin);
            context.EndUsers.Add(chief1);
            context.EndUsers.Add(chief2);
            context.EndUsers.Add(chief3);
            context.EndUsers.Add(employee);
            context.EndUsers.Add(employee2);
            context.EndUsers.Add(employee3);
            context.EndUsers.Add(employee4);
            context.EndUsers.Add(employee5);
            context.EndUsers.Add(employee6);
            context.EndUsers.Add(employee7);
            context.EndUsers.Add(employee8);
            context.EndUsers.Add(employee9);
            context.EndUsers.Add(employee10);
            context.EndUsers.Add(employee11);
            context.EndUsers.Add(employee12);

            //Department dinMor = new Department()
            //{
            //    Id = 2,
            //    Users = new List<User>(),
            //    Name = "Din mor"
            //};
            //context.Departments.Add(dinMor);

            //User bille = new User()
            //{
            //    Department = dinMor,
            //    Id = 1,
            //    UserName = "TheBug",
            //    LastName = "Bug",
            //    Role = Role.Administrator,
            //    Password = "345#¤%ertERT",
            //    Absences = new List<Absence>(),
            //    Email = "dr.iversen2@hotmail.com",
            //    FirstName = "Bille"
            //    ,
            //    EditFromDate = DateTime.Now.AddMonths(2)
            //};
            //context.EndUsers.Add(bille);
            //User bille2 = new User()
            //{
            //    Department = dinMor,
            //    Id = 2,
            //    UserName = "TheBug2",
            //    LastName = "Bug2",
            //    Role = Role.Administrator,
            //    Password = "345#¤%ertERT",
            //    Absences = new List<Absence>(),
            //    Email = "huskatgodkende@gmail.com",
            //    FirstName = "Bille2"
            //    ,
            //    EditFromDate = DateTime.Now.AddMonths(-1)
            //};
            //context.EndUsers.Add(bille2);
            base.Seed(context);
        }
    }
}
