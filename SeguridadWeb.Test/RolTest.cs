using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeguridadWeb.LogicaDeNegocio;
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.Test
{
    [TestClass]
   public class RolTest
    {
        [TestMethod]
        public async Task CrearAsyncTest()
        {
            Rol rol = new Rol();
            rol.Nombre = "Test Rol 2 ";
            int result = await RolDAL.CrearAsync(rol);
            Assert.IsFalse(result == 0);
        }
        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            Rol rol = new Rol();
            rol.Id = 2;
            rol.Nombre = "Test Modificar";
            int result = await RolDAL.ModificarAsync(rol);
            Assert.IsFalse(result == 0);
        }
    }
}
