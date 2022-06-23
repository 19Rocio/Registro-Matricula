using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.LogicaDeNegocio
{
   public class EncargadoBL
    {
        public async Task<int>CrearAsync(Encargado encargado)
        {
            return await EncargadoDAL.CrearAsync(encargado);
        }
        public async Task<int> ModificarAsync(Encargado encargado)
        {
            return await EncargadoDAL.ModificarAsync(encargado);
        }
        public async Task<int> EliminarAsync(Encargado encargado)
        {
            return await EncargadoDAL.EliminarAsync(encargado);
        }
        public async Task<Encargado> ObtenerPorIdAsync(Encargado pEncargado)
        {
            return await EncargadoDAL.ObtenerPorIdAsync(pEncargado);
        }
        public async Task<List<Encargado>> ObtenerTodos()
        {
            return await EncargadoDAL.ObtenerTodosAsync();
        }
        public async Task<List<Encargado>> BuscarAsync(Encargado encargado)
        {
            return await EncargadoDAL.BuscarAsync(encargado);
        }
    }
}
