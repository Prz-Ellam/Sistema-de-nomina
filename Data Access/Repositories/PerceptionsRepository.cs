using Data_Access.Entities;
using Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class PerceptionsRepository : IGenericRepository<Perceptions>
    {
        private readonly string create, update, delete, readAll;
        private readonly string applyEmployee, undoEmployee, applyDepartment, undoDepartment;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        PerceptionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AddPerception";
            update = "sp_UpdatePerception";
            delete = "sp_DeletePerception";
            readAll = "sp_ReadPerceptions";

            applyEmployee = "sp_ApplyEmployee";
            undoEmployee = "sp_UndoEmployee";
            applyDepartment = "sp_ApplyDepartment";
            undoDepartment = "sp_UndoDepartment";

        }

        public int Create(Perceptions perception)
        {
            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Perceptions perception)
        {
            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            return mainRepository.ExecuteNonQuery(delete, sqlParams);
        }

        public List<Perceptions> ReadAll()
        {
            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            throw new NotImplementedException();
        }
    }
}
