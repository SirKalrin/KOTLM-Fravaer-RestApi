using System.Linq;
using KOTLM_Fravaer_DLL.Entities;

namespace FravaerAPITests
{
    class TestDepartmentDbSet : TestDbSet<Department>
    {
        public override Department Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
