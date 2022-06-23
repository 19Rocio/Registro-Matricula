using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public partial class Matricula
    {
        public Matricula()
        {
           
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Alumno")]
        [Column("idAlumno")]
        [Display(Name = "Alumno")]
        public int IdAlumno { get; set; }
        [ForeignKey("Docente")]
        [Column("idDocente")]
        [Display(Name = "Docente")]
        public int IdDocente { get; set; }
        [ForeignKey("Grado")]
        [Column("idGrado")]
        [Display(Name = "Grado")]
        public int IdGrado { get; set; }
        
        [ForeignKey("Seccion")]
        [Column("idSeccion")]
        [Display(Name = "Seccion")]
        public int IdSeccion { get; set; }
        [Display(Name = "Año Lectivo")]
        public DateTime AnioLectivo { get; set; }
        [Display(Name = "Año de Ingreso")]
        public DateTime AnioIngreso { get; set; }
        [Display(Name = "Año de Egreso")]
        public DateTime? AnioEgreso { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public Alumno Alumno { get; set; }
        public Docente Docente { get; set; }
       
        public Grado Grado { get; set; }
        public Seccion Seccion { get; set; }
    }
}
