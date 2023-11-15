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
using System.Runtime.InteropServices;

using UserEsquema;
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;
namespace Login_Los_2_chinos
{
    public partial class FromAgregar : Form
    {
        public FromAgregar()
        {
            InitializeComponent();
            cam = new VideoCaptureDevice();
            read = new BarcodeReader();
            EncenderCama();
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region AGREGAR PRODUCTOS
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string consultaExistencia = "SELECT COUNT(*) FROM Articulos WHERE UPPER(Detalle) = UPPER(@Detalle) AND UPPER(Presentacion) = UPPER(@Presentacion)";
                SqlCommand comandoExistencia = new SqlCommand(consultaExistencia, conn);
                comandoExistencia.Parameters.AddWithValue("@Detalle", txtDetalle.Text);
                comandoExistencia.Parameters.AddWithValue("@Presentacion", txtPresentacion.Text);

                int cantidadExistente = (int)comandoExistencia.ExecuteScalar();
                if (cantidadExistente > 0)
                {
                    VariablesGlobales.MessageBox_Show("ARTICULO", "El artículo '" + txtDetalle.Text + "' ya existe.", false);
                }
                else
                {
                    string consulta = "INSERT INTO Articulos VALUES (@CodidoBarra, @Detalle, @Presentacion, @Venta, @Compra, @Stock)";
                    SqlCommand comandoAgregar = new SqlCommand(consulta, conn);
                    comandoAgregar.Parameters.AddWithValue("@CodidoBarra", txtCodigoBarra.Text);
                    comandoAgregar.Parameters.AddWithValue("@Detalle", txtDetalle.Text);
                    comandoAgregar.Parameters.AddWithValue("@Presentacion", txtPresentacion.Text);
                    comandoAgregar.Parameters.AddWithValue("@Venta", Convert.ToDecimal(txtVenta.Text));
                    comandoAgregar.Parameters.AddWithValue("@Compra", Convert.ToDecimal(txtCompra.Text));
                    comandoAgregar.Parameters.AddWithValue("@Stock", txtStock.Text);

                    VariablesGlobales variablesGlobales = new VariablesGlobales();
                    bool resultado = variablesGlobales.ValidarCampos(txtCodigoBarra, txtDetalle, txtPresentacion, txtStock);
                    if (resultado)
                    {
                        VariablesGlobales.MessageBox_Show("ARTICULO", "¿Estás seguro de agregar el artículo: '" + txtDetalle.Text + "'?", true);
                        if (VariablesGlobales.ResultadoDialogo == "YES")
                        {
                            comandoAgregar.ExecuteNonQuery();
                            VariablesGlobales.MessageBox_Show("ARTICULO", "El artículo: '" + txtDetalle.Text + "' fue agregado", false);
                            LIMPIAR();
                        }
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("ARTICULO", "El artículo no fue agregado", false);
                    }
                }
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text) && string.IsNullOrEmpty(txtDetalle.Text) && string.IsNullOrEmpty(txtPresentacion.Text) && string.IsNullOrEmpty(txtStock.Text) && (string.IsNullOrWhiteSpace(txtVenta.Text) && (string.IsNullOrWhiteSpace(txtCompra.Text)))) VariablesGlobales.MessageBox_Show("Error de validación", "Estan todos los campos vacíos.", false);
                else if (string.IsNullOrWhiteSpace(txtVenta.Text)) VariablesGlobales.MessageBox_Show("Error de validación", "El campos VENTAS no puede estar vacío.", false);
                else if (string.IsNullOrWhiteSpace(txtVenta.Text)) VariablesGlobales.MessageBox_Show("Error de validación", "El campos COMPRA no puede estar vacío.", false);
                else if (!decimal.TryParse(txtVenta.Text, out _)) VariablesGlobales.MessageBox_Show("Error de validación", "Ingrese un valor numérico válido en VENTAS", false);
                else if (!decimal.TryParse(txtCompra.Text, out _)) VariablesGlobales.MessageBox_Show("Error de validación", "Ingrese un valor numérico válido en COMPRAS", false);
            }
            finally
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgAgregar.DataSource = tabla;
                conn.Close();
            }
        }
        #endregion

        #region MODIFICAR PRODUCTOS
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (decimal.TryParse(txtVenta.Text, out decimal precioVenta) &&
                    decimal.TryParse(txtCompra.Text, out decimal precioCompra))
                {
                    SqlCommand comando = new SqlCommand("UPDATE Articulos SET CodigoBarras = @codigoBarras, detalle = @detalle, presentacion = @presentacion, PrecioVenta = @PrecioVenta, PrecioCompra = @PrecioCompra, Stock = @Stock WHERE Articuloid = @articuloid", conn);
                    comando.Parameters.AddWithValue("@codigoBarras", txtCodigoBarra.Text);
                    comando.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                    comando.Parameters.AddWithValue("@presentacion", txtPresentacion.Text);
                    comando.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    comando.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    comando.Parameters.AddWithValue("@Stock", txtStock.Text);
                    comando.Parameters.AddWithValue("@articuloid", txtArticuloid.Text);

                    VariablesGlobales variablesGlobales = new VariablesGlobales();
                    bool resultado = variablesGlobales.ValidarCampos(txtCodigoBarra, txtDetalle, txtPresentacion, txtStock);

                    if (resultado)
                    {
                        VariablesGlobales.MessageBox_Show("ARTICULO", "ESTAS SEGURO DE MODIFICAR EL ARTICULO:  " + txtDetalle.Text + "", true);
                        if (VariablesGlobales.ResultadoDialogo == "YES")
                        {
                            comando.ExecuteNonQuery();
                            VariablesGlobales.MessageBox_Show("ARTICULO", "EL ARTICULO: " + txtDetalle.Text + " FUE MODIFICADO", false);
                            LIMPIAR();
                        }
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("   PRODUCTO", "EL PRODUTO NO FUE ELIMINAR", false);
                    }
                }
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text) && string.IsNullOrEmpty(txtDetalle.Text) && string.IsNullOrEmpty(txtPresentacion.Text) && string.IsNullOrEmpty(txtStock.Text) && (string.IsNullOrWhiteSpace(txtVenta.Text) && (string.IsNullOrWhiteSpace(txtCompra.Text)))) VariablesGlobales.MessageBox_Show("Error de validación", "Estan todos los campos vacíos.", false);
                else if (string.IsNullOrWhiteSpace(txtVenta.Text)) VariablesGlobales.MessageBox_Show("Error de validación", "El campos VENTAS no puede estar vacío.", false);
                else if (string.IsNullOrWhiteSpace(txtVenta.Text)) VariablesGlobales.MessageBox_Show("Error de validación", "El campos COMPRA no puede estar vacío.", false);
                else if (!decimal.TryParse(txtVenta.Text, out _)) VariablesGlobales.MessageBox_Show("Error de validación", "Ingrese un valor numérico válido en VENTAS", false);
                else if (!decimal.TryParse(txtCompra.Text, out _)) VariablesGlobales.MessageBox_Show("Error de validación", "Ingrese un valor numérico válido en COMPRAS", false);
            }
            finally
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgAgregar.DataSource = tabla;
                conn.Close();
            }
        }
        #endregion

        #region ELIMINAR PRODUCTOS
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (txtNombre.Text == "" && txtArticuloidBuscar.Text == "")
                {
                    VariablesGlobales.MessageBox_Show("ARTICULOS", "DEBES DE INGRESAR UN DETALLE O ID DE UN ARTICULO", false);
                }
                else if (txtNombre.Text == "")
                {
                    string detalleABuscar = txtArticuloidBuscar.Text;
                    string consultaEliminar = "DELETE FROM Articulos WHERE Articuloid = @Articuloid";
                    SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                    comando.Parameters.AddWithValue("@Articuloid", detalleABuscar);

                    VariablesGlobales.MessageBox_Show("ARTICULOS", "¿ESTAS SEGURO DE ELIMINAR EL PRODUCTO?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO NO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                }
                else if (txtArticuloidBuscar.Text == "")
                {
                    string detalleABuscar = txtNombre.Text;
                    string consultaEliminar = "DELETE TOP(1) FROM Articulos WHERE Detalle = @Detalle";
                    SqlCommand comando = new SqlCommand(consultaEliminar, conn);
                    comando.Parameters.AddWithValue("@Detalle", detalleABuscar);

                    VariablesGlobales.MessageBox_Show("ARTICULOS", "¿ESTAS SEGURO DE ELIMINAR EL PRODUCTO?", true);
                    if (VariablesGlobales.ResultadoDialogo == "YES")
                    {
                        comando.ExecuteNonQuery();
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                    if (VariablesGlobales.ResultadoDialogo == "NO")
                    {
                        VariablesGlobales.MessageBox_Show("PRODUCTO", "EL PRODUCTO NO FUE ELIMINADO", false);
                        txtNombre.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                VariablesGlobales.MessageBox_Show("ERROR", "ERROR: " + ex.Message, false);
            }
            finally
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgAgregar.DataSource = tabla;
                conn.Close();
            }
        }
        #endregion

        #region EVENTOS
        public void LIMPIAR()
        {
            txtArticuloid.Text = string.Empty;
            txtCodigoBarra.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtVenta.Text = string.Empty;
            txtCompra.Text = string.Empty;
            txtStock.Text = string.Empty;
        }
        private void FromAgregar_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgAgregar.DataSource = tabla;
            txtCodigoBarra.Focus();

        }
        private void dtgAgregar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCodigoBarra.Text = dtgAgregar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDetalle.Text = dtgAgregar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPresentacion.Text = dtgAgregar.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtVenta.Text = dtgAgregar.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCompra.Text = dtgAgregar.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtStock.Text = dtgAgregar.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtArticuloid.Text = dtgAgregar.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch (Exception)
            {
            }

        }
        public Panel Panell
        {
            get { return PanelMover; }
        }
        #endregion

        #region MANEJAR FOCUS TXTBOX
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
                txtStock.Focus();
                e.Handled = true;
            }
        }
        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAgregar.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region BUSCAR EN DATA GRID
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNombre.Text;
            (dtgAgregar.DataSource as DataTable).DefaultView.RowFilter = $"Detalle LIKE '%{filtro}%' OR Detalle LIKE '%{filtro}%'";
        }
        private void txtArticuloidd_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtArticuloidBuscar.Text;
            if (int.TryParse(filtro, out int numeroFiltro))
            {
                (dtgAgregar.DataSource as DataTable).DefaultView.RowFilter = $"Articuloid = {numeroFiltro}";
            }
            else
            {
                // Si el valor ingresado no es un número válido, elimina el filtro
                (dtgAgregar.DataSource as DataTable).DefaultView.RowFilter = "";
            }
        }
        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtArticuloidBuscar.Text = string.Empty;
        }
        private void txtArticuloid_Enter(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
        }

        #endregion

        #region CAMARA PARA COMPLETAR EL TEXBOX

        private VideoCaptureDevice cam;
        private BarcodeReader read;
        public void EncenderCama()
        {
            try
            {
                if (cam != null && cam.IsRunning)
                {
                    ApagarCam();
                }

                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    cam = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    cam.NewFrame += new NewFrameEventHandler(Camaraa_NewFrame);
                    cam.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        public void ApagarCam()
        {
            if (cam != null && cam.IsRunning)
            {
                cam.SignalToStop();
                cam.WaitForStop();
                cam.NewFrame -= new NewFrameEventHandler(Camaraa_NewFrame);
            }
        }

        private void Camaraa_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Result resultado = read.Decode((Bitmap)eventArgs.Frame.Clone());
                if (resultado != null)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtCodigoBarra.Text = resultado.Text;
                        txtDetalle.Focus();
                    });
                }
            }
            catch (Exception )
            {
                
            }
        }

        private void FromAgregar_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApagarCam();
        }
        #endregion


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mensaje_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
