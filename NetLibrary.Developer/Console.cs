using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Developer
{
    public partial class Console : Form
    {
        public const String VERSION = "1.0.0.0";
        public const String AUTOR  = "Joseph Girón Flores";
        public string LastText { get; private set; }
        public Console()
        {
            InitializeComponent();
            this.Clear();
        }

        public void WriteLine(string text)
        {
            //var a = String.Format(CultureInfo.GetCultureInfo("en"), "{0}", 5000.2);
            //var b = String.Format(CultureInfo.GetCultureInfo("es-ES"), "{0}", 5000.2);
            WriteLine(text, true);
        }
        public void WriteLine(string text, bool addDate)
        {
            text = text.Trim();
            if (String.IsNullOrEmpty(text)) return;

            if (addDate)
                text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + text;
            
            this.LastText = text;
            this.txLog.Text += Environment.NewLine + text;
        }

        public void Clear()
        {
            this.execute("exit");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.execute(this.textBox1.Text);
                this.textBox1.Text = "";
            }
        }

        public void execute(string command)
        {
            command = command.Trim();
            if (String.IsNullOrEmpty(command)) return;
            switch (command.ToLower())
            {
                case "exit":
                    this.Hide();
                    break;
                case "clear":
                    this.txLog.Text = "";
                    this.txLog.Text = Application.ProductName + " " + Application.ProductVersion;
                    break;
                case "version":
                    this.WriteLine("Console version: " + VERSION + " Author: " + AUTOR, false);
                    break;
                default:
                    this.WriteLine(command);
                    break;
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            execute(this.textBox1.Text);
        }
    }
}
