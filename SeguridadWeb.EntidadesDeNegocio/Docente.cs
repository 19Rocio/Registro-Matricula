using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeguridadWeb.EntidadesDeNegocio
{

    public class Docente
    {
        public Docente(){}

        [Key]
        [Column("idDocente")]
        [Display(Name ="ID")]
        public int IdDocente { get; set; }
        [Column("nombre")]        
        public string Nombre { get; set; }
        [Column("apellido")]
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        [Column("asignatura")]
        public string Asignatura { get; set; }

        [ForeignKey("Usuario")]
        [Display(Name ="Usuario")]
        [Column("idUser")]
        public int? IdUser { get; set; }
        public Usuario Usuario { get; set; }
        

       
        [ForeignKey("Grado")]
        [Display(Name = "Grado")]
        [Column("idGrado")]
        public int IdGrado { set; get; }
        public Grado Grado { set; get; }


        [ForeignKey("Seccion")]
        [Display(Name = "Seccion")]
        [Column("idSeccion")]
        public int IdSeccion { set; get; }
        public Seccion Seccion { get; set; }
       

        [NotMapped]
        public int Top_Aux { set; get; }
    }
}
