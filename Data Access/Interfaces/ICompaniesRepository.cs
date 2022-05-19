using Data_Access.Entidades;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface ICompaniesRepository
    {
        bool Create(Companies company);
        bool Update(Companies company);
        CompaniesViewModel Read(int companyId);
        int Verify(int id);
        DateTime GetCreationDate(int companyId, bool firstDay);

    }
}
