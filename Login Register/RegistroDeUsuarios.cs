using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class RegistroDeUsuarios : Form
    {
        public RegistroDeUsuarios()
        {
            InitializeComponent();
            // Llamas a las funciones para configurar los controles
            ConfigurarCuadroDeTexto(txtNombre, "Nombre");
            ConfigurarCuadroDeTexto(txtContraseña, "Contraseña");
            ConfigurarCuadroDeTexto(txtEmail, "Email");
            ConfigurarCuadroDeTexto(txtUsuario, "Usuario");
            ConfigurarCuadroDeTexto(txtCelular, "Celular");
            ConfigurarComboBox(cmbAcceso, "Acceso");
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        public void RegistrarUsuario(string Nombre, string Acceso, string Email, string Celular, string Usuario, string Contrasena)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, Acceso, Email, Celular, Usuario, Contrasena) VALUES (@nombre, @acceso, @email, @celular, @usuario, @contrasena)", conn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@acceso", Acceso);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@celular", Celular);
                cmd.Parameters.AddWithValue("@usuario", Usuario);
                cmd.Parameters.AddWithValue("@contrasena", Contrasena);
             
                VariablesGlobales.MessageBox_Show("   REGISTRO", "¿ESTAS SEGURO DE AGREGAR EL USUARIO?", true);
                if (VariablesGlobales.ResultadoDialogo == "YES")
                {
                    VariablesGlobales.MessageBox_Show("   REGISTRO", "EL USUARIO FUE AGREGADO CORRECTAMENTE", false);
                    SqlDataAdapter set = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    set.Fill(dt);
                }
                if (VariablesGlobales.ResultadoDialogo == "NO")
                {
                    VariablesGlobales.MessageBox_Show("   REGISTRO", "EL USUARIO NO FUE AGREGADO CORRECTAMENTE", false);
                }
            } catch (Exception )
            {
                VariablesGlobales.MessageBox_Show("   REGISTRO", "EL USUARIO NO FUE AGREGADO CORRECTAMENTE", false);
            }
            finally
            {
                LIMPIAR();
                txtNombre.Text = "Nombre";
                txtContraseña.Text = "Contraseña";
                txtEmail.Text = "Email";
                txtUsuario.Text = "Usuario";
                txtCelular.Text = "Celular";
                cmbAcceso.Text = "Acceso";
                conn.Close();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            RegistrarUsuario(this.txtNombre.Text, this.cmbAcceso.Text,this.txtEmail.Text,this.txtCelular.Text,this.txtUsuario.Text,this.txtContraseña.Text);
        }

        #region EVENTOS
        public void LIMPIAR()
        {
            txtNombre.Clear();
            cmbAcceso.SelectedIndex = -1;
            txtEmail.Clear();
            txtCelular.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
        }
        #endregion

        #region EVENTOS ENTER LEAVE
        // Función para configurar cuadros de texto
        private void ConfigurarCuadroDeTexto(TextBox textBox, string textoPredeterminado)
        {
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == textoPredeterminado)
                {
                    textBox.Text = string.Empty;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = textoPredeterminado;
                }
            };
        }

        // Función para configurar ComboBox
        private void ConfigurarComboBox(ComboBox comboBox, string textoPredeterminado)
        {
            comboBox.Enter += (sender, e) =>
            {
                if (comboBox.Text == textoPredeterminado)
                {
                    comboBox.Text = string.Empty;
                }
            };

            comboBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    comboBox.Text = textoPredeterminado;
                }
            };
        }
        #endregion

        #region CERRAR MINIMIZAR MOVER PANTALLA
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Mover Pantalla
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel9_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
