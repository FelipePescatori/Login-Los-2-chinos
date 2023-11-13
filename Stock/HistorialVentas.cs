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
    public partial class HistorialVentas : Form
    {
        public HistorialVentas()
        {
            InitializeComponent();

            //para manejar los CheckBox
            chkPorDia.Checked = true;
            chkPorMes.Checked = false;
            selectByMonth = false;
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");
        private void HistorialVentas_Load(object sender, EventArgs e)
        {
            // Cargar los datos desde la base de datos en el DataGridView
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Ventas", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgHistorialVentas.DataSource = tabla;
            CalcularTotal();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }

        #region MANEJAR CALENDARIO 
        private List<DateTime> selectedDates = new List<DateTime>();
        private Color selectedColor = Color.Violet;
        private bool selectByMonth = false;
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Captura la fecha seleccionada en el MonthCalendar
            DateTime selectedDate = monthCalendar1.SelectionStart.Date;

            // Verifica si la selección es por día o por mes
            if (selectByMonth)
            {
                // Si se selecciona por mes, agrega todas las fechas del mes al conjunto de fechas seleccionadas
                DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
                {
                    if (!selectedDates.Contains(date))
                    {
                        selectedDates.Add(date);
                        monthCalendar1.AddBoldedDate(date);
                    }
                }
            }
            else
            {
                // Si se selecciona por día, verifica si la fecha ya está en la lista de fechas seleccionadas
                if (selectedDates.Contains(selectedDate))
                {
                    // Si ya está en la lista, la quitamos
                    selectedDates.Remove(selectedDate);

                    // Restablece el color de fondo al color predeterminado (blanco)
                    monthCalendar1.RemoveBoldedDate(selectedDate);
                }
                else
                {
                    // Si no está en la lista, la agregamos y cambiamos el color de fondo
                    selectedDates.Add(selectedDate);
                    monthCalendar1.AddBoldedDate(selectedDate);
                }
            }

            // Actualiza el MonthCalendar para reflejar los cambios de color
            monthCalendar1.UpdateBoldedDates();

            // Realiza una consulta para filtrar los datos en función de las fechas seleccionadas
            string dateFilter = string.Join("', '", selectedDates.Select(d => d.ToString("yyyy-MM-dd")));
            string sql = $"SELECT * FROM Ventas WHERE Fecha IN ('{dateFilter}')";

            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Actualiza el contenido del DataGridView con las filas resultantes
            dtgHistorialVentas.DataSource = tabla;
            CalcularTotal();
        }

        private void chkPorDia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPorDia.Checked)
            {
                chkPorMes.Checked = false;
                selectByMonth = false;
            }
        }

        private void chkPorMes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPorMes.Checked)
            {
                chkPorDia.Checked = false;
                selectByMonth = true;
            }
        }

        private void linkResetear_Click(object sender, EventArgs e)
        {
            // Restablece las fechas seleccionadas y el color del MonthCalendar
            selectedDates.Clear();
            monthCalendar1.RemoveAllBoldedDates();
            monthCalendar1.UpdateBoldedDates();

            // Realiza una consulta para filtrar los datos en función de las fechas seleccionadas
            string sql = "SELECT * FROM Ventas";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Actualiza el contenido del DataGridView con todas las filas
            dtgHistorialVentas.DataSource = tabla;
            CalcularTotal();
        }
        #endregion

        #region METODO CALCULAR TOTAL
        public void CalcularTotal() 
        {
            decimal montoTotal = 0;

            foreach (DataGridViewRow row in dtgHistorialVentas.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal monto = Convert.ToDecimal(row.Cells["Monto"].Value);
                    montoTotal += monto;
                }
            }
            //para ajustar el tamaño de la caja de texto segun el monto
            txtMontoTotal.Text = montoTotal.ToString("C");
            int nuevoAncho = TextRenderer.MeasureText(txtMontoTotal.Text, txtMontoTotal.Font).Width + 10;
            int nuevoAlto = TextRenderer.MeasureText(txtMontoTotal.Text, txtMontoTotal.Font).Height;
            txtMontoTotal.Size = new Size(nuevoAncho, nuevoAlto);
        }
        #endregion
    }
}
