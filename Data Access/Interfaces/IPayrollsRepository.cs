using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IPayrollsRepository
    {
        bool GeneratePayrolls(DateTime date, int companyId);
        IEnumerable<PayrollViewModel> ReadByDate(DateTime date);
        DateTime GetDate(int companyId, bool firstDay);
        bool IsPayrollProcess(int companyId);
        PayrollReceiptViewModel GetPayrollReceipt(int employeeNumber, DateTime date);
        bool StartPayroll(int companyId, DateTime date);
        bool DeletePayroll(int companyId, DateTime date);
    }
}
