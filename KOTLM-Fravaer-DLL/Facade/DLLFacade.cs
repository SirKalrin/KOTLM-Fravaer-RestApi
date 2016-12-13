using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;
using KOTLM_Fravaer_DLL.Interfaces;
using KOTLM_Fravaer_DLL.Repositories;

namespace KOTLM_Fravaer_DLL.Facade
{
    public class DLLFacade
    {
        private IRepository<Absence, int, DateTime> _absenceRepository;
        private IRepository<Department, int, DateTime> _departmentRepository;
        private IRepository<User, int, DateTime> _userRepository;

        public IRepository<Absence, int, DateTime> GetAbsenceRepository()
        {
            return _absenceRepository ?? (_absenceRepository = new AbsenceRepository());
        }
   
        public IRepository<Department, int, DateTime> GetDepartmentRepository()
        {
            return _departmentRepository ?? (_departmentRepository = new DepartmentRepository());
        }

        public IRepository<User, int, DateTime> GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository());
        }
    }
}
