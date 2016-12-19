using System.Linq;
using KOTLM_Fravaer_DLL.Entities;

namespace FravaerAPITests
{
    class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
