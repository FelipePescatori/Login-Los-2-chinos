using Login_Los_2_chinos.Stock;
using Login_Los_2_chinos.Venta;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class Administrador : Form
    {

        #region HORA / TIEMPO / NOMBRE / CERRAR MINIMIZAR MAXIMIZAR Y MOVER VENTANA

        #region MOVER VENTANA
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
        public Administrador(string Nombre, int UsuarioId, bool Acceso)
        {
            InitializeComponent();
            lbNombre.Text = Nombre;
            lblUsuarioId.Text = UsuarioId.ToString();
            AccesoUsuario = Acceso;
            if(AccesoUsuario)
            {
                txtAcceso.Text = "ADMINISTRADOR";
            }
            else
            {
                txtAcceso.Text = "OPERADOR";
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lbHora.Text = DateTime.Now.ToLongTimeString();
            lbTiempo.Text = DateTime.Now.ToShortDateString();
        }
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
        #endregion 

        private Form formularioAbierto = null;
        private bool AccesoUsuario;

        #region BTN VENTA
        bool PresVenta = false;
        private void btnVenta_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelVenta.BackColor = Color.Green;
            PresVenta = true;
            // Verifica si hay un formulario abierto y ciérralo si es necesario
            if (formularioAbierto != null && !formularioAbierto.IsDisposed)
            {
                formularioAbierto.Close();
            }
            FormVenta AbrirVenta = new FormVenta(lblUsuarioId.Text);

            AbrirVenta.TopLevel = false;
            AbrirVenta.FormBorderStyle = FormBorderStyle.None;
            AbrirVenta.Dock = DockStyle.Fill;


            PanelAdministrador.Controls.Clear();
            PanelAdministrador.Controls.Add(AbrirVenta);
            AbrirVenta.Show();
            formularioAbierto = AbrirVenta;
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
            if (AccesoUsuario)
            {
                // El usuario tiene acceso de Administrador
                ResetearColor();
                PanelArticulos.BackColor = Color.FromArgb(255, 124, 12);
                btnArticulos.BackColor = Color.Gainsboro;
                PreesArticulo = true;
                if (formularioAbierto != null && !formularioAbierto.IsDisposed)
                {
                    formularioAbierto.Close();
                }
                FromAgregar AbririArticulos = new FromAgregar();

                AbririArticulos.TopLevel = false;
                AbririArticulos.FormBorderStyle = FormBorderStyle.None;
                AbririArticulos.Dock = DockStyle.Fill;
                PanelAdministrador.Controls.Clear();

                PanelAdministrador.Controls.Add(AbririArticulos);
                AbririArticulos.Show();
                formularioAbierto = AbririArticulos;
            }
            else
            {
                VariablesGlobales.MessageBox_Show("ACCESO", "No tienen acceso para entrar porque eres Operador", false);
            }
        }
        private void btnArticulos_MouseEnter(object sender, EventArgs e)
        {
            PanelArticulos.BackColor = Color.FromArgb(255, 124, 12);
        }

        private void btnArticulos_MouseLeave(object sender, EventArgs e)
        {
            if (!PreesArticulo)
            {
                PanelArticulos.BackColor = Color.White;
            }
        }
        #endregion

        #region BTN USUARIOS
        bool PreesUsuario = false;
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (AccesoUsuario)
            {
                ResetearColor();
                PanelUsuario.BackColor = Color.FromArgb(104, 84, 196);
                btnUsuarios.BackColor = Color.Gainsboro;
                PreesUsuario = true;
                if (formularioAbierto != null && !formularioAbierto.IsDisposed)
                {
                    formularioAbierto.Close();
                }
                FromUsuarios AbrirUsuario = new FromUsuarios();

                AbrirUsuario.TopLevel = false;
                AbrirUsuario.FormBorderStyle = FormBorderStyle.None;
                AbrirUsuario.Dock = DockStyle.Fill;
                PanelAdministrador.Controls.Clear();

                PanelAdministrador.Controls.Add(AbrirUsuario);
                AbrirUsuario.Show();
                formularioAbierto = AbrirUsuario;
            }
            else
            {
                VariablesGlobales.MessageBox_Show("ACCESO", "No tienen acceso para entrar porque eres Operador", false);
            }
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
            if (AccesoUsuario)
            {
                ResetearColor();
                PanelProveedores.BackColor = Color.FromArgb(24, 116, 204);
                btnProveedores.BackColor = Color.Gainsboro;
                PreesProveedores = true;
                // Verifica si hay un formulario abierto y ciérralo si es necesario
                if (formularioAbierto != null && !formularioAbierto.IsDisposed)
                {
                    formularioAbierto.Close();
                }
                FromPorveedores VentanaProveedores = new FromPorveedores();

                VentanaProveedores.TopLevel = false;
                VentanaProveedores.FormBorderStyle = FormBorderStyle.None;
                VentanaProveedores.Dock = DockStyle.Fill;
                PanelAdministrador.Controls.Clear();

                PanelAdministrador.Controls.Add(VentanaProveedores);
                VentanaProveedores.Show();
                formularioAbierto = VentanaProveedores;
            }
            else
            {
                VariablesGlobales.MessageBox_Show("ACCESO", "No tienen acceso para entrar porque eres Operador", false);
            }

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

        #region BTN HISTORIAL DE VENTAS
        bool PreesHistorialDeVenta = false;
        private void btnHistorialDeVentas_Click(object sender, EventArgs e)
        {
            if (AccesoUsuario)
            {
                ResetearColor();
                PanelHistorialDeVenta.BackColor = Color.Yellow;
                btnHistorialDeVentas.BackColor = Color.Gainsboro;
                PreesHistorialDeVenta = true;
                // Verifica si hay un formulario abierto y ciérralo si es necesario
                if (formularioAbierto != null && !formularioAbierto.IsDisposed)
                {
                    formularioAbierto.Close();
                }
                HistorialVentas AbrirStock = new HistorialVentas();

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
                // Actualiza la variable del formulario abierto
                formularioAbierto = AbrirStock;
            }
            else
            {
                VariablesGlobales.MessageBox_Show("ACCESO", "No tienen acceso para entrar porque eres Operador", false);
            }

        }
        private void btnHistorialDeVentas_MouseEnter(object sender, EventArgs e)
        {
            PanelHistorialDeVenta.BackColor = Color.Yellow;
        }

        private void btnHistorialDeVentas_MouseLeave(object sender, EventArgs e)
        {
            if (!PreesHistorialDeVenta)
            {
                PanelHistorialDeVenta.BackColor = Color.White;
            }
        }
        #endregion

        #region BTN CAMARAS
        bool PreesCamaras = false;
        private void btnCamara_Click(object sender, EventArgs e)
        {
            ResetearColor();
            PanelCamaras.BackColor = Color.Red;
            btnCamara.BackColor = Color.Gainsboro;
            PreesCamaras = true;
            // Verifica si hay un formulario abierto y ciérralo si es necesario
            if (formularioAbierto != null && !formularioAbierto.IsDisposed)
            {
                formularioAbierto.Close();
            }
            FormCamaras AbrirCamaras = new FormCamaras();

            AbrirCamaras.TopLevel = false;
            AbrirCamaras.FormBorderStyle = FormBorderStyle.None;
            AbrirCamaras.Dock = DockStyle.Fill;


            PanelAdministrador.Controls.Clear();
            PanelAdministrador.Controls.Add(AbrirCamaras);
            AbrirCamaras.Show();
            formularioAbierto = AbrirCamaras;
        }
        private void btnCamara_MouseEnter(object sender, EventArgs e)
        {
            PanelCamaras.BackColor = Color.Red;
        }

        private void btnCamara_MouseLeave(object sender, EventArgs e)
        {
            if (!PreesCamaras)
            {
                PanelCamaras.BackColor = Color.White;
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
            PanelHistorialDeVenta .BackColor = Color.White;
            btnHistorialDeVentas.BackColor = Color.White;
            PreesHistorialDeVenta = false;
            PanelCamaras.BackColor = Color.White;
            btnCamara.BackColor = Color.White;
            PreesCamaras = false;
        }
        #endregion

    }
}
