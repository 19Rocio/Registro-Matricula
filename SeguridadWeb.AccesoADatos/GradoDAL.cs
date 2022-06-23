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
  public class GradoDAL
    {
        public static async Task<int> CrearAsync(Grado pGrado)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pGrado);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Grado pGrado)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Grado = await bdContexto.Grado.FirstOrDefaultAsync(s => s.Id == pGrado.Id);
                Grado.Grado1 = pGrado.Grado1;
                bdContexto.Update(Grado);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Grado pGrado)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Grado = await bdContexto.Grado.FirstOrDefaultAsync(s => s.Id == pGrado.Id);
                bdContexto.Grado.Remove(Grado);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<List<Grado>> ObtenerTodosAsync()
        {
            var grados = new List<Grado>();
            using(var dbContexto = new BDContexto())
            {
                grados = await dbContexto.Grado.ToListAsync();
            }
            return grados;
        }
        public static async Task<Grado> ObtenerPorIdAsync(Grado pGrado)
        {
            var rol = new Grado();
            using (var bdContexto = new BDContexto())
            {
                rol = await bdContexto.Grado.FirstOrDefaultAsync(s => s.Id == pGrado.Id);
            }
            return rol;
        }
        //---------------------------------------------------------------------------------------------------
        internal static IQueryable<Grado> QuerySelect(IQueryable<Grado> pQuery, Grado pGrado)
        {
            if (pGrado.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pGrado.Id);
            if (!string.IsNullOrWhiteSpace(pGrado.Grado1))
                pQuery = pQuery.Where(s => s.Grado1.Contains(pGrado.Grado1));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pGrado.Top_Aux > 0)
                pQuery = pQuery.Take(pGrado.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Grado>> BuscarAsync(Grado pGrado)
        {
            var grados = new List<Grado>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Grado.AsQueryable();
                select = QuerySelect(select, pGrado);
                grados = await select.ToListAsync();
            }
            return grados;
        }
    }
}
