using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class KeyValueItem
    {
        string displayValue;
        int hiddenValue;

        public KeyValueItem(string display, int hidden)
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
