using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class FromAgregar : Form
    {
        public FromAgregar()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conn.Open();
            //Se usan 'comillas simples cuando es string' sino "123"
            string Consulta = "insert into Articulos values ('" + txtDetalle.Text + "','" + txtPresentacion.Text + "'," + txtVenta.Text + "," + txtCompra.Text +")";
            SqlCommand comandoAgregar = new SqlCommand(Consulta, conn);
            comandoAgregar.ExecuteNonQuery();
            VariablesGlobales.MessageBox_Show("   AGREGAR","EL ARTICULO FUE AGREGADO CORRECTAMENTE",false);
            LIMIAR();
            conn.Close();
        }
        private void FromAgregar_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgAgregar.DataSource = tabla;
            conn.Close();
        }

        #region METODOS
        public void LIMIAR()
        {
            txtDetalle.Clear();
            txtPresentacion.Clear();
            txtVenta.Clear();
            txtCompra.Clear();
        }

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
                btnAgregar.Focus();
                e.Handled = true;
            }
        }
    }
}
