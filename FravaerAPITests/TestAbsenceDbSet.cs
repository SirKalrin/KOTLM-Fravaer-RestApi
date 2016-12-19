using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FravaerAPITests;
using KOTLM_Fravaer_DLL.Entities;

namespace FravaerAPIUnitTests
{
    class TestAbsenceDbSet : TestDbSet<Absence>
    {
        public override Absence Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
