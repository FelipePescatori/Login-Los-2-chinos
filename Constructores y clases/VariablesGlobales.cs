using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using FluentValidation;
using System.Text.RegularExpressions;

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


        #region VALIDAR
        public bool ValidarCampos(TextBox txtCodigoBarra,TextBox txtDetalle, TextBox txtPresentacion, TextBox txtStock)
        {
            bool resultado = true;
            //NULL
            if (string.IsNullOrEmpty(txtCodigoBarra.Text))
            {
                MessageBox_Show("Error de validación", "El campo CODIGO DE BARRA no puede estar vacio", false);
                resultado = false;
            }
            if (string.IsNullOrEmpty(txtDetalle.Text))
            {
                MessageBox_Show("Error de validación", "El campo DETALLE no puede estar vacío",false);
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtPresentacion.Text))
            {
                MessageBox_Show("Error de validación", "El campo PRESENTACION no puede estar vacío",false );
                resultado = false;
            }
            else if (string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox_Show("Error de validación", "El campo STOCK no puede estar vacío", false);
                resultado = false;
            }
            //VALIDACIONES 
            else if (!Regex.IsMatch(txtCodigoBarra.Text, @"^\d{13}$"))
            {
                MessageBox_Show("Error de validación", "Ingrese exactamente 13 números en CODIGO DE BARRA", false);
                resultado = false;
            }
            else if (!Regex.IsMatch(txtDetalle.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                MessageBox_Show("Error de validación", "Ingrese solo letras, números y espacios en DETALLE", false);
                resultado = false;
            }
            else if (!Regex.IsMatch(txtPresentacion.Text, @"^[a-zA-Z0-9\s/]+$"))
            {
                MessageBox_Show("Error de validación", "Ingrese solo letras o números en PRESENTACION", false);
                resultado = false;
            }         
            else if (!Regex.IsMatch(txtStock.Text, @"^\d+$"))
            {
                MessageBox_Show("Error de validación", "Ingrese solo números en STOCK", false);
                resultado = false;
            }

            return resultado;
        }
        #endregion
    }
}
