using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Companies
    {
        int id;
        string businessName;
        int address;
        string email;
        string rfc;
        string employer_registration;
        DateTime start_date;
        bool active;

        public int Id { get => id; set => id = value; }
        public string BusinessName { get => businessName; set => businessName = value; }
        public int Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public string Employer_registration { get => employer_registration; set => employer_registration = value; }
        public DateTime Start_date { get => start_date; set => start_date = value; }
        public bool Active { get => active; set => active = value; }
    }
}
