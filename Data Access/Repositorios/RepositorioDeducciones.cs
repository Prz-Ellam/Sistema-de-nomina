using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class RepositorioDeducciones
    {
        private readonly string create, update, delete, readAll;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioDeducciones()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarDeduccion";
            update = "sp_ActualizarDeduccion";
            delete = "sp_EliminarDeduccion";
            readAll = "sp_LeerDeducciones";
        }

        public bool Create(Deducciones deduccion)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", deduccion.Nombre);
            sqlParams.Add("@tipo_monto", deduccion.TipoMonto);
            sqlParams.Add("@fijo", deduccion.Fijo);
            sqlParams.Add("@porcentual", deduccion.Porcentual);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Deducciones deduccion)
        {
            sqlParams.Start();
            sqlParams.Add("@id_deduccion", deduccion.IdDeduccion);
            sqlParams.Add("@nombre", deduccion.Nombre);
            sqlParams.Add("@tipo_monto", deduccion.TipoMonto);
            sqlParams.Add("@fijo", deduccion.Fijo);
            sqlParams.Add("@porcentual", deduccion.Porcentual);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int idDeduccion)
        {
            sqlParams.Start();
            sqlParams.Add("@id_deduccion", idDeduccion);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DeductionViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            List<DeductionViewModel> deductions = new List<DeductionViewModel>();
            foreach (DataRow row in table.Rows)
            {
                deductions.Add(new DeductionViewModel
                {
                    IdDeduccion = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    TipoMonto = Convert.ToChar(row[2]),
                    Fijo = Convert.ToDecimal(row[3]),
                    Porcentual = Convert.ToDecimal(row[4])
                });
            }

            return deductions;
        }

    }
}
