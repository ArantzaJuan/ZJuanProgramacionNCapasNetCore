using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pais
    {
        [DisplayName("Pais:")]
        public int IdPais { get; set; }
        [Required]
        public string Nombre { get; set; }
        public List<object> Paises { get; set; }
    }
}
