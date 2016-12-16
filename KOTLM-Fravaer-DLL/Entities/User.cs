using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTLM_Fravaer_DLL.Entities
{
    public enum Role
    {
        Medarbejder, Afdelingsleder, Administrator
    }
    public class User : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Absence> Absences { get; set; }
        public Department Department { get; set; }
        public Role Role { get; set; }
        public DateTime EditFromDate { get; set; }

    }
}
