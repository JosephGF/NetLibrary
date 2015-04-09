using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetLibrary.Web.MVC.Extensions.ModelState
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// Genera un objeto Json a partir de los valores del ModelState
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Devuelve una cadena con los valores del ModelState</returns>
        public static string ModelStateToJson(this Controller controller)
        {
            string jsonResult = "{}";
            ModelStateItems results = new ModelStateItems();
            for (int i = 0; i < controller.ModelState.Values.Count; i++)
            {
                System.Web.Mvc.ModelState state = controller.ModelState.Values.ToList()[i];
                string key = controller.ModelState.Keys.ToList()[0];
                foreach (ModelError errors in state.Errors)
                    results.AddMessage(key, errors.ErrorMessage == "" ? errors.Exception.Message : errors.ErrorMessage);
            }

            jsonResult = NetLibrary.Serialization.JSONEncode(results);
            return jsonResult;
        }

        /// <summary>
        /// Crea un objeto System.Web.Mvc.JsonResult que presenta el resultado del ModelState.
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Resultado del json que representa el estado del Modelo.</returns>
        public static JsonResult JsonModelState(this Controller controller)
        {
            JsonResult result = new JsonResult();
            result.Data = ModelStateToJson(controller);

            return result;
        }
    }
}
