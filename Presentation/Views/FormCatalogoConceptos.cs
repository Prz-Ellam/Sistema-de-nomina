using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                        perceptionId = -1;
                        deductionId = -1;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAgregar.Enabled = false;
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
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
                MessageBox.Show("No esta bien");
            }
            else if (rbPerception.Checked)
            {
                FillPerception();
                AddPerception();
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                AddDeduction();
                ListDeductions();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Accion no soportada");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No esta bien");
            }
            else if (rbPerception.Checked)
            {
                FillPerception();
                UpdatePerception();
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                UpdateDeduction();
                ListDeductions();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Accion no soportada");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No esta bien");
            }
            else if (rbPerception.Checked)
            {
                FillPerception();
                DeletePerception();
                ListPerceptions();
                ClearForm();
            }
            else if (rbDeduction.Checked)
            {
                FillDeduction();
                DeleteDeduction();
                ListDeductions();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Accion no soportada");
            }
        }

        private void AddPerception()
        {
            if (ConceptsState == EntityState.Add)
            {
                perceptionRepository.Create(percepcion);
            }
        }

        public void UpdatePerception()
        {
            if (ConceptsState == EntityState.Modify)
            {
                perceptionRepository.Update(percepcion);
            }
        }

        public void DeletePerception()
        {
            if (ConceptsState == EntityState.Modify)
            {
                perceptionRepository.Delete(perceptionId);
            }
        }

        private void AddDeduction()
        {
            if (ConceptsState == EntityState.Add)
            {
                deductionRepository.Create(deduccion);
            }
        }

        private void UpdateDeduction()
        {
            if (ConceptsState == EntityState.Modify)
            {
                deductionRepository.Update(deduccion);
            }
        }

        private void DeleteDeduction()
        {
            if (ConceptsState == EntityState.Modify)
            {
                deductionRepository.Delete(deductionId);
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
            percepcion.TipoMonto = (rbFijo.Checked ? 'F' : 'P');
            percepcion.Fijo = nudFijo.Value;
            percepcion.Porcentual = nudPorcentual.Value;
        }

        public void FillDeduction()
        {
            deduccion.IdDeduccion = deductionId;
            deduccion.Nombre = txtName.Text;
            deduccion.TipoMonto = (rbFijo.Checked ? 'F' : 'P');
            deduccion.Fijo = nudFijo.Value;
            deduccion.Porcentual = nudPorcentual.Value;
        }

        public void ClearForm()
        {
            txtName.Text = string.Empty;
            nudFijo.Value = 0.0m;
            nudPorcentual.Value = 0.0m;
            rbPerception.Checked = false;
            rbDeduction.Checked = false;
            rbFijo.Checked = false;
            rbPorcentual.Checked = false;
        }

        private void rbFijo_CheckedChanged(object sender, EventArgs e)
        {
            nudFijo.Enabled = rbFijo.Checked;
        }

        private void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            nudPorcentual.Enabled = rbPorcentual.Checked;
        }


        private void dtgPerceptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPerceptionPrevIndex || index == -1)
            {
                ClearForm();
                ConceptsState = EntityState.Add;
                dtgPerceptionPrevIndex = -1;
            }
            else
            {
                FillPerceptionForm(index);
                ConceptsState = EntityState.Modify;
                dtgPerceptionPrevIndex = index;
            }
        }

        private void FillPerceptionForm(int rowIndex)
        {
            if (rowIndex == -1)
            {
                return;
            }

            var row = dtgPerceptions.Rows[rowIndex];

            rbPerception.Checked = true;
            perceptionId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            char type = Convert.ToChar(row.Cells[2].Value);
            if (type == 'F')
            {
                rbFijo.Checked = true;
            }
            else
            {
                rbPorcentual.Checked = true;
            }
        }

        private void dtgDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgDeductionPrevIndex || index == -1)
            {
                ClearForm();
                ConceptsState = EntityState.Add;
                dtgDeductionPrevIndex = -1;
            }
            else
            {
                FillDeductionForm(index);
                ConceptsState = EntityState.Modify;
                dtgDeductionPrevIndex = index;
            }
        }

        private void FillDeductionForm(int rowIndex)
        {
            if (rowIndex == -1)
            {
                return;
            }

            var row = dtgDeductions.Rows[rowIndex];

            rbDeduction.Checked = true;
            deductionId = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            char type = Convert.ToChar(row.Cells[2].Value);
            if (type == 'F')
            {
                rbFijo.Checked = true;
            }
            else
            {
                rbPorcentual.Checked = true;
            }
        }
    }
}
