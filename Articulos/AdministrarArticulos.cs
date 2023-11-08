using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidarLibrary;

namespace Login_Los_2_chinos
{
    public partial class AdministrarArticulos : Form
    {
        public AdministrarArticulos(string Nombre)
        {
            InitializeComponent();
            this.lblNombre.Text = Nombre;
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            FromAgregar AgregarArticulo = new FromAgregar();

            // Establece el formulario secundario como un control secundario del panel
            AgregarArticulo.TopLevel = false;
            AgregarArticulo.FormBorderStyle = FormBorderStyle.None;
            AgregarArticulo.Dock = DockStyle.Fill;

            // Limpia cualquier control existente en el panel
            PanelFormulario.Controls.Clear();

            // Agrega el formulario secundario al panel
           PanelFormulario.Controls.Add(AgregarArticulo);

            // Muestra el formulario secundario
            AgregarArticulo.Show();
        }

        private void btnModificarArticulos_Click(object sender, EventArgs e)
        {
            FromModificar ModificarArticulo = new FromModificar();

            // Establece el formulario secundario como un control secundario del panel
            ModificarArticulo.TopLevel = false;
            ModificarArticulo.FormBorderStyle = FormBorderStyle.None;
            ModificarArticulo.Dock = DockStyle.Fill;

            // Limpia cualquier control existente en el panel
            PanelFormulario.Controls.Clear();

            // Agrega el formulario secundario al panel
            PanelFormulario.Controls.Add(ModificarArticulo);

            // Muestra el formulario secundario
            ModificarArticulo.Show();
        }

        private void btnElimianarArticulos_Click(object sender, EventArgs e)
        {
            FormEliminarArticulos EliminarArticulo = new FormEliminarArticulos();

            // Establece el formulario secundario como un control secundario del panel
            EliminarArticulo.TopLevel = false;
            EliminarArticulo.FormBorderStyle = FormBorderStyle.None;
            EliminarArticulo.Dock = DockStyle.Fill;

            // Limpia cualquier control existente en el panel
            PanelFormulario.Controls.Clear();

            // Agrega el formulario secundario al panel
            PanelFormulario.Controls.Add(EliminarArticulo);

            // Muestra el formulario secundario
            EliminarArticulo.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
