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
        private IAbsenceRepository _absenceRepository;
        private IRepository<Department, int> _departmentRepository;
        private IRepository<User, int> _userRepository;

        public IAbsenceRepository GetAbsenceRepository(IFravaerContext context)
        {
            return _absenceRepository ?? (_absenceRepository = new AbsenceRepository(context));
        }
   
        public IRepository<Department, int> GetDepartmentRepository(IFravaerContext context)
        {
            return _departmentRepository ?? (_departmentRepository = new DepartmentRepository(context));
        }

        public IRepository<User, int> GetUserRepository(IFravaerContext context)
        {
            return _userRepository ?? (_userRepository = new UserRepository(context));
        }
    }
}
