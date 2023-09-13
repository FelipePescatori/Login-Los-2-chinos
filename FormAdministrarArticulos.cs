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
            dtgAdministrarArticulos.DataSource = tabla;
            conn.Close();
        }
        #region BOTONES
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var VentanaAgregar = new FromAgregar();
            VentanaAgregar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string detalleABuscar = txtBuscar.Text;
            string consultaEliminar = "DELETE FROM Articulos WHERE Detalle = @Detalle";
            SqlCommand comando = new SqlCommand(consultaEliminar, conn);
            comando.Parameters.AddWithValue("@Detalle", detalleABuscar);

            bool encontrado = false; // Variable para rastrear si se encontró el detalle
            foreach (DataGridViewRow fila in dtgAdministrarArticulos.Rows)
            {
                if (fila.Cells["Detalle"].Value != null && fila.Cells["Detalle"].Value.ToString() == detalleABuscar)
                {
                    encontrado = true;
                    break;
                }
            }
            if (encontrado)
            {
                VariablesGlobales.MessageBox_Show("   ARTICULOS", "¿ESTAS SEGURO DE ELIMINAR EL PRODUCTO?", true);
                if (VariablesGlobales.ResultadoDialogo == "YES")
                {
                    comando.ExecuteNonQuery();
                    VariablesGlobales.MessageBox_Show("   PRODUCTO", "EL PRODUTO FUE ELIMINADO", false);
                    dtgAdministrarArticulos.Refresh();
                    SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgAdministrarArticulos.DataSource = tabla;
                    txtBuscar.Clear();
                } if (VariablesGlobales.ResultadoDialogo == "NO")
                {
                    VariablesGlobales.MessageBox_Show("   PRODUCTO", "EL PRODUTO NO FUE ELIMINAR", false);
                    txtBuscar.Clear();
                }
                dtgAdministrarArticulos.Invalidate();
                conn.Close();
            }

            conn.Close();
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var VentanaModificar = new FromModificar();
            VentanaModificar.ShowDialog();
        }
        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text;
            (dtgAdministrarArticulos.DataSource as DataTable).DefaultView.RowFilter = $"Detalle LIKE '%{filtro}%' OR Detalle LIKE '%{filtro}%'";
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
