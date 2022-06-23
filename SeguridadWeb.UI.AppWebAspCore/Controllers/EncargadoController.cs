using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/***************************************/
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SeguridadWeb.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles ="ADMINISTRADOR,PROFESOR,ALUMNO")]

    public class EncargadoController : Controller
    {
        EncargadoBL encargadoBL = new EncargadoBL();
        // GET: EncargadoController
        public async Task<IActionResult> Index(Encargado encargardo = null)
        {
            if (encargardo == null)
                encargardo = new Encargado();
            if (encargardo.Top_Aux == 0)
                encargardo.Top_Aux = 5;
            else if (encargardo.Top_Aux == -1)
                encargardo.Top_Aux = 0;
            var encargardos = await encargadoBL.BuscarAsync(encargardo);
            ViewBag.Top = encargardo.Top_Aux;
            return View(encargardos);
        }

        // GET: EncargadoController/Details/5
             public async Task<IActionResult> Details(int id)
        {
            var encargado = await encargadoBL.ObtenerPorIdAsync(new Encargado { IdEncargado = id });
            return View(encargado);
        }


        // GET: EncargadoController/Create
        [Authorize(Roles = "ADMINISTRADOR,PROFESOR")]
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: EncargadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Encargado encargardo)
        {
            try
            {
                int result = await encargadoBL.CrearAsync(encargardo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(encargardo);
            }
        }

        // GET: EncargadoController/Edit/5
        [Authorize(Roles ="ADMINISTRADOR,PROFESOR,ALUMNO")]
        public async Task<IActionResult> Edit(int id)
        {
            var encargado = await encargadoBL.ObtenerPorIdAsync(new Encargado{IdEncargado = id});
            ViewBag.Error = "";
            return View(encargado);
        }

        // POST: EncargadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Encargado encargado)
        {
            try
            {
                int result = await encargadoBL.ModificarAsync(new Encargado{ IdEncargado = id });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(encargado);
            }
        }

        // GET: EncargadoController/Delete/5
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var encargado = await encargadoBL.ObtenerPorIdAsync(new Encargado { IdEncargado = id });
            return View(encargado);
        }

        // POST: EncargadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,Encargado pEncargado)
        {
            try
            {
                int result = await encargadoBL.EliminarAsync(new Encargado { IdEncargado = id});
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pEncargado);
            }
        }
    }
}
