using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public partial class MessageBoxFrom : Form
    {
        public MessageBoxFrom()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            this.AcceptButton = btnaceptar;
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Simular un clic en el botón
                btnaceptar.PerformClick();
            }
        }
        public static string ResultDialog;

        public void btnSi_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        public void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
