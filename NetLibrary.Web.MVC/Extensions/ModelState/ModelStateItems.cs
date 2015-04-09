using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Web.MVC.Extensions.ModelState
{
    public class ModelStateItems
    {
        public ModelStateItems() { }

        public ModelStateItems(string tag)
        {
            this.Tag = tag;
        }

        private List<ModelStateMessages> _values = new List<ModelStateMessages>();
        /// <summary>
        /// Comprueba si hay mensajes de error
        /// </summary>
        public bool HasErrors
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Error).Count() > 0).Count() > 0; }
        }
        /// <summary>
        /// Comprueba si existen mensajes de advertencia
        /// </summary>
        public bool HasExclamations
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Exclamation).Count() > 0).Count() > 0; }
        }
        /// <summary>
        /// Comprueba si existen mensajes de información
        /// </summary>
        public bool HasInformations
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Information).Count() > 0).Count() > 0; }
        }
        /// <summary>
        /// Comprueba si no existen errores
        /// </summary>
        [Obsolete("Usado por compatibilidad, sustituido por 'HasErrors'")]
        public bool Success
        {
            get { return !this.HasErrors; }
        }
        /// <summary>
        /// Obtiene los mensajes definidos para una key concreta
        /// </summary>
        /// <param name="key">Key a buscar</param>
        /// <returns>JsonMessages encontrado</returns>
        public ModelStateMessages this[string key]
        {
            get
            {
                return this._values.Where(v => v.Key == key).FirstOrDefault();
            }
            set
            {
                ModelStateMessages item = this._values.Where(v => v.Key == key).FirstOrDefault();
                if (item == null)
                {
                    this._values.Remove(item);
                }

                value.Key = key;
                this._values.Add(value);
            }
        }
        /// <summary>
        /// Anexa un nuevo mensaje, si no existe la key la crea
        /// </summary>
        /// <param name="key">key del mensaje</param>
        /// <param name="message">texto del mensaje</param>
        public void AddMessage(string key, string message)
        {
            ModelStateMessages jsonMessage = this[key];
            if (jsonMessage == null)
            {
                jsonMessage = new ModelStateMessages(key);
            }

            jsonMessage.AddMessage(message);
            this._values.Add(jsonMessage);
        }
        /// <summary>
        /// Numero de keys actuales
        /// </summary>
        public int Count { get { return this._values.Count; } }

        /// <summary>
        /// Lista de mensajes generados
        /// </summary>
        /*        public List<JsonMessages> Messages
                {
                    get { return this._values; }
                }
         */

        /// <summary>
        /// Información adicional para devolver al navegador
        /// </summary>
        public string Tag { get; set; }

        public List<ModelStateMessages> Errors
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Error).Count() > 0).ToList(); }
        }
        public List<ModelStateMessages> Exclamations
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Exclamation).Count() > 0).ToList(); }
        }
        public List<ModelStateMessages> Informations
        {
            get { return this._values.Where(r => r.Messages.Where(m => m.Type == TypeMessage.Information).Count() > 0).ToList(); }
        }
    }
}
