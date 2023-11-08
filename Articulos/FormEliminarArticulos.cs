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
using ValidarLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login_Los_2_chinos
{
    public partial class FormEliminarArticulos : Form
    {
        public FormEliminarArticulos()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        
        #region BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (txtNombre.Text == "" && txtArticuloid.Text == "")
                {
                    VariablesGlobales.MessageBox_Show("ARTICULOS", "DEBES DE INGRESAR UN DETALLE O ID DE UN ARTICULO", false);
                }
                else if (txtNombre.Text == "")
                {
                    string detalleABuscar = txtArticuloid.Text;
                    string consultaEliminar = "DELETE FROM Articulos WHERE Articuloid = @Articuloid";
                    SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                    comando.Parameters.AddWithValue("@Articuloid", detalleABuscar);

                    VariablesGlobales.MessageBox_Show("ARTICULOS", "¿ESTAS SEGURO DE ELIMINAR EL PRODUCTO?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO NO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                }
                else if (txtArticuloid.Text == "")
                {
                    string detalleABuscar = txtNombre.Text;
                    string consultaEliminar = "DELETE TOP(1) FROM Articulos WHERE Detalle = @Detalle";
                    SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                    comando.Parameters.AddWithValue("@Detalle", detalleABuscar);

                    VariablesGlobales.MessageBox_Show("ARTICULOS", "¿ESTAS SEGURO DE ELIMINAR EL PRODUCTO?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO NO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                VariablesGlobales.MessageBox_Show("ERROR", "ERROR: " + ex.Message, false);
            }
            finally
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgAdministrarArticulos.DataSource = tabla;
                conn.Close();
            }
        }

        #endregion

        #region EVENTOS
        private void FormEliminarArticulos_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgAdministrarArticulos.DataSource = tabla;
        }
        #endregion

        #region BUSCAR
        private void txtArticuloidd_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtArticuloid.Text;
            if (int.TryParse(filtro, out int numeroFiltro))
            {
                (dtgAdministrarArticulos.DataSource as DataTable).DefaultView.RowFilter = $"Articuloid = {numeroFiltro}";
            }
            else
            {
                // Si el valor ingresado no es un número válido, elimina el filtro
                (dtgAdministrarArticulos.DataSource as DataTable).DefaultView.RowFilter = "";
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNombre.Text;
            (dtgAdministrarArticulos.DataSource as DataTable).DefaultView.RowFilter = $"Detalle LIKE '%{filtro}%' OR Detalle LIKE '%{filtro}%'";
        }
        #endregion

        #region ADMINISTRAR LOS TXTBUSCAR

        //Para Borrar unos de los 2 txtbox
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            txtArticuloid.Text = string.Empty;
        }

        private void txtArticuloid_Enter(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
        }
        #endregion

    }
}
