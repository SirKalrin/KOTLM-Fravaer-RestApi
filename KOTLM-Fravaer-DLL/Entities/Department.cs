using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTLM_Fravaer_DLL.Entities
{
    public class Department : AbstractEntity
    {
        public List<User> Users { get; set; }
        public User DepartmentChief { get; set; }

    }
}
