using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTLM_Fravaer_DLL.Entities
{
    public class Department : AbstractEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }

    }
}
