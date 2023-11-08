using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Los_2_chinos.Venta
{
    public class CarritoItem
    {
        public string CodigoBarra { get; set; }
        public string Detalle { get; set; }
        public decimal PrecioVenta { get; set; }
        public int CantidadEnGramos { get; set; }
        public int Cantidad { get; set; }



        public CarritoItem(string codigoBarra, string detalle, decimal precioVenta, int cantidadEnGramos, int cantidad)
        {
            CodigoBarra = codigoBarra;
            Detalle = detalle;
            PrecioVenta = precioVenta;
            CantidadEnGramos = cantidadEnGramos;
            Cantidad = cantidad;
        }
    }
}
