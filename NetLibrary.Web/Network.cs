using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using NetLibrary;
using NetLibrary.Images;

namespace NetLibrary.Web
{
    public class Network
    {
        public static bool IsHability()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
        /// <summary>
        /// Crea si es posible un objeto Uri con la url especificada
        /// </summary>
        /// <param name="url">Url desde la que se espera convertir</param>
        /// <returns>Devuelve un objeto Uri, null si no se puede crear</returns>
        public static Uri CreateUri(string url)
        {
            Uri uriResult = null;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

            return uriResult;
        }

        /// <summary>
        /// Guarda un recurso de internet desde una url especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <param name="localFile">Ruta completa del Fichero local</param>
        public static FileInfo DownloadFile(string url, string localFile)
        {
            FileInfo fi = new FileInfo(localFile);
            Uri uri = CreateUri(url);
            return DownloadFile(uri, fi);
        }

        /// <summary>
        /// Guarda un recurso de internet desde una url especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <param name="localFile">Fichero locao</param>
        public static FileInfo DownloadFile(string url, FileInfo localFile)
        {
            Uri uri = CreateUri(url);
            return DownloadFile(uri, localFile);
        }

        /// <summary>
        /// Guarda un recurso de internet desde una url especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="localFile">Fichero locao</param>
        public static FileInfo DownloadFile(Uri uri, FileInfo localFile)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(uri, localFile.FullName);

            return localFile;
        }

