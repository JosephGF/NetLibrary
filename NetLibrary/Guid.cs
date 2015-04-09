using System;
using System.Text.RegularExpressions;

namespace NetLibrary
{
    /// <summary>
    /// Genera un elemento GUID basado en una fecha con el siguiente formato
    /// checksum(2)ss(2)mm(2)hh(2)-MM(2)dd(2)-4yyy(3:hex)-ms(4:hex)-xxxxxxxxxxxx(12:hex)
    /// </summary>
    public class GuidDate
    {
        /// <summary>
        /// Valor máximo para el float pasado como dato
        /// </summary>
        public const long MAX_DATA = 281474976710655;
        /// <summary>
        /// Año máximo soportado
        /// </summary>
        public const short MAX_YEARS = 4095;
        
        /// <summary>
        /// Version del generador de GUID
        /// </summary>
        public const string VERSION = "2.1";
        /// <summary>
        /// Define la clave por defecto para calcular el CRC8 (checksum) de un GuidDate
        /// </summary>
        public const string DEFAULT_CRCKEY = "";

        /// <summary>
        /// Indica si los valores de los dos objetos System.Guid especificados no son iguales.
        /// </summary>
        /// <param name="a">Primer objeto que se va a comparar.</param>
        /// <param name="b">Segundo objeto que se va a comparar.</param>
        /// <returns>Es true si a y b no son iguales; de lo contrario, es false.</returns>
        public static bool operator !=(GuidDate a, GuidDate b)
        {
            return a.ToString() != b.ToString();
        }
        /// <summary>
        /// Indica si los valores de los dos objetos System.Guid especificados son iguales.
        /// </summary>
        /// <param name="a">Primer objeto que se va a comparar.</param>
        /// <param name="b">Segundo objeto que se va a comparar.</param>
        /// <returns>true si a y b son iguales; en caso contrario, false.</returns>
        public static bool operator ==(GuidDate a, GuidDate b)
        {
            return a.ToString() == b.ToString();
        }

        public static explicit operator Guid(GuidDate guid)
        {
            return guid.Guid;
        }

        public static explicit operator GuidDate(Guid guid)
        {
            return new GuidDate(guid);
        }

        /// <summary> Compara esta instancia con un objeto System.Guid especificado y devuelve una indicación de los valores relativos. </summary>
        /// <param name="value">Objeto que se va a comparar con esta instancia.</param>
        /// <returns>
        ///     Número con signo que indica los valores relativos de esta instancia y value.
        ///     Valor devuelto Descripción Un entero negativo Esta instancia es menor que
        ///     value. Zero Esta instancia es igual que value. Un entero positivo. Esta instancia
        ///     es mayor que value.
        /// </returns>
        public int CompareTo(GuidDate value)
        {
            return this.Guid.CompareTo(value.Guid);
        }

        /// <summary>
        ///     Compara esta instancia con un objeto System.Guid especificado y devuelve
        ///     una indicación de los valores relativos.
        /// </summary>
        /// <param name="value">Objeto que se va a comparar con esta instancia.</param>
        /// <returns>Número con signo que indica los valores relativos de esta instancia y value.
        ///     Valor devuelto Descripción Un entero negativo Esta instancia es menor que
        ///     value. Zero Esta instancia es igual que value. Un entero positivo. Esta instancia
        ///     es mayor que value.</returns>
        public int CompareTo(Guid value)
        {
            return this.Guid.CompareTo(value);
        }

        /// <summary>
        ///     Compara esta instancia con un objeto especificado y devuelve una indicación  de los valores relativos.
        /// </summary>
        /// <param name="value">Objeto que se va a comparar o null.</param>
        /// <returns>
        ///  Número con signo que indica los valores relativos de esta instancia y value.
        ///     Valor devuelto Descripción Un entero negativo Esta instancia es menor que
        ///     value. Zero Esta instancia es igual que value. Un entero positivo. Esta instancia
        ///     es mayor que value o bien value es null.</returns>
        public int CompareTo(object value)
        {
            return this.Guid.CompareTo(value);
        }

