using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class FromModificar : Form
    {
        public FromModificar()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        public void btnVer_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgAdminArticulos.DataSource = tabla;
            conn.Close();
        }

        private void dtgAdminArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtArticuloid.Text = dtgAdminArticulos.SelectedCells[0].Value.ToString();
            txtDetalle.Text = dtgAdminArticulos.SelectedCells[1].Value.ToString();
            txtPresentacion.Text = dtgAdminArticulos.SelectedCells[2].Value.ToString();
            txtVenta.Text = dtgAdminArticulos.SelectedCells[3].Value.ToString();
            txtCompra.Text = dtgAdminArticulos.SelectedCells[4].Value.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Verifica si los valores de los TextBox son válidos
                if (!int.TryParse(txtArticuloid.Text, out int articuloid) ||
                    string.IsNullOrEmpty(txtDetalle.Text) ||
                    string.IsNullOrEmpty(txtPresentacion.Text) ||
                    !decimal.TryParse(txtVenta.Text, out decimal precioVenta) ||
                    !decimal.TryParse(txtCompra.Text, out decimal precioCompra))
                {
                    VariablesGlobales.MessageBox_Show("ERROR", "Verifica los valores ingresados.", false);
                    return; 
                }

                // Utiliza parámetros en la consulta SQL
                string ConsultaModificar = "UPDATE Articulos SET Detalle = @Detalle, Presentacion = @Presentacion, PrecioVenta = @PrecioVenta, PrecioCompra = @PrecioCompra WHERE Articuloid = @Articuloid";
                SqlCommand comando = new SqlCommand(ConsultaModificar, conn);

                // Asigna valores a los parámetros
                comando.Parameters.AddWithValue("@Articuloid", articuloid);
                comando.Parameters.AddWithValue("@Detalle", txtDetalle.Text);
                comando.Parameters.AddWithValue("@Presentacion", txtPresentacion.Text);
                comando.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                comando.Parameters.AddWithValue("@PrecioCompra", precioCompra);

                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant > 0)
                {
                    VariablesGlobales.MessageBox_Show("    ARTICULO", "EL ARTICULO: " + txtDetalle.Text + " FUE MODIFICADO", false);
                }
                else
                {
                    VariablesGlobales.MessageBox_Show("    ERROR", "EL ARTICULO: " + txtDetalle.Text + " NO FUE MODIFICADO", false);
                }
            }
            catch (Exception ex)
            {
                VariablesGlobales.MessageBox_Show("    ERROR", "ERROR: " + ex.Message, false);
            }
            finally
            {
                conn.Close();
            }
        }

        private void AdmArticulos_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgAdminArticulos.DataSource = tabla;
            conn.Close();
        }
    }

}
