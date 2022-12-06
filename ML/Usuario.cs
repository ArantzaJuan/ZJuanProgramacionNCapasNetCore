using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        //[Required]
        public string Nombre { get; set; }
        //[Required]
        //[DisplayName("Apellido paterno:")]
        public string ApellidoPaterno { get; set; }
        //[DisplayName("Apellido materno:")]
        public string ApellidoMaterno { get; set; }
       // [Required]
        public string Sexo { get; set; }
      //  [Required]
        public string Telefono { get; set; }
      //  [Required]
        public string Email { get; set; }
       // [Required]
      //  [DisplayName("Fecha de nacimiento:")]
        public string FechaDeNacimiento { get; set; }
       // [Required]
        public string Password { get; set; }
        public string Celular { get; set; }
        public string CURP { get; set; }
       // [Required]
        public string UserName { get; set; }
        
        public ML.Rol Rol { get; set; }
        public string Imagen { get; set; }
        public bool Status { get; set; }

        public ML.Direccion Direccion { get; set; }
        public List<object> Usuarios { get; set; }


    }
}
