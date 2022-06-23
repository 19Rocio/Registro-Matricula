using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.LogicaDeNegocio
{
   public class AlumnoBL
    {
        public async Task<int> CrearAync(Alumno pAlumno)
        {
            return await AlumnoDAL.CrearAsync(pAlumno);
        }
        public async Task<int> ModificarAsync(Alumno pAlumno)
        {
            return await AlumnoDAL.ModificarAsync(pAlumno);
        }
        public async Task<int> EliminarAsync(Alumno pAlumno)
        {
            return await AlumnoDAL.Eliminar(pAlumno);
        }
        public async Task<List<Alumno>> ObtenerAlumnoAsync()
        {
            return await AlumnoDAL.ObtenerTodosAsync();
        }
        public async Task<Alumno> ObtenerPorIdAsync(Alumno pAlumn)
        {
            return await AlumnoDAL.ObtenerPorIdAsync(pAlumn);
        }
        public async Task<List<Alumno>> BuscarAsync(Alumno pAlumno)
        {
            return await AlumnoDAL.BuscarAsync(pAlumno);
        }
        public async Task<List<Alumno>> BuscarIncluirUserAsync(Alumno pAlumno)
        {
            return await AlumnoDAL.BuscarIncluirUserAsync(pAlumno);
        }
    }
}
