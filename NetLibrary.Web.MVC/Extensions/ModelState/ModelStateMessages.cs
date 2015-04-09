using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Web.MVC.Extensions.ModelState
{

    public class ModelStateMessages
    {
        private List<ModelStateMessage> _mesages = new List<ModelStateMessage>();

        /// <summary>
        /// Key que define el origen de los mensaje
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Mensajes actuales
        /// </summary>
        public List<ModelStateMessage> Messages
        {
            get { return _mesages; }
            set { _mesages = value; }
        }

        public ModelStateMessages(string key)
        {
            this.Key = key;
        }

        public ModelStateMessages(string key, string[] messages)
        {
            this.Key = key;
            foreach (string msg in messages)
            {
                this.Messages.Add(new ModelStateMessage(msg));
            }
        }

        public ModelStateMessages(string key, string message)
        {
            this.Key = key;
            this.AddMessage(message);
        }

        public ModelStateMessages(string key, TypeMessage type, string[] messages)
        {
            this.Key = key;
            foreach (string msg in messages)
            {
                this.AddMessage(type, msg);
            }
        }

        public ModelStateMessages(string key, TypeMessage type, string message)
        {
            this.Key = key;
            this.AddMessage(type, message);
        }

        public void AddMessage(string message)
        {
            this.Messages.Add(new ModelStateMessage(message));
        }

        public void AddMessage(TypeMessage type, string message)
        {
            this.Messages.Add(new ModelStateMessage(type, message));
        }
    }
}
