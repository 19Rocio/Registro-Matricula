using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-------------------------------------
using Microsoft.EntityFrameworkCore;
using SeguridadWeb.EntidadesDeNegocio;

namespace SeguridadWeb.AccesoADatos
{
   public class MatriculaDAL
    {
        public static async Task<int> CrearAsync(Matricula pMatricula)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pMatricula);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Matricula pMatricula)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Matricula = await bdContexto.Matricula.FirstOrDefaultAsync(s => s.Id == pMatricula.Id);
                Matricula.IdAlumno = pMatricula.IdAlumno;
                Matricula.IdDocente = pMatricula.IdDocente;
                Matricula.IdGrado = pMatricula.IdGrado;
                Matricula.IdSeccion = pMatricula.IdSeccion;
                bdContexto.Update(Matricula);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Matricula pMatricula)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Matricula = await bdContexto.Matricula.FirstOrDefaultAsync(s => s.Id == pMatricula.Id);
                bdContexto.Matricula.Remove(Matricula);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Matricula> ObtenerPorIdAsync(Matricula pMatricula)
        {
            var Matricula = new Matricula();
            using (var bdContexto = new BDContexto())
            {
                Matricula = await bdContexto.Matricula.FirstOrDefaultAsync(s => s.Id == pMatricula.Id);
            }
            return Matricula;
        }
        
        public static async Task<List<Matricula>> ObtenerTodosAsync()
        {
            var Matriculaes = new List<Matricula>();
            using (var bdContexto = new BDContexto())
            {
                Matriculaes = await bdContexto.Matricula.ToListAsync();
            }
            return Matriculaes;
        }
        internal static IQueryable<Matricula> QuerySelect(IQueryable<Matricula> pQuery, Matricula pMatricula)
        {
            if (pMatricula.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pMatricula.Id);
            if (pMatricula.IdAlumno > 0)
                pQuery = pQuery.Where(s => s.IdAlumno == pMatricula.IdAlumno);
            if (pMatricula.IdDocente > 0)
                pQuery = pQuery.Where(s => s.IdDocente == pMatricula.IdDocente);
            if (pMatricula.IdGrado > 0)
                pQuery = pQuery.Where(s => s.IdGrado == pMatricula.IdGrado);
            if (pMatricula.IdSeccion > 0)
                pQuery = pQuery.Where(s => s.IdSeccion == pMatricula.IdSeccion);
            
            if (pMatricula.AnioLectivo.Year > 1000)
            {
                DateTime date = new DateTime(pMatricula.AnioLectivo.Year);
                pQuery = pQuery.Where(s => s.AnioLectivo == date);
            }
            if (pMatricula.Top_Aux > 0)                
                pQuery = pQuery.Take(pMatricula.Top_Aux).AsQueryable();
            
            return pQuery;
        }
        public static async Task<List<Matricula>> BuscarAsync(Matricula pMatricula)
        {
            var Matriculas = new List<Matricula>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Matricula.AsQueryable();
                select = QuerySelect(select, pMatricula);
                Matriculas = await select.ToListAsync();
            }
            return Matriculas;
        }
        public static async Task<List<Matricula>> BuscarIncluirTodosAsync(Matricula pMatricula)
        {
            var matriculas = new List<Matricula>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Matricula.AsQueryable();
                select = QuerySelect(select, pMatricula).Include(s => s.Alumno)
                    .Include(s => s.Docente)
                    .Include(s => s.Grado)
                    .Include(s => s.Seccion)
                    .AsQueryable();
                matriculas = await select.ToListAsync();
            }
            return matriculas;
        }
    }
}
