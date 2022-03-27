using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Views
{
    public interface ICrudeable
    {
        void AddEntity();
        void UpdateEntity();
        void DeleteEntity();
        void ListEntity();
        //void FillEntity();
        //void FillForm(int index);
        //void ClearForm();
    }
}
