﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    class DeductionsRepository
    {
        private readonly string create, update, delete, readAll;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        DeductionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AddDeduction";
            update = "sp_UpdateDeduction";
            delete = "sp_DeleteDeduction";
        }


    }
}
