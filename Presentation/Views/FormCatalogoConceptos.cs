using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomMessageBox;
using Data_Access.Entidades;
using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormCatalogoConceptos : Form
    {
        private IPerceptionsRepository perceptionRepository;
        private IDeductionsRepository deductionRepository;
        private Perceptions percepcion;
        private Deductions deduccion;
        int perceptionId = -1;
        int deductionId = -1;
        int dtgPerceptionPrevIndex = -1;
        int dtgDeductionPrevIndex = -1;

        private EntityState conceptsState;
        private EntityState ConceptsState
        {
            get
            {
                return conceptsState;
            }

            set
            {
                conceptsState = value;

                switch (conceptsState)
                {
                    case EntityState.Add:
                    {
                        btnAgregar.Enabled = true;
                        btnActualizar.Enabled = false;
                        btnEliminar.Enabled = false;
                        gbTipoConcepto.Enabled = true;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAgregar.Enabled = false;
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
                        gbTipoConcepto.Enabled = false;
                        break;
                    }
                }
            }
        }

        public FormCatalogoConceptos()
        {
            InitializeComponent();
            perceptionRepository = new PerceptionsRepository();
            deductionRepository = new DeductionsRepository();
            percepcion = new Perceptions();
            deduccion = new Deductions();
        }

        private void FormCatalogoConceptos_Load(object sender, EventArgs e)
        {
            ConceptsState = EntityState.Add;
            ListPerceptions();
            ListDeductions();

            dtgPerceptions.DoubleBuffered(true);
            dtgDeductions.DoubleBuffered(true);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (rbPerception.Checked)
            {
                FillPerception();
                ValidationResult result = AddPerception();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                ValidationResult result = AddDeduction();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                RJMessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (rbPerception.Checked)
            {
                FillPerception();
                ValidationResult result = UpdatePerception();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                ValidationResult result = UpdateDeduction();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                RJMessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (rbPerception.Checked)
            {
                DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?",
                    "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                {
                    return;
                }

                FillPerception();
                ValidationResult result = DeletePerception();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?",
                    "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                {
                    return;
                }

                FillDeduction();
                ValidationResult result = DeleteDeduction();

                if (result.State == ValidationState.Error)
                {
                    RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                RJMessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ValidationResult AddPerception()
        {
            if (ConceptsState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(percepcion).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = perceptionRepository.Create(percepcion);
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

        public ValidationResult UpdatePerception()
        {
            if (ConceptsState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(percepcion).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = perceptionRepository.Update(percepcion);
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

        public ValidationResult DeletePerception()
        {
            if (ConceptsState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                bool result = perceptionRepository.Delete(perceptionId);
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

        private ValidationResult AddDeduction()
        {
            if (ConceptsState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(deduccion).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = deductionRepository.Create(deduccion);
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

        private ValidationResult UpdateDeduction()
        {
            if (ConceptsState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(deduccion).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = deductionRepository.Update(deduccion);
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

        private ValidationResult DeleteDeduction()
        {
            if (ConceptsState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                bool result = deductionRepository.Delete(deductionId);
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

        private void ListPerceptions()
        {
            try
            {
                dtgPerceptions.DataSource = perceptionRepository.Read(txtFilterPerception.Text.Trim(), Session.companyId);
            }
            catch (Exception ex)
            {
                RJMessageBox.Show(ex.ToString());
            }
        }

        private void ListDeductions()
        {
            try
            {
                dtgDeductions.DataSource = deductionRepository.Read(txtFilterDeduction.Text.Trim(), Session.companyId);
            }
            catch (Exception ex)
            {
                RJMessageBox.Show(ex.ToString());
            }
        }


        public void FillPerception()
        {
            percepcion.PerceptionId = perceptionId;
            percepcion.Name = txtName.Text;
            percepcion.AmountType = rbFijo.Checked ? 'F' : (rbPorcentual.Checked ? 'P' : ' ');
            percepcion.Fixed = nudFijo.Value;
            percepcion.Porcentual = nudPorcentual.Value;
            percepcion.CompanyId = Session.companyId;
        }

        public void FillDeduction()
        {
            deduccion.DeductionId = deductionId;
            deduccion.Name = txtName.Text;
            deduccion.AmountType = rbFijo.Checked ? 'F' : (rbPorcentual.Checked ? 'P' : ' ');
            deduccion.Fixed = nudFijo.Value;
            deduccion.Porcentual = nudPorcentual.Value;
            deduccion.CompanyId = Session.companyId;
        }

        public void ClearForm()
        {
            perceptionId = -1;
            deductionId = -1;
            txtName.Clear();
            nudFijo.Value = 0.0m;
            nudPorcentual.Value = 0.0m;
            rbPerception.Checked = false;
            rbDeduction.Checked = false;
            rbFijo.Checked = false;
            rbPorcentual.Checked = false;

            ConceptsState = EntityState.Add;
            dtgPerceptionPrevIndex = -1;
            dtgDeductionPrevIndex = -1;
        }

        private void rbFijo_CheckedChanged(object sender, EventArgs e)
        {
            nudFijo.Enabled = rbFijo.Checked;
            nudPorcentual.Value = 0.0m;
        }

        private void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            nudPorcentual.Enabled = rbPorcentual.Checked;
            nudFijo.Value = 0.0m;
        }

        private void dtgPerceptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgPerceptionPrevIndex || index < 0 || index > dtgPerceptions.RowCount)
            {
                ClearForm();
            }
            else
            {
                FillPerceptionForm(index);
            }
        }

        private void FillPerceptionForm(int index)
        {
            if (index < 0 || index > dtgPerceptions.RowCount)
            {
                return;
            }

            var row = dtgPerceptions.Rows[index];
            rbPerception.Checked = true;
            perceptionId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            char type = Convert.ToChar(row.Cells[2].Value);
            if (type == 'F')
            {
                rbFijo.Checked = true;
                nudFijo.Value = Convert.ToDecimal(row.Cells[3].Value);
            }
            else
            {
                rbPorcentual.Checked = true;
                nudPorcentual.Value = Convert.ToDecimal(row.Cells[4].Value);
            }

            ConceptsState = EntityState.Modify;
            dtgPerceptionPrevIndex = index;
        }

        private void dtgDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgDeductionPrevIndex || index < 0 || index > dtgDeductions.RowCount)
            {
                ClearForm();
            }
            else
            {
                FillDeductionForm(index);
            }
        }

        private void FillDeductionForm(int index)
        {
            if (index < 0 || index > dtgDeductions.RowCount)
            {
                return;
            }

            var row = dtgDeductions.Rows[index];
            rbDeduction.Checked = true;
            deductionId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            char type = Convert.ToChar(row.Cells[2].Value);
            if (type == 'F')
            {
                rbFijo.Checked = true;
                nudFijo.Value = Convert.ToDecimal(row.Cells[3].Value);
            }
            else
            {
                rbPorcentual.Checked = true;
                nudPorcentual.Value = Convert.ToDecimal(row.Cells[4].Value);
            }

            ConceptsState = EntityState.Modify;
            dtgDeductionPrevIndex = index;
        }

        private void txtFilterPerception_TextChanged(object sender, EventArgs e)
        {
            ListPerceptions();
        }

        private void txtFilterDeduction_TextChanged(object sender, EventArgs e)
        {
            ListDeductions();
        }
    }
}