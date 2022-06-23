using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------
using Microsoft.EntityFrameworkCore;
using SeguridadWeb.EntidadesDeNegocio;

namespace SeguridadWeb.AccesoADatos
{
   public class SeccionDAL
    {
        public static async Task<int> CrearAsync(Seccion seccion)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                bdContexto.Add(seccion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Seccion pSeccion)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                var seccion = await bdContexto.Seccion.FirstOrDefaultAsync(s => s.Id == pSeccion.Id);
                seccion.Seccion1 = pSeccion.Seccion1;
                bdContexto.Update(seccion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Seccion pSeccion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var seccion = await bdContexto.Seccion.FirstOrDefaultAsync(s => s.Id == pSeccion.Id);
                bdContexto.Seccion.Remove(seccion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Seccion> ObtenerPorIdAsync(Seccion pSeccion)
        {
            var seccion = new Seccion();
            using (var bdContexto = new BDContexto())
            {
                seccion = await bdContexto.Seccion.FirstOrDefaultAsync(s => s.Id == pSeccion.Id);
            }
            return seccion;
        }
        public static async Task<List<Seccion>> ObtenerTodosAsync()
        {
            var secciones = new List<Seccion>();
            using (var bdContexto = new BDContexto())
            {
                secciones = await bdContexto.Seccion.ToListAsync();
            }
            return secciones;
        }
        internal static IQueryable<Seccion> QuerySelect(IQueryable<Seccion> pQuery,Seccion pSeccion)
        {
            if (pSeccion.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pSeccion.Id);
            if (!string.IsNullOrWhiteSpace(pSeccion.Seccion1))
                pQuery = pQuery.Where(s => s.Seccion1.Contains(pSeccion.Seccion1));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pSeccion.Top_Aux > 0)
                pQuery = pQuery.Take(pSeccion.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Seccion>> BuscarAsync(Seccion pSeccion)
        {
            var secciones = new List<Seccion>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Seccion.AsQueryable();
                select = QuerySelect(select, pSeccion);
                secciones = await select.ToListAsync();
            }
            return secciones;
        }
    }
}
