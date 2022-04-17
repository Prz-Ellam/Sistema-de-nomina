﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation.Helpers;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Data_Access.Entidades;
using System.Data.SqlClient;

namespace Presentation.Views
{
    public partial class FormDepartamentos : Form
    {
        private RepositorioDepartamentos repository = new RepositorioDepartamentos();
        private Departamentos department = new Departamentos();
        int dtgPrevIndex = -1;
        int departmentId = -1;

        private EntityState departmentState;
        private EntityState DepartmentState
        {
            get
            {
                return departmentState;
            }

            set
            {
                departmentState = value;

                switch (departmentState)
                {
                    case EntityState.Add:
                    {
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        break;
                    }
                }
            }
        }

        public FormDepartamentos()
        {
            InitializeComponent();
        }

        private void FormDepartments_Load(object sender, EventArgs e)
        {
            DepartmentState = EntityState.Add;
            ListDepartments();

            dtgDepartaments.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillDepartment();
            string message = AddDepartment();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListDepartments();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillDepartment();
            string message = UpdateDepartment();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListDepartments();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillDepartment();
            string message = DeleteDepartment();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListDepartments();
            ClearForm();
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPrevIndex || index == -1)
            {
                ClearForm();
            }
            else
            {
                FillForm(index);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtgDepartaments.DataSource = repository.ReadLike(txtFilter.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void lblBaseSalary_Click(object sender, EventArgs e)
        {
            nudBaseSalary.Focus();
        }

        private void lblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }



        public string AddDepartment()
        {
            if (DepartmentState != EntityState.Add)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                bool result = repository.Create(department);

                if (result)
                {
                    return "La operación se realizó éxitosamente";
                }
                else
                {
                    return "No se pudo realizar la operación";
                }
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public string UpdateDepartment()
        {
            if (DepartmentState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                int result = repository.Update(department);
                if (result > 0)
                {
                    return "La operación se realizó éxitosamente";
                }
                else
                {
                    return "No se pudo realizar la operación";
                }
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public string DeleteDepartment()
        {
            if (DepartmentState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                int result = repository.Delete(departmentId);

                if (result > 0)
                {
                    return "La operación se realizó éxitosamente";
                }
                else
                {
                    return "No se pudo realizar la operación";
                }
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public void ListDepartments()
        {
            try
            {
                dtgDepartaments.DataSource = repository.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void FillDepartment()
        {
            department.IdDepartamento = departmentId;
            department.Nombre = txtName.Text;
            department.SueldoBase = nudBaseSalary.Value;
            department.IdEmpresa = Session.company_id;
        }

        public void ClearForm()
        {
            departmentId = -1;
            txtName.Clear();
            nudBaseSalary.Value = 0.0m;

            DepartmentState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index == -1)
            {
                return;
            }

            var row = dtgDepartaments.Rows[index];
            departmentId = Convert.ToInt32(row.Cells[0].Value); // Podria tronar si la celda 0 no es numero
            txtName.Text = row.Cells[1].Value.ToString();
            nudBaseSalary.Value = Convert.ToDecimal(row.Cells[2].Value); // Lo mismo

            DepartmentState = EntityState.Modify;
            dtgPrevIndex = index;
        }

    }
}
