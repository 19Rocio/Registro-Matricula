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
using Rotativa.AspNetCore;
using SeguridadWeb.AccesoADatos;

namespace SeguridadWeb.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles="ADMINISTRADOR,PROFESOR,ALUMNO")]

    public class MatriculaController : Controller
    {
        MatriculaBL matriculaBL = new MatriculaBL();
        AlumnoBL alumnoBL = new AlumnoBL();
        DocenteBL docenteBL = new DocenteBL();
        EncargadoBL encargadoBL = new EncargadoBL();
        GradoBL gradoBL = new GradoBL();
        SeccionBL seccionBL = new SeccionBL();
        BDContexto bdcontex = new BDContexto();
        // GET: MatriculaController
        public async Task<IActionResult> Index(Matricula matricula = null)
        {
            if (matricula == null)
                matricula = new Matricula();
            if (matricula.Top_Aux == 0)
                matricula.Top_Aux = 5;
            else if (matricula.Top_Aux == -1)
                matricula.Top_Aux = 0;
            var taskbuscar = matriculaBL.BuscarIncluirTodosAsync(matricula);
            var taskbuscarTodosAlumnos = alumnoBL.ObtenerAlumnoAsync();
            var taskbuscarTodosDocente = docenteBL.ObtenerTodosAsync();
            var taskbuscarTodosGrados = gradoBL.ObtenerTodosAsync();
            var taskbuscarTodoSeccion = seccionBL.ObtenerTodosAsync();
            var matriculas = await taskbuscar;
            ViewBag.Top = matricula.Top_Aux;
            ViewBag.Alumnos = await taskbuscarTodosAlumnos;
            ViewBag.Docentes = await taskbuscarTodosDocente;
            ViewBag.Grados = await taskbuscarTodosGrados;
            ViewBag.Secciones = await taskbuscarTodoSeccion;
            return View(matriculas);
        }
        
        // GET: MatriculaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var matricula = await matriculaBL.ObtenerPorIdAsync(new Matricula { Id = id });
            matricula.Alumno = await alumnoBL.ObtenerPorIdAsync(new Alumno { IdAlumno = matricula.IdAlumno });
            matricula.Docente = await docenteBL.ObtenerPorIdAsync(new Docente { IdDocente = matricula.IdDocente });
            matricula.Grado = await gradoBL.ObtenerPorIdAsync(new Grado { Id = matricula.IdGrado });
            matricula.Seccion = await seccionBL.ObtenerPorIdAsync(new Seccion { Id = matricula.IdSeccion });
            return View(matricula);
        }
        public async Task<IActionResult> DetailsPDF(int id)
        {
            var matricula = await matriculaBL.ObtenerPorIdAsync(new Matricula { Id = id });
            matricula.Alumno = await alumnoBL.ObtenerPorIdAsync(new Alumno { IdAlumno = matricula.IdAlumno });
            matricula.Docente = await docenteBL.ObtenerPorIdAsync(new Docente { IdDocente = matricula.IdDocente });
            matricula.Grado = await gradoBL.ObtenerPorIdAsync(new Grado { Id = matricula.IdGrado });
            matricula.Seccion = await seccionBL.ObtenerPorIdAsync(new Seccion { Id = matricula.IdSeccion });
           
            return new ViewAsPdf("DetailsPDF", matricula)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = "--page-offset 0 --footer-right [page] --footer-font-size 12"
            }; ;
        }
        // GET: MatriculaController/Create
        [Authorize(Roles = "ADMINISTRADOR,PROFESOR")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Alumnos = await alumnoBL.ObtenerAlumnoAsync();
            ViewBag.Docentes = await docenteBL.ObtenerTodosAsync();
            ViewBag.Grados = await gradoBL.ObtenerTodosAsync();
            ViewBag.Secciones = await seccionBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: MatriculaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Matricula matricula)
        {
            try
            {
                int result = await matriculaBL.CrearAsync(matricula);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Alumnos = await alumnoBL.ObtenerAlumnoAsync();
                ViewBag.Docentes = await docenteBL.ObtenerTodosAsync();
                ViewBag.Grados = await gradoBL.ObtenerTodosAsync();
                ViewBag.Secciones = await seccionBL.ObtenerTodosAsync();
                return View(matricula);
            }
        }

        // GET: MatriculaController/Edit/5
        [Authorize(Roles ="ADMINISTRADOR,PROFESOR,ALUMNO")]
        public async Task<IActionResult> Edit(Matricula pMatricula)
        {
            var matricula = await matriculaBL.ObtenerPorIdAsync(pMatricula);
            var taskbuscarTodosAlumnos = alumnoBL.ObtenerAlumnoAsync();
            var taskbuscarTodosDocente = docenteBL.ObtenerTodosAsync();
            var taskbuscarTodosEncargado = encargadoBL.ObtenerTodos();
            var taskbuscarTodosGrados = gradoBL.ObtenerTodosAsync();
            var taskbuscarTodoSeccion = seccionBL.ObtenerTodosAsync();
            ViewBag.Alumnos = await taskbuscarTodosAlumnos;
            ViewBag.Docentes = await taskbuscarTodosDocente;
            ViewBag.Encargado = await taskbuscarTodosEncargado;
            ViewBag.Grados = await taskbuscarTodosGrados;
            ViewBag.Secciones = await taskbuscarTodoSeccion;
            ViewBag.Error = "";
            return View(matricula);
        }

        // POST: MatriculaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Matricula matricula)
        {
            try
            {
                int result = await matriculaBL.ModificarAsync(matricula);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Alumnos = await alumnoBL.ObtenerAlumnoAsync();
                ViewBag.Docentes = await docenteBL.ObtenerTodosAsync();
                ViewBag.Encargado = await encargadoBL.ObtenerTodos();
                ViewBag.Grados = await gradoBL.ObtenerTodosAsync();
                ViewBag.Secciones = await seccionBL.ObtenerTodosAsync(); 
                ViewBag.Error = ex.Message;
                return View(matricula);
            }
        }

        // GET: MatriculaController/Delete/5
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<IActionResult> Delete(Matricula pMatricula)
        {
            var matricula = await matriculaBL.ObtenerPorIdAsync(pMatricula);
            matricula.Alumno = await alumnoBL.ObtenerPorIdAsync(new Alumno { IdAlumno = matricula.IdAlumno });
            matricula.Docente = await docenteBL.ObtenerPorIdAsync(new Docente { IdDocente = matricula.IdDocente });
            matricula.Grado = await gradoBL.ObtenerPorIdAsync(new Grado { Id = matricula.IdGrado });
            matricula.Seccion = await seccionBL.ObtenerPorIdAsync(new Seccion { Id = matricula.IdSeccion });
            ViewBag.Error = "";
            return View(matricula);
        }

        // POST: MatriculaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Matricula pMatricula)
        {
            try
            {
                int result = await matriculaBL.EliminarAsync(pMatricula);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var matricula = await matriculaBL.ObtenerPorIdAsync(pMatricula);
                if (matricula == null)
                    matricula = new Matricula();
                if (matricula.Id > 0)
                    matricula.Alumno = await alumnoBL.ObtenerPorIdAsync(new Alumno { IdAlumno = matricula.IdAlumno });
                return View(pMatricula);
            }
        }
    }
}
