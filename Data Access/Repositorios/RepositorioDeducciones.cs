using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;

namespace Data_Access.Repositorios
{
    public class RepositorioDeducciones
    {
        private readonly string create, update, delete, readAll;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioDeducciones()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AddDeduction";
            update = "sp_UpdateDeduction";
            delete = "sp_DeleteDeduction";
        }

        public int Create(Deducciones deduccion)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", deduccion.Nombre);
            sqlParams.Add("@tipo_monto", deduccion.TipoMonto);
            sqlParams.Add("@fijo", deduccion.Fijo);
            sqlParams.Add("@porcentual", deduccion.Porcentual);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

    }
}
