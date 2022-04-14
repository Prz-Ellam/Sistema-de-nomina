using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositoryParameters : IEnumerable
    {
        private List<SqlParameter> parameters;

        public void Start()
        {
            if (parameters != null)
            {
                parameters.Clear();
            }
            parameters = new List<SqlParameter>();
        }

        public void Add(string name, object value)
        {
            parameters.Add(new SqlParameter(name, value));
        }

        public IEnumerator GetEnumerator()
        {
            return parameters.GetEnumerator();
        }
    }

}
