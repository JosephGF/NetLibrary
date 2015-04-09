using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NetLibrary.Win32
{
    /// <summary>
    /// Clase que realiza una gestión básica del registro
    /// </summary>
    public class WinRegistry
    {
        private RegistryKey _baseRegistryKey;
        private String _subKey;

        /// <summary>
        /// Base key por defecto = Registry.LocalMachine
        /// SubKey = SOFTWARE\\” + Application.ProductName
        /// </summary>
        public WinRegistry()
        {
            this._baseRegistryKey = Registry.LocalMachine;
            this._subKey = "SOFTWARE\\" + Application.ProductName;
        }

        /// <summary>
        /// Base key por defecto = Registry.LocalMachine
        /// </summary>
        /// <param name="subkey">Define la subkey sobre la que se operará</param>
        public WinRegistry(String subkey)
        {
            this._baseRegistryKey = Registry.LocalMachine;
            this._subKey = subkey;
        }

        /// <summary>
        /// SubKey = SOFTWARE\\” + Application.ProductName
        /// </summary>
        /// <param name="registryKey">Define la key base sobre la que se operará</param>
        public WinRegistry(RegistryKey registryKey)
        {
            this._baseRegistryKey = registryKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryKey">Define la key base sobre la que se operará</param>
        /// <param name="subkey">Define la subkey sobre la que se operará</param>
        public WinRegistry(RegistryKey registryKey, String subkey)
        {
            this._baseRegistryKey = registryKey;
            this._subKey = subkey;
        }

        public string Read(string KeyName)
        {
            // Opening the registry key 
            RegistryKey rk = _baseRegistryKey;
            // Open a subKey as read-only 
            RegistryKey sk1 = rk.OpenSubKey(_subKey);
            // If the RegistrySubKey doesn't exist -> (null) 
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value 
                    // or null is returned. 
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error! 
                    //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = _baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(_subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(_subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Deleting SubKey " + _subKey);
                return false;
            }
        }

        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(_subKey);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Deleting SubKey " + _subKey);
                return false;
            }
        }

        public int SubKeyCount()
        {
            try
            {
                // Setting
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Retriving subkeys of " + _subKey);
                return 0;
            }
        }

        public int ValueCount()
        {
            try
            {
                // Setting
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.ValueCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Retriving keys of " + _subKey);
                return 0;
            }
        }
    }
}
