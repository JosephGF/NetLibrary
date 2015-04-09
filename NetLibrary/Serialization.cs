using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Xml;

namespace NetLibrary
{
    /// <summary>
    /// Parent class can be [Serializabe]
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Devuelve un string con el objeto convertido al formato Json
        /// </summary>
        /// <param name="obj">Objeto inicial a codificar</param>
        /// <returns>Cadena de caracteres en formato Json</returns>
        public virtual string JsonEncode(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
        /// <summary>
        /// Devuelve un string con el objeto convertido al formato Json
        /// </summary>
        /// <param name="obj">Objeto inicial a codificar</param>
        /// <returns>Cadena de caracteres en formato Json</returns>
        public static string JSONEncode(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        /// <summary>
        /// Devuelve un string con el objeto convertido al formato Json
        /// </summary>
        /// <param name="obj">Objeto inicial a codificar</param>
        /// <param name="typeResolver">Definición para resolver distintos tipos</param>
        /// <returns>Cadena de caracteres en formato Json</returns>
        public static string JSONEncode(object obj, JavaScriptTypeResolver typeResolver)
        {
            return new JavaScriptSerializer(typeResolver).Serialize(obj);
        }

        /// <summary>
        /// Convierte una cadena de caracteres JSon a un objeto del tipo especificado
        /// </summary>
        /// <param name="json">Cadena de caracteres con formato JSon</param>
        /// <param name="tipo">Tipo de objeto al que se corresponde el JSon</param>
        /// <returns>Objeto reslutado del JSon</returns>
        public virtual object JsonDecode(string json, Type tipo)
        {
            return new JavaScriptSerializer().Deserialize(json, tipo);
        }

        /// <summary>
        /// Convierte una cadena de caracteres JSon a un objeto del tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto al que se corresponde el JSON</typeparam>
        /// <param name="json">Cadena de caracteres con formato JSon</param>
        /// <returns>Objeto reslutado del JSon</returns>
        public virtual T JsonDecode<T>(string json, Type tipo) where T:class
        {
            return (T)JsonDecode(json, typeof(T));
        }

        /// <summary>
        /// Convierte una cadena de caracteres JSon a un objeto del tipo especificado
        /// </summary>
        /// <param name="json">Cadena de caracteres con formato JSon</param>
        /// <param name="tipo">Tipo de objeto al que se corresponde el JSON</param>
        /// <returns>Objeto reslutado del JSon</returns>
        public static object JSONDecode(string json, Type tipo)
        {
            return new JavaScriptSerializer().Deserialize(json, tipo);
        }

        /// <summary>
        /// Convierte una cadena de caracteres JSon a un objeto del tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto al que se corresponde el JSON</typeparam>
        /// <param name="json">Cadena de caracteres con formato JSon</param>
        /// <returns>Objeto reslutado del JSon</returns>
        public static T JSONDecode<T>(string json) where T:class
        {
            return (T)JSONDecode(json, typeof(T));
        }

        /// <summary>
        /// Devuelve una cadena de caracteres con el valor serializado y comprimido del objeto recibido como parametro
        /// </summary>
        /// <param name="data">Objeto origen para serializar</param>
        /// <returns>cadena de caracteres con el valor serializado y comprimido</returns>
        public static string Serializa(object data)
        {
            return new Serialization().Encode(data);
        }
        /// <summary>
        /// Devuelve una cadena de caracteres con el valor serializado y comprimido del objeto recibido como parametro
        /// </summary>
        /// <param name="data">Objeto origen para serializar</param>
        /// <returns>cadena de caracteres con el valor serializado y comprimido</returns>
        public virtual string Encode(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            BinaryFormatter formatter = new BinaryFormatter();
            byte[] dataBytes;

            /* Serialize the data to a byte array. */
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                dataBytes = stream.ToArray();
            }

            /* Compress the serialized data. */
            byte[] compressedBytes = Compress(dataBytes);


            /* Encrypt the data. */
            /* byte[] encryptedBytes = encryptionProvider.Encrypt
                 (compressedBytes, encryptionPassPhrase);
             */
            /* URL encode the result. */
            return HttpServerUtility.UrlTokenEncode(compressedBytes);
        }

        /// <summary>
        /// Descomprime y Deserializa una cadena pasada como string obteniedo el valor del objeto original
        /// </summary>
        /// <param name="value">Cadena de caracteres serializada</param>
        /// <returns>Objeto del tipo original</returns>
        public static object Deserializa(string data)
        {
            return new Serialization().Decode(data);
        }
        /// <summary>
        /// Descomprime y Deserializa una cadena pasada como string obteniedo el valor del objeto original
        /// </summary>
        /// <param name="value">Cadena de caracteres serializada</param>
        /// <returns>Objeto del tipo original</returns>
        public virtual object Decode(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            /* Decode the data. */
            byte[] decoded = HttpServerUtility.UrlTokenDecode(value);
            /* Decrypt the data. */
            //byte[] unencrypted = encryptionStrategy.Decrypt(decoded, encryptionPassPhrase);
            byte[] uncompressedBytes;

            /* Decompress the data. */
            uncompressedBytes = Decompress(decoded);

            /* Reinstantiate the object instance. */
            BinaryFormatter formatter = new BinaryFormatter();
            object deserialized;

            using (MemoryStream stream = new MemoryStream(uncompressedBytes))
            {
                deserialized = formatter.Deserialize(stream);
            }

            return deserialized;
        }

        /// <summary>
        /// Serializa un objeto en formato xml
        /// </summary>
        /// <param name="obj">Objeto que se deserializará</param>
        /// <returns>cadena que corresponde al objeto serializado</returns>
        public static string SerializaXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }
        /// <summary>
        /// Deserializa un string en formato xml a un objeto
        /// </summary>
        /// <param name="type">Tipo de objeto que se corresponde con el xml</param>
        /// <param name="xmlString">Cadena del xml</param>
        /// <returns>Objeto deserializado</returns>
        public static object DesseralizaXML(Type type, string xmlString)
        {
            StringReader reader = new StringReader(xmlString);
            XmlSerializer serializer = new XmlSerializer(type);
            object instance = serializer.Deserialize(reader);

