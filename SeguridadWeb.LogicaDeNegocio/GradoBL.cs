using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.LogicaDeNegocio
{
   public class GradoBL
    {
        public async Task<int> CrearAsync(Grado grado)
        {
            return await GradoDAL.CrearAsync(grado);
        }
        public async Task<int> ModificarAsync(Grado grado)
        {
            return await GradoDAL.ModificarAsync(grado);
        }
        public async Task<int> EliminarAsync(Grado grado)
        {
            return await GradoDAL.EliminarAsync(grado);
        }
        public async Task<Grado> ObtenerPorIdAsync(Grado pGrado)
        {
            return await GradoDAL.ObtenerPorIdAsync(pGrado);
        }
        public async Task<List<Grado>> ObtenerTodosAsync()
        {
            return await GradoDAL.ObtenerTodosAsync();
        }
        public async Task<List<Grado>> BuscarAsync(Grado grado)
        {
            return await GradoDAL.BuscarAsync(grado);
        }
    }
}
