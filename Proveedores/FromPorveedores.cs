using Login_Los_2_chinos;
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
    public partial class FromPorveedores : Form
    {
        public FromPorveedores()
        {
            InitializeComponent();

        }
        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conn.Open();
            VariablesGlobales.MessageBox_Show("   PROVEEDORES", "¿ESTAS SEGURO DE AGREGAR EL PROVEEDOR?", true);
            if (VariablesGlobales.ResultadoDialogo == "YES")
            {
                string Consulta = "insert into Proveedores values (@Nombre, @CUIT, @Email, @Celular, @Rubro, @Direccion)";
                SqlCommand comandoAgregar = new SqlCommand(Consulta, conn);
                //comandoAgregar.Parameters.AddWithValue("@ProveedorID", txtProveedorID.Text);
                comandoAgregar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                comandoAgregar.Parameters.AddWithValue("@CUIT", txtCUIT.Text);
                comandoAgregar.Parameters.AddWithValue("@Email", txtEmail.Text);
                comandoAgregar.Parameters.AddWithValue("@Celular", txtCelular.Text);
                comandoAgregar.Parameters.AddWithValue("@Rubro", txtRubro.Text);
                comandoAgregar.Parameters.AddWithValue("@Direccion", txtDireccion.Text);

                comandoAgregar.ExecuteNonQuery();
                VariablesGlobales.MessageBox_Show("PROVEEDORES", "EL PROVEEDOR FUE AGREGADO CORRECTAMENTE", false);
                LIMPIAR();
            }
            if (VariablesGlobales.ResultadoDialogo == "NO")
            {
                VariablesGlobales.MessageBox_Show("PORVEEDORES", "EL PROVEEDOR NO FUE AGREGADO", false);
            }
            conn.Close();
        }
        #endregion

        #region MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            conn.Open();
            VariablesGlobales.MessageBox_Show("PROVEEDORES", "¿ESTAS SEGURO DE MODIFICADO EL PROVEEDORE?", true);
            if (VariablesGlobales.ResultadoDialogo == "YES")
            {
                string Consulta = "UPDATE Proveedores SET Nombre = @Nombre, CUIT = @CUIT, Email = @Email, Celular = @Celular, Rubro = @Rubro, Direccion = @Direccion WHERE ProveedorID = @ProveedorID";
                SqlCommand comandoAgregar = new SqlCommand(Consulta, conn);
                comandoAgregar.Parameters.AddWithValue("@ProveedorID", txtProveedorID.Text);
                comandoAgregar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                comandoAgregar.Parameters.AddWithValue("@CUIT", txtCUIT.Text);
                comandoAgregar.Parameters.AddWithValue("@Email", txtEmail.Text);
                comandoAgregar.Parameters.AddWithValue("@Celular", txtCelular.Text);
                comandoAgregar.Parameters.AddWithValue("@Rubro", txtRubro.Text);
                comandoAgregar.Parameters.AddWithValue("@Direccion", txtDireccion.Text);

                comandoAgregar.ExecuteNonQuery();
                VariablesGlobales.MessageBox_Show("PROVEEDORES", "EL PROVEEDORE FUE MODIFICADO CORRECTAMENTE", false);
            }
            if (VariablesGlobales.ResultadoDialogo == "NO")
            {
                VariablesGlobales.MessageBox_Show("PROVEEDORES", "EL PROVEEDORE NO FUE MODIFICADO", false);
            }
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Proveedores", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgProveedores.DataSource = tabla;
            LIMPIAR();
            conn.Close();
        }

        private void dtgProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtProveedorID.Text = dtgProveedores.SelectedCells[0].Value.ToString();
                txtNombre.Text = dtgProveedores.SelectedCells[1].Value.ToString();
                txtCUIT.Text = dtgProveedores.SelectedCells[2].Value.ToString();
                txtEmail.Text = dtgProveedores.SelectedCells[3].Value.ToString();
                txtCelular.Text = dtgProveedores.SelectedCells[4].Value.ToString();
                txtRubro.Text = dtgProveedores.SelectedCells[5].Value.ToString();
                txtDireccion.Text = dtgProveedores.SelectedCells[6].Value.ToString();
            }
            catch (Exception) { }

        }
        #endregion

        #region ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (txtNombreBuscar.Text == "")
            {
                MessageBox.Show("ProveedorID");
                string detalleABuscar = txtProveedorIdBuscar.Text;
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
            if (txtProveedorIdBuscar.Text == "")
            {
                MessageBox.Show("Nombre");
                string detalleABuscar = txtNombreBuscar.Text;
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

        private void txtProveedorIdBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtProveedorIdBuscar.Text;
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
        private void txtNombreBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNombreBuscar.Text;
            (dtgProveedores.DataSource as DataTable).DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%'";
            dtgProveedores.Refresh();
        }
        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtProveedorIdBuscar.Text = string.Empty;
        }
        private void txtProveedorIdBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.Text = string.Empty;
        }
        #endregion

        private void LIMPIAR()
        {
            txtProveedorID.Text = "";
            txtNombre.Text = "";
            txtCUIT.Text = "";
            txtEmail.Text = "";
            txtCelular.Text = "";
            txtRubro.Text = "";
            txtDireccion.Text = "";
        }

        private void FromPorveedores_Load(object sender, EventArgs e)
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

        private void dtgProveedores_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtProveedorID.Text = dtgProveedores.SelectedCells[0].Value.ToString();
            txtNombre.Text = dtgProveedores.SelectedCells[1].Value.ToString();
            txtCUIT.Text = dtgProveedores.SelectedCells[2].Value.ToString();
            txtEmail.Text = dtgProveedores.SelectedCells[3].Value.ToString();
            txtCelular.Text = dtgProveedores.SelectedCells[4].Value.ToString();
            txtRubro.Text = dtgProveedores.SelectedCells[5].Value.ToString();
            txtDireccion.Text = dtgProveedores.SelectedCells[6].Value.ToString();
        }
    }
}
