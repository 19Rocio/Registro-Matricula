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
   public class EncargadoDAL
    {
        public static async Task<int> CrearAsync(Encargado encargado)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(encargado);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Encargado pEncargado = null)
        {
            int result = 0;
            using(var bdContext = new BDContexto())
            {
                if (pEncargado == null)
                {
                    pEncargado = new Encargado();
                    var encargado = await bdContext.Encargado.FirstOrDefaultAsync(s => s.IdEncargado == pEncargado.IdEncargado);//aqui esta el problema no trae el id
                    encargado.Nombre = pEncargado.Nombre;
                    encargado.Apellido = pEncargado.Apellido;
                    encargado.Nacionalidad = pEncargado.Nacionalidad;
                    encargado.Dui = pEncargado.Dui;
                    encargado.Correo = pEncargado.Correo;
                    encargado.Telefono = pEncargado.Telefono;
                    encargado.Estadocivil = pEncargado.Estadocivil;
                    encargado.Parentesco = pEncargado.Parentesco;
                    encargado.UltimoGrado = pEncargado.UltimoGrado;
                    bdContext.Update(encargado);
                    result = await bdContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Encargado pEncargado)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Encargado = await bdContexto.Encargado.FirstOrDefaultAsync(s => s.IdEncargado == pEncargado.IdEncargado);
                bdContexto.Encargado.Remove(Encargado);
                
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Encargado> ObtenerPorIdAsync(Encargado pEncargado)
        {
            var encargado = new Encargado();
            using (var bdContexto = new BDContexto())
            {
                encargado = await bdContexto.Encargado.FirstOrDefaultAsync(s => s.IdEncargado == pEncargado.IdEncargado);
            }
            return encargado;
        }
        //---------------------------------------------------------------------------------------------------
        internal static IQueryable<Encargado> QuerySelect(IQueryable<Encargado> pQuery, Encargado pEncargado)
        {
            if (pEncargado.IdEncargado > 0)
                pQuery = pQuery.Where(s => s.IdEncargado == pEncargado.IdEncargado);
            if (!string.IsNullOrWhiteSpace(pEncargado.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pEncargado.Nombre));
            if (!string.IsNullOrWhiteSpace(pEncargado.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pEncargado.Apellido));
             if (!string.IsNullOrWhiteSpace(pEncargado.Parentesco))
                pQuery = pQuery.Where(s => s.Parentesco.Contains(pEncargado.Parentesco));

            pQuery = pQuery.OrderByDescending(s => s.IdEncargado).AsQueryable();
            if (pEncargado.Top_Aux > 0)
                pQuery = pQuery.Take(pEncargado.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Encargado>> BuscarAsync(Encargado pEncargado)
        {
            var Encargados = new List<Encargado>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Encargado.AsQueryable();
                select = QuerySelect(select, pEncargado);
                Encargados = await select.ToListAsync();
            }
            return Encargados;
        }
        public static async Task<List<Encargado>> ObtenerTodosAsync()
        {
            var Encargados = new List<Encargado>();
            using (var bdContexto = new BDContexto())
            {
                Encargados = await bdContexto.Encargado.ToListAsync();
            }
            return Encargados;
        }
    }
}