        /// <summary>
        /// Devuelve un valor que indica si esta instancia y un objeto System.Guid especificado representan el mismo valor.
        /// </summary>
        /// <param name="g"> Objeto que se va a comparar con esta instancia.</param>
        /// <returns>true si g es igual a esta instancia; en caso contrario, false.</returns>
        public bool Equals(GuidDate g)
        {
            return this.Guid.Equals(g.Guid);
        }

        /// <summary>
        ///  Devuelve un valor que indica si esta instancia y un objeto System.Guid especificado representan el mismo valor.
        /// </summary>
        /// <param name="g"> Objeto que se va a comparar con esta instancia.</param>
        /// <returns>true si g es igual a esta instancia; en caso contrario, false.</returns>
        public bool Equals(Guid g)
        {
            return this.Guid.Equals(g);
        }

        /// <summary>
        /// Devuelve un valor que indica si esta instancia equivale a un objeto especificado.
        /// </summary>
        /// <param name="o">Objeto que se va a comparar con esta instancia.</param>
        /// <returns>true si o es un System.Guid con el mismo valor que esta instancia; en caso contrario, false.</returns>
        public override bool Equals(object o)
        {
            return this.Guid.Equals(o);
        }

        /// <summary>
        /// Devuelve una matriz de bytes de 16 elementos que contiene el valor de esta instancia.
        /// </summary>
        /// <returns>Matriz de bytes de 16 elementos.</returns>
        public byte[] ToByteArray()
        {
            return this.Guid.ToByteArray();
        }

        protected static Random _rnd = new Random();

