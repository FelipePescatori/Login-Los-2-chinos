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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using UserEsquema;
using Login_Los_2_chinos.Venta;

namespace Login_Los_2_chinos
{
    public partial class FromUsuarios : Form
    {
        public FromUsuarios()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        public void CargarDTG()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Usuarios", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgUsuarios.DataSource = tabla;
        }
        private void FromUsuarios_Load(object sender, EventArgs e)
        {
            CargarDTG();
            txtNombree.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Acceso = @Acceso, Email = @Email, Celular = @Celular, Usuario = @Usuario, Contrasena = @Contrasena WHERE Usuarioid = @Usuarioid", conn);

                // Asegúrate de asignar los valores correctos desde los TextBox
                int idUsuario;
                if (int.TryParse(txtUsuarioID.Text, out idUsuario))
                {
                    cmd.Parameters.AddWithValue("@Usuarioid", idUsuario);
                }

                cmd.Parameters.AddWithValue("@Nombre", txtNombree.Text);
                cmd.Parameters.AddWithValue("@Acceso", txtAcceso.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@Contrasena", txtContraseña.Text);

                // Mostrar un mensaje de confirmación
                DialogResult resultado = MessageBox.Show("¿ESTÁS SEGURO DE MODIFICAR EL USUARIO?", "MODIFICACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("EL USUARIO FUE MODIFICADO CORRECTAMENTE", "MODIFICACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("EL USUARIO NO FUE MODIFICADO CORRECTAMENTE", "MODIFICACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                CargarDTG(); // Asumo que esta función carga los datos en un DataGridView

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dtgUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtUsuarioID.Text = dtgUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombree.Text = dtgUsuarios.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAcceso.Text = dtgUsuarios.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtEmail.Text = dtgUsuarios.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCelular.Text = dtgUsuarios.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtUsuario.Text = dtgUsuarios.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtContraseña.Text = dtgUsuarios.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroDeUsuarios AbrirRegistroUsuarios = new RegistroDeUsuarios();
            AbrirRegistroUsuarios.Show();

        }
    }
}
