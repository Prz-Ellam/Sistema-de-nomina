using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Entities;
using Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPercepciones
    {
        private readonly string create, update, delete, leer;
        private readonly string applyEmployee, undoEmployee, applyDepartment, undoDepartment;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioPercepciones()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarPercepcion";
            update = "sp_ActualizarPercepcion";
            delete = "sp_EliminarPercepcion";
            leer = "sp_LeerPercepciones";

            applyEmployee = "sp_ApplyEmployee";
            undoEmployee = "sp_UndoEmployee";
            applyDepartment = "sp_ApplyDepartment";
            undoDepartment = "sp_UndoDepartment";

            sqlParams = new RepositoryParameters();

        }

        public int Create(Percepciones percepcion)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", percepcion.Nombre);
            sqlParams.Add("@tipo_monto", percepcion.TipoMonto);
            sqlParams.Add("@fijo", percepcion.Fijo);
            sqlParams.Add("@porcentual", percepcion.Porcentual);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Percepciones perception)
        {
            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            return mainRepository.ExecuteNonQuery(delete, sqlParams);
        }

        public List<Percepciones> Leer()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(leer, sqlParams);
            List<Percepciones> perceptions = new List<Percepciones>();
            foreach (DataRow row in table.Rows) 
            {
                perceptions.Add(new Percepciones
                {
                    IdPercepcion = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    TipoMonto = Convert.ToChar(row[2]),
                    Fijo = Convert.ToDecimal(row[3]),
                   // Porcentual = Convert.ToDecimal(row[4])
                });
            }

            return perceptions;
        }
    }
}
