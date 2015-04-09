using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Web.MVC.Extensions.ModelState
{
    public class ModelStateMessage
    {
        /// <summary>
        /// Tipo de mensaje actual
        /// </summary>
        public TypeMessage Type { get; set; }
        /// <summary>
        /// Texto del mensaje
        /// </summary>
        public string Text { get; set; }

        public ModelStateMessage(string text)
        {
            this.Type = TypeMessage.Error;
            this.Text = text;
        }

        public ModelStateMessage(TypeMessage type, string text)
        {
            this.Type = type;
            this.Text = text;
        }
    }
}
