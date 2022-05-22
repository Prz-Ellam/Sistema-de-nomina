using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class PerceptionsRepository : IPerceptionsRepository
    {
        private readonly string create, update, delete, read;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public PerceptionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            create = "sp_AgregarPercepcion";
            update = "sp_ActualizarPercepcion";
            delete = "sp_EliminarPercepcion";
            read = "sp_LeerPercepciones";
        }

        public bool Create(Perceptions perception)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", perception.Name);
            sqlParams.Add("@tipo_monto", perception.AmountType);
            sqlParams.Add("@fijo", perception.Fixed);
            sqlParams.Add("@porcentual", perception.Porcentual);
            sqlParams.Add("@id_empresa", perception.CompanyId);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Perceptions perception)
        {
            sqlParams.Start();
            sqlParams.Add("@id_percepcion", perception.PerceptionId);
            sqlParams.Add("@nombre", perception.Name);
            sqlParams.Add("@tipo_monto", perception.AmountType);
            sqlParams.Add("@fijo", perception.Fixed);
            sqlParams.Add("@porcentual", perception.Porcentual);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int perceptionId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_percepcion", perceptionId);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public IEnumerable<PerceptionViewModel> Read(string filter, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);
            List<PerceptionViewModel> perceptions = new List<PerceptionViewModel>();
            foreach (DataRow row in table.Rows)
            {
                perceptions.Add(new PerceptionViewModel
                {
                    IdPercepcion = Convert.ToInt32(row["ID Percepcion"]),
                    Nombre = row["Nombre"].ToString(),
                    TipoMonto = Convert.ToChar(row["Tipo de monto"]),
                    Fijo = Convert.ToDecimal(row["Fijo"]),
                    Porcentual = Convert.ToDecimal(row["Porcentual"]),
                    TipoDuracion = Convert.ToChar(row["Tipo de duración"])
                });
            }

            return perceptions;
        }
    }
}
