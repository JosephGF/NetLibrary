using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Debugger
{
    [DataContract]
    public class DebugErrorData
    {
        [IgnoreDataMember]
        public Bitmap Screenshot { get; protected set; }

        [IgnoreDataMember]
        public Exception Exception { get; protected set; }

        [IgnoreDataMember]
        public Information SystemInformation { get; set; }

        [DataMember(Name = "StackTrace")]
        public string StackTrace
        {
            get
            {
                return this.Exception.StackTrace ?? "";
            }
        }

        [DataMember(Name = "Details")]
        public string Details
        {
            get
            {
                StringBuilder msgError = new StringBuilder();
                Exception exAux = this.Exception;

                while (exAux != null)
                {
                    msgError.AppendLine(exAux.Message);
                    exAux = exAux.InnerException;
                }

                return msgError.ToString();
            }
        }

        [DataMember(Name = "Screenshot")]
        public string ScreenShot64
        {
            get
            {
                return EncodeBase64(this.Screenshot) ?? "";
            }
        }

        [DataMember(Name = "SystemInformation")]
        public string SystemInformationStr { get { return this.SystemInformation.ToString(); } }

        [Obsolete("Only use for serializated")]
        public DebugErrorData() { }

        public DebugErrorData(Exception ex)
        {
            this.Screenshot = ScreenCapture();
            this.Exception = ex;
            this.SystemInformation = new Information();
        }

        public override string ToString()
        {
            StringBuilder strInfo = new StringBuilder();

            strInfo.AppendLine(this.Exception.GetType().FullName);
            strInfo.AppendLine("----------------------------------------------");
            strInfo.Append("Details");
            strInfo.Append(" -> ");
            strInfo.AppendLine(this.Details);
            strInfo.AppendLine("----------------------------------------------");

            strInfo.Append("StackTrace");
            strInfo.Append(" -> ");
            strInfo.AppendLine(this.StackTrace);

            return strInfo.ToString();
        }

        /// <summary>
        /// Devuelve el resultado como XML
        /// </summary>
        public string XMLData
        {
            get
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    DataContractSerializerSettings settings = new DataContractSerializerSettings();
                    settings.SerializeReadOnlyTypes = true;
                    DataContractSerializer ser = new DataContractSerializer(typeof(DebugErrorData), settings);
                    ser.WriteObject(ms, this);

                    ms.Position = 0;
                    using (var streamReader = new StreamReader(ms))
                    {
                        string result = streamReader.ReadToEnd();
                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve el resultado como JSON
        /// </summary>
        public string JSONData
        {
            get
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();// System.Runtime.Serialization.Json.DataContractJsonSerializer();// new DataContractSerializerSettings();
                    settings.SerializeReadOnlyTypes = true;
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DebugErrorData), settings);
                    ser.WriteObject(ms, this);

                    ms.Position = 0;
                    using (var streamReader = new StreamReader(ms))
                    {
                        string result = streamReader.ReadToEnd();
                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve el resultado como HTML
        /// </summary>
        public string HTMLData
        {
            get
            {
                return @"<html>
    <head>
        <title>[TITLE]</title>
    </head>
    <body>
        <h2 style='color:red'>[TITLE]</h2>
        <h3>[MESSAGE]</h3>
        <p>[DETAILS]</p>
        <p>[STACKTRACE]</p>
        <img src='data:image/jpg;base64,[IMAGE]' style='max-width:100%;'/>
    </body>
</html>"
                    .Replace("[TITLE]", this.SystemInformation.ApplicationName)
                    .Replace("[MESSAGE]", this.Exception.GetType().FullName)
                    .Replace("[DETAILS]", this.Details.Replace(Environment.NewLine, "<br />"))
                    .Replace("[STACKTRACE]", this.StackTrace.Replace(Environment.NewLine, "<br />"))
                    .Replace("[IMAGE]", this.ScreenShot64);

            }
        }

        private static string EncodeBase64(Bitmap bitmap)
        {
            ImageConverter convert = new ImageConverter();
            byte[] bytes = (byte[])convert.ConvertTo(bitmap, typeof(byte[]));
            return System.Convert.ToBase64String(bytes);
        }
        private static Bitmap ScreenCapture()
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format16bppRgb555);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }
    }
}
