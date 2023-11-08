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
    public partial class FormAgregarProveedor : Form
    {
        public FormAgregarProveedor()
        {
            InitializeComponent();
        }

        #region AGREGAR
        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
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

        #region EVENTOS
        private void FormAgregarProveedor_Load(object sender, EventArgs e)
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

        private void LIMPIAR()
        {
            txtNombre.Text = "";
            txtCUIT.Text = "";
            txtEmail.Text = "";
            txtCelular.Text = "";
            txtRubro.Text = "";
            txtDireccion.Text = "";
        }
        #endregion
    }
}