        /// <summary>
        /// Obtiene el Guid Actual
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// Obtiene la fecha asociada al GUID
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return GuidDate.GuidToDate(this.Guid, this.CRCKey);
            }
        }

        /// <summary>
        /// Obtiene el valor asociado al GUID (Será un valor radom si no se especificó ninguno)
        /// </summary>
        public long Data
        {
            get
            {
                long data;
                GuidDate.GuidToDate(this.Guid, out data, this.CRCKey);
                return data;
            }
        }

        /// <summary>
        /// Indica si es válido el checsun del guid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.Validate();
            }
        }

        /// <summary>
        /// Obtiene la key actual usada para generar el CRC8
        /// </summary>
        public string CRCKey { get; protected set; }

        /// <summary>
        /// Devuelve una cadena que representa el GUID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Guid.ToString();
        }

        /// <summary>
        ///     Returns a string representation of the value of this System.Guid instance, according to the provided format specifier.
        /// </summary>
        /// <returns></returns>
        /// <param name="format">
        ///     A single format specifier that indicates how to format the value of this
        ///     System.Guid. The format parameter can be "N", "D", "B", "P", or "X". If format
        ///     is null or an empty string (""), "D" is used.
        /// </param>
        /// <returns>The value of this System.Guid, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
        /// <exception cref="System.FormatException">The value of format is not null, an empty string (""), "N", "D", "B", "P", or "X"</exception>
        public string ToString(string format)
        {
            return this.Guid.ToString(format);
        }

        /// <summary>
        /// Valida que el checksun del GUID sea correcto
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            return GuidDate.Validate(this.Guid, this.CRCKey);
        }

        /// <summary>
        /// Valida que el checksun del GUID sea correcto
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <param name="crcKey">Key para calcular el checksun</param>
        /// <returns>Indica si el checksun es correcto</returns>
        public static bool Validate(string guid, string crcKey = GuidDate.DEFAULT_CRCKEY)
        {
            return Validate(guid, "U", crcKey);
        }
        /// <summary>
        /// Valida que el checksun del GUID sea correcto
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <param name="crcKey">Key para calcular el checksun</param>
        /// <returns>Indica si el checksun es correcto</returns>
        public static bool Validate(string guid, string format = "U", string crcKey = GuidDate.DEFAULT_CRCKEY)
        {
            if ("U".Equals(format))
                format = GuidDate.DetectedFormat(guid);

            if (!GuidDate.GuidFormatValidate(guid, format)) 
                return false;


            string guidAux = new Regex(@"[^0-9a-fA-F]").Replace(guid, "");
            string guidOrigin = guidAux.Substring(2, guidAux.Length - 2);
            string guidCRC = guidAux.Substring(0, 2);

            string crc = CreateCRC(guidOrigin, crcKey);
            return crc == guidCRC;
        }
        /// <summary>
        /// Valida que el checksun del GUID sea correcto
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <param name="crcKey">Key para calcular el checksun</param>
        /// <returns>Indica si el checksun es correcto</returns>
        public static bool Validate(Guid guid, string crcKey = GuidDate.DEFAULT_CRCKEY) {
            return Validate(guid.ToString("D"), "D", crcKey);
        }

        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(Guid guid)
        {
            this.Guid = guid;
            this.CRCKey = DEFAULT_CRCKEY;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(Guid guid, string crcKey) : this(guid)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(string guid)
        {
            this.CRCKey = DEFAULT_CRCKEY;
            this.Guid = new Guid(guid);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(string guid, string crcKey) : this(guid)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(byte[] b)
        {
            this.Guid = new Guid(b);
            this.CRCKey = DEFAULT_CRCKEY;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(byte[] b, string crcKey) : this(b)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate()
        {
            this.Guid = GuidDate.NewGuid(DateTime.Now).Guid;
            this.CRCKey = CRCKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(decimal data)
        {
            this.Guid = GuidDate.NewGuid(DateTime.Now, data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="auxInformation">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(decimal data, string crcKey) : this(data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(short data)
        {
            this.Guid = GuidDate.NewGuid(DateTime.Now, data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(short data, string crcKey) : this(data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(int data)
        {
            this.Guid = GuidDate.NewGuid(DateTime.Now, data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(int data, string crcKey) : this(data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(long data)
        {
            this.Guid = GuidDate.NewGuid(DateTime.Now, data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(long data, string crcKey) : this(data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date)
        {
            long i = _rnd.Next();
            this.Guid = GuidDate.NewGuid(date, i).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, string crcKey) : this(date)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, decimal data)
        {
            this.Guid = GuidDate.NewGuid(date, (long)data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, decimal data, string crcKey) : this(date, data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, short data)
        {
            this.Guid = GuidDate.NewGuid(date, (long)data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, short data, string crcKey) : this(date, data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, int data)
        {
            this.Guid = GuidDate.NewGuid(date, (long)data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, int data, string crcKey) : this(date, data)
        {
            this.CRCKey = crcKey;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, long data)
        {
            this.Guid = GuidDate.NewGuid(date, data).Guid;
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public GuidDate(DateTime date, long data, string crcKey) : this(date, data)
        {
            this.CRCKey = crcKey;
        }

        protected static string CreateCRC(string guid, string crcKey = GuidDate.DEFAULT_CRCKEY)
        {
            string guidAux = new Regex(@"[^0-9a-fA-F]").Replace(guid, "");
            int i, j, c, CRC = 0, genPoly = 0x107;
            char[] charArr = (crcKey + guidAux).ToCharArray();
            string[] toHexArr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            for (j = 0; j < charArr.Length; j++)
            {
                c = (int)charArr[j];
                CRC ^= c;
                for (i = 0; i < 8; i++)
                {
                    if (CRC >= 0x80)
                        CRC = (CRC << 1) ^ genPoly;
                    else
                        CRC <<= 1;
                }
                CRC &= 0xff;
            }

            string crc = toHexArr[CRC >> 4] + toHexArr[CRC & 0xf];
            return crc;
        }

        /// <summary>
        /// Regenera el Checsun del Guid indicado
        /// </summary>
        /// <param name="data">Guid</param>
        /// <param name="crcKey"></param>
        /// <returns></returns>
        public static GuidDate RegenerateGuid(string data, string crcKey = GuidDate.DEFAULT_CRCKEY)
        {
            string guidOrigin = data.Substring(2, data.Length - 2);
            string aux = CreateCRC(guidOrigin, crcKey) + guidOrigin;
            return new GuidDate(aux);
        }

        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid()
        {
            return GuidDate.NewGuid(GuidDate.DEFAULT_CRCKEY);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(string crcKey)
        {
            return GuidDate.NewGuid(DateTime.Now, GuidDate._rnd.Next(), crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(decimal data)
        {
            return GuidDate.NewGuid(DateTime.Now, data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(decimal data, string crcKey)
        {
            return GuidDate.NewGuid(DateTime.Now, data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(short data)
        {
            return GuidDate.NewGuid(DateTime.Now, data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(short data, string crcKey)
        {
            return GuidDate.NewGuid(DateTime.Now, data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(int data)
        {
            return GuidDate.NewGuid(DateTime.Now, data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(int data, string crcKey)
        {
            return GuidDate.NewGuid(DateTime.Now, data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(long data)
        {
            return GuidDate.NewGuid(DateTime.Now, data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(long data, string crcKey)
        {
            return GuidDate.NewGuid(DateTime.Now, data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date)
        {
            return GuidDate.NewGuid(date, GuidDate.DEFAULT_CRCKEY);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, string crcKey)
        {
            long i = _rnd.Next();
            return GuidDate.NewGuid(date, i, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, decimal data)
        {
            return GuidDate.NewGuid(date, (long)data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, decimal data, string crcKey)
        {
            return GuidDate.NewGuid(date, (long)data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, short data)
        {
            return GuidDate.NewGuid(date, (long)data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, short data, string crcKey)
        {
            return GuidDate.NewGuid(date, (long)data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, int data)
        {
            return GuidDate.NewGuid(date, (long)data);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, int data, string crcKey)
        {
            return GuidDate.NewGuid(date, (long)data, crcKey);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, long data)
        {
            return GuidDate.NewGuid(date, data, GuidDate.DEFAULT_CRCKEY);
        }
        /// <summary>
        /// Crea un guid basado en la fecha especificada
        /// </summary>
        /// <param name="date">Fecha sobre la que se generará el guid</param>
        /// <param name="data">Información auxiliar - si el valor es mayor de 281474976710655 (0xFFFFFFFFFFFF) se truncará</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Cadena que representa el guid creado</returns>
        public static GuidDate NewGuid(DateTime date, long data, string crcKey)
        {
            if (date.Year > GuidDate.MAX_YEARS)
                throw new InvalidDateGuidException("El año no puede superar 4095 (0xFFF)", date, data);

            if (data > GuidDate.MAX_DATA)
                throw new InvalidDateGuidException("El valor no puede superar " + GuidDate.MAX_DATA + " (0xFFFFFFFFFFFF)", date, data);

            string auxGuid = String.Format("{0}{1}4{2}{3}{4}", date.ToString("ssmmHH"), date.ToString("MMdd"), date.Year.ToString("x").PadLeft(3).Substring(0, 3), date.Millisecond.ToString("x").PadLeft(4, '0'), data.ToString("x").PadLeft(12, '0').Substring(0, 12));

            auxGuid = CreateCRC(auxGuid, crcKey) + auxGuid;
            return new GuidDate(auxGuid, crcKey);
        }

        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="guid">Cadena que representa el guid</param>
        /// <param name="auxInformation">Devolverá la información auxiliar contenida</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(string guid, out long dataOut, string crcKey)
        {
            DateTime date = DateTime.MinValue;
            string error = String.Empty;
            long data = 0;
            if (!GuidDate.Validate(guid, crcKey))
               error = "El guid no tiene un formato correcto o no coincide el checksum";

            string guidAux = new Regex(@"[^0-9a-fA-F]").Replace(guid, "");

            try
            {
                int h, m, s, d, M, y, ms;
                s = Convert.ToInt32(guidAux.Substring(2, 2));
                m = Convert.ToInt32(guidAux.Substring(4, 2));
                h = Convert.ToInt32(guidAux.Substring(6, 2));
                M = Convert.ToInt32(guidAux.Substring(8, 2));
                d = Convert.ToInt32(guidAux.Substring(10, 2));
                y = Convert.ToInt32(guidAux.Substring(13, 3), 16);
                ms = Convert.ToInt32(guidAux.Substring(16, 4), 16);
                dataOut = data = Convert.ToInt64(guidAux.Substring(20, 12), 16);
                date = new DateTime(y, M, d, h, m, s, ms);
            }
            catch (Exception ex)
            {
                throw new InvalidDateGuidException(ex.Message, ex, date, data);
            }

            if (!String.IsNullOrEmpty(error))
                throw new InvalidDateGuidException(error, date, data);

            return date;
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="strGuid">Cadena que representa el guid</param>
        /// <param name="auxInformation">Devolverá la información auxiliar contenida</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(string strGuid, out long auxInformation)
        {
            return GuidDate.GuidToDate(strGuid, out auxInformation, GuidDate.DEFAULT_CRCKEY);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="strGuid">Cadena que representa el guid</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(string strGuid)
        {
            long aux;
            return GuidDate.GuidToDate(strGuid, out aux);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="strGuid">Cadena que representa el guid</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(string strGuid, string crcKey)
        {
            long aux;
            return GuidDate.GuidToDate(strGuid, out aux, crcKey);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="dataOut">Devolverá la información auxiliar contenida</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(Guid guid, out long dataOut)
        {
            return GuidDate.GuidToDate(guid.ToString(), out dataOut);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="dataOut">Devolverá la información auxiliar contenida</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(Guid guid, out long dataOut, string crcKey)
        {
            return GuidDate.GuidToDate(guid.ToString(), out dataOut, crcKey);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(Guid guid)
        {
            long aux;
            return GuidDate.GuidToDate(guid.ToString(), out aux);
        }
        /// <summary>
        /// Obtiene la fecha a partir de un Guid basado en la misma
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="crcKey">Key usada para calcular el CRC</param>
        /// <returns>Fecha obtenida</returns>
        public static DateTime GuidToDate(Guid guid, string crcKey)
        {
            long aux;
            return GuidDate.GuidToDate(guid.ToString(), out aux, crcKey);
        }

        private static bool GuidFormatValidate(string guid, string format = "U")
        {
            if ("U".Equals(format))
                format = GuidDate.DetectedFormat(guid);

            string regexp = GuidDate.getGuidFormat(format).Replace("0", "[a-f0-9]")
                                                          .Replace("{", "\\{")
                                                          .Replace("}", "\\}")
                                                          .Replace("(", "\\(")
                                                          .Replace(")", "\\)");
            return (Regex.Match(guid, "" + regexp + "", RegexOptions.IgnoreCase).Success && !"U".Equals(format));
        }
        private static string getGuidFormat(string format)
        {
            switch(format) {
                case "N":
                    return "00000000000040000000000000000000";
                case "D":
                    return "00000000-0000-4000-0000-000000000000";
                case "B":
                    return "{00000000-0000-4000-0000-000000000000}";
                case "P":
                    return "(00000000-0000-4000-0000-000000000000)";
                case "X":
                    return "{0x00000000,0x0000,0x0000, {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}";
                default:
                    return "00000000-0000-4000-0000-000000000000";
            }
        }
        private static string DetectedFormat(string strGuid)
        {
            if (strGuid.Length == "00000000000000000000000000000000".Length)
                return "N";
            if (strGuid.Length == "00000000-0000-0000-0000-000000000000".Length)
                return "D";
            if (strGuid.Length == "%00000000-0000-0000-0000-000000000000%".Length)
                if (strGuid.StartsWith("{"))
                    return "B";
                else if (strGuid.StartsWith("("))
                    return "P";
            if (strGuid.Length == "{0x00000000,0x0000,0x0000, {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}".Length)
                return "X";

            return "U";
        }

        public class InvalidDateGuidException : Exception
        {
            public DateTime Date { get; protected set; }
            public long Data { get; protected set; }

            public InvalidDateGuidException() : base() {}
            public InvalidDateGuidException(string message) : base(message) { }
            public InvalidDateGuidException(string message, Exception innerException) : base(message, innerException) { }

            public InvalidDateGuidException(DateTime date, long data) : base() 
            {
                this.Date = date;
                this.Data = data;
            }
            public InvalidDateGuidException(string message, DateTime date, long data) : base(message)
            {
                this.Date = date;
                this.Data = data;
            }
            public InvalidDateGuidException(string message, Exception innerException, DateTime date, long data) : base(message, innerException)
            {
                this.Date = date;
                this.Data = data;
            }
        }
    }
}