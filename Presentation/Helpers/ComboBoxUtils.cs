using Data_Access.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Helpers
{
    public class ComboBoxUtils
    {
        public static object FindHiddenValue(int value, ref ComboBox cb)
        {
            foreach (var item in cb.Items)
            {
                if (((PairItem)item).HiddenValue == value)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
