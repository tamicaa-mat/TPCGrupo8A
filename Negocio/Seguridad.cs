﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public static class Seguridad
    {


        public static bool SesionActiva (object user){
            //metodo para validar si tengo una sesion activa 
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.ID != 0)
            {
                return true;
            }
            else { return false; }
        }

    }






}
