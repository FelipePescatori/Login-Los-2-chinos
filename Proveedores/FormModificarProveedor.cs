using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidarLibrary;

namespace Login_Los_2_chinos.Proveedores
{
    public partial class FormModificarProveedor : Form
    {
        public FormModificarProveedor()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
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
            txtProveedorID.Text = dtgProveedores.SelectedCells[0].Value.ToString();
            txtNombre.Text = dtgProveedores.SelectedCells[1].Value.ToString();
            txtCUIT.Text = dtgProveedores.SelectedCells[2].Value.ToString();
            txtEmail.Text = dtgProveedores.SelectedCells[3].Value.ToString();
            txtCelular.Text = dtgProveedores.SelectedCells[4].Value.ToString();
            txtRubro.Text = dtgProveedores.SelectedCells[5].Value.ToString();
            txtDireccion.Text = dtgProveedores.SelectedCells[6].Value.ToString();
        }

        private void FormModificarProveedor_Load(object sender, EventArgs e)
        {
            //Refrescar la pagina cuando le doy el foco 
            dtgProveedores.Refresh();
            //mostrar en el dataGridView los datos de la base de datos
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Proveedores", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgProveedores.DataSource = tabla;
        }
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

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.Text = "";
        }

        private void txtCUIT_Enter(object sender, EventArgs e)
        {
            txtCUIT.Text = "";
        }

        private void txtRubro_Enter(object sender, EventArgs e)
        {
            txtRubro.Text = "";
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.Text = "";
        }

        private void txtCelular_Enter(object sender, EventArgs e)
        {
            txtCelular.Text = "";
        }

        private void txtDireccion_Enter(object sender, EventArgs e)
        {
            txtDireccion.Text = "";
        }

        #region VALIDAR TEXBOX
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            // el ! es para buscar los errores en ves de las correcto
            if (!Validator.IsAlphabetic(txtNombre.Text))
            {
                txtNombre.Focus();
                errorProvider1.SetError(txtNombre, "Ingrese solo letras");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNombre, string.Empty);
                e.Cancel = false;
            }
        }

        
        private void txtRubro_Validating(object sender, CancelEventArgs e)
        {
            if (!Validator.IsAlphabetic(txtRubro.Text))
            {
                txtRubro.Focus();
                errorProvider1.SetError(txtRubro, "Ingrese solo letras");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtRubro, string.Empty);
                e.Cancel = false;
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!Validator.IsEmail(txtEmail.Text))
            {
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Ingrese solo letras");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtEmail, string.Empty);
                e.Cancel = false;
            }
        }

        private void txtCelular_Validating(object sender, CancelEventArgs e)
        {
            if (!Validator.IsAlphaNumeric(txtCelular.Text))
            {
                txtCelular.Focus();
                errorProvider1.SetError(txtCelular, "Ingrese solo letras");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCelular, string.Empty);
                e.Cancel = false;
            }
        }

        private void txtDireccion_Validating(object sender, CancelEventArgs e)
        {
            if (!Validator.IsDomicilio(txtDireccion.Text))
            {
                txtDireccion.Focus();
                errorProvider1.SetError(txtDireccion, "Ingrese solo letras");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtDireccion, string.Empty);
                e.Cancel = false;
            }
        }

        private void txtCUIT_Validating(object sender, CancelEventArgs e)
        {
            if (!Validator.IsAlphaNumeric(txtCUIT.Text))
            {
                txtCUIT.Focus();
                errorProvider1.SetError(txtCUIT, "ingrese un CUIT válido en formato por ejemplo: 20-12345678-1");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCUIT, string.Empty);
                e.Cancel = false;
            }
        }


        #endregion
    }
}
