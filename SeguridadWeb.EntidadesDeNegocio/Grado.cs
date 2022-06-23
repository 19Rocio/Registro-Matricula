using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public partial class Grado
    {
        public Grado()
        {
          
        }
        [Column("id")]
        public int Id { get; set; }
        [Column("grado")]
        [Display(Name="Grado")]
        public string Grado1 { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Matricula> Matriculas { get; set; }
    }
}
