using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_OLC1
{
    public partial class TextControl : UserControl
    {
        private string ruta;
        private string nombre;

        public TextControl()
        {
            InitializeComponent();
        }

        public string Ruta { get => ruta; set => ruta = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public string TextoEntrada
        {
            get { return this.textEntrada.Text; }
            set { this.textEntrada.Text = value; }
        }

       
    }

}
