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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles="ADMINISTRADOR,PROFESOR")]
    public class DocenteController : Controller
    {
        DocenteBL docBL = new DocenteBL();
        GradoBL gradoBL = new GradoBL();
        SeccionBL seccionBL = new SeccionBL();
        UsuarioBL userBL = new UsuarioBL();

        // GET: DocenteController
        
        public async Task<IActionResult> Index(Docente doc = null)
        {
            if (doc == null)
                doc = new Docente();
            if (doc.Top_Aux == 0)
                doc.Top_Aux = 5;
            else if (doc.Top_Aux == -1)
                doc.Top_Aux = 0;
            var taskBuscar = docBL.BuscarIncluirGrado(doc);
            var taskObtenerGrados = gradoBL.ObtenerTodosAsync();
            var taskObtenerSeccion = seccionBL.ObtenerTodosAsync();
            var taskObtenerUsuarios = userBL.ObtenerTodosAsync();
            var docentes = await taskBuscar;
            ViewBag.Top = doc.Top_Aux;
            ViewBag.Grados = await taskObtenerGrados;
            ViewBag.Secciones = await taskObtenerSeccion;
            ViewBag.Usuarios = await taskObtenerUsuarios;
            return View(docentes);
        }

        // GET: DocenteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doc = await docBL.ObtenerPorIdAsync(new Docente { IdDocente = id });
            doc.Usuario = await userBL.ObtenerPorIdAsync(new Usuario { Id = (int)doc.IdUser });
            doc.Grado = await gradoBL.ObtenerPorIdAsync(new Grado { Id = doc.IdGrado });
            doc.Seccion = await seccionBL.ObtenerPorIdAsync(new Seccion { Id = doc.IdSeccion });
            return View(doc);
        }

        // GET: DocenteController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: DocenteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Docente docente)
        {
            try
            {
                int result = await docBL.CrearAsync(docente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(docente);
            }
        }

        // GET: DocenteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var docente = await docBL.ObtenerPorIdAsync(new Docente { IdDocente = id});
            ViewBag.Error = "";
            return View(docente);
        }

        // POST: DocenteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Docente doc)
        {
            try
            {
                int result = await docBL.ModificarAysnc(doc);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(doc);
            }
        }

        // GET: DocenteController/Delete/5
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var doc = await docBL.ObtenerPorIdAsync(new Docente{ IdDocente = id});
            
            return View(doc);
        }

        // POST: DocenteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Docente pDoc)
        {
            try
            {
                int result = await docBL.EliminarAsync(new Docente { IdDocente = id });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var docentes = await docBL.ObtenerPorIdAsync(new Docente { IdDocente = id});
               
                return View(docentes);
            }
        }
    }
}