        /// <summary>
        /// Obtiene el texto de una web especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <returns>Cadena con el contenido</returns>
        public static string UrlToText(Uri uri)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString(uri);
        }

        /// <summary>
        /// Obtiene el texto de una web especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <returns>Cadena con el contenido</returns>
        public static string UrlToText(string url)
        {
            Uri uri = CreateUri(url);
            return UrlToText(uri);
        }

        /// <summary>
        /// Obtiene un array de bytes de una web especificada
        /// </summary>
        /// <param name="uri">Url del recurso</param>
        /// <returns>Array de byte con el contenido</returns>
        public static byte[] UrlToByte(Uri uri)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadData(uri);
        }

        /// <summary>
        /// Obtiene un array de bytes de una web especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <returns>Array de byte con el contenido</returns>
        public static byte[] UrlToByte(string url)
        {
            Uri uri = CreateUri(url);
            return UrlToByte(uri);
        }

        /// <summary>
        /// Obtiene una imagen desde una web especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <returns>Bitmap imagen</returns>
        public static Bitmap UrlToBitmap(Uri uri)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData(uri);

            return Images.Convert.BytesToBitmap(bytes);
        }

        /// <summary>
        /// Obtiene una imagen desde una web especificada
        /// </summary>
        /// <param name="ur">Url del recurso</param>
        /// <returns>Bitmap imagen</returns>
        public static Bitmap UrlToBitmap(string url)
        {
            Uri uri = CreateUri(url);
            return UrlToBitmap(uri);
        }

        /// <summary>
        /// Obtiene un json especificado
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <returns>Cadena con el JSON obtenido</returns>
        public static string UrlToJSON(string url)
        {
            return UrlToJSON(new Uri(url));
        }

        /// <summary>
        /// Obtiene un json especificado
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <returns>Cadena con el JSON obtenido</returns>
        public static string UrlToJSON(Uri uri)
        {
            return UrlToJSON(uri, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// Obtiene un json especificado
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="encoding">Codificación del resultado</param>
        /// <returns>Cadena con el JSON obtenido</returns>
        public static string UrlToJSON(Uri uri, System.Text.Encoding encoding)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = encoding;
            string json = webClient.DownloadString(uri);

            return json;
        }
        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="type">Tipo de objeto que se espera</param>
        /// <returns>Objeto obtenido</returns>
        public static object UrlToJSON(Uri uri, Type type)
        {
            string json = UrlToJSON(uri);
            object obj = Serialization.JSONDecode(json, type);
            return obj;
        }
       
        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <param name="type">Tipo de objeto que se espera</param>
        /// <returns>Objeto obtenido</returns>
        public static object UrlToJSON(string url, Type type)
        {
            Uri uri = CreateUri(url);
            return UrlToJSON(uri, type);
        }

        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se espera</typeparam>
        /// <param name="url">Url del recurso</param>
        /// <returns>Objeto obtenido</returns>
        public static T UrlToJSON<T>(string url) where T : class
        {
            object obj = UrlToJSON(url, typeof(T));
            return (T)obj;
        }

        /// <summary>
        /// Verifica si la uri especificada es válida
        /// </summary>
        /// <param name="url">Url que se comprueba</param>
        /// <returns>Booleano con el resultado de la comprobación</returns>
        public static bool IsValidURL(string url)
        {
            Uri uri = CreateUri(url);
            return IsValidURL(url);
        }
        /// <summary>
        /// Verifica si la uri especificada es válida
        /// </summary>
        /// <param name="uri">Uri que se comprueba</param>
        /// <returns>Booleano con el resultado de la comprobación</returns>
        public static bool IsValidURL(Uri uri)
        {
            WebRequest webRequest = WebRequest.Create(uri);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception ex)//If exception thrown then couldn't get response from address
            {
                return false;
            }
            return true;
        }

        #region Procesos Asincronos
        public delegate void WebEventHandler(object sender, WebEventArgs e);
        public static event WebEventHandler onWebProcessChange;
        public static event WebEventHandler onWebProcessFinished;

        /// <summary>
        /// Guarda un recurso de internet desde una url especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <param name="localFile">Fichero locao</param>
        public static void DownlodadFileAsync(string url, FileInfo localFile)
        {
            Uri uri = CreateUri(url);
            DownlodadFileAsync(uri, localFile);
        }
        /// <summary>
        /// Guarda un recurso de internet desde una url especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="localFile">Fichero locao</param>
        public static void DownlodadFileAsync(Uri uri, FileInfo localFile)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(webClient_DownloadCompleted);
            webClient.DownloadFileAsync(uri, localFile.FullName);

        }

        /// <summary>
        /// Obtiene el texto de una web especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        public static void UrlToTextAsync(string url)
        {
            Uri uri = CreateUri(url);
            UrlToTextAsync(uri);
        }
        /// <summary>
        /// Obtiene el texto de una web especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        public static void UrlToTextAsync(Uri uri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCompleted);
            webClient.DownloadStringAsync(uri);
        }

        /// <summary>
        /// Obtiene un array de bytes de una web especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        public static void UrlToByteAsync(string url)
        {
            Uri uri = CreateUri(url);
            UrlToByteAsync(uri);
        }
        /// <summary>
        /// Obtiene un array de bytes de una web especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        public static void UrlToByteAsync(Uri uri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(webClient_DownloadCompleted);
            webClient.DownloadDataAsync(uri);
        }

        /// <summary>
        /// Obtiene una imagen desde una web especificada
        /// </summary>
        /// <param name="url">Url del recurso</param>
        public static void UrlToBitmapAsync(string url)
        {
            Uri uri = CreateUri(url);
            UrlToBitmapAsync(uri);
        }
        /// <summary>
        /// Obtiene una imagen desde una web especificada
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        public static void UrlToBitmapAsync(Uri uri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(webClient_DownloadCompleted);
            webClient.DownloadDataAsync(uri, typeof(Bitmap));
        }

        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <param name="url">Url del recurso</param>
        /// <param name="type">Tipo de objeto que se espera</param>
        public static void UrlToJSONAsync(string url, Type type)
        {
            Uri uri = CreateUri(url);
            UrlToJSONAsync(uri, type);
        }
        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="type">Tipo de objeto que se espera</param>
        public static void UrlToJSONAsync(Uri uri, Type type)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCompleted);
            webClient.DownloadStringAsync(uri, type);
        }
        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se espera</typeparam>
        /// <param name="uri">Uri del recurso</param>
        /// <returns>Objeto json obtenido</returns>
        public static Task<T> UrlToJSONAsyncTask<T>(Uri uri) where T : class
        {
            return UrlToJSONAsyncTask<T>(uri, Encoding.UTF8);
        }

        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se espera</typeparam>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="encoding">Codificacion del texto json</param>
        /// <returns>Objeto json obtenido</returns>
        public static async Task<T> UrlToJSONAsyncTask<T>(Uri uri, Encoding encoding) where T : class
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = encoding;
            var strJSON = await webClient.DownloadStringTaskAsync(uri);
            return Serialization.JSONDecode<T>(strJSON);
        }

        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se espera</typeparam>
        /// <param name="uri">Uri del recurso</param>
        /// <param name="encoding">Codificacion del texto json</param>
        /// <returns>Objeto json obtenido</returns>
        public static async Task<T> UrlToJSONAsyncTask<T>(string uri, Encoding encoding) where T : class
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = encoding;
            var strJSON = await webClient.DownloadStringTaskAsync(uri);
            return Serialization.JSONDecode<T>(strJSON);
        }
        /// <summary>
        /// Obtiene un objejo desde un json especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se espera</typeparam>
        /// <param name="uri">Uri del recurso</param>
        /// <returns>Objeto json obtenido</returns>
        public static Task<T> UrlToJSONAsyncTask<T>(string uri) where T : class
        {
            return UrlToJSONAsyncTask<T>(uri, Encoding.UTF8);
        }

        private static void webClient_DownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            object data = null;
            Type tipo = (Type)e.UserState;

            if (e is System.Net.DownloadDataCompletedEventArgs)
            {
                data = ((DownloadDataCompletedEventArgs)e).Result;
                if (tipo != null)
                {
                    if (tipo.Equals(typeof(Bitmap)))
                        data = Images.Convert.BytesToBitmap((byte[])data);
                    else
                        data = Serialization.ByteArrayToObject((byte[])data);
                }
            }
            else if (e is System.Net.DownloadStringCompletedEventArgs)
            {
                data = ((DownloadStringCompletedEventArgs)e).Result;

                if (tipo != null)
                    data = Serialization.JSONDecode(data.ToString(), tipo);
            }

            throwEvent(onWebProcessFinished, sender, new WebEventArgs(e.Error, e.Cancelled));
        }
        private static void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throwEvent(onWebProcessChange, sender, new WebEventArgs(e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive));
        }
        private static void throwEvent(WebEventHandler evento, object sender, WebEventArgs args)
        {
            if (evento == null)
                return;

            evento(sender, args);
        }
        #endregion
    }
}
