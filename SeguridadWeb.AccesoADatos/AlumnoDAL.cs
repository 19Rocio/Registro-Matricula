using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------
using Microsoft.EntityFrameworkCore;
using SeguridadWeb.EntidadesDeNegocio;

namespace SeguridadWeb.AccesoADatos
{
    public class AlumnoDAL
    {
        public static async Task<int> CrearAsync(Alumno alumno)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(alumno);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Alumno pAlumno)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var alumno = await bdContexto.Alumno.FirstOrDefaultAsync(s => s.IdAlumno == pAlumno.IdAlumno);
                alumno.Nombre = pAlumno.Nombre;
                alumno.Apellido = pAlumno.Apellido;
                alumno.Nie = pAlumno.Nie;
                alumno.Dui = pAlumno.Dui;
                alumno.FechaNac = pAlumno.FechaNac;
                alumno.NPartida = pAlumno.NPartida;
                alumno.NFolio = pAlumno.NFolio;
                alumno.NTomo = pAlumno.NTomo;
                alumno.Nacionalidad = pAlumno.Nacionalidad;
                alumno.Genero = pAlumno.Genero;
                alumno.Telefono = pAlumno.Telefono;
                alumno.Correo = pAlumno.Correo;
                alumno.Direccion = pAlumno.Direccion;
                alumno.Departamento = pAlumno.Departamento;
                alumno.Canton = pAlumno.Canton;
                alumno.Caserio = pAlumno.Caserio;
                alumno.Municipio = pAlumno.Municipio;
                alumno.ZonaResidencia = pAlumno.ZonaResidencia;
                alumno.Estadocivil = pAlumno.Estadocivil;
                alumno.ConvivenciaFamiliar = pAlumno.ConvivenciaFamiliar;
                alumno.NombreEncargado = pAlumno.NombreEncargado;
                alumno.ApellidoEncargado = pAlumno.ApellidoEncargado;
                alumno.DuiEncargado = pAlumno.DuiEncargado;
                alumno.TelefonoEncargado = pAlumno.TelefonoEncargado;
                bdContexto.Update(alumno);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }
        public static async Task<int> Eliminar(Alumno pAlumno)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {               
                var Alumno = await bdContexto.Alumno.FirstOrDefaultAsync(s => s.IdAlumno == pAlumno.IdAlumno);
                bdContexto.Alumno.Remove(Alumno);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------------
        internal static IQueryable<Alumno> QuerySelect(IQueryable<Alumno> pQuery, Alumno pAlumno)
        {
            if (pAlumno.IdAlumno > 0)
                pQuery = pQuery.Where(s => s.IdAlumno == pAlumno.IdAlumno);
            if (pAlumno.IdUser > 0)
                pQuery = pQuery.Where(s => s.IdUser == pAlumno.IdUser);

            if (!string.IsNullOrWhiteSpace(pAlumno.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pAlumno.Nombre));
            if (!string.IsNullOrWhiteSpace(pAlumno.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pAlumno.Apellido));
            if (pAlumno.Nie > 0)
                pQuery = pQuery.Where(s => s.Nie == pAlumno.Nie);

            pQuery = pQuery.OrderByDescending(s => s.IdAlumno).AsQueryable();
            if (pAlumno.Top_Aux > 0)
                pQuery = pQuery.Take(pAlumno.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<bool> ExisteNie(Alumno pAlumno, BDContexto bdContext, Matricula matricula)
        {
            bool result = false;
            var nieExiste = await bdContext.Alumno.FirstOrDefaultAsync(s => s.Nie == pAlumno.Nie && matricula.IdAlumno == pAlumno.IdAlumno);
            if (nieExiste != null)
                result = true;
            return result;
        }
        public static async Task<List<Alumno>> BuscarPorNie(Alumno pAlumno,Matricula matricula)
        {
            var alumnos = new List<Alumno>();
            using (var bdContexto = new BDContexto())
            {
                bool existeNie = await ExisteNie(pAlumno, bdContexto, matricula);
                    if(existeNie == false)
                {
                    var select = bdContexto.Alumno.AsQueryable();
                    select = QuerySelect(select, pAlumno);
                    alumnos = await select.ToListAsync();
                }
                return alumnos;
            }
           
        }
        public static async Task<Alumno> ObtenerPorIdAsync(Alumno pAlumn)
        {
            var alumn = new Alumno();
            using (var bdContexto = new BDContexto())
            {
                alumn = await bdContexto.Alumno.FirstOrDefaultAsync(s => s.IdAlumno == pAlumn.IdAlumno);
            }
            return alumn;
        }

        public static async Task<List<Alumno>> BuscarAsync(Alumno pAlumno)
        {
            var alumnos = new List<Alumno>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Alumno.AsQueryable();
                select = QuerySelect(select, pAlumno);
                alumnos = await select.ToListAsync();
            }
            return alumnos;
        }
        public static async Task<List<Alumno>> BuscarIncluirUserAsync(Alumno pAlumno)
        {
            var alumnos = new List<Alumno>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Alumno.AsQueryable();
                select = QuerySelect(select, pAlumno).Include(s => s.Usuario).AsQueryable();
                alumnos = await select.ToListAsync();
            }
            return alumnos;
        }
        public static async Task<List<Alumno>> ObtenerTodosAsync()
        {
            var alumnos = new List<Alumno>();
            using (var bdContexto = new BDContexto())
            {
                alumnos = await bdContexto.Alumno.ToListAsync();
            }
            return alumnos;
        }
        public static  List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();
            using (var bdContexto = new BDContexto())
            {
                alumnos =  bdContexto.Alumno.ToList();                
            }
            return alumnos;
        }
    }
}