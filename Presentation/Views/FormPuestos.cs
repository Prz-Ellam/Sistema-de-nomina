using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access.Repositorios;
using Presentation.Helpers;
using Data_Access.Entidades;
using System.Data.SqlClient;
using CustomMessageBox;
using Data_Access.Interfaces;

namespace Presentation.Views
{
    public partial class FormPuestos : Form
    {
        private IPositionsRepository repository;
        private Positions position;
        int dtgPrevIndex = -1;
        int positionId = -1;

        private EntityState positionState;
        private EntityState PositionState
        {
            get
            {
                return positionState;
            }

            set
            {
                positionState = value;

                switch (positionState)
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

        public FormPuestos()
        {
            InitializeComponent();
            repository = new PositionsRepository();
            position = new Positions();
        }

        private void Positions_Load(object sender, EventArgs e)
        {
            PositionState = EntityState.Add;
            ListPositions();
            dtgPositions.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillPosition();
            ValidationResult result = AddPosition();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListPositions();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillPosition();
            ValidationResult result = UpdatePosition();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListPositions();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?", 
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            FillPosition();
            ValidationResult result = DeletePosition();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListPositions();
            ClearForm();
        }

        private void dtgPositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgPrevIndex || index < 0 || index > dtgPositions.RowCount)
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
            ListPositions();
        }

        public ValidationResult AddPosition()
        {
            if (PositionState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(position).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Create(position);
                if (result)
                {
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult UpdatePosition()
        {
            if (PositionState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(position).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Update(position);
                if (result)
                {
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult DeletePosition()
        {
            if (PositionState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                bool result = repository.Delete(position.PositionId);
                if (result)
                {
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public void ListPositions()
        {
            try
            {
                dtgPositions.DataSource = repository.Read(txtFilter.Text.Trim(), Session.companyId);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString());
            }
        }

        public void FillPosition()
        {
            position.PositionId = positionId;
            position.Name = txtName.Text.Trim();
            position.WageLevel = nudWageLevel.Value;
            position.CompanyId = Session.companyId;
        }

        public void ClearForm()
        {
            positionId = -1;
            txtName.Clear();
            nudWageLevel.Value = decimal.Zero;

            PositionState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index < 0 || index > dtgPositions.RowCount)
            {
                return;
            }

            var row = dtgPositions.Rows[index];
            positionId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            nudWageLevel.Value = Convert.ToDecimal(row.Cells[2].Value);

            PositionState = EntityState.Modify;
            dtgPrevIndex = index;
        }
    }
}