using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos.Venta
{
    public partial class FormCantidadEnGramos : Form
    {
        public FormCantidadEnGramos()
        {
            InitializeComponent();
        }

        public int CantidadEnGramos { get; private set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCantidadEnGramos.Text, out int cantidad))
            {
                CantidadEnGramos = cantidad;
                DialogResult = DialogResult.OK; // Establece el resultado como OK
                Close(); // Cierra el cuadro de diálogo
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad válida en gramos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
