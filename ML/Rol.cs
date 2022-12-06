using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [DisplayName("Rol:")]
        public int IdRol { get; set; }
        [Required]
        
        public string NombreRol { get; set; }
        public List<object> Roles { get; set; }
    }
}
