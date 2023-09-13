using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class FormAdministrarArticulos : Form
    {
        public FormAdministrarArticulos()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        private void FormAdministrarArticulos_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvAdministrarArticulos.DataSource = tabla;
            conn.Close();
        }

        #region BOTONES
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var ventanaAgregar = new FromAgregar();
            ventanaAgregar.ShowDialog();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            var ventanaModificar = new AdmArticulos();
            ventanaModificar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var ventanaEliminar = new FromEliminar();
            ventanaEliminar.ShowDialog();
        }
        #endregion


    }
}
