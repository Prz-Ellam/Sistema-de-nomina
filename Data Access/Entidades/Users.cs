using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Users
    {
        private int id;
        private string email;
        private string password;
        private string position;
        private int companyId;

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Position { get => position; set => position = value; }
        public string Password { get => password; set => password = value; }
        public int CompanyId { get => companyId; set => companyId = value; }
    }
}
