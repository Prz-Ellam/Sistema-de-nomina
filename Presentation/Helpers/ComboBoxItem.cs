using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class ComboBoxItem
    {
        string displayValue;
        int hiddenValue;

        public ComboBoxItem(string display, int hidden)
        {
            displayValue = display;
            hiddenValue = hidden;
        }

        public int HiddenValue
        {
            get
            {
                return hiddenValue;
            }
        }

        public override string ToString()
        {
            return displayValue;
        }
    }
}
