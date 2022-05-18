using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IGenericRepository<T, U> 
        where T : class
        where U : class
    {
        bool Create(T entity);
        
        bool Update(T entity);

        bool Delete(int id);

        List<U> Read(string filter, int companyId);
    }
}
