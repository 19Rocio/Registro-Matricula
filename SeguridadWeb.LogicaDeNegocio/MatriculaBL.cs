using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.LogicaDeNegocio
{
   public class MatriculaBL
    {
        public async Task<int> CrearAsync(Matricula matricula)
        {
            return await MatriculaDAL.CrearAsync(matricula);
        }
        public async Task<int> ModificarAsync(Matricula matricula)
        {
            return await MatriculaDAL.ModificarAsync(matricula);
        }
        public async Task<int> EliminarAsync(Matricula matricula)
        {
            return await MatriculaDAL.EliminarAsync(matricula);
        }
        public async Task<Matricula> ObtenerPorIdAsync(Matricula pMatricula)
        {
            return await MatriculaDAL.ObtenerPorIdAsync(pMatricula);
        }
        public async Task<List<Matricula>> BuscarAsync(Matricula pMatricula)
        {
            return await MatriculaDAL.BuscarAsync(pMatricula);
        }
    
        public async Task<List<Matricula>> BuscarIncluirTodosAsync(Matricula pMatricula)
        {
            return await MatriculaDAL.BuscarIncluirTodosAsync(pMatricula);
        }
        public async Task<List<Alumno>> BuscarPorNie(Alumno alumno, Matricula matricula)
        {
            return await AlumnoDAL.BuscarPorNie(alumno, matricula);
        }
    }
}
