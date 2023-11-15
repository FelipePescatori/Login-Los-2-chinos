using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
//CAMARA
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
//TICKET
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Login_Los_2_chinos.Venta
{
    public partial class FormVenta : Form
    {
        public FormVenta(string UsuarioId)
        {
            InitializeComponent();
            Camara = new VideoCaptureDevice();
            Reader = new BarcodeReader();
            TimerCodigoDeBarra.Interval = 2500;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            EncenderCamara();
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;
            lblUsuarioId.Text = UsuarioId;
        }

        SqlConnection conn = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True");

        #region DATAGRID 
        private void dtgVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgVentas.SelectedCells.Count > 0)
                {
                    txtBuscarCodigoBarra.Text = dtgVentas.SelectedCells[0].Value?.ToString() ?? string.Empty;
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region CALCULAR CARRITO TOTAL Y VUELTO

        List<CarritoItem> carrito = new List<CarritoItem>();
        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dtgCarrito.Rows)
            {
                // Verifica si la fila es una fila de datos (no una fila de encabezado)
                if (!row.IsNewRow)
                {
                    if (Convert.ToInt32(row.Cells["CantidadEnGramos"].Value) == 0)
                    {
                        decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        total += precio * cantidad;
                    }
                    else
                    {
                        decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                        int CantidadEnGramos = Convert.ToInt32(row.Cells["CantidadEnGramos"].Value);
                        total += precio * CantidadEnGramos / 1000;
                    }
                }
            }

            return total;
        }
        private void GenerarTicketVentaPDF(TicketVenta ticket, string rutaArchivo)
        {
            float anchoPaginaEnPuntos = 226.772f;
            float altoPaginaEnPuntos = 800f;
            float margenIzquierdoEnPuntos = Element.ALIGN_MIDDLE;

            Document doc = new Document(new iTextSharp.text.Rectangle(anchoPaginaEnPuntos, altoPaginaEnPuntos));
            doc.SetMargins(margenIzquierdoEnPuntos, 10, 10, 10);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            string rutaImagen = @"D:\VIOLETA\Login Los 2 chinos\Venta\Tickets\Logo 2 Chinos.jpg";
            if (File.Exists(rutaImagen))
            {
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                imagen.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
                imagen.ScaleToFit(200f, 100f);
                doc.Add(imagen);
            }

            // Establecer el estilo de fuente para los mensajes
            BaseFont fuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font estiloTitulo = new iTextSharp.text.Font(fuente, 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font estiloTipoDeProducto = new iTextSharp.text.Font(fuente, 6, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font estiloMensaje = new iTextSharp.text.Font(fuente, 7);
            // Agregar el contenido del ticket
            Paragraph titulo = new Paragraph("LOS 2 CHINOS", estiloTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph("CUIT NRM: 30-52999439-3", estiloMensaje));
            doc.Add(new Paragraph("DIRECCION: Asunción del Paraguay 429", estiloMensaje));
            doc.Add(new Paragraph("**************************** Ticket de Venta *****************************", estiloMensaje));
            doc.Add(new Paragraph($"Número de Venta: {ticket.NumeroVenta}", estiloMensaje));
            doc.Add(new Paragraph($"Fecha de Venta: {ticket.FechaVenta.ToString("dd/MM/yyyy")}", estiloMensaje));
            doc.Add(new Paragraph($"Hora de Venta: {ticket.FechaVenta.ToString("HH:mm:ss")}", estiloMensaje));
            doc.Add(new Paragraph($"Cajero/a:{lblNombre.Text}", estiloMensaje));
            bool mostrarXGramos = false;
            foreach (CarritoItem item in ticket.ProductosVendidos)
            {
                if (item.CantidadEnGramos > 0) { mostrarXGramos = true; break; }
            }
            if (mostrarXGramos)
            {
                doc.Add(new Paragraph("------------------------------------------------------------------------------------------", estiloMensaje));
                Paragraph xGramos = new Paragraph("X GRAMOS", estiloTipoDeProducto);
                xGramos.Alignment = Element.ALIGN_CENTER;
                doc.Add(xGramos);
            }
            foreach (CarritoItem item in ticket.ProductosVendidos)
            {

                if (item.CantidadEnGramos > 0)
                {
                    doc.Add(new Paragraph("------------------------------------------------------------------------------------------", estiloMensaje));
                    Paragraph Detalle = new Paragraph($"{item.Detalle}", estiloMensaje);
                    Detalle.SpacingAfter = 2f;
                    doc.Add(Detalle);

                    decimal totalGramos = item.CantidadEnGramos * item.PrecioVenta / 1000;
                    string textoIzquierda = $"{item.CantidadEnGramos}Grm X {item.PrecioVenta}";
                    string textoDerecha = $"{totalGramos:C}";

                    PdfPTable tabla = new PdfPTable(2);
                    tabla.WidthPercentage = 102;
                    tabla.DefaultCell.Border = 0;

                    PdfPCell celdaIzquierda = new PdfPCell(new Phrase(textoIzquierda, estiloMensaje)); ;
                    celdaIzquierda.HorizontalAlignment = Element.ALIGN_LEFT;
                    celdaIzquierda.Border = 0;
                    PdfPCell celdaDerecha = new PdfPCell(new Phrase(textoDerecha, estiloMensaje));
                    celdaDerecha.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdaDerecha.Border = 0;
                    float desplazamientoX = 6;
                    celdaDerecha.PaddingRight = desplazamientoX;

                    tabla.AddCell(celdaIzquierda);
                    tabla.AddCell(celdaDerecha);

                    doc.Add(tabla);
                    doc.Add(new Paragraph($"{item.CodigoBarra}", estiloMensaje));
                }

            }

            bool mostrarXUnidad = false;
            foreach (CarritoItem item in ticket.ProductosVendidos)
            {
                if (item.Cantidad > 0) { mostrarXUnidad = true; break; }
            }
            if (mostrarXUnidad)
            {
                doc.Add(new Paragraph("------------------------------------------------------------------------------------------", estiloMensaje));
                Paragraph XUnidad = new Paragraph("X UNIDAD", estiloTipoDeProducto);
                XUnidad.Alignment = Element.ALIGN_CENTER;
                doc.Add(XUnidad);
            }
            foreach (CarritoItem item in ticket.ProductosVendidos)
            {
                if (item.Cantidad > 0)
                {
                    doc.Add(new Paragraph("------------------------------------------------------------------------------------------", estiloMensaje));
                    Paragraph Detalle = new Paragraph($"{item.Detalle}", estiloMensaje);
                    Detalle.SpacingAfter = 2f;
                    doc.Add(Detalle);
                    decimal totalUnidad = item.Cantidad * item.PrecioVenta;
                    string textoIzquierda = $"{item.Cantidad} X {item.PrecioVenta}";
                    string textoDerecha = $"{totalUnidad:C}";

                    PdfPTable tabla = new PdfPTable(2);
                    tabla.WidthPercentage = 102;
                    tabla.DefaultCell.Border = 0;

                    PdfPCell celdaIzquierda = new PdfPCell(new Phrase(textoIzquierda, estiloMensaje)); ;
                    celdaIzquierda.HorizontalAlignment = Element.ALIGN_LEFT;
                    celdaIzquierda.Border = 0;
                    PdfPCell celdaDerecha = new PdfPCell(new Phrase(textoDerecha, estiloMensaje));
                    celdaDerecha.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdaDerecha.Border = 0;
                    float desplazamientoX = 6;
                    celdaDerecha.PaddingRight = desplazamientoX;

                    tabla.AddCell(celdaIzquierda);
                    tabla.AddCell(celdaDerecha);

                    doc.Add(tabla);
                    doc.Add(new Paragraph($"{item.CodigoBarra}", estiloMensaje));
                }
            }
            doc.Add(new Paragraph("------------------------------------------------------------------------------------------", estiloMensaje));
            doc.Add(new Paragraph(" ", estiloMensaje));
            doc.Add(new Paragraph("===================================================", estiloMensaje));
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.DefaultCell.Border = 0;

            PdfPCell celdaIzquierdaa = new PdfPCell(new Phrase("MONTO TOTAL:", estiloTitulo));
            celdaIzquierdaa.HorizontalAlignment = Element.ALIGN_LEFT;
            celdaIzquierdaa.Border = 0;

            PdfPCell celdaDerechaa = new PdfPCell(new Phrase($"{ticket.MontoTotal}", estiloTitulo));
            celdaDerechaa.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaDerechaa.Border = 0;

            table.AddCell(celdaIzquierdaa);
            table.AddCell(celdaDerechaa);
            doc.Add(table);
            doc.Add(new Paragraph("===================================================", estiloMensaje));
            doc.Add(new Paragraph(" ", estiloMensaje));
            Paragraph GRACIAS = new Paragraph("¡GRACIAS POR SU COMPRA!\n", estiloMensaje);
            GRACIAS.Alignment = Element.ALIGN_CENTER;
            doc.Add(GRACIAS);
            doc.Close();
            writer.Close();
        }
        public void ActualizarTotal_Y_Vuetlo()
        {
            decimal total = CalcularTotal();
            txtTotal.Text = total.ToString("C");
            if (decimal.TryParse(txtMontoPago.Text, out decimal montoPago))
            {
                decimal cambio = montoPago - total; // Calcula el cambio
                if (cambio >= 0)
                {
                    txtCambio.Text = cambio.ToString("C");
                }
                else
                {
                    txtCambio.Text = (cambio).ToString("C");
                }
            }
        }
        private void FormVenta_Activated(object sender, EventArgs e)
        {
            ActualizarTotal_Y_Vuetlo();
        }
        private void ActualizarColorControles(decimal montoPago, decimal total)
        {
            if (montoPago >= total)
            {
                Color verdePersonalizado = Color.FromArgb(26, 141, 26);
                txtMontoPago.ForeColor = txtCambio.ForeColor = verdePersonalizado;
            }
            else
            {
                Color rojoBrillante = Color.FromArgb(255, 26, 26);
                txtMontoPago.ForeColor = txtCambio.ForeColor = rojoBrillante;
            }
        }
        private void AjustarTamañoTextBox(TextBox textBox, int offsetX = 10, int offsetY = 0)
        {
            int nuevoAncho = TextRenderer.MeasureText(textBox.Text, textBox.Font).Width + offsetX;
            int nuevoAlto = TextRenderer.MeasureText(textBox.Text, textBox.Font).Height + offsetY;
            textBox.Size = new Size(nuevoAncho, nuevoAlto);
        }
        private void txtMontoPago_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoPago.Text, out decimal montoPago) &&
            decimal.TryParse(txtTotal.Text.Replace("$", ""), out decimal total))
            {
                ActualizarColorControles(montoPago, total);
            }
            AjustarTamañoTextBox(txtMontoPago, txtMontoPago.Text == "" ? 140 : 10, txtMontoPago.Text == "" ? 31 : 0);
            ActualizarTotal_Y_Vuetlo();
        }
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoPago.Text, out decimal montoPago) &&
            decimal.TryParse(txtTotal.Text.Replace("$", ""), out decimal total))
            {
                ActualizarColorControles(montoPago, total);
            }
            AjustarTamañoTextBox(txtTotal);
            ActualizarTotal_Y_Vuetlo();
        }
        private void txtCambio_TextChanged(object sender, EventArgs e)
        {
            int nuevoAncho = TextRenderer.MeasureText(txtCambio.Text, txtCambio.Font).Width + 10;
            int nuevoAlto = TextRenderer.MeasureText(txtCambio.Text, txtCambio.Font).Height;
            txtCambio.Size = new Size(nuevoAncho, nuevoAlto);
        }

        bool ventaFinalizada = false;
        bool ValidarMonto = false;
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnVender.PerformClick();// Simular un clic en el botón
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            ventaFinalizada = true;
            foreach (CarritoItem item in carrito)
            {
                // Resta la cantidad vendida del stock en la base de datos
                ActualizarStockEnBaseDeDatos(item.CodigoBarra, item.Cantidad, item.CantidadEnGramos);
            }
            if (txtMontoPago.Text == "")
            {
                VariablesGlobales.MessageBox_Show("MONTO", "INGRESE UN MONTO PORFAVOR", false);
            }
            if (ValidarMonto && ventaFinalizada)
            {
                string numeroVenta = GenerarNumeroDeVenta();
                string totalText = txtTotal.Text;
                totalText = totalText.Replace("$", "");
                if (decimal.TryParse(totalText, out decimal Monto))
                {
                    string userId = lblUsuarioId.Text;
                    RegistrarVentaEnTablaDeVentas(numeroVenta, Monto, Int32.Parse(userId));
                }

                TicketVenta ticket = new TicketVenta(numeroVenta, DateTime.Now, carrito, Monto, 1);
                string carpetaDestino = "D:\\VIOLETA\\Login Los 2 chinos\\Venta\\Tickets\\";
                if (!Directory.Exists(carpetaDestino))
                {
                    Directory.CreateDirectory(carpetaDestino);
                }
                string nombreArchivo = $"TicketVenta_{ticket.NumeroVenta}.pdf";
                string rutaArchivo = Path.Combine(carpetaDestino, nombreArchivo);
                GenerarTicketVentaPDF(ticket, rutaArchivo);

                carrito.Clear();
                dtgCarrito.Rows.Clear();
                VariablesGlobales.MessageBox_Show("VENDIDO", "COMPRO EFECTUADA", false);
                txtMontoPago.Text = "";
                txtCambio.Text = "";
                ValidarMonto = false;
            }
            else VariablesGlobales.MessageBox_Show("MONTO", "MONTO NO VALIDO" + txtCambio.Text.Replace("-", ""), false);

            ActualizarTotal_Y_Vuetlo();
            conn.Close();
        }
        private void ActualizarStockEnBaseDeDatos(string codigoBarra, int cantidadVendida, int cantidadEnGramoss)
        {
            if (ventaFinalizada)
            {
                try
                {
                    int Pago = int.Parse(txtMontoPago.Text);
                    string totalText = txtTotal.Text.Replace("$", "");
                    decimal Total = decimal.Parse(totalText);

                    if (Pago >= Total)
                    {
                        ValidarMonto = true;
                        string cambioText = txtCambio.Text.Replace("$", "");
                        if (decimal.TryParse(cambioText, out decimal cambio) && cambio >= 0)
                        {
                            conn.Open();
                            if (cantidadEnGramoss > 0)
                            {
                                //restamos el stock en la consulta 
                                string consulta = "UPDATE Articulos SET Stock = Stock - @CantidadEnGramos WHERE CodigoBarras = @CodigoBarra";
                                using (SqlCommand comando = new SqlCommand(consulta, conn))
                                {
                                    comando.Parameters.AddWithValue("@CantidadEnGramos", cantidadEnGramoss);
                                    comando.Parameters.AddWithValue("@CodigoBarra", codigoBarra);
                                    comando.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string consulta = "UPDATE Articulos SET Stock = Stock - @CantidadVendida WHERE CodigoBarras = @CodigoBarra";
                                using (SqlCommand comando = new SqlCommand(consulta, conn))
                                {
                                    comando.Parameters.AddWithValue("@CantidadVendida", cantidadVendida);
                                    comando.Parameters.AddWithValue("@CodigoBarra", codigoBarra);
                                    comando.ExecuteNonQuery();
                                }
                            }

                            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);
                            dtgVentas.DataSource = tabla;
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Monto no válido faltan " + txtCambio.Text.ToString().Replace("-", ""));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public string GenerarNumeroDeVenta()
        {
            // Recuperar los valores guardados en otro método o parte de tu aplicación
            int ultimoNumeroSerie = Properties.Settings.Default.UltimoNumeroSerie;
            int ultimoNumeroVenta = Properties.Settings.Default.UltimoNumeroVenta;
            if (ultimoNumeroVenta >= 99999)
            {
                ultimoNumeroSerie++;
                ultimoNumeroVenta = 0;
            }
            ultimoNumeroVenta++;
            // Guardar los valores actualizados en la configuración
            Properties.Settings.Default.UltimoNumeroSerie = ultimoNumeroSerie;
            Properties.Settings.Default.UltimoNumeroVenta = ultimoNumeroVenta;
            Properties.Settings.Default.Save();
            string numeroDeVenta = ultimoNumeroSerie.ToString("D4") + " - " + ultimoNumeroVenta.ToString("D5");
            return numeroDeVenta;
        }
        private void RegistrarVentaEnTablaDeVentas(string numeroVenta, decimal Monto, int usuarioID)
        {
            conn.Open();
            // Crea la consulta SQL INSERT para registrar la venta
            string insertQuery = "INSERT INTO Ventas (VentaId, Fecha, Monto, UsuarioID) VALUES (@VentaId, GETDATE(), @Monto, @UsuarioID)";
            using (SqlCommand command = new SqlCommand(insertQuery, conn))
            {
                // Define los parámetros de la consulta
                command.Parameters.AddWithValue("@VentaId", numeroVenta);
                command.Parameters.AddWithValue("@Monto", Monto);
                command.Parameters.AddWithValue("@UsuarioID", usuarioID);

                // Ejecuta la consulta
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
        #endregion

        #region AGREGAR A CARRITO Y ELIMINAR
        private void txtBuscarCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            string codigoBarra = txtBuscarCodigoBarra.Text;

            if (!string.IsNullOrWhiteSpace(codigoBarra))
            {
                try
                {
                    string consulta = "SELECT * FROM Articulos WHERE CodigoBarras = @CodigoBarras";
                    using (SqlConnection conexion = new SqlConnection("Server=FELIPE\\SQLEXPRESS;Database=Los 2 Chinos;Trusted_Connection=True"))
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        comando.Parameters.AddWithValue("@CodigoBarras", codigoBarra);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int stock = Convert.ToInt32(reader["Stock"]);
                                string presentacion = reader["Presentacion"].ToString();
                                if (presentacion == "1000")
                                {
                                    AgregarProductoGramos(codigoBarra, stock, reader);
                                }
                                else
                                {
                                    AgregarProductoUnidad(codigoBarra, stock, reader);
                                }
                            }
                            else
                            {
                                if (txtBuscarCodigoBarra.Text == string.Empty)
                                {
                                    txtBuscarCodigoBarra.Clear();
                                }
                                else
                                {
                                    VariablesGlobales.MessageBox_Show("CODIGO DE BARRA", "EL CODIGO DE BARRA: " + codigoBarra + " NO FUE ENCONTRADO", false);
                                    txtBuscarCodigoBarra.Clear();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBuscarCodigoBarra.Clear();
                }
            }
        }
        int Cont = 0;
        private void AgregarProductoGramos(string codigoBarra, int stock, SqlDataReader reader)
        {
            Cont++;
            if (Cont == 1)
            {
                // Si la presentación es 1000, muestra un cuadro de diálogo para que el usuario ingrese la cantidad en gramos
                FormCantidadEnGramos cantidadEnGramosForm = new FormCantidadEnGramos();
                if (cantidadEnGramosForm.ShowDialog() == DialogResult.OK)
                {
                    // El usuario ingresó la cantidad en gramos en el cuadro de diálogo
                    int cantidadEnGramos = cantidadEnGramosForm.CantidadEnGramos;

                    // Verifica si hay suficiente stock para agregar la cantidad en gramos
                    if (stock >= cantidadEnGramos)
                    {
                        string detalle = reader["Detalle"].ToString();
                        decimal precioVenta = Convert.ToDecimal(reader["PrecioVenta"]);

                        // Verifica si el producto ya está en el carrito
                        CarritoItem productoExistente = carrito.FirstOrDefault(item => item.CodigoBarra == codigoBarra);
                        if (productoExistente != null)
                        {
                            // Si el producto ya está en el carrito, aumenta la cantidad en gramos
                            if (productoExistente.CantidadEnGramos + cantidadEnGramos <= stock)
                            {
                                productoExistente.CantidadEnGramos += cantidadEnGramos;
                                // Actualiza la cantidad en la fila correspondiente del DataGridView
                                foreach (DataGridViewRow row in dtgCarrito.Rows)
                                {
                                    if (row.Cells["CodigoDeBarra"].Value.ToString() == codigoBarra)
                                    {
                                        row.Cells["CantidadEnGramos"].Value = productoExistente.CantidadEnGramos;
                                        break;
                                    }
                                }
                            }
                            else MessageBox.Show("No hay suficiente stock para este producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Si el producto no está en el carrito, agrégalo con la cantidad en gramos
                            CarritoItem itemPorGramo = new CarritoItem(codigoBarra, detalle, precioVenta, cantidadEnGramos, 0);
                            carrito.Add(itemPorGramo);
                            dtgCarrito.Rows.Add(codigoBarra, detalle, precioVenta, cantidadEnGramos, 0);

                            // Reduce el stock del producto en la base de datos
                            // ActualizarStockEnBaseDeDatos(codigoBarra,0, cantidadEnGramos);
                        }
                        ActualizarTotal_Y_Vuetlo();
                        txtBuscarCodigoBarra.Clear();
                    }
                    else MessageBox.Show("No hay suficiente stock para este producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Cont = 0;
            }
            else
            {

            }
        }

        private void AgregarProductoUnidad(string codigoBarra, int stock, SqlDataReader reader)
        {
            if (stock > 0)
            {
                string detalle = reader["Detalle"].ToString();
                decimal precioVenta = Convert.ToDecimal(reader["PrecioVenta"]);

                // Verifica si el producto ya está en el carrito
                CarritoItem productoExistente = carrito.FirstOrDefault(item => item.CodigoBarra == codigoBarra);
                int cantidadAAgregar = 1;

                if (int.TryParse(txtCantidad.Text, out int cantidadIngresada))
                {
                    cantidadAAgregar = cantidadIngresada;
                }

                if (productoExistente != null)
                {
                    if (productoExistente.Cantidad + cantidadAAgregar < stock)
                    {
                        // Aumenta la cantidad en el carrito
                        productoExistente.Cantidad += cantidadAAgregar;

                        // Actualiza la cantidad en la fila correspondiente del DataGridView
                        foreach (DataGridViewRow row in dtgCarrito.Rows)
                        {
                            if (row.Cells["CodigoDeBarra"].Value.ToString() == codigoBarra)
                            {
                                row.Cells["Cantidad"].Value = productoExistente.Cantidad;
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay suficiente stock para este producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Si el producto no está en el carrito, agrégalo
                    CarritoItem itemPorUnidad = new CarritoItem(codigoBarra, detalle, precioVenta, 0, cantidadAAgregar);
                    carrito.Add(itemPorUnidad);
                    dtgCarrito.Rows.Add(codigoBarra, detalle, precioVenta, 0, cantidadAAgregar);
                }
                ActualizarTotal_Y_Vuetlo();
                txtBuscarCodigoBarra.Clear();
                txtCantidad.Text = "1"; // Restablece el valor predeterminado
            }
            else
            {
                MessageBox.Show("No hay suficiente stock para este producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dtgCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dtgCarrito.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    DataGridViewCell cantidadCell = dtgCarrito.Rows[e.RowIndex].Cells["Cantidad"];
                    int cantidad = Convert.ToInt32(cantidadCell.Value);

                    DataGridViewRow row = dtgCarrito.Rows[e.RowIndex];
                    int cantidadEnGramoss = Convert.ToInt32(row.Cells["CantidadEnGramos"].Value);

                    if (cantidad > 1)
                    {
                        cantidadCell.Value = cantidad - 1;
                    }
                    else if (cantidad == 1 || cantidad == 0)
                    {
                        string codigoBarra = dtgCarrito.Rows[e.RowIndex].Cells["CodigoDeBarra"].Value.ToString();
                        CarritoItem itemAEliminar = carrito.FirstOrDefault(item => item.CodigoBarra == codigoBarra);

                        if (itemAEliminar != null)
                        {
                            carrito.Remove(itemAEliminar);
                        }

                        dtgCarrito.Rows.RemoveAt(e.RowIndex);
                    }

                    ActualizarTotal_Y_Vuetlo();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtgCarrito.Rows.Clear();
            carrito.Clear();
        }
        #endregion

        #region CAMARA PARA LEER CODIGO DEBARA
        private VideoCaptureDevice Camara;
        private BarcodeReader Reader;
        private void FormVenta_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dtgVentas.DataSource = tabla;
        }
        public void EncenderCamara()
        {
            try
            {
                if (Camara != null && Camara.IsRunning)
                {
                    ApagarCamara();
                }

                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    Camara = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    Camara.NewFrame += new NewFrameEventHandler(Camara_NewFrame);
                    Camara.Start();
                    TimerCodigoDeBarra.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        public void ApagarCamara()
        {
            if (Camara != null && Camara.IsRunning)
            {
                Camara.SignalToStop();
                Camara.WaitForStop();
                Camara.NewFrame -= new NewFrameEventHandler(Camara_NewFrame);
            }
            TimerCodigoDeBarra.Stop();
            TimerCodigoDeBarra.Enabled = false;
        }
        private void FormVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApagarCamara();
        }
        private void Camara_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (System.Drawing.Image)eventArgs.Frame.Clone();
        }
        private void TimerCodigoDeBarra_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    Result resultado = Reader.Decode((Bitmap)pictureBox1.Image);
                    if (resultado != null)
                    {
                        txtBuscarCodigoBarra.Text = resultado.Text;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void txtBuscarDetalle_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarDetalle.Text;
            (dtgVentas.DataSource as DataTable).DefaultView.RowFilter = $"Detalle LIKE '%{filtro}%'";
            dtgVentas.Refresh();
        }
    }
}
