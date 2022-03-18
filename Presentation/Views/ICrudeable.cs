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
        void EditEntity();
        void DeleteEntity();
        void FillEntity();
        void FillForm(int index);
        void FillDataGridView();
        void ClearForm();
    }
}
