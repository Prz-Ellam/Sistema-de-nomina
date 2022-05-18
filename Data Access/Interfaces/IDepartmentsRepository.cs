using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entidades;
using Data_Access.ViewModels;

namespace Data_Access.Interfaces
{
    public interface IDepartmentsRepository : IGenericRepository<Departments, DepartmentsViewModel>
    {
        List<DepartmentsPayrollViewModel> ReadPayrolls(int companyId, DateTime date);
    }
}
