using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Interfaces;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class DeductionsRepository : IDeductionsRepository
    {
        private readonly string create, update, delete, read;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public DeductionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            create = "sp_AgregarDeduccion";
            update = "sp_ActualizarDeduccion";
            delete = "sp_EliminarDeduccion";
            read = "sp_LeerDeducciones";
        }

        public bool Create(Deductions deduction)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", deduction.Name);
            sqlParams.Add("@tipo_monto", deduction.AmountType);
            sqlParams.Add("@fijo", deduction.Fixed);
            sqlParams.Add("@porcentual", deduction.Porcentual);
            sqlParams.Add("@id_empresa", deduction.CompanyId);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Deductions deduction)
        {
            sqlParams.Start();
            sqlParams.Add("@id_deduccion", deduction.DeductionId);
            sqlParams.Add("@nombre", deduction.Name);
            sqlParams.Add("@tipo_monto", deduction.AmountType);
            sqlParams.Add("@fijo", deduction.Fixed);
            sqlParams.Add("@porcentual", deduction.Porcentual);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int idDeduccion)
        {
            sqlParams.Start();
            sqlParams.Add("@id_deduccion", idDeduccion);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public IEnumerable<DeductionViewModel> Read(string filter, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);
            List<DeductionViewModel> deductions = new List<DeductionViewModel>();
            foreach (DataRow row in table.Rows)
            {
                deductions.Add(new DeductionViewModel
                {
                    IdDeduccion = Convert.ToInt32(row["ID Deduccion"]),
                    Nombre = row["Nombre"].ToString(),
                    TipoMonto = Convert.ToChar(row["Tipo de monto"]),
                    Fijo = Convert.ToDecimal(row["Fijo"]),
                    Porcentual = Convert.ToDecimal(row["Porcentual"]),
                    TipoDuracion = Convert.ToChar(row["Tipo de duración"])
                });
            }

            return deductions;
        }

    }
}