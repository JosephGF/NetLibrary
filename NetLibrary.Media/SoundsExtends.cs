using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace NetLibrary.Media
{
    public class SoundsExtends
    {
        #region Funcionalidad Estatica
        private static System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        /// <summary>
        /// Ruta del fichero .wav
        /// </summary>
        public static string SoundLocation
        {
            get { return player.SoundLocation; }
            set { player.SoundLocation = value; player.LoadAsync(); }
        }
        /// <summary>
        /// Devuelve si ha terminado la carga del fichero de audo
        /// </summary>
        public bool IsLoadCompleted
        {
            get { return SoundsExtends.player.IsLoadCompleted; }
        }

        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="soundLocation">Url o Ruta del fichero wav</param>
        public static void Play(string soundLocation)
        {
            SoundsExtends.Play(soundLocation, false);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="soundLocation">Url o Ruta del fichero wav</param>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public static void Play(string soundLocation, bool sync)
        {
            SoundsExtends.Play(soundLocation, sync, SoundsExtends.player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="soundLocation">Url o Ruta del fichero wav</param>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        /// <param name="soundPlayer">Medio de reproduccion</param>
        public static void Play(string soundLocation, bool sync, SoundPlayer soundPlayer)
        {
            SoundsExtends.SoundLocation = soundLocation;
            if (sync)
                soundPlayer.PlaySync();
            else
                soundPlayer.Play();

        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente de forma asincrona
        /// </summary>
        public static void Play()
        {
            SoundsExtends.Play(false, SoundsExtends.player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public static void Play(bool sync)
        {
            SoundsExtends.Play(sync, SoundsExtends.player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        /// <param name="soundPlayer">Medio de reproduccion</param>
        public static void Play(bool sync, SoundPlayer soundPlayer)
        {
            if (sync)
                soundPlayer.PlaySync();
            else
                soundPlayer.Play();

        }
        /// <summary>
        /// Reproduce repetidamente el fichero cargado de forma asincrona
        /// </summary>
        public static void Loop()
        {
            SoundsExtends.Loop(SoundsExtends.player);
        }
        /// <summary>
        /// Reproduce repetidamente el fichero cargado de forma asincrona
        /// </summary>
        /// <param name="soundPlayer">Medio de reproduccion</param>
        public static void Loop(SoundPlayer soundPlayer)
        {
            soundPlayer.PlayLooping();
        }
        /// <summary>
        /// Termina la reproduccion de la pista actual
        /// </summary>
        public static void Stop()
        {
            SoundsExtends.Stop(SoundsExtends.player);
        }
        /// <summary>
        /// Termina la reproduccion de la pista actual
        /// </summary>
        /// <param name="soundPlayer">Medio de reproduccion</param>
        public static void Stop(SoundPlayer soundPlayer)
        {
            soundPlayer.Stop();
        }
        /// <summary>
        /// Carga el fichero actual
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public static void Load(bool sync)
        {
            SoundsExtends.Load(sync, SoundsExtends.player);
        }
        /// <summary>
        /// Carga el fichero actual de forma asincrona
        /// </summary>
        public static void Load()
        {
            SoundsExtends.Load(false, SoundsExtends.player);
        }
        /// <summary>
        /// Carga el fichero actual
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        /// <param name="soundPlayer">Medio de reproduccion</param>
        public static void Load(bool sync, SoundPlayer soundPlayer)
        {
            if (sync)
                soundPlayer.Load();
            else
                soundPlayer.LoadAsync();
        }
        /// <summary>
        /// Reproduce el sonido por defecto del sistema
        /// </summary>
        /// <param name="systemSound">Tipo de sonido</param>
        public static void SystemSounds(SystemSound systemSound)
        {
            systemSound.Play();
        }
        /// <summary>
        /// Reproduce un sonido con el altavoz interno del equipo
        /// </summary>
        public static void Beep()
        {
            System.Media.SystemSounds.Beep.Play();
        }
        /// <summary>
        /// Reproduce un sonido con el altavoz interno del equipo
        /// </summary>
        /// <param name="frequency">Frecuencia de la onda de sonido</param>
        /// <param name="duration">Duración del sonido</param>
        /// <returns>Devuelve si se ha podido reproducir</returns>
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);
    }
        #endregion
    #region Funcionalidad Dinamica
    public class Sounds : Component
    {
        private SoundPlayer _player = null;
        /// <summary>
        /// Ruta del fichero .wav
        /// </summary>
        public string SoundLocation
        {
            get { return this._player.SoundLocation; }
            set { this._player.SoundLocation = value; this._player.LoadAsync(); }
        }

        /// <summary>
        /// Devuelve si ha terminado la carga del fichero de audo
        /// </summary>
        public bool IsLoadCompleted
        {
            get { return this._player.IsLoadCompleted; }
        }

        public Sounds()
        {
            _player = new SoundPlayer();
        }

        /// <summary>
        /// Carga el fichero actual de forma asincrona
        /// </summary>
        public void Load()
        {
            SoundsExtends.Load(false, _player);
        }
        /// <summary>
        /// Carga el fichero actual
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public void Load(bool sync)
        {
            SoundsExtends.Load(sync, _player);
        }

        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="soundLocation">Url o Ruta del fichero wav</param>
        public void Play(string soundLocation)
        {
            SoundsExtends.Play(soundLocation, false, _player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="soundLocation">Url o Ruta del fichero wav</param>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public void Play(string soundLocation, bool sync)
        {
            SoundsExtends.Play(soundLocation, sync, this._player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente de forma asincrona
        /// </summary>
        public void Play()
        {
            SoundsExtends.Play(false, this._player);
        }
        /// <summary>
        /// Reproduce el sonido cargado anteriormente
        /// </summary>
        /// <param name="sync">Determina si se reproduce de forma sincrona o asincrona</param>
        public void Play(bool sync)
        {
            SoundsExtends.Play(sync, this._player);
        }

        /// <summary>
        /// Reproduce repetidamente el fichero cargado de forma asincrona
        /// </summary>
        public void Loop()
        {
            SoundsExtends.Loop(this._player);
        }

        /// <summary>
        /// Termina la reproduccion de la pista actual
        /// </summary>
        public void Stop()
        {
            SoundsExtends.Stop(this._player);
        }
    #endregion
    }
}
