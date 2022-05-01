using Data_Access.Connections;
using Data_Access.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioTelefonos
    {
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();
        private string create, update, delete, readEmployeePhones, readCompanyPhones;

        public RepositorioTelefonos()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarTelefono";
            update = "sp_ActualizarTelefono";
            delete = "sp_EliminarTelefono";
            readEmployeePhones = "sp_LeerTelefonosEmpleados";
            readCompanyPhones = "sp_LeerTelefonosEmpresas";
        }

        public bool Create(Telefonos phone, char owner)
        {
            sqlParams.Start();
            sqlParams.Add("@telefono", phone.Nombre);
            sqlParams.Add("@id_propietario", phone.IdPropietario);
            sqlParams.Add("@propietario", owner);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Telefonos phone, char owner)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", phone.Nombre);
            sqlParams.Add("@id_propietario", phone.IdPropietario);
            sqlParams.Add("@propietario", owner);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete()
        {
            sqlParams.Start();

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
        public List<Telefonos> ReadEmployeePhones(int employeeNumber)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);

            DataTable table = mainRepository.ExecuteReader(readEmployeePhones, sqlParams);
            List<Telefonos> phones = new List<Telefonos>();

            foreach(DataRow row in table.Rows)
            {
                phones.Add(new Telefonos
                {
                    IdTelefono = Convert.ToInt32(row["ID Telefono"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return phones;
        }
        */

        public List<string> ReadEmployeePhones(int employeeNumber)
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

        public List<string> ReadCompanyPhones(int companyId)
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

        /*
        public List<Telefonos> ReadCompanyPhones(int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(readCompanyPhones, sqlParams);
            List<Telefonos> phones = new List<Telefonos>();

            foreach (DataRow row in table.Rows)
            {
                phones.Add(new Telefonos
                {
                    IdTelefono = Convert.ToInt32(row["ID Telefono"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return phones;
        }
        */
    }
}
