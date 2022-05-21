using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IPhonesRepository
    {
        IEnumerable<string> ReadEmployeePhones(int employeeNumber);
        IEnumerable<string> ReadCompanyPhones(int companyId);
    }
}
