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

namespace Data_Access.Repositorios
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly string create, read;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public CompaniesRepository()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AddCompany";
            read = "sp_ReadCompany";
        }

        public int Create(Empresas company)
        {
            sqlParams.Start();
            sqlParams.Add("@business_name", company.BusinessName);
            sqlParams.Add("@address", company.Address);
            sqlParams.Add("@email", company.Email);
            sqlParams.Add("@rfc", company.Rfc);
            sqlParams.Add("@employer_registration", company.Employer_registration);
            sqlParams.Add("@start_date", company.Start_date);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public Empresas Read(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@company_id", id);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);

            Empresas company;
            foreach (DataRow row in table.Rows)
            {
                company = new Empresas {
                    Id = Convert.ToInt32(row[0]),
                    BusinessName = row[1].ToString(),
                    Address = Convert.ToInt32(row[2]),
                    Email = row[3].ToString(),
                    Rfc = row[4].ToString(),
                    Employer_registration = row[5].ToString(),
                    Start_date = Convert.ToDateTime(row[6]),
                    Active = Convert.ToBoolean(row[7])
                };

                return company;
            }

            return null;
        }
    }
}
