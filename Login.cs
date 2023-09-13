﻿using System;
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

        public void login(string Usuario, string Contrasena)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Nombre, Acceso FROM Usuarios WHERE Usuario = @usuario AND Contrasena = @contrasena", conn);
                cmd.Parameters.AddWithValue("@usuario", Usuario);
                cmd.Parameters.AddWithValue("@contrasena", Contrasena);
                SqlDataAdapter set = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                set.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][1].ToString() == "Administrador")
                    {
                        new Administrador(dt.Rows[0][0].ToString()).Show();
                    }
                    else if ((dt.Rows[0][1].ToString() == "Operador"))
                    {
                        new Operador(dt.Rows[0][0].ToString()).Show();
                    }
                }
                else
                {
                    VariablesGlobales.MessageBox_Show("    Error", "   Usuario o Contraseña incorrectos",false);
                    txtUsuario.Clear();
                    txtUsuario.Text = "USUARIO";
                    txtContraseña.Clear();
                    txtContraseña.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size);
                    txtContraseña.Text = "CONTRASEÑA";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex.Message);
            }
            finally { conn.Close(); }

        }
        #endregion

        #region MOVER VENTANA
        //Acordarce agregar el usgin using System.Runtime.InteropServices;
        //y el evento MousDown con esto: 
        //ReleaseCapture();
        //SendMessage(this.Handle, 0x112, 0xf012, 0);
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
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.PasswordChar = '*';
            }
        }
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                txtContraseña.Text = "CONTRASEÑA";
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.PasswordChar = '\0';
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            login(this.txtUsuario.Text, this.txtContraseña.Text);
            txtContraseña.PasswordChar = '*';
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
        #endregion


    }
}