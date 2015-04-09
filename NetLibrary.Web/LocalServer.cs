using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetLibrary.Web
{
    public class LocalServer : IDisposable
    {
        public EventHandler<EventArgs> OnResponseRecibed;
        private HttpListener _httpListener = new HttpListener();
        private HttpListenerContext _context;
        private Thread _responseThread;
        private int _port = 5000;

        public LocalServer()
        {
            initialize();
        }

        public LocalServer(int port)
        {
            this._port = port;
            initialize();
        }

        private void initialize()
        {
            this._httpListener.Prefixes.Add("http://localhost:" + _port + "/");
        }

        public void Start()
        {
            _responseThread = new Thread(ResponseThread);
            
        }

        void ResponseThread()
        {
            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext(); // get a context
                // Now, you'll find the request URL in context.Request.Url
                byte[] _responseArray = Encoding.UTF8.GetBytes("<html><head><title>Localhost server -- port 5000</title></head>" +
                                                                "<body>Welcome to the <strong>Localhost server</strong> -- <em>port " + this._port + "!</em></body></html>"); // get the bytes to response

                if (OnResponseRecibed != null)
                    OnResponseRecibed(context, null);

                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                //context.Response.KeepAlive = false; // set the KeepAlive bool to false
                //context.Response.Close(); // close the connection
                Console.WriteLine("Respone given to a request.");

            }
        }

        public void Dispose()
        {
            _context.Response.Close();
            _responseThread.Abort();
            this._httpListener.Close();
        }
    }
}
