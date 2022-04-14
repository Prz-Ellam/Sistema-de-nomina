using Data_Access.Connections;
using System;
using System.Collections.Generic;
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
        }
    }
}
