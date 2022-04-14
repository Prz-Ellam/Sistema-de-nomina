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
        int entityID = -1;

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
                        entityID = -1;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No esta bien");
            }
            else if (rbPerception.Checked)
            {
                AddPerception();
            }
            else if (rbDeduction.Checked)
            {
                AddDeduction();
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

            }
        }

        public void DeletePerception()
        {
            if (ConceptsState == EntityState.Modify)
            {

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

            }
        }

        private void DeleteDeduction()
        {
            if (ConceptsState == EntityState.Modify)
            {
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

        private void rbFijo_CheckedChanged(object sender, EventArgs e)
        {
            nudFijo.Enabled = rbFijo.Checked;
            nudPorcentual.Enabled = !rbFijo.Checked;
        }

        private void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            nudPorcentual.Enabled = rbPorcentual.Checked;
            nudFijo.Enabled = !rbPorcentual.Checked;
        }

        private void FormCatalogoConceptos_Load(object sender, EventArgs e)
        {
            ListPerceptions();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!rbPerception.Checked && !rbDeduction.Checked)
            {
                MessageBox.Show("No esta bien");
            }
            else if (rbPerception.Checked)
            {
                UpdatePerception();
            }
            else if (rbDeduction.Checked)
            {
                UpdateDeduction();
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
                DeletePerception();
            }
            else if (rbDeduction.Checked)
            {
                DeleteDeduction();
            }
            else
            {
                MessageBox.Show("Accion no soportada");
            }
        }
    }
}
