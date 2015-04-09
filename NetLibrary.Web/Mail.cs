using NetLibrary.Debugger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NetLibrary.Web
{
    public class Mail
    {
        /// <summary>
        /// Servidores smtp pre-configurados
        /// </summary>
        public enum SmtpServer
        {
            /// <summary>
            /// Host de Gmail (smtp.gmail.com)
            /// </summary>
            Gmail,
            /// <summary>
            /// Host de Hotmail (smtp.live.com)
            /// </summary>
            Hotmail,
            /// <summary>
            /// Host de Outlook (smtp-mail.outlook.com)
            /// </summary>
            Outlook,
            /// <summary>
            /// Host de Yahoo (smtp.mail.yahoo.com)
            /// </summary>
            Yahoo
        }
        /// <summary>
        /// Tipos de ficheros adjuntos aceptados
        /// </summary>
        public enum ContentTypes
        {
            Octet, Pdf, Rtf, Zip, Soap_Xml, Gif, Jpg, Tiff, Html, Plain, Xml, RichText
        }

        /// <summary>
        /// Nombre de destinatario que aparecerá
        /// </summary>
        public static string SenderName { get; set; }
        /// <summary>
        /// Constructor estático (inicializa propiedades estáticas)
        /// </summary>
        static Mail()
        {
            Mail.SenderName = System.Windows.Forms.Application.ProductName;
        }

        /// <summary>
        /// Email que se mostrará como cuenta que envia el correo (Puede no funcionar dependiendo del servicio), por defecto ["soporte@geonet.es"]
        /// </summary>
        public string SenderEmail { get; set; }
        private SmtpClient _smtp;
        private MailMessage _message;
        public EventHandler<AsyncCompletedEventArgs> OnSendComplete;

        /// <summary>
        /// Crea una instancia de la clase
        /// </summary>
        /// <param name="email">Email desde el que se enviará el correo</param>
        /// <param name="password">Contraseña que se enviara</param>
        /// <param name="host">Host del proveedor del email</param>
        public Mail(string email, string password, string host)
        {
            initialize();
            this._smtp = CreateSmtpClient(email, password, host);
        }

        /// <summary>
        /// Crea una instancia de la clase
        /// </summary>
        /// <param name="email">Email desde el que se enviará el correo</param>
        /// <param name="password">Contraseña que se enviara</param>
        /// <param name="password"></param>
        /// <param name="server">Nombre del proveedor de mail</param>
        public Mail(string email, string password, SmtpServer server)
        {
            initialize();
            this._smtp = CreateSmtpClient(email, password, server);
        }

        /// <summary>
        /// Crea una instancia de la clase
        /// </summary>
        /// <param name="client">Datos del proveedor del email</param>
        public Mail(SmtpClient client)
        {
            initialize();
            this._smtp = client;
        }

        private void initialize()
        {
            this.SenderEmail = "suppot@netlibrary.es";
        }

        private SmtpClient CreateSmtpClient(string email, string password, SmtpServer server)
        {
            return this.CreateSmtpClient(email, password, this.getHost(server));
        }

        private SmtpClient CreateSmtpClient(string email, string password, string host)
        {
            var smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.Timeout = 20000;
            return smtp;
        }

        private string getHost(SmtpServer server)
        {
            switch (server)
            {
                case SmtpServer.Gmail:
                    return "smtp.gmail.com";
                case SmtpServer.Hotmail:
                    return "smtp.live.com";
                case SmtpServer.Outlook:
                    return "smtp-mail.outlook.com";
                case SmtpServer.Yahoo:
                    return "smtp.mail.yahoo.com";
                default:
                    throw new ArgumentException("SmtpServer Host no está definido");
            }
        }

        private string GetContentTypeString(ContentTypes content)
        {
            switch (content)
            {
                case ContentTypes.Octet:
                    return "application/octet-stream";
                case ContentTypes.Pdf:
                    return "application/pdf";
                case ContentTypes.Rtf:
                    return "application/rtf";
                case ContentTypes.Zip:
                    return "application/zip";
                case ContentTypes.Soap_Xml:
                    return "application/soap+xml";
                case ContentTypes.Gif:
                    return "image/gif";
                case ContentTypes.Jpg:
                    return "image/jpeg";
                case ContentTypes.Tiff:
                    return "image/tiff";
                case ContentTypes.Html:
                    return "text/html";
                case ContentTypes.Plain:
                    return "text/plain";
                case ContentTypes.Xml:
                    return "text/xml";
                case ContentTypes.RichText:
                    return "text/richtext";
                default:
                    return "application/octet-stream";
            }

        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="file">Path del fichero</param>
        /// <returns></returns>
        public Mail AttachFile(string file)
        {
            this._message.Attachments.Add(new Attachment(file));
            return this;
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará como texto plano ("attached.txt")</param>
        /// <returns></returns>
        public Mail AttachFile(System.IO.MemoryStream sFile)
        {
            return this.AttachFile(sFile, "attached.txt", ContentTypes.Plain);
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail AttachFile(System.IO.Stream sFile, string name, ContentTypes contentType)
        {
            return this.AttachFile(sFile, name, GetContentTypeString(contentType));
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail AttachFile(System.IO.Stream sFile, string name, string contentType)
        {
            this._message.Attachments.Add(new Attachment(sFile, name, contentType));
            return this;
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail AttachFile(System.IO.Stream sFile, ContentTypes contentType)
        {
            return this.AttachFile(sFile, "attached", contentType);
        }

        /// <summary>
        /// Envia el email especificado
        /// </summary>
        /// <param name="async">Indica si se envia de forma asincrona o no</param>
        /// <returns></returns>
        public Boolean Send(bool async = false)
        {
            try
            {
                if (this._smtp == null)
                    throw new ArgumentNullException("Smtp", "No se ha definido ningun servidor de correo.");

                if (this._message == null)
                    throw new ArgumentNullException("Message", "No se ha definido ningun mensaje, debe llamar antes a la función 'Message'");

                this._message.Sender = new MailAddress(this.SenderEmail, Mail.SenderName, Encoding.UTF8);
                this._smtp.SendCompleted += Smtp_SendCompleted;

                if (async)
                    this._smtp.SendAsync(this._message, this._message);
                else
                    this._smtp.Send(this._message);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Se produjo un error al intentar enviar un email: " + e.Message);

                if (OnSendComplete != null)
                {
                    AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(e, false, this._message);
                    OnSendComplete(this._smtp, args);
                }
                return false;
            }
        }

        void Smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (OnSendComplete != null)
                OnSendComplete(sender, e);

            if (e.Error == null)
                Debug.WriteLine(e.Error);
            else
                Console.WriteLine("Email has been succesfully sent.");
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <returns></returns>
        public Mail Message(string body)
        {
            return this.Message(null, body, null, null, null, MailPriority.Normal);
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <returns></returns>
        public Mail Message(string toAddress, string body)
        {
            return this.Message(toAddress, body, null, null, null, MailPriority.Normal);
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <param name="priority">Prioridad del email</param>
        /// <returns></returns>
        public Mail Message(string toAddress, string body, MailPriority priority)
        {
            return this.Message(toAddress, body, null, null, null, priority);
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <param name="subject">Asunto del email</param>
        /// <returns></returns>
        public Mail Message(string toAddress, string body, string subject)
        {
            return this.Message(toAddress, body, subject, null, null, MailPriority.Normal);
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <param name="subject">Asunto del email</param>
        /// <param name="priority">Prioridad del email</param>
        /// <returns></returns>
        public Mail Message(string toAddress, string body, string subject, MailPriority priority)
        {
            return this.Message(toAddress, body, subject, null, null, priority);
        }

        /// <summary>
        /// Define el mensaje del email
        /// </summary>
        /// <param name="toAddress">Direccion a la que va dirigida (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="body">Cuerpo del email (Se enviará como html)</param>
        /// <param name="subject">Asunto del email</param>
        /// <param name="copyTo">Con copia a... (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="hideCopyTo">Con copia oculta a... (Varios destinatarios separados por ',' -> d1@dir.com,d2@dir.com,...)</param>
        /// <param name="priority">Prioridad del email</param>
        /// <returns></returns>
        public Mail Message(string toAddress, string body, string subject, string copyTo, string hideCopyTo, MailPriority priority)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(this.SenderEmail);
            msg.Sender = new MailAddress(this.SenderEmail);

            msg.Subject = subject;
            msg.Priority = priority;
            msg.Body = body;

            if (copyTo != null && copyTo.Length > 0)
                msg.CC.Add(copyTo);

            if (hideCopyTo != null && hideCopyTo.Length > 0)
                msg.Bcc.Add(hideCopyTo);

            msg.IsBodyHtml = true;
            msg.To.Add(toAddress);

            this._message = msg;
            return this;
        }
    }

    public class Mail2
    {
        /// <summary>
        /// Servidores smtp pre-configurados
        /// </summary>
        /// 
        public enum SmtpServer
        {
            /// <summary>
            /// Host de Gmail (smtp.gmail.com)
            /// </summary>
            Gmail,
            /// <summary>
            /// Host de Hotmail (smtp.live.com)
            /// </summary>
            Hotmail,
            /// <summary>
            /// Host de Outlook (smtp-mail.outlook.com)
            /// </summary>
            Outlook,
            /// <summary>
            /// Host de Yahoo (smtp.mail.yahoo.com)
            /// </summary>
            Yahoo
        }
        /// <summary>
        /// Tipos de ficheros adjuntos aceptados
        /// </summary>
        public enum ContentTypes
        {
            Octet, Pdf, Rtf, Zip, Soap_Xml, Gif, Jpg, Tiff, Html, Plain, Xml, RichText
        }

        public EventHandler<AsyncCompletedEventArgs> OnSendComplete;
        private SmtpClient _smtp;
        private List<String> _to  = new List<String>();
        private List<String> _cc  = new List<String>();
        private List<String> _cco = new List<String>();
        private string _subject;

        private MailMessage _message;

        public Mail2(string sender, string password, SmtpServer server)
        {
            this.CreateSmtpClient(sender, password, server);
            this._message = new MailMessage();
        }
        public Mail2(string sender, string password, string host)
        {
            this.CreateSmtpClient(sender, password, host);
            this._message = new MailMessage();
        }

        private SmtpClient CreateSmtpClient(string email, string password, SmtpServer server)
        {
            return this.CreateSmtpClient(email, password, this.getHost(server));
        }
        private SmtpClient CreateSmtpClient(string email, string password, string host)
        {
            var smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.Timeout = 20000;
            return smtp;
        }
        private string getHost(SmtpServer server)
        {
            switch (server)
            {
                case SmtpServer.Gmail:
                    return "smtp.gmail.com";
                case SmtpServer.Hotmail:
                    return "smtp.live.com";
                case SmtpServer.Outlook:
                    return "smtp-mail.outlook.com";
                case SmtpServer.Yahoo:
                    return "smtp.mail.yahoo.com";
                default:
                    throw new ArgumentException("SmtpServer Host no está definido");
            }
        }
        private string GetContentTypeString(ContentTypes content)
        {
            switch (content)
            {
                case ContentTypes.Octet:
                    return "application/octet-stream";
                case ContentTypes.Pdf:
                    return "application/pdf";
                case ContentTypes.Rtf:
                    return "application/rtf";
                case ContentTypes.Zip:
                    return "application/zip";
                case ContentTypes.Soap_Xml:
                    return "application/soap+xml";
                case ContentTypes.Gif:
                    return "image/gif";
                case ContentTypes.Jpg:
                    return "image/jpeg";
                case ContentTypes.Tiff:
                    return "image/tiff";
                case ContentTypes.Html:
                    return "text/html";
                case ContentTypes.Plain:
                    return "text/plain";
                case ContentTypes.Xml:
                    return "text/xml";
                case ContentTypes.RichText:
                    return "text/richtext";
                default:
                    return "application/octet-stream";
            }

        }


        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="file">Path del fichero</param>
        /// <returns></returns>
        public Mail2 AttachFile(string file)
        {
            this._message.Attachments.Add(new Attachment(file));
            return this;
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará como texto plano ("attached.txt")</param>
        /// <returns></returns>
        public Mail2 AttachFile(System.IO.MemoryStream sFile)
        {
            return this.AttachFile(sFile, "attached.txt", ContentTypes.Plain);
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail2 AttachFile(System.IO.Stream sFile, string name, ContentTypes contentType)
        {
            return this.AttachFile(sFile, name, GetContentTypeString(contentType));
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail2 AttachFile(System.IO.Stream sFile, string name, string contentType)
        {
            this._message.Attachments.Add(new Attachment(sFile, name, contentType));
            return this;
        }

        /// <summary>
        /// Adjunta el fichero especificado
        /// </summary>
        /// <param name="sFile">MemoryStream del fichero qeu se adjuntará</param>
        /// <param name="name">Nombre del fichero adjunto</param>
        /// <param name="contentType">Tipo de fichero</param>
        /// <returns></returns>
        public Mail2 AttachFile(System.IO.Stream sFile, ContentTypes contentType)
        {
            return this.AttachFile(sFile, "attached", contentType);
        }

        public Mail2 To(string adress)
        {
            this._message.To.Add(new MailAddress(adress));
            return this;
        }

        public Mail2 CC(string adress)
        {
            this._message.CC.Add(new MailAddress(adress));
            return this;
        }

        public Mail2 BCC(string adress)
        {
            this._message.Bcc.Add(new MailAddress(adress));
            return this;
        }

        public Mail2 Message(string body)
        {
            this._message.Body = body;
            return this;
        }

        public Mail2 Message(string subject, string body)
        {
            this._message.Subject = subject;
            this._message.Body = body;
            return this;
        }

        public Mail2 Message(string subject, string body, bool isBodyHtml)
        {
            this._message.Subject = subject;
            this._message.Body = body;
            this._message.IsBodyHtml = isBodyHtml;
            return this;
        }
    }
}
