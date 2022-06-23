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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles ="ADMINISTRADOR,PROFESOR")]

    public class GradoController : Controller
    {
        GradoBL gradoBL = new GradoBL();
        // GET: GradoController
        public async Task<IActionResult> Index(Grado grado = null)
        {
            if (grado == null)
                grado = new Grado();
            if (grado.Top_Aux == 0)
                grado.Top_Aux = 5;
            else if (grado.Top_Aux == -1)
                grado.Top_Aux = 0;
            var grados = await gradoBL.BuscarAsync(grado);
            ViewBag.Top = grado.Top_Aux;
            return View(grados);
        }



        // GET: GradoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var grado = await gradoBL.ObtenerPorIdAsync(new Grado { Id = id });
            return View(grado);
        }

        // GET: GradoController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: GradoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grado grado)
        {
            try
            {
                int result = await gradoBL.CrearAsync(grado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(grado);
            }
        }
        // GET: GradoController/Edit/5
        public async Task<IActionResult> Edit(Grado pGrado)
        {
            var grado = await gradoBL.ObtenerPorIdAsync(pGrado);
            ViewBag.Error = "";
            return View(grado);
        }

        // POST: GradoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grado grado)
        {
            try
            {
                int result = await gradoBL.ModificarAsync(grado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(grado);
            }
        }

        // GET: GradoController/Delete/5
        public async Task<IActionResult> Delete(Grado pGrado)
        {
            var grado = await gradoBL.ObtenerPorIdAsync(pGrado);
            return View(grado);
        }

        // POST: GradoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Grado pGrado)
        {
            try
            {
                int result = await gradoBL.EliminarAsync(pGrado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pGrado);
            }
        }
    }
}
