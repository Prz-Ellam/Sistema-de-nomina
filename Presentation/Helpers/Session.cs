using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public static class Session
    {
        public static int id { get; set; }
        public static int companyId { get; set; }
        public static string email { get; set; }
        public static string position { get; set; }
        
        public static void LogOut()
        {
            id = 0;
            companyId = 0;
            email = null;
            position = null;
        }
    }
}
