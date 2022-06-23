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
    public class DocenteDAL
    {
        public static async Task<int> CrearAsync(Docente docente)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(docente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Docente pDocente)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Docente = await bdContexto.Docente.FirstOrDefaultAsync(s => s.IdDocente == pDocente.IdDocente);
                Docente.Nombre = pDocente.Nombre;
                Docente.Apellido = pDocente.Apellido;                
                Docente.Correo = pDocente.Correo;
                Docente.Telefono = pDocente.Telefono;
                Docente.Asignatura = pDocente.Asignatura;

                
                bdContexto.Update(Docente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Docente pDocente)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Docente = await bdContexto.Docente.FirstOrDefaultAsync(s => s.IdDocente == pDocente.IdDocente);
                bdContexto.Docente.Remove(Docente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Docente> ObtenerPorIdAsync(Docente pDocente)
        {
            var docente = new Docente();
            using (var bdContexto = new BDContexto())
            {
                docente = await bdContexto.Docente.FirstOrDefaultAsync(s => s.IdDocente == pDocente.IdDocente);
            }
            return docente;
        }
        //---------------------------------------------------------------------------------------------------
        internal static IQueryable<Docente> QuerySelect(IQueryable<Docente> pQuery, Docente pDocente)
        {
            if (pDocente.IdDocente > 0)
                pQuery = pQuery.Where(s => s.IdDocente == pDocente.IdDocente);
            if (pDocente.IdUser > 0)
                pQuery = pQuery.Where(s => s.IdUser == pDocente.IdUser);
            if (pDocente.IdSeccion > 0)
                pQuery = pQuery.Where(s => s.IdSeccion == pDocente.IdSeccion);
            if (pDocente.IdGrado > 0)
                pQuery = pQuery.Where(s => s.IdGrado == pDocente.IdGrado);

            if (!string.IsNullOrWhiteSpace(pDocente.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pDocente.Nombre));
            if (!string.IsNullOrWhiteSpace(pDocente.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pDocente.Apellido));
            
            if (!string.IsNullOrWhiteSpace(pDocente.Asignatura))
                pQuery = pQuery.Where(s => s.Asignatura.Contains(pDocente.Asignatura));

            pQuery = pQuery.OrderByDescending(s => s.IdDocente).AsQueryable();

            if (pDocente.Top_Aux > 0)
                pQuery = pQuery.Take(pDocente.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Docente>> BuscarAsync(Docente pDocente)
        {
            var docentes = new List<Docente>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Docente.AsQueryable();
                select = QuerySelect(select, pDocente);
                docentes = await select.ToListAsync();
            }
            return docentes;
        }
        public static async Task<List<Docente>> ObtenerTodosAsync()
        {
            var docentes = new List<Docente>();
            using (var bdContexto = new BDContexto())
            {
                docentes = await bdContexto.Docente.ToListAsync();
            }
            return docentes;
        }
        public static async Task<List<Docente>> BuscarIncluirGradoAsync(Docente pDocente)
        {
            var docentes = new List<Docente>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Docente.AsQueryable();
                select = QuerySelect(select, pDocente).Include(s => s.Grado)
                .Include(s=> s.Seccion)
                .Include(s => s.Usuario)
                .AsQueryable();
                docentes = await select.ToListAsync();
            }
            return docentes;
        }
    }
}

