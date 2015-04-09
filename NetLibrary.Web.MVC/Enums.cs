using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Web.MVC
{
    /// <summary>
    /// Acciones del Model
    /// </summary>
    public enum Accion
    {
        /// <summary>
        /// Creacion
        /// </summary>
        Create = 1
      ,
        /// <summary>
        /// Modificacion
        /// </summary>
        Edit = 2
      ,
        /// <summary>
        /// Eliminacion
        /// </summary>
        Delete = 4
      ,
        /// <summary>
        /// Consulta Elemento
        /// </summary>
        View = 8
     ,
        /// <summary>
        /// Consulta Multiples Elementos
        /// </summary>
        ViewList = 16
    }

    public enum AccionResultType
    {
        ViewResult,
        PartialViewResult,
        JsonResult,
        ContentResult,
        JavaScriptResult,
        FileResult,
        FormResult,
        Custom
    }
}

