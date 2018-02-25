using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsuarioWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}