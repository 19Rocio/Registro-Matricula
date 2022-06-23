using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Agregar librerias
using SeguridadWeb.EntidadesDeNegocio;
//******************************


namespace SeguridadWeb.WebAPI.Auth
{
   public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}
