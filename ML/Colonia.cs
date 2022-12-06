﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        [DisplayName("Colonia:")]
        public int IdColonia { get; set; }
        [Required]
        public string Nombre { get; set; }
        public ML.Municipio Municipio { get; set; }
        public List<object> Colonias { get; set; }
    }
}
