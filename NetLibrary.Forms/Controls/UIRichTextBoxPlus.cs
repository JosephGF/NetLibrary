using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace NetLibrary.Forms.Controls
{
    [ToolboxBitmap(typeof(RichTextBox))]
    public class UIRichTextBoxPlus : RichTextBox
    {
        private Font _defaultFont;
        private Color _defaultBackColor;
        private Color _defaultForeColor;
        private Boolean _reestablecerFormatosAnteriores = true;
        private string _version = "RichTextBox Plus Ver. 2.1";
        private List<RichTextBoxColors> _listTextColors = new List<RichTextBoxColors>();

        /// <summary>
        /// Versión del control
        /// </summary>
        public string Version
        {
            get { return _version; }
        }

        /// <summary>
        /// Obtiene la lista de colores
        /// </summary>
        /// <returns>Lista de colores</returns>
        public List<RichTextBoxColors> GetListTextColors()
        {
            return _listTextColors;

        }

        /// <summary>
        /// Si es true, reestablece el formato del texto seleccionado cada vez que se llama a un metodo de colorear/cambiar fuente
        /// </summary>
        public Boolean ReestablecerFormatosAnteriores
        {
            get { return _reestablecerFormatosAnteriores; }
            set { _reestablecerFormatosAnteriores = value; }
        }

        /// <summary>
        /// Devuelve el texto en formato HTML
        /// </summary>
        public string HTMLText
        {
            get { return "" /*Utiles.RtfToHtml.ConverRtfToHTML(this.Rtf)*/; }
        }

        /// <summary>
        /// Establece la lista de colores
        /// </summary>
        /// <param name="value">Lita de colores</param>
        public void SetListTextColors(List<RichTextBoxColors> value)
        {
            _listTextColors = value;
        }

        public UIRichTextBoxPlus()
        {
            this._defaultFont = new Font(this.Font, FontStyle.Regular);
            this._defaultBackColor = Color.White;
            this._defaultForeColor = Color.Black;
        }

        /// <summary>
        /// Colorea el texto
        /// </summary>
        /// <param name="objetoCambio">Color</param>
        /// <param name="palabras">Palabras a buscar</param>
        /// <param name="tipoBusqueda">Tipo de busqueda</param>
        /// <param name="cambio">Tipo de cambio</param>
        /// <param name="startIndex">Posición de inicio del texto</param>
        /// <param name="endIndex">Posición final del texto</param>
        /// <param name="todoElTexto">En todo el texto</param>
        /// <returns>Ocurrencias encontradas</returns>
        private Int32 Colorear(Object objetoCambio, string[] palabras, RichTextBoxFinds tipoBusqueda, Cambio cambio, Int32 startIndex, Int32 endIndex, bool todoElTexto)
        {
            Int32 indexCaracter = 0;
            Int32 lenghtCadena = 0;
            Int32 iniIndexSel = 0;
            Int32 lenTextoSel = 0;
            Int32 numOcurrencias = 0;
            Boolean restaurar = true;

            Font fuenteInicial = this.SelectionFont;

            iniIndexSel = this.SelectionStart;
            lenTextoSel = this.SelectionLength;

            Int32 endIndexOf = -1;

            if (endIndex < this.Text.Length)
                endIndexOf = this.Text.IndexOf(" ", endIndex);

            if (endIndexOf < 0)
                endIndexOf = this.Text.Length;

            Int32 startIndexOf = this.Find(" ", 0, endIndexOf - 1, RichTextBoxFinds.Reverse);

            if (startIndexOf < 0)
                startIndexOf = 0;

            if (startIndexOf > endIndexOf)
                startIndexOf = endIndexOf;

            if (todoElTexto)
            {
                startIndexOf = 0;
                endIndexOf = this.Text.Length;
            }

            for (Int32 numPalabra = 0; numPalabra < palabras.Length; numPalabra++)
            {

                /*if (startIndexOf > 0)
                    indexCaracter = this.Text.IndexOf(" ", startIndexOf);
                else
                    indexCaracter = 0;*/

                lenghtCadena = palabras[numPalabra].Length;
                indexCaracter = 0;
                while (indexCaracter >= 0 && indexCaracter <= this.Text.Length && indexCaracter < endIndexOf)
                {
                    if (this.Text.Length > indexCaracter)
                        indexCaracter = this.Find(palabras[numPalabra], indexCaracter, endIndexOf, tipoBusqueda);
                    else
                        indexCaracter = -1;

                    if (indexCaracter > -1)
                    {
                        numOcurrencias++;

                        /*this.SelectionStart = indexCaracter;
                        this.SelectionLength = lenghtCadena;*/
                        this.Select(indexCaracter, lenghtCadena);

                        switch (cambio)
                        {
                            case Cambio.TEXTO:
                                this.SelectionColor = (Color)objetoCambio;
                                break;
                            case Cambio.FONDO:
                                this.SelectionBackColor = (Color)objetoCambio;
                                break;
                            case Cambio.FUENTE:
                                this.SelectionFont = (Font)objetoCambio;
                                break;
                        }

                        indexCaracter = indexCaracter + lenghtCadena;
                        restaurar = false;
                    }
                }
            }

            if (restaurar && _reestablecerFormatosAnteriores)
            {

                ReestablecerFormato(startIndexOf, endIndexOf, cambio);
            }

            this.SelectionStart = iniIndexSel;
            this.SelectionLength = lenTextoSel;
            this.SelectionFont = fuenteInicial;
            this.SelectionColor = this.ForeColor;
            this.SelectionBackColor = this.BackColor;

            return numOcurrencias;
        }

        /// <summary>
        /// Colorea las palabras que coincidan con la busqueda en el color indicado.
        /// </summary>
        /// <param name="color">Color con el que se marcarán las palabras</param>
        /// <param name="palabras">Palabras que se remarcarán</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 ColorearTexto(Color color, string[] palabras, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return Colorear(color, palabras, tipoBusqueda, Cambio.TEXTO, this.SelectionStart, this.SelectionStart + this.SelectionLength, todoElTexto);
        }

        /// <summary>
        /// Colorea el fondo de las palabras que coincidan con la busqueda en el color indicado.
        /// </summary>
        /// <param name="color">Color con el que se marcará el fondo de las palabras</param>
        /// <param name="palabras">Palabras que se remarcarán</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 ColorearFondo(Color color, string[] palabras, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return Colorear(color, palabras, tipoBusqueda, Cambio.FONDO, this.SelectionStart, this.SelectionStart + this.SelectionLength, todoElTexto);
        }

        /// <summary>
        /// Cambia la fuente a las palabras que coincidan con la busqueda.
        /// </summary>
        /// <param name="fuente">Fuente con el que se marcarán las palabras</param>
        /// <param name="palabras">Palabras que se remarcarán</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 CambiarFuente(Font fuente, string[] palabras, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return Colorear(fuente, palabras, tipoBusqueda, Cambio.FUENTE, this.SelectionStart, this.SelectionStart + this.SelectionLength, todoElTexto);
        }

        private Int32 ColorearRango(Object objetoCambio, string[] caracterInicio, string[] caracterFin, RichTextBoxFinds tipoBusqueda, Cambio cambio, bool todoElTexto)
        {
            if (caracterInicio.Length != caracterFin.Length)
                throw new Exception("El número de caracteres de inicio no se corresponde con los de fin");

            Int32 indexCaracter = 0;
            Int32 lenghtCadena = 0;
            Int32 iniIndexSel = 0;
            Int32 lenTextoSel = 0;
            Int32 numOcurrencias = 0;

            iniIndexSel = this.SelectionStart;
            lenTextoSel = this.SelectionLength;

            for (Int32 index = 0; index < caracterInicio.Length; index++)
            {
                indexCaracter = 0;
                lenghtCadena = 0;

                while (indexCaracter >= 0 && indexCaracter <= this.Text.Length)
                {

                    indexCaracter = indexCaracter + lenghtCadena;

                    if (this.Text.Length > indexCaracter)
                        indexCaracter = this.Find(caracterInicio[index], indexCaracter, this.Text.Length, tipoBusqueda);
                    else
                        indexCaracter = -1;

                    if (indexCaracter > -1)
                    {
                        numOcurrencias++;
                        lenghtCadena = this.Find(caracterFin[index], indexCaracter + 1, this.Text.Length, tipoBusqueda);

                        if (lenghtCadena > 0 && lenghtCadena < this.Text.Length)
                            lenghtCadena = lenghtCadena - indexCaracter + caracterFin[index].Length;
                        else
                            lenghtCadena = this.Text.Length - indexCaracter;

                        /*this.SelectionStart = indexCaracter;
                        this.SelectionLength = lenghtCadena;*/
                        this.Select(indexCaracter, lenghtCadena);

                        switch (cambio)
                        {
                            case Cambio.TEXTO:
                                this.SelectionColor = (Color)objetoCambio;
                                break;
                            case Cambio.FONDO:
                                this.SelectionBackColor = (Color)objetoCambio;
                                break;
                            case Cambio.FUENTE:
                                this.SelectionFont = (Font)objetoCambio;
                                break;
                        }
                    }
                }
            }

            this.SelectionStart = iniIndexSel;
            this.SelectionLength = lenTextoSel;

            return numOcurrencias;
        }

        /// <summary>
        /// Colorea las palabras que estén entre el rango de la busqueda en el color indicado.
        /// </summary>
        /// <param name="color">Color con el que se marcarán las palabras</param>
        /// <param name="caracterInicio">Caracter inicial del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="caracterFin">Caracter final del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 ColorearTextoRango(Color color, string[] caracterInicio, string[] caracterFin, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return ColorearRango(color, caracterInicio, caracterFin, tipoBusqueda, Cambio.TEXTO, todoElTexto);
        }

        /// <summary>
        /// Colorea el fondo las palabras que estén entre el rango de la busqueda en el color indicado.
        /// </summary>
        /// <param name="color">Color con el que se marcará el fondo de las palabras</param>
        /// <param name="caracterInicio">Caracter inicial del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="caracterFin">Caracter final del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 ColorearFondoRango(Color color, string[] caracterInicio, string[] caracterFin, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return ColorearRango(color, caracterInicio, caracterFin, tipoBusqueda, Cambio.FONDO, todoElTexto);
        }

        /// <summary>
        /// Cambia la fuente las palabras que estén entre el rango de la busqueda a la fuente indicada.
        /// </summary>
        /// <param name="fuente">Fuente con la que se marcarán las palabras</param>
        /// <param name="caracterInicio">Caracter inicial del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="caracterFin">Caracter final del rango (Si hay mas de uno, las posiciones en los arrays de caracter inicio y fin deben ser las mismas)</param>
        /// <param name="tipoBusqueda">Define el tipo de busqueda para marcar las palabras</param>
        /// <param name="todoElTexto">Analiza todo el texto de inicio a fin (Aconsejable poner a true sólo si se inserta desde código)</param>
        /// <returns>Devuelve el número de cambios realizados</returns>
        public Int32 CambiarFuenteRango(Font fuente, string[] caracterInicio, string[] caracterFin, RichTextBoxFinds tipoBusqueda, bool todoElTexto)
        {
            return ColorearRango(fuente, caracterInicio, caracterFin, tipoBusqueda, Cambio.FUENTE, todoElTexto);
        }

        /// <summary>
        /// Elimina todos los estilos aplicados dejando el estado original
        /// </summary>
        public void ReestablecerFormato()
        {
            Int32 iniIndexSel = this.SelectionStart;
            Int32 lenTextoSel = this.SelectionLength;

            this.SelectAll();

            this.SelectionColor = this.ForeColor;
            this.SelectionBackColor = this.BackColor;
            this.SelectionFont = this.Font;

            this.SelectionStart = iniIndexSel;
            this.SelectionLength = lenTextoSel;
        }

        /// <summary>
        /// Elimina todos los estilos aplicados dejando el estado original en el rango seleccionado
        /// </summary>
        /// <param name="startIndex">Inicio del rango</param>
        /// <param name="endIndex">Fin del rango</param>
        /// <param name="cambio">Tipo de cambio</param>
        private void ReestablecerFormato(Int32 startIndex, Int32 endIndex, Cambio cambio)
        {
            Int32 iniIndexSel = this.SelectionStart;
            Int32 lenTextoSel = this.SelectionLength;

            this.Select(startIndex, endIndex);

            switch (cambio)
            {
                case Cambio.TEXTO:
                    this.SelectionColor = this.ForeColor;
                    break;
                case Cambio.FONDO:
                    this.SelectionBackColor = this.BackColor;
                    break;
                case Cambio.FUENTE:
                    this.SelectionFont = this.Font;
                    break;
            }

            this.Select(iniIndexSel, lenTextoSel);
        }

        /// <summary>
        /// Elimina todos los estilos aplicados dejando el estado original en el rango seleccionado
        /// </summary>
        /// <param name="startIndex">Inicio del rango</param>
        /// <param name="endIndex">Fin del rango</param>
        private void ReestablecerFormato(Int32 startIndex, Int32 endIndex)
        {
            Int32 iniIndexSel = this.SelectionStart;
            Int32 lenTextoSel = this.SelectionLength;

            this.Select(startIndex, endIndex);

            this.SelectionColor = this.ForeColor;
            this.SelectionBackColor = this.BackColor;
            this.SelectionFont = this.Font;

            this.Select(iniIndexSel, lenTextoSel);
        }

        /// <summary>
        /// Redibuja el texto
        /// </summary>
        public void RefrescarTexto()
        {
            if (_listTextColors == null || _listTextColors.Count < 1)
                return;

            Int32 indexCaracter = 0;
            Int32 lenghtCadena = 0;
            Int32 iniIndexSel = 0;
            Int32 lenTextoSel = 0;
            Int32 numOcurrencias = 0;

            iniIndexSel = this.SelectionStart;
            lenTextoSel = this.SelectionLength;

            foreach (RichTextBoxColors textColor in _listTextColors)
            {
                switch (textColor.TipoColor)
                {
                    case TipoColorear.TEXTO:
                        this.ColorearTexto(textColor.ColorEdit, textColor.ListaPalabras1.ToArray(), textColor.TipoBusqueda, false);
                        break;
                    case TipoColorear.FONDO:
                        this.ColorearFondo(textColor.ColorEdit, textColor.ListaPalabras1.ToArray(), textColor.TipoBusqueda, false);
                        break;
                    case TipoColorear.TEXTO_RANGO:
                        this.ColorearTextoRango(textColor.ColorEdit, textColor.ListaPalabras1.ToArray(), textColor.ListaPalabras2.ToArray(), textColor.TipoBusqueda, false);
                        break;
                    case TipoColorear.FONDO_RANGO:
                        this.ColorearFondoRango(textColor.ColorEdit, textColor.ListaPalabras1.ToArray(), textColor.ListaPalabras2.ToArray(), textColor.TipoBusqueda, false);
                        break;
                }
            }

            this.SelectionStart = iniIndexSel;
            this.SelectionLength = lenTextoSel;
        }
    }

    public enum TipoColorear
    {
        /// <summary>
        /// Dar color al texto
        /// </summary>
        TEXTO,
        /// <summary>
        /// Dar color al fondo
        /// </summary>
        FONDO,
        /// <summary>
        /// Dar color al texto del rango
        /// </summary>
        TEXTO_RANGO,
        /// <summary>
        /// Dar color al fondo del rango
        /// </summary>
        FONDO_RANGO
    }

    enum Cambio
    {
        /// <summary>
        /// Cambie el texto
        /// </summary>
        TEXTO,
        /// <summary>
        /// Cambia el fondo
        /// </summary>
        FONDO,
        /// <summary>
        /// Cambia la fuente
        /// </summary>
        FUENTE
    }

    public class RichTextBoxColors
    {
        private Color _colorEdit = Color.Black;
        private List<string> _listaPalabras1 = new List<string>();
        private List<string> _listaPalabras2 = new List<string>();
        private TipoColorear _tipoColor = TipoColorear.TEXTO;
        private RichTextBoxFinds _tipoBusqueda = RichTextBoxFinds.None;

        public Color ColorEdit
        {
            get { return _colorEdit; }
            set { _colorEdit = value; }
        }

        public List<string> ListaPalabras1
        {
            get { return _listaPalabras1; }
            set { _listaPalabras1 = value; }
        }

        public List<string> ListaPalabras2
        {
            get { return _listaPalabras2; }
            set { _listaPalabras2 = value; }
        }

        public TipoColorear TipoColor
        {
            get { return _tipoColor; }
            set { _tipoColor = value; }
        }

        public RichTextBoxFinds TipoBusqueda
        {
            get { return _tipoBusqueda; }
            set { _tipoBusqueda = value; }
        }
    }
}
