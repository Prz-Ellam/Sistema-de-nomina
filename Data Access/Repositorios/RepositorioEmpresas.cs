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
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly string create, update, read, verify;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public CompaniesRepository()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarEmpresa";
            update = "sp_ActualizarEmpresa";
            read = "sp_LeerEmpresa";
            verify = "sp_ObtenerEmpresa";
        }

        public bool Create(Empresas company)
        {
            sqlParams.Start();
            sqlParams.Add("@razon_social", company.RazonSocial);
            sqlParams.Add("@correo_electronico", company.CorreoElectronico);
            sqlParams.Add("@rfc", company.Rfc);
            sqlParams.Add("@registro_patronal", company.RegistroPatronal);
            sqlParams.Add("@fecha_inicio", company.FechaInicio);
            sqlParams.Add("@id_administrador", company.IdAdministrador);
            sqlParams.Add("@calle", company.Calle);
            sqlParams.Add("@numero", company.Numero);
            sqlParams.Add("@colonia", company.Colonia);
            sqlParams.Add("@ciudad", company.Ciudad);
            sqlParams.Add("@estado", company.Estado);
            sqlParams.Add("@codigo_postal", company.CodigoPostal);

            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("row_count");
            telefonos.Columns.Add("telefono");

            for (int i = 0; i < company.Telefonos.Count; i++)
            {
                telefonos.Rows.Add(i.ToString(), company.Telefonos[i]);
            }

            sqlParams.Add("@telefonos", telefonos);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Empresas company)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", company.IdEmpresa);
            sqlParams.Add("@razon_social", company.RazonSocial);
            sqlParams.Add("@correo_electronico", company.CorreoElectronico);
            sqlParams.Add("@rfc", company.Rfc);
            sqlParams.Add("@registro_patronal", company.RegistroPatronal);
            sqlParams.Add("@fecha_inicio", company.FechaInicio);
            sqlParams.Add("@calle", company.Calle);
            sqlParams.Add("@numero", company.Numero);
            sqlParams.Add("@colonia", company.Colonia);
            sqlParams.Add("@ciudad", company.Ciudad);
            sqlParams.Add("@estado", company.Estado);
            sqlParams.Add("@codigo_postal", company.CodigoPostal);

            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("row_count");
            telefonos.Columns.Add("telefono");

            for (int i = 0; i < company.Telefonos.Count; i++)
            {
                telefonos.Rows.Add(i.ToString(), company.Telefonos[i]);
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
            DataTable table = mainRepository.ExecuteReader(verify, sqlParams);

            foreach (DataRow row in table.Rows)
            {
                return Convert.ToInt32(row[0]);
            }

            return -1;
        }
    }
}
