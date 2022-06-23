using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/***************************************/
using SeguridadWeb.EntidadesDeNegocio;
using SeguridadWeb.AccesoADatos;
using SeguridadWeb.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace SeguridadWeb.UI.AppWebAspCore.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles ="ADMINISTRADOR,PROFESOR,ALUMNO")]

    public class AlumnoController : Controller
    {
        AlumnoBL alumnBL = new AlumnoBL();
        UsuarioBL userBL = new UsuarioBL();


        // GET: AlumnoController
        public async Task<IActionResult> Index(Alumno alumn = null)
        {
            if (alumn == null)
                alumn = new Alumno();
            if (alumn.Top_Aux == 0)
                alumn.Top_Aux = 5;
            else if (alumn.Top_Aux == -1)
                alumn.Top_Aux = 0;
            var taskbuscar = alumnBL.BuscarIncluirUserAsync(alumn);            
            var taskObtenerTodosUser = userBL.ObtenerTodosAsync();
            var alumnos = await taskbuscar;
            ViewBag.Top = alumn.Top_Aux;
            ViewBag.Usuarios = await taskObtenerTodosUser;
            return (IActionResult)View(alumnos);
        }
        [HttpGet]
        public JsonResult Listar()
        {
            List<Alumno> oListaAlumno = AlumnoDAL.Listar();  //Declaramos una variable de tipo lista del modelo a llamar donde almacenamos los datos que se envian desde la capa DAL  
            return Json(new 
            {
                data = oListaAlumno.Select(c => new Alumno
                {
                    IdAlumno = c.IdAlumno,
                    Nie = c.Nie,
                    Dui = c.Dui,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    FechaNac = c.FechaNac,
                    Direccion = c.Direccion,
                    NombreEncargado = c.NombreEncargado,
                    ApellidoEncargado = c.ApellidoEncargado,
                    DuiEncargado = c.DuiEncargado,
                    TelefonoEncargado = c.TelefonoEncargado
                })
            });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await alumnBL.ObtenerAlumnoAsync();
            return Json(new { data = todos });
        }

       
        // GET: AlumnoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var alumn = await alumnBL.ObtenerPorIdAsync(new Alumno { IdAlumno = id });
            alumn.Usuario = await userBL.ObtenerPorIdAsync(new Usuario { Id = (int)alumn.IdUser });
            return (IActionResult)View(alumn);
        }
        [Authorize(Roles ="ADMINISTRADOR,PROFESOR")]
        // GET: AlumnoController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await userBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return (IActionResult)View();
        }

        // POST: AlumnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno Alumn)
        {
            try
            {
                int result = await alumnBL.CrearAync(Alumn);
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await userBL.ObtenerTodosAsync();
                return (IActionResult)View(Alumn);
            }
        }

        // GET: AlumnoController/Edit/5
        public JsonResult Editar(int id)
        {
            var alumno = alumnBL.ObtenerPorIdAsync(new Alumno {IdAlumno = id });  //Declaramos una variable de tipo lista del modelo a llamar donde almacenamos los datos que se envian desde la capa DAL  
            return Json(new { data = alumno});
         }
        public async Task<IActionResult> Edit(int id)
        {
            var alumno = await alumnBL.ObtenerPorIdAsync(new Alumno { IdAlumno = id});
            var taskbuscarTodosUsuarios = userBL.ObtenerTodosAsync();
            ViewBag.Usuarios = await taskbuscarTodosUsuarios;
            ViewBag.Error = "";
            return (IActionResult)View(alumno);
        }

        // POST: AlumnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Alumno alumn)
        {
            try
            {
                int result = await alumnBL.ModificarAsync(alumn);
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await userBL.ObtenerTodosAsync();
                return (IActionResult)View(alumn);
            }
        }

        // GET: AlumnoController/Delete/5
        [Authorize (Roles="ADMINISTRADOR,PROFESOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var alumno = await alumnBL.ObtenerPorIdAsync(new Alumno {IdAlumno = id});
            alumno.Usuario = await userBL.ObtenerPorIdAsync(new Usuario { Id = (int)alumno.IdUser });
            ViewBag.Error = "";
            return (IActionResult)View(alumno);
        }

        // POST: AlumnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Alumno pAlumn)
        {
            try
            {
                int result = await alumnBL.EliminarAsync(new Alumno { IdAlumno = id });
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var alumno = await alumnBL.ObtenerPorIdAsync(pAlumn);
                if (alumno == null)
                    alumno = new Alumno();
                if (alumno.IdAlumno > 0)
                    alumno.Usuario = await userBL.ObtenerPorIdAsync(new Usuario { Id = (int)alumno.IdUser });
                return (IActionResult)View(alumno);
            }
        }
    }
}
