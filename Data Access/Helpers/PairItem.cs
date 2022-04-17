using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Helpers
{
    public struct PairItem
    {
        private string displayValue;
        private int hiddenValue;

        public PairItem(string display, int hidden)
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
