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
using Data_Access.Entidades;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormCatalogoConceptos : Form
    {
        private RepositorioPercepciones perceptionRepository = new RepositorioPercepciones();
        private RepositorioDeducciones deductionRepository = new RepositorioDeducciones();
        private Percepciones percepcion = new Percepciones();
        private Deducciones deduccion = new Deducciones();
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
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rbPerception.Checked)
            {
                FillPerception();
                ValidationResult result = AddPerception();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                ValidationResult result = AddDeduction();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                // Es imposible que esto suceda pero igualmente se deja la validacion
                MessageBox.Show("Acción no soportada", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rbPerception.Checked)
            {
                FillPerception();
                ValidationResult result = UpdatePerception();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                ValidationResult result = UpdateDeduction();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Acción no soportada", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No escogió un tipo de concepto", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rbPerception.Checked)
            {
                DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?",
                    "Sistema de nómina dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                {
                    return;
                }

                FillPerception();
                ValidationResult result = DeletePerception();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?",
                    "Sistema de nómina dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                {
                    return;
                }

                FillDeduction();
                ValidationResult result = DeleteDeduction();

                if (result.State == ValidationState.Error)
                {
                    MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDeductions();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Acción no soportada", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtgPerceptions.DataSource = perceptionRepository.Leer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListDeductions()
        {
            try
            {
                dtgDeductions.DataSource = deductionRepository.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void FillPerception()
        {
            percepcion.IdPercepcion = perceptionId;
            percepcion.Nombre = txtName.Text;
            percepcion.TipoMonto = rbFijo.Checked ? 'F' : (rbPorcentual.Checked ? 'P' : ' ');
            percepcion.Fijo = nudFijo.Value;
            percepcion.Porcentual = nudPorcentual.Value;
            percepcion.IdEmpresa = Session.company_id;
        }

        public void FillDeduction()
        {
            deduccion.IdDeduccion = deductionId;
            deduccion.Nombre = txtName.Text;
            deduccion.TipoMonto = rbFijo.Checked ? 'F' : (rbPorcentual.Checked ? 'P' : ' ');
            deduccion.Fijo = nudFijo.Value;
            deduccion.Porcentual = nudPorcentual.Value;
            deduccion.IdEmpresa = Session.company_id;
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
            if (index == dtgPerceptionPrevIndex || index == -1)
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
            if (index == -1)
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
            if (index == dtgDeductionPrevIndex || index == -1)
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
            if (index == -1)
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
            try
            {
                dtgPerceptions.DataSource = perceptionRepository.ReadLike(txtFilterPerception.Text, Session.company_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFilterDeduction_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtgDeductions.DataSource = deductionRepository.ReadLike(txtFilterDeduction.Text, Session.company_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
