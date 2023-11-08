using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Los_2_chinos
{
    public class Proveedores
    {
        public int ProveedorID { get; set; }
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Rubro { get; set; }
        public string Direccion { get; set; }

        public Proveedores(int proveedorID, string nombre, string cUIT, string email, string celular, string rubro, string direccion)
        {
            ProveedorID = proveedorID;
            Nombre = nombre;
            CUIT = cUIT;
            Email = email;
            Celular = celular;
            Rubro = rubro;
            Direccion = direccion;
        }
    }

}
