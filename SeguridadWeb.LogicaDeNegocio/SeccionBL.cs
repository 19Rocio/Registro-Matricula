using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;


namespace SeguridadWeb.LogicaDeNegocio
{
   public class SeccionBL
    {
        public async Task<int> CrearAsync(Seccion seccion)
        {
            return await SeccionDAL.CrearAsync(seccion);
        }
        public async Task<int> ModificarAsync(Seccion seccion)
        {
            return await SeccionDAL.ModificarAsync(seccion);
        }
        public async Task<int> EliminarAsync(Seccion seccion)
        {
            return await SeccionDAL.EliminarAsync(seccion);
        }
        public async Task<List<Seccion>> ObtenerTodosAsync()
        {
            return await SeccionDAL.ObtenerTodosAsync();
        }
        public async Task<Seccion> ObtenerPorIdAsync(Seccion pSeccion)
        {
            return await SeccionDAL.ObtenerPorIdAsync(pSeccion);
        }
        public async Task<List<Seccion>> BuscarAsync(Seccion seccion)
        {
            return await SeccionDAL.BuscarAsync(seccion);
        }
    }
}
