using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;

namespace KOTLM_Fravaer_DLL.Interfaces
{
    /*
     * This interface enable implementing members to have the ReadInterval method, while still upholding the IRepository standarts.
     */ 
    public interface IAbsenceRepository : IRepository<Absence, int>
    {
        List<Absence> ReadInterval(DateTime d1, DateTime d2);
    }
}
