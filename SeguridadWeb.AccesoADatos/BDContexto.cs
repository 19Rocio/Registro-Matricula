using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ********************************
using Microsoft.EntityFrameworkCore;
using SeguridadWeb.EntidadesDeNegocio;

namespace SeguridadWeb.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Docente> Docente { get; set; }        
        public DbSet<Encargado> Encargado { get; set; }
        public DbSet<Grado> Grado { get; set; }
        public DbSet<Seccion> Seccion { get; set; }
        public DbSet<Matricula> Matricula { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Conexion Local
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-OFGK424\SQLEXPRESS;Initial Catalog=RegistroBD;Integrated Security=True");
            //Conexion Somee
            //optionsBuilder.UseSqlServer(@"workstation id=RegistroBD.mssql.somee.com;packet size=4096;user id=RocioL19_SQLLogin_1;pwd=oof6kck3ic;data source=RegistroBD.mssql.somee.com;persist security info=False;initial catalog=RegistroBD");
            //Conexion Smartasp
         // optionsBuilder.UseSqlServer(@"Data Source=SQL5053.site4now.net;Initial Catalog=db_a82cd0_registrobd;User Id=db_a82cd0_registrobd_admin;Password=GateWay19");

        }
    }
}
