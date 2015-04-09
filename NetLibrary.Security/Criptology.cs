using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetLibrary.Security
{
    public class Criptology
    {
        /// <summary>
        /// Devuelve el hash calculado para el valor pasado como parametro
        /// 
        /// Nota: No se puede recuperar el valor original
        /// </summary>
        /// <param name="value">cadena de caractéres a encriptar</param>
        /// <returns>Valor hash obtenido</returns>
        public static string GetHash(string value)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(value);

            SHA1 sha = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha.ComputeHash(data));
        }

        //Esta constante de cadena se utiliza como valor de "sal" para las llamadas de función PasswordDeriveBytes.
        // El tamaño de la IV (en bytes) debe ser = (tamaño de clave / 8). Tamaño de clave por defecto es 256, por lo que la IV debe ser
        // 32 bytes de longitud. Utilizando una cadena de caracteres 16 aquí nos da 32 bytes cuando se convierte en una matriz de bytes.
        private const string initVector = "tu89geji340t89u2";

        // Tamaño de la llave de encriptacion.
        private const int keysize = 256;

        /// <summary>
        /// Encripta una cadena según una contraseña proporcionada
        /// </summary>
        /// <param name="value">Texto a encriptar</param>
        /// <param name="key">contraseña de encriptación</param>
        /// <returns>Devuelve la cadena encriptada</returns>
        public static string EncryptStringKey(string value, string key)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);
            PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Desencripta una cadena según la contraseña especificada
        /// </summary>
        /// <param name="value">Texto encriptado</param>
        /// <param name="key">Contraseña usada para su encriptación</param>
        /// <returns>Devuelve la cadena desencriptada</returns>
        public static string DecryptStringKey(string value, string key)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(value);
                PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                throw new Exception("Error al desencriptar, key incorrecta.");
            }
            catch (System.FormatException ex)
            {
                //TEXTO ENCRIPTADO NO ESTA BASE64
                throw new Exception("Error al desencriptar, formato origen incorrecto");
            }

        }

        /// <summary>
        /// Encripta un fichero según una contraseña proporcionada
        /// </summary>
        /// <param name="pathFile">Ruta del fichero a encriptar</param>
        /// <param name="key">contraseña de encriptación</param>
        /// <returns>Devuelve un bit de arrays encriptado</returns>
        public static byte[] EncryptFileKey(string pathFile, string key)
        {
            if (File.Exists(pathFile))
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = File.ReadAllBytes(pathFile);
                PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return memoryStream.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Encripta un fichero según una contraseña proporcionada
        /// </summary>
        /// <param name="pathFile">Ruta del fichero a desencriptar</param>
        /// <param name="key">contraseña de encriptación</param>
        /// <returns>Devuelve un bit de arrays desencriptado</returns>
        public static byte[] DecrypFileKey(string pathFile, string key)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = File.ReadAllBytes(pathFile);
            PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return plainTextBytes;
        }
    }
}
