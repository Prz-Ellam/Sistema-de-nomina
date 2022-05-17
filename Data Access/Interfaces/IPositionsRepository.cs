using Data_Access.Entidades;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IPositionsRepository : IGenericRepository<Positions>
    {
        List<PositionsViewModel> ReadAll(string filter, int companyId);
    }
}
