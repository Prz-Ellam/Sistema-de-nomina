﻿using Data_Access.Entidades;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IEmployeesRepository : IGenericRepository<Empleados, EmployeesViewModel>
    {
        EmployeesViewModel GetEmployeeById(int employeeId);
        bool UpdateByEmployee(Empleados employee);
        List<EmployeePayrollsViewModel> ReadEmployeePayrolls(int companyId, DateTime date);
        DateTime GetHiringDate(int employeeNumber, bool firstDay);
    }
}
