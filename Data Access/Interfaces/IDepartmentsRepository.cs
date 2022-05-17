using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entidades;
using Data_Access.ViewModels;

namespace Data_Access.Interfaces
{
    public interface IDepartmentsRepository : IGenericRepository<Departments>
    {
        List<DepartmentsViewModel> ReadAll(string like, int companyId);
        List<DepartmentsPayrollViewModel> ReadPayrolls(int companyId, DateTime date);
    }
}
