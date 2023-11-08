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
using ValidarLibrary;

namespace Login_Los_2_chinos
{
    public partial class FromModificar : Form
    {
        
        public FromModificar()
        {
            InitializeComponent();
            
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (decimal.TryParse(txtVenta.Text, out decimal precioVenta) &&
                    decimal.TryParse(txtCompra.Text, out decimal precioCompra))
                {
                    SqlCommand comando = new SqlCommand("UPDATE Articulos SET detalle = @detalle, presentacion = @presentacion, PrecioVenta = @PrecioVenta, PrecioCompra = @PrecioCompra, Stock = @Stock WHERE Articuloid = @articuloid", conn);
                    comando.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                    comando.Parameters.AddWithValue("@presentacion", txtPresentacion.Text);
                    comando.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    comando.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    comando.Parameters.AddWithValue("@Stock", txtStock.Text);
                    comando.Parameters.AddWithValue("@articuloid", txtArticuloid.Text);
                    //VALIDAR CON UN METODO EN VARIABLES GLOBALES
                    VariablesGlobales variablesGlobales = new VariablesGlobales();
                   // bool resultado = variablesGlobales.ValidarCampos(txtDetalle, txtPresentacion, txtVenta, txtCompra);
                    
                   
                        VariablesGlobales.MessageBox_Show("ARTICULO", "ESTAS SEGURO DE MODIFICAR EL ARTICULO:  "+ txtDetalle.Text +"", true);
                        if (VariablesGlobales.ResultadoDialogo == "YES")
                        {
                            comando.ExecuteNonQuery();
                            VariablesGlobales.MessageBox_Show("ARTICULO", "EL ARTICULO: " + txtDetalle.Text + " FUE MODIFICADO", false);
                            LIMPIAR();
                        }
                    
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("   PRODUCTO", "EL PRODUTO NO FUE ELIMINAR", false);
                    }                   
                }
            }
            catch (Exception ex)
            {
                VariablesGlobales.MessageBox_Show("    ERROR", "ERROR: " + ex.Message, false);
            }
            finally
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgModificar.DataSource = tabla;
                conn.Close();
            }
        }
        #endregion

        #region EVENTOS
        public void LIMPIAR()
        {
            txtArticuloid.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtVenta.Text = string.Empty;
            txtCompra.Text = string.Empty;
        }
        private void FromModificar_Load(object sender, EventArgs e)
        {
            //Refrescar la pagina cuando le doy el foco 
            dtgModificar.Refresh();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgModificar.DataSource = tabla;
        }
        //Enviar datos del data grid a los textbox
        private void dtgAdminArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDetalle.Text = dtgModificar.SelectedCells[1].Value.ToString();
            txtPresentacion.Text = dtgModificar.SelectedCells[2].Value.ToString();
            txtVenta.Text = dtgModificar.SelectedCells[3].Value.ToString();
            txtCompra.Text = dtgModificar.SelectedCells[4].Value.ToString();
            txtStock.Text = dtgModificar.SelectedCells[5].Value.ToString();
            txtArticuloid.Text = dtgModificar.SelectedCells[6].Value.ToString();
        }
        #endregion

        #region BUSCAR TXTBOX
        private void txtArticuloid_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtArticuloidBuscar.Text;
            if (int.TryParse(filtro, out int numeroFiltro))
            {
                (dtgModificar.DataSource as DataTable).DefaultView.RowFilter = $"Articuloid = {numeroFiltro}";
            }
            else
            {
                // Si el valor ingresado no es un número válido, elimina el filtro
                (dtgModificar.DataSource as DataTable).DefaultView.RowFilter = "";
            }
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNombre.Text;
            (dtgModificar.DataSource as DataTable).DefaultView.RowFilter = $"Detalle LIKE '%{filtro}%' OR Detalle LIKE '%{filtro}%'";
        }
        private void txtArticuloidBuscar_Leave(object sender, EventArgs e)
        {
            txtArticuloidBuscar.Text = string.Empty;
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
        }
        #endregion

        #region MANEJAR TEXBOX INDEX

        #endregion

        private void txtDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPresentacion.Focus();
                e.Handled = true;
            }
        }

        private void txtPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtVenta.Focus();
                e.Handled = true;
            }
        }

        private void txtVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCompra.Focus();
                e.Handled = true;
            }
        }

        private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnModificar.Focus();
                e.Handled = true;
            }
        }
    }
}
