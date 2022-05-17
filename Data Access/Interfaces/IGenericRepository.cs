using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        bool Create(T entity);
        
        bool Update(T entity);

        bool Delete(int id);

        //List<T> ReadAll();

        //List<T> ReadLike(string exp);
    }
}
