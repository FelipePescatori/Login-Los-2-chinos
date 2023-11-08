using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserEsquema{
    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Acceso { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public Usuarios(int usuarioId, string nombre, string acceso, string email, string celular, string usuario, string contrasena)
        {
            UsuarioId = usuarioId;
            Nombre = nombre;
            Acceso = acceso;
            Email = email;
            Celular = celular;
            Usuario = usuario;
            Contrasena = contrasena;
        }
    }
}
