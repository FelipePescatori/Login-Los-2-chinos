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
    public partial class FormEliminarProveedor : Form
    {
        public FormEliminarProveedor()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (txtNombre.Text == "")
            {
                MessageBox.Show("ProveedorID");
                string detalleABuscar = txtProveedorID.Text;
                string consultaEliminar = "DELETE FROM Proveedores WHERE ProveedorID = @ProveedorID";
                SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                comando.Parameters.AddWithValue("@ProveedorID", detalleABuscar);

                bool encontrado = false; // Variable para rastrear si se encontró el ProveedorID
                foreach (DataGridViewRow fila in dtgProveedores.Rows)
                {
                    if (fila.Cells["ProveedorID"].Value != null && fila.Cells["ProveedorID"].Value.ToString() == detalleABuscar)
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado)
                {
                    VariablesGlobales.MessageBox_Show("   PROVEEDORES", "¿ESTAS SEGURO DE ELIMINAR EL PROVEEDOR?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("   PROVEEDORES", "EL PROVEEDOR FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("   PROVEEDORES", "EL PROVEEDOR NO FUE ELIMINAR", false);
                        txtNombre.Clear();
                    }
                    dtgProveedores.Refresh();
                    conn.Close();
                }
            }
            if (txtProveedorID.Text == "")
            {
                MessageBox.Show("Nombre");
                string detalleABuscar = txtNombre.Text;
                string consultaEliminar = "DELETE FROM Proveedores WHERE Nombre = @Nombre";
                SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                comando.Parameters.AddWithValue("@Nombre", detalleABuscar);

                bool encontrado = false; // Variable para rastrear si se encontró el detalle
                foreach (DataGridViewRow fila in dtgProveedores.Rows)
                {
                    if (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString() == detalleABuscar)
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado)
                {
                    VariablesGlobales.MessageBox_Show("   PROVEEDORES", "¿ESTAS SEGURO DE ELIMINAR EL PROVEEDOR?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("   PROVEEDORES", "EL PROVEEDOR FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("   PROVEEDORES", "EL PROVEEDOR NO FUE ELIMINAR", false);
                        txtNombre.Clear();
                    }
                    dtgProveedores.Refresh();
                    conn.Close();
                }
            }
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Proveedores", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgProveedores.DataSource = tabla;
            conn.Close();        
        }
        #endregion

        #region LOAD
        private void FormEliminarProveedor_Load(object sender, EventArgs e)
        {
            //Refrescar la pagina cuando le doy el foco 
            dtgProveedores.Refresh();
            //mostrar en el dataGridView los datos de la base de datos
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Proveedores", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgProveedores.DataSource = tabla;
            dtgProveedores.Columns[0].Width = 90;
        }
        #endregion

        #region MANEJAR BUSCADORES
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNombre.Text;
            (dtgProveedores.DataSource as DataTable).DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%'";
            dtgProveedores.Refresh();
        }

        private void txtProveedorID_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtProveedorID.Text;
            int proveedorID;
            if (int.TryParse(filtro, out proveedorID))
            {
                (dtgProveedores.DataSource as DataTable).DefaultView.RowFilter = $"ProveedorID = {proveedorID}";
            }
            else
            {
                (dtgProveedores.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            dtgProveedores.Refresh();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtProveedorID.Text = string.Empty;
        }

        private void txtProveedorID_Enter(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
        }
        #endregion
    }
}
