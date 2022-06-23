using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public class Alumno 
    {
       
        [Key]        
        [Column("idAlumno")]
        [Display(Name="Id")]
        public int IdAlumno { get; set; }
        [Column("nombre")]
        public string Nombre { get; set;}
        [Column("apellido")]
        public string Apellido { get; set; }
        public int Nie { get; set; }
        public string Dui { get; set; }
        [Column("fechaNac")]
        [Display(Name ="Fecha de Nacimiento")]
        public DateTime FechaNac { get; set; }              
        public int NPartida { get; set; }
        public int? NFolio { get; set; }
        public int? NLibro { get; set; }
        public int? NTomo { get; set; }
        public string Nacionalidad { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Canton { get; set; }
        public string Caserio { get; set; }
        public string Municipio { get; set; }
        public string ZonaResidencia { get; set; }
        [Display(Name ="Estado Civíl")]
        public string Estadocivil { get; set; }
        public string ConvivenciaFamiliar { get; set; }
        [Display(Name ="Nombres de encargado")]
        public string NombreEncargado { get; set; }
        [Display(Name ="Apellidos de encargado")]
        public string ApellidoEncargado { get; set; }

        [Display(Name ="Documento de identidad de encargado")]
        public string DuiEncargado { get; set; }

        [Display(Name ="Telefono")]
        public string TelefonoEncargado { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name ="Usuario")]
        public int? IdUser { get; set; }
        public Usuario Usuario { get; set; }
       
        [NotMapped]
        public int Top_Aux { get; set; }
        
     
    }
}
