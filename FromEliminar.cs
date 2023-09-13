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

namespace Login_Los_2_chinos
{
    public partial class FromEliminar : Form
    {
        public FromEliminar()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string consultaEliminar = " Delete from Articulos where Articuloid = " + txtEliminar.Text + "";
            SqlCommand comandoo = new SqlCommand(consultaEliminar, conn);
            comandoo.ExecuteNonQuery();
            VariablesGlobales.MessageBox_Show("   ARTICULOS", " EL ARTICULO FUE ELIMINADO", true);
            txtEliminar.Clear();
            conn.Close();
        }
        private void FromEliminar_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgEliminar.DataSource = tabla;
            conn.Close();
        }
    }
}
