
using Login_Los_2_chinos.Stock;
using Login_Los_2_chinos.Venta;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class Administrador : Form
    {
        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region HORA / TIEMPO / NOMBRE
        public Administrador(string Nombre)
        {
            InitializeComponent();
            lbNombre.Text = Nombre;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lbHora.Text = DateTime.Now.ToLongTimeString();
            lbTiempo.Text = DateTime.Now.ToShortDateString();
        }

        #endregion

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var AbrirLogin = new Login();
            AbrirLogin.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void btnCamara_Click(object sender, EventArgs e)
        {
            var VentanaCamara = new CAMARA();
            VentanaCamara.ShowDialog();
        }





        private void lbHora_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

       

        FromAgregar fromagregar = new FromAgregar();
        private void pictureBox6_Click(object sender, EventArgs e)
        {

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            FormStock AbrirStock = new FormStock();

            // Establece el formulario secundario como un control secundario del panel
            AbrirStock.TopLevel = false;
            AbrirStock.FormBorderStyle = FormBorderStyle.None;
            AbrirStock.Dock = DockStyle.Fill;

            // Limpia cualquier control existente en el panel
            PanelAdministrador.Controls.Clear();

            // Agrega el formulario secundario al panel
            PanelAdministrador.Controls.Add(AbrirStock);

            // Muestra el formulario secundario
            AbrirStock.Show();
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            FormStock AbrirStock = new FormStock();

            // Establece el formulario secundario como un control secundario del panel
            AbrirStock.TopLevel = false;
            AbrirStock.FormBorderStyle = FormBorderStyle.None;
            AbrirStock.Dock = DockStyle.Fill;

            // Limpia cualquier control existente en el panel
            PanelAdministrador.Controls.Clear();

            // Agrega el formulario secundario al panel
            PanelAdministrador.Controls.Add(AbrirStock);

            // Muestra el formulario secundario
            AbrirStock.Show();
        }
        #region BTN VENTA
        bool PresVenta = false;
        private void btnVenta_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelVenta.BackColor = Color.Green;
            PresVenta = true;
            FormVenta AbrirVenta = new FormVenta();

            AbrirVenta.TopLevel = false;
            AbrirVenta.FormBorderStyle = FormBorderStyle.None;
            AbrirVenta.Dock = DockStyle.Fill;


            PanelAdministrador.Controls.Clear();
            PanelAdministrador.Controls.Add(AbrirVenta);
            AbrirVenta.Show();
        }
        private void btnVenta_MouseEnter(object sender, EventArgs e)
        {
            PanelVenta.BackColor = Color.Green;

        }
        private void btnVenta_MouseLeave(object sender, EventArgs e)
        {
            if (!PresVenta)
            {
                PanelVenta.BackColor = Color.White;
            }

        }
        #endregion
        #region BTN ARTICULOS

        bool PreesArticulo = false;
        private void btnArticulos_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelArticulos.BackColor = Color.FromArgb(255, 124, 12);
            btnArticulos.BackColor = Color.Gainsboro;
            PreesArticulo = true;
            FromAgregar VentanaAdministrarArticulos = new FromAgregar();

            VentanaAdministrarArticulos.TopLevel = false;
            VentanaAdministrarArticulos.FormBorderStyle = FormBorderStyle.None;
            VentanaAdministrarArticulos.Dock = DockStyle.Fill;
            PanelAdministrador.Controls.Clear();

            PanelAdministrador.Controls.Add(VentanaAdministrarArticulos);
            VentanaAdministrarArticulos.Show();
        }
        private void btnArticulos_MouseEnter(object sender, EventArgs e)
        {
            PanelArticulos.BackColor = Color.FromArgb(255, 124, 12);
        }

        private void btnArticulos_MouseLeave(object sender, EventArgs e)
        {
            if(!PreesArticulo)
            {
                PanelArticulos.BackColor = Color.White;
            }
        }
        #endregion

        #region BTN USUARIOS
        bool PreesUsuario = false;
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelUsuario.BackColor = Color.FromArgb(104, 84, 196);
            btnUsuarios.BackColor = Color.Gainsboro;
            PreesUsuario = true;

            var VentanaDeUsuario = new RegistroDeUsuarios();
            VentanaDeUsuario.ShowDialog();
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.FlatAppearance.BorderSize = 0;



        }
        private void btnUsuarios_MouseEnter(object sender, EventArgs e)
        {
            PanelUsuario.BackColor = Color.FromArgb(104, 84, 196);
        }

        private void btnUsuarios_MouseLeave(object sender, EventArgs e)
        {
            if (!PreesUsuario)
            {
                PanelUsuario.BackColor = Color.White;
            }
        }
        #endregion

        #region BTN PROVEEDORES
        bool PreesProveedores = false;
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelProveedores.BackColor = Color.FromArgb(24, 116, 204);
            btnProveedores.BackColor = Color.Gainsboro;
            PreesProveedores = true;
            FromPorveedores VentanaProveedores = new FromPorveedores();

            VentanaProveedores.TopLevel = false;
            VentanaProveedores.FormBorderStyle = FormBorderStyle.None;
            VentanaProveedores.Dock = DockStyle.Fill;
            PanelAdministrador.Controls.Clear();

            PanelAdministrador.Controls.Add(VentanaProveedores);
            VentanaProveedores.Show();
        }
        private void btnProveedores_MouseEnter(object sender, EventArgs e)
        {
            PanelProveedores.BackColor = Color.FromArgb(24, 116, 204);
        }
        private void btnProveedores_MouseLeave(object sender, EventArgs e)
        {
            if(!PreesProveedores)
            {
                PanelProveedores.BackColor = Color.White;
            }
        }
        #endregion
        #region MANEJAR COLORES
        private void ResetearColor()
        {
            PanelVenta.BackColor = Color.White;
            btnVenta.BackColor = Color.White;
            PresVenta = false;
            PanelArticulos.BackColor = Color.White;
            btnArticulos.BackColor = Color.White;
            PreesArticulo = false;
            PanelUsuario.BackColor = Color.White;
            btnUsuarios.BackColor = Color.White;
            PreesUsuario = false;
            PanelProveedores.BackColor = Color.White;
            btnProveedores .BackColor = Color.White;
            PreesProveedores = false;
        }
        #endregion


    }
}
