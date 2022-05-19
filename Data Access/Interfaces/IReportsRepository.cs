using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IReportsRepository
    {
        IEnumerable<GeneralPayrollReportsViewModel> GeneralPayrollReport(DateTime date);
        IEnumerable<Headcounter1ViewModel> Headcounter1(int companyId, int departmentId, DateTime date);
        IEnumerable<Headcounter2ViewModel> Headcounter2(int companyId, int departmentId, DateTime date);
        IEnumerable<PayrollReportsViewModel> PayrollReport(int year);
    }
}
