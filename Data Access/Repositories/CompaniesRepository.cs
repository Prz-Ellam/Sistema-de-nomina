using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Data_Access.Repositories;

namespace Data_Access.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly string create, read;
        private MainRepository mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public CompaniesRepository()
        {
            mainRepository = MainRepository.GetInstance();
            create = "sp_AddCompany";
            read = "sp_ReadCompany";
        }

        public int Create(Companies company)
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

        public Companies Read(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@company_id", id);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);

            Companies company;
            foreach (DataRow row in table.Rows)
            {
                company = new Companies {
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
