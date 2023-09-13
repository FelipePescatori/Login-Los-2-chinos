using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Los_2_chinos
{
    public class Articulos
    {
        public int ArticuloId { get; set; }
        public string Detalle { get; set; }
        public string Presentacion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }

        public Articulos(int articuloId, string detalle, string presentacion, decimal precioVenta, decimal precioCompra)
        {
            ArticuloId = articuloId;
            Detalle = detalle;
            Presentacion = presentacion;
            PrecioVenta = precioVenta;
            PrecioCompra = precioCompra;
        }
    }

}
