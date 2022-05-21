using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class PhonesRepository : IPhonesRepository
    {
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;
        private string readEmployeePhones, readCompanyPhones;

        public PhonesRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            readEmployeePhones = "sp_LeerTelefonosEmpleados";
            readCompanyPhones = "sp_LeerTelefonosEmpresas";
        }

        public IEnumerable<string> ReadEmployeePhones(int employeeNumber)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);

            DataTable table = mainRepository.ExecuteReader(readEmployeePhones, sqlParams);
            List<string> phones = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                phones.Add(row["Telefono"].ToString());
            }

            return phones;
        }

        public IEnumerable<string> ReadCompanyPhones(int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(readCompanyPhones, sqlParams);
            List<string> phones = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                phones.Add(row["Telefono"].ToString());
            }

            return phones;
        }
    }
}
