using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access.Entities;
using Data_Access.Repositorios;
using Presentation.Helpers;
using Data_Access.ViewModels;
using Data_Access.Entidades;

namespace Presentation.Views
{
    public partial class FormPuestos : Form
    {
        private RepositorioPuestos repository = new RepositorioPuestos();
        private Puestos position = new Puestos();
        int dtgPrevIndex = -1;
        int entityID = -1;

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
        }

        private void Positions_Load(object sender, EventArgs e)
        {
            PositionState = EntityState.Add;
            FillDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillEntity();
            AddEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEntity();
            EditEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillEntity();
            DeleteEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void lblWageLevel_Click(object sender, EventArgs e)
        {
            nudWageLevel.Focus();
        }

        private void lblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

        private void dtgPositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPrevIndex || index == -1)
            {
                ClearForm();
                PositionState = EntityState.Add;
                dtgPrevIndex = -1;
            }
            else
            {
                FillForm(index);
                PositionState = EntityState.Modify;
                dtgPrevIndex = index;
            }
        }

        public void AddEntity()
        {
            if (positionState == EntityState.Add)
            {
                Tuple<bool, string> feedback = new DataValidation(position).Validate();
                if (!feedback.Item1)
                {
                    MessageBox.Show(feedback.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int rowsAffected = repository.Create(position);
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Algo fallo");
                }
            }
        }

        public void EditEntity()
        {
            if (positionState == EntityState.Modify)
            {
                repository.Update(position);
            }
        }

        public void DeleteEntity()
        {
            if (positionState == EntityState.Modify)
            {
                repository.Delete(entityID);
            }
        }

        public void FillEntity()
        {
            position.IdPuesto = entityID;
            position.Nombre = txtName.Text;
            position.NivelSalarial = nudWageLevel.Value;
            position.IdEmpresa = /* Session.company_id */ 1;
        }

        public void FillForm(int index)
        {
            var row = dtgPositions.Rows[index];
            entityID = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            nudWageLevel.Value = Convert.ToDecimal(row.Cells[2].Value);
        }

        public void FillDataGridView()
        {
            List<PositionsViewModel> positions = repository.ReadAll();
            dtgPositions.DataSource = positions;
        }

        public void ClearForm()
        {
            txtName.Clear();
            nudWageLevel.Value = 0.0m;
            PositionState = EntityState.Add;
        }
    }
}
