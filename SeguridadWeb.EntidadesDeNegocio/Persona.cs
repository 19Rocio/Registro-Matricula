using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeguridadWeb.EntidadesDeNegocio
{
    public partial class Persona
    {
        [Column("id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        //public string FullName
        //{
        //    get
        //    {
        //        return Nombre + "," + Apellido;
        //    }
        //}
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNac { get; set; }
        public string Nacionalidad { get; set; }
        public string Genero { get; set; }
        public string Dui { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        [Column("estadoCivil")]
        [Display(Name ="Estado Civíl")]
        public string Estadocivil { get; set; }
        public Persona()
        {

        }
        public Persona( string nombre, string apellido,DateTime fechaNac, string nacionalidad, string genero, string dui, string correo, string telefono, string estadocivil)
        {
            
            Nombre = nombre;
            Apellido = apellido;
            FechaNac = fechaNac;
            Nacionalidad = nacionalidad;
            Genero = genero;
            Dui = dui;
            Correo = correo;
            Telefono = telefono;
            Estadocivil = estadocivil;
        }
    }
}


