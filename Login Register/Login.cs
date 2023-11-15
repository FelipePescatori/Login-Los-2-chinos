using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

using UserEsquema;

namespace Login_Los_2_chinos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region METODOS VALIDAR DATOS USUARIO
        public int login(string Usuario, string Contrasena)
        {
            int usuarioID = -1;  // Valor por defecto en caso de que no se encuentre un usuario válido
            bool AccesoAdmin = false; // Valor por defecto

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UsuarioID, Nombre, Acceso FROM Usuarios WHERE Usuario = @usuario AND Contrasena = @contrasena", conn);
                cmd.Parameters.AddWithValue("@usuario", Usuario);
                cmd.Parameters.AddWithValue("@contrasena", Contrasena);
                SqlDataAdapter set = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                set.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    usuarioID = Convert.ToInt32(dt.Rows[0]["UsuarioID"]);  // Captura el UsuarioID si se encuentra un usuario válido
                    this.Hide();
                    if (dt.Rows[0]["Acceso"].ToString() == "Administrador")
                    {
                        AccesoAdmin = true;
                        new Administrador(dt.Rows[0]["Nombre"].ToString(), usuarioID, AccesoAdmin).Show();
                    }
                    else if (dt.Rows[0]["Acceso"].ToString() == "Operador")
                    {
                        AccesoAdmin = false;
                        new Administrador(dt.Rows[0]["Nombre"].ToString(), usuarioID, AccesoAdmin).Show();
                    }
                }
                else
                {
                    VariablesGlobales.MessageBox_Show("    Error", "   Usuario o Contraseña incorrectos", false);
                    txtUsuario.Clear();
                    txtUsuario.Text = "USUARIO";
                    txtContraseña.Clear();
                    txtContraseña.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size);
                    txtContraseña.Text = "CONTRASEÑA";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return usuarioID;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            login(this.txtUsuario.Text, this.txtContraseña.Text);
            txtContraseña.PasswordChar = '\0';
            pictureVer.BringToFront();
            
        }
        #endregion

        #region MOVER VENTANA
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        #endregion

        #region EVENTOS USUARIO Y CONTRASEÑA
        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }
        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "CONTRASEÑA")
            {
                txtContraseña.Text = "";
                pictureOcultar.BringToFront();
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.PasswordChar = '*';
            }
        }
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                txtContraseña.Text = "CONTRASEÑA";
                pictureVer.BringToFront();
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.PasswordChar = '\0';
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void pictureVer_Click(object sender, EventArgs e)
        {
            //mandamos al frente el picture ocultar
            pictureOcultar.BringToFront();
            txtContraseña.PasswordChar = '*';
        }
        private void pictureOcultar_Click(object sender, EventArgs e)
        {
            pictureVer.BringToFront();
            txtContraseña.PasswordChar = '\0';
        }

        //PARA LOS FOCUS
        private void Login_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtContraseña.Focus();
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAceptar.Focus();
                e.Handled = true;
            }
        }
        private void Login_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        #endregion

    }
}
