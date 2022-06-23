using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public partial class Seccion
    {
        public Seccion()
        {
            
        }
        [Column("id")]
        public int Id { get; set; }
        [Column("seccion")]
        [Display(Name="Seccion")]
        public string Seccion1 { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Matricula> Matriculas { get; set; }
    }
}
