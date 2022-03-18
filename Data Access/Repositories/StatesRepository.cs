using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Data_Access.Entities;

namespace Data_Access.Repositories
{
    public class StatesRepository
    {
        private readonly string jsonPath;
        public StatesRepository()
        {
            jsonPath = "../../../Assets/Json/mexico.json";
        }

        public List<States> GetAll()
        {
            return JsonSerializer.Deserialize<List<States>>(File.ReadAllText(jsonPath));
        }
    }
}
