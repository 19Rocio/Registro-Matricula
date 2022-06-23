using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.LogicaDeNegocio
{
   public class DocenteBL
    {
        public async Task<int>CrearAsync(Docente pDocente)
        {
            return await DocenteDAL.CrearAsync(pDocente);
        }
        public async Task<int>ModificarAysnc(Docente pDocente)
        {
            return await DocenteDAL.ModificarAsync(pDocente);
        }
        public async Task<int>EliminarAsync(Docente pDocente)
        {
            return await DocenteDAL.EliminarAsync(pDocente);
        }
        public async Task<List<Docente>> ObtenerTodosAsync()
        {
            return await DocenteDAL.ObtenerTodosAsync();
        }
        public async Task<List<Docente>> BuscarAsync(Docente pDocente)
        {
            return await DocenteDAL.BuscarAsync(pDocente);
        }
        public async Task<Docente> ObtenerPorIdAsync(Docente pDocente)
        {
            return await DocenteDAL.ObtenerPorIdAsync(pDocente);
        }
        public async Task<List<Docente>> BuscarIncluirGrado(Docente pDocente)
        {
            return await DocenteDAL.BuscarIncluirGradoAsync(pDocente);
        }
    }
}

