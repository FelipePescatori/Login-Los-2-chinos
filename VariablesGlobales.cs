using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;

namespace Login_Los_2_chinos
{
    public class VariablesGlobales
    {

        #region MENSAJE DE ERROR 

        public static string ResultadoDialogo;
        public static void MessageBox_Show(string titulo, string mensaje, bool SIoNO)
        {
            MessageBoxFrom mbf = new MessageBoxFrom();
            mbf.lblTitulo.Text = titulo;
            mbf.frMessageBox.Text = mensaje;

            if (SIoNO == true)
            {
                mbf.tabControl1.SelectedIndex = 1;
            }
            else if(SIoNO == false)
            {
                mbf.tabControl1.SelectedIndex = 0;
            }

            mbf.ShowDialog();

            if (mbf.DialogResult == DialogResult.OK)
            {
                ResultadoDialogo = "OK";
            }
            else if(mbf.DialogResult == DialogResult.Yes)
            {
                ResultadoDialogo = "YES";
            }
            else if (mbf.DialogResult == DialogResult.No)
            {
                ResultadoDialogo = "NO";
            }
            mbf.Close();
        }
        #endregion

    }
}
