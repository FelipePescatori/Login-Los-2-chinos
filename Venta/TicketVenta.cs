using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Los_2_chinos.Venta
{
    public class TicketVenta
    {
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<CarritoItem> ProductosVendidos { get; set; }
        public decimal MontoTotal { get; set; }
        public int UsuarioID { get; set; }

        public TicketVenta(string numeroVenta, DateTime fechaVenta, List<CarritoItem> productosVendidos, decimal montoTotal, int usuarioID)
        {
            NumeroVenta = numeroVenta;
            FechaVenta = fechaVenta;
            ProductosVendidos = productosVendidos;
            MontoTotal = montoTotal;
            UsuarioID = usuarioID;
        }
    }
}
