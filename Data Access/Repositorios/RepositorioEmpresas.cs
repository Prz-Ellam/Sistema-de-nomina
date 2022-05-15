using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Interfaces;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class RepositorioEmpresas
    {
        private readonly string create, update, read, getCompany, getCreationDate;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioEmpresas()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            create = "sp_AgregarEmpresa";
            update = "sp_ActualizarEmpresa";
            read = "sp_LeerEmpresa";
            getCompany = "sp_ObtenerEmpresa";
            getCreationDate = "sp_ObtenerFechaCreacion";
        }

        public bool Create(Companies company)
        {
            sqlParams.Start();
            sqlParams.Add("@razon_social", company.BusinessName);
            sqlParams.Add("@correo_electronico", company.Email);
            sqlParams.Add("@rfc", company.Rfc);
            sqlParams.Add("@registro_patronal", company.EmployerRegistration);
            sqlParams.Add("@fecha_inicio", company.StartDate);
            sqlParams.Add("@id_administrador", company.AdministratorId);
            sqlParams.Add("@calle", company.Street);
            sqlParams.Add("@numero", company.Number);
            sqlParams.Add("@colonia", company.Suburb);
            sqlParams.Add("@ciudad", company.City);
            sqlParams.Add("@estado", company.State);
            sqlParams.Add("@codigo_postal", company.PostalCode);

            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("row_count");
            telefonos.Columns.Add("telefono");

            for (int i = 0; i < company.Phones.Count; i++)
            {
                telefonos.Rows.Add(i.ToString(), company.Phones[i]);
            }

            sqlParams.Add("@telefonos", telefonos);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Companies company)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", company.CompanyId);
            sqlParams.Add("@razon_social", company.BusinessName);
            sqlParams.Add("@correo_electronico", company.Email);
            sqlParams.Add("@rfc", company.Rfc);
            sqlParams.Add("@registro_patronal", company.EmployerRegistration);
            sqlParams.Add("@fecha_inicio", company.StartDate);
            sqlParams.Add("@calle", company.Street);
            sqlParams.Add("@numero", company.Number);
            sqlParams.Add("@colonia", company.Suburb);
            sqlParams.Add("@ciudad", company.City);
            sqlParams.Add("@estado", company.State);
            sqlParams.Add("@codigo_postal", company.PostalCode);

            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("row_count");
            telefonos.Columns.Add("telefono");

            for (int i = 0; i < company.Phones.Count; i++)
            {
                telefonos.Rows.Add(i.ToString(), company.Phones[i]);
            }

            sqlParams.Add("@telefonos", telefonos);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public CompaniesViewModel Read(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", id);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);

            CompaniesViewModel company;
            foreach (DataRow row in table.Rows)
            {
                company = new CompaniesViewModel {
                    IdEmpresa = Convert.ToInt32(row["ID Empresa"]),
                    RazonSocial = row["Razon social"].ToString(),
                    Calle = row["Calle"].ToString(),
                    Numero = row["Numero"].ToString(),
                    Colonia = row["Colonia"].ToString(),
                    Ciudad = row["Ciudad"].ToString(),
                    Estado = row["Estado"].ToString(),
                    CodigoPostal = row["Código postal"].ToString(),
                    CorreoElectronico = row["Correo electrónico"].ToString(),
                    RegistroPatronal = row["Registro Patronal"].ToString(),
                    Rfc = row["RFC"].ToString(),
                    FechaInicio = Convert.ToDateTime(row["Fecha de Inicio"])
                };

                return company;
            }

            return null;
        }

        public int Verify(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_administrador", id);
            DataTable table = mainRepository.ExecuteReader(getCompany, sqlParams);

            foreach (DataRow row in table.Rows)
            {
                return Convert.ToInt32(row[0]);
            }

            return -1;
        }

        public DateTime GetCreationDate(int companyId, bool firstDay)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@primer_dia", firstDay);
            DataTable table = mainRepository.ExecuteReader(getCreationDate, sqlParams);

            foreach (DataRow row in table.Rows)
            {
                return Convert.ToDateTime(row["Fecha de inicio"]);
            }

            return DateTime.MinValue;
        }
    }
}
