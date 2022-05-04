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
    public class RepositorioPercepciones
    {
        private readonly string create, update, delete, leer;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioPercepciones()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarPercepcion";
            update = "sp_ActualizarPercepcion";
            delete = "sp_EliminarPercepcion";
            leer = "sp_LeerPercepciones";
            sqlParams = new RepositoryParameters();

        }

        public bool Create(Percepciones percepcion)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", percepcion.Nombre);
            sqlParams.Add("@tipo_monto", percepcion.TipoMonto);
            sqlParams.Add("@fijo", percepcion.Fijo);
            sqlParams.Add("@porcentual", percepcion.Porcentual);
            sqlParams.Add("@id_empresa", percepcion.IdEmpresa);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Percepciones percepcion)
        {
            sqlParams.Start();
            sqlParams.Add("@id_percepcion", percepcion.IdPercepcion);
            sqlParams.Add("@nombre", percepcion.Nombre);
            sqlParams.Add("@tipo_monto", percepcion.TipoMonto);
            sqlParams.Add("@fijo", percepcion.Fijo);
            sqlParams.Add("@porcentual", percepcion.Porcentual);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_percepcion", id);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<PerceptionViewModel> Leer()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(leer, sqlParams);
            List<PerceptionViewModel> perceptions = new List<PerceptionViewModel>();
            foreach (DataRow row in table.Rows) 
            {
                perceptions.Add(new PerceptionViewModel
                {
                    IdPercepcion = Convert.ToInt32(row["ID Percepcion"]),
                    Nombre = row["Nombre"].ToString(),
                    TipoMonto = Convert.ToChar(row["Tipo de monto"]),
                    Fijo = Convert.ToDecimal(row["Fijo"]),
                    Porcentual = Convert.ToDecimal(row["Porcentual"])
                });
            }

            return perceptions;
        }
    }
}