            return instance;
        }

        /// <summary>
        /// Codifica la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <returns>Cadena codificada en base 64</returns>
        public static string EncodeBase64(string str)
        {
            return EncodeBase64(str, System.Text.Encoding.Default);
        }
        /// <summary>
        /// Codifica la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <param name="encoding">Codificacion del texto</param>
        /// <returns>Cadena codificada en base 64</returns>
        public static string EncodeBase64(string str, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(str);
            return System.Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodificación la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena inicial</param>
        /// <returns>Cadena decodificada</returns>
        public static string DecodeBase64(string str)
        {
            return DecodeBase64(str, System.Text.Encoding.Default);
        }
        /// <summary>
        /// Decodificación la cadena de texto a base64
        /// </summary>
        /// <param name="str">Cadena en base64</param>
        /// <param name="encoding">Codificacion del texto</param>
        /// <returns>Cadena decodificada</returns>
        public static string DecodeBase64(string str, Encoding encoding)
        {
            byte[] bytes = System.Convert.FromBase64String(str);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Comprime un array de byte
        /// </summary>
        /// <param name="raw">array original</param>
        /// <returns>array coprimido</returns>
        public byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }
        /// <summary>
        /// Descomprime un array de byte
        /// </summary>
        /// <param name="gzip">array de byte comprimido</param>
        /// <returns>array de byte original</returns>
        public byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        /// <summary>
        /// Convierte el objeto especificada a un array de bytes
        /// </summary>
        /// <param name="obj">Objeto a convertir</param>
        /// <returns>Array de bytes con la información del objeto</returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        /// <summary>
        /// Convierte el objeto especificada a un array de bytes
        /// </summary>
        /// <param name="obj">Objeto a convertir</param>
        /// <returns>Array de bytes con la información del objeto</returns>
        public byte[] ToByteArray(object obj)
        {
            return Serialization.ObjectToByteArray(obj);
        }

        /// <summary>
        /// Convierte un array de bytes al tipo object
        /// </summary>
        /// <param name="arrBytes">Array de bytes a transformar</param>
        /// <returns>Objeto resultado</returns>
        public static object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);
            return obj;
        }

        /// <summary>
        /// Realiza una copia de un stream a otro
        /// </summary>
        /// <param name="readStream">Stream a leer</param>
        /// <param name="writeStream">Stream resultado</param>
        static void CopyBuffered(Stream readStream, Stream writeStream)
        {
            byte[] bytes = new byte[128];
            int byteCount;

            while ((byteCount = readStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                writeStream.Write(bytes, 0, byteCount);
            }
        }
    }
}
