using Data_Access.Connections;
using Data_Access.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioDomicilios
    {
        private readonly string create, update, delete;
        private MainConnection repository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioDomicilios()
        {
            repository = MainConnection.GetInstance();
            create = "sp_AgregarDomicilio";
            update = "sp_ActualizarDomicilio";
            delete = "sp_EliminarDomicilio";
        }

        public int Create(Domicilios domicilio)
        {
            sqlParams.Start();
            sqlParams.Add("@calle", domicilio.Calle);
            sqlParams.Add("@numero", domicilio.Numero);
            sqlParams.Add("@colonia", domicilio.Colonia);
            sqlParams.Add("@ciudad", domicilio.Ciudad);
            sqlParams.Add("@estado", domicilio.Estado);
            sqlParams.Add("@codigo_postal", domicilio.CodigoPostal);

            DataTable table = repository.ExecuteReader(create, sqlParams);

            foreach(DataRow row in table.Rows)
            {
                return Convert.ToInt32(row[0].ToString());
            }

            return -1;

        }

        public int Update(Domicilios domicilio)
        {
            sqlParams.Start();
            sqlParams.Add("@id_domicilio", domicilio.IdDomicilio);
            sqlParams.Add("@calle", domicilio.Calle);
            sqlParams.Add("@numero", domicilio.Numero);
            sqlParams.Add("@colonia", domicilio.Colonia);
            sqlParams.Add("@ciudad", domicilio.Ciudad);
            sqlParams.Add("@estado", domicilio.Estado);
            sqlParams.Add("@codigo_postal", domicilio.CodigoPostal);

            return repository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int idDomicilio)
        {
            sqlParams.Start();
            sqlParams.Add("@id_domicilio", idDomicilio);

            return repository.ExecuteNonQuery(delete, sqlParams);
        }
    }
}
