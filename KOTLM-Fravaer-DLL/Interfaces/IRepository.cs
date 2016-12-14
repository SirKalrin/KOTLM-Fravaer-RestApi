using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOTLM_Fravaer_DLL.Entities;

namespace KOTLM_Fravaer_DLL.Interfaces
{
    /*
     * This interface ensures CRUD functionality in the implementing classes
     */
    public interface IRepository<T, K> where T : AbstractEntity
    {
        T Create(T t);
        T Read(K id);
        List<T> ReadAll();
        T Update(T t);
        bool Delete(K id);
    }
}
