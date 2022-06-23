using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public partial class Encargado
    {               
        [Key]
        [Column("idEncargado")]
        [Display(Name ="Id")]
        public int  IdEncargado { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("apellido")]
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
        public string Dui { get; set; }
        public string Correo { get; set; }        
        public string Telefono { get; set; }
        [Display(Name = "Estado Civíl")]
        public string Estadocivil { get; set; }
        public string Parentesco { get; set; }
        public string UltimoGrado { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

    }
}
