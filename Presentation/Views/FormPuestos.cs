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
using System.Data.SqlClient;

namespace Presentation.Views
{
    public partial class FormPuestos : Form
    {
        private RepositorioPuestos repository = new RepositorioPuestos();
        private Puestos position = new Puestos();
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
            string message = AddPosition();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListPositions();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillPosition();
            string message = UpdatePosition();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListPositions();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillPosition();
            string message = DeletePosition();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListPositions();
            ClearForm();
        }

        private void dtgPositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                dtgPositions.DataSource = repository.ReadLike(txtFilter.Text);
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

        private void lblWageLevel_Click(object sender, EventArgs e)
        {
            nudWageLevel.Focus();
        }

        private void lblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

        

        public string AddPosition()
        {
            if (positionState != EntityState.Add)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(position).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                int result = repository.Create(position);
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

        public string UpdatePosition()
        {
            if (positionState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(position).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                int result = repository.Update(position);
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

        public string DeletePosition()
        {
            if (positionState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                int result = repository.Delete(positionId);

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

        public void ListPositions()
        {
            try
            {
                dtgPositions.DataSource = repository.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void FillPosition()
        {
            position.IdPuesto = positionId;
            position.Nombre = txtName.Text;
            position.NivelSalarial = nudWageLevel.Value;
            position.IdEmpresa = Session.company_id;
        }

        public void ClearForm()
        {
            positionId = -1;
            txtName.Clear();
            nudWageLevel.Value = 0.0m;

            PositionState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index == -1)
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
