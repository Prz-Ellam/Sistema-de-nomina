using Data_Access.Entities;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class PositionsRepository
    {
        private readonly string create, update, delete, readAll, readLike;
        private MainRepository mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public PositionsRepository()
        {
            mainRepository = MainRepository.GetInstance();
            create = "sp_AddPosition";
            update = "sp_UpdatePosition";
            delete = "sp_DeletePosition";
            readAll = "sp_ReadPositions";
        }

        public int Create(Positions position)
        {
            sqlParams.Start();
            sqlParams.Add("@name", position.Name);
            sqlParams.Add("@wage_level", position.WageLevel);
            sqlParams.Add("@company_id", position.CompanyId);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Positions position)
        {
            sqlParams.Start();
            sqlParams.Add("@id", position.Id);
            sqlParams.Add("@name", position.Name);
            sqlParams.Add("@wage_level", position.WageLevel);

            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id", id);

            return mainRepository.ExecuteNonQuery(delete, sqlParams);
        }

        public List<PositionsViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            List<PositionsViewModel> departments = new List<PositionsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new PositionsViewModel
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Name = row["Nombre"].ToString(),
                    WageLevel = Convert.ToDecimal(row["Nivel salarial"])

                });
            }

            return departments;
        }


    }
}
