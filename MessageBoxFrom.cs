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
