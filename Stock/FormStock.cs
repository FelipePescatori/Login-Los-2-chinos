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

namespace Login_Los_2_chinos.Stock
{
    public partial class FormStock : Form
    {
        public FormStock()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        private void FormStock_Load(object sender, EventArgs e)
        {

            // Cargar los datos desde la base de datos en el DataGridView
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Ventas", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgStock.DataSource = tabla;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }
    }    
}
