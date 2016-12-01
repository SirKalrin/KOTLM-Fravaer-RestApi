using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KOTLM_Fravaer_DLL.Entities
{
    public enum Status
    {
        S, HS, F, HF, FF, HFF, K, B, BS, AF, A, HA, SN
    }
    public class Absence : AbstractEntity
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }

    }
}
