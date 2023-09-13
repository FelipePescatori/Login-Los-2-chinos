using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var VentanaAdministrarArticulos = new FormAdministrarArticulos();
            VentanaAdministrarArticulos.ShowDialog();
        }

        private  void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


    }
}
