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

    public class SeccionController : Controller
    {
        SeccionBL seccionBL = new SeccionBL();
        // GET: SeccionController
        public async Task<IActionResult> Index(Seccion seccion = null)
        {
            if (seccion == null)
                seccion = new Seccion();
            if (seccion.Top_Aux == 0)
                seccion.Top_Aux = 5;
            else if (seccion.Top_Aux == -1)
                seccion.Top_Aux = 0;
            var secciones = await seccionBL.BuscarAsync(seccion);
            ViewBag.Top = seccion.Top_Aux;
            return View(secciones);
        }


        // GET: SeccionController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var seccion = await seccionBL.ObtenerPorIdAsync(new Seccion { Id = id });
            return View(seccion);
        }

        // GET: SeccionController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: SeccionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seccion seccion)
        {
            try
            {
                int result = await seccionBL.CrearAsync(seccion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(seccion);
            }
        }

        // GET: SeccionController/Edit/5
        public async Task<IActionResult> Edit(Seccion pSeccion)
        {
            var seccion = await seccionBL.ObtenerPorIdAsync(pSeccion);
            ViewBag.Error = "";
            return View(seccion);
        }

        // POST: SeccionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seccion seccion)
        {
            try
            {
                int result = await seccionBL.ModificarAsync(seccion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(seccion);
            }
        }

        // GET: SeccionController/Delete/5
        public async Task<IActionResult> Delete(Seccion pSeccion)
        {
            var seccion = await seccionBL.ObtenerPorIdAsync(pSeccion);
            return View(seccion);
        }

        // POST: SeccionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Seccion pSeccion)
        {
            try
            {
                int result = await seccionBL.EliminarAsync(pSeccion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pSeccion);
            }
        }
    }
}
