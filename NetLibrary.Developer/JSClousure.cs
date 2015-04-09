using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;

namespace NetLibrary.Developer
{
    public enum Optimization
    {
        WHITESPACE_ONLY,
        SIMPLE_OPTIMIZATIONS,
        ADVANCED_OPTIMIZATIONS
    }

    /// <summary>
    /// A C# wrapper around the Google Closure Compiler web service.
    /// </summary>
    public class GoogleClosure
    {
        private XmlDocument _xml;

        public XmlDocument GetRequest { get { return _xml; } }
        public string GetWarnings
        {
            get
            {
                var str = "";
                foreach (XmlNode warn in _xml.SelectNodes("warning"))
                {
                    str += warn.InnerText + "\r\n";
                }
                return str;
            }
        }

        public string OriginalSize
        {
            get { return _xml.SelectSingleNode("//originalSize").InnerText; }
        }

        public string originalGzipSize
        {
            get { return _xml.SelectSingleNode("//originalGzipSize").InnerText; }
        }

        public string compressedSize
        {
            get { return _xml.SelectSingleNode("//compressedSize").InnerText; }
        }

        public string compressedGzipSize
        {
            get { return _xml.SelectSingleNode("//compressedGzipSize").InnerText; }
        }

        public string compileTime
        {
            get { return _xml.SelectSingleNode("//compileTime").InnerText; }
        }

        private const string PostData = "js_code={0}&output_format=xml&output_info=compiled_code&output_info=warnings&output_info=errors&output_info=statistics&compilation_level={1}";
        private const string ApiEndpoint = "http://closure-compiler.appspot.com/compile";

        /// <summary>
        /// Compresses the specified file using Google's Closure Compiler algorithm.
        /// <remarks>
        /// The file to compress must be smaller than 200 kilobytes.
        /// </remarks>
        /// </summary>
        /// <param name="file">The absolute file path to the javascript file to compress.</param>
        /// <returns>A compressed version of the specified JavaScript file.</returns>
        public string Compress(string file, Optimization optimization = Optimization.SIMPLE_OPTIMIZATIONS)
        {
            string source = File.ReadAllText(file);
            _xml = CallApi(source);
            try
            {
                return _xml.SelectSingleNode("//compiledCode").InnerText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Calls the API with the source file as post data.
        /// </summary>
        /// <param name="source">The content of the source file.</param>
        /// <returns>The Xml response from the Google API.</returns>
        private static XmlDocument CallApi(string source, Optimization optimization = Optimization.SIMPLE_OPTIMIZATIONS)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("content-type", "application/x-www-form-urlencoded");
                string data = string.Format(PostData, HttpUtility.UrlEncode(source), optimization.ToString());
                string result = client.UploadString(ApiEndpoint, data);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                return doc;
            }
        }
    }
}