using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Validar
    {
        private string nombreEr;
        private string cadenas;

        public Validar(string nombreEr, string cadenas)
        {
            this.nombreEr = nombreEr;
            this.cadenas = cadenas;
        }

        public string NombreEr { get => nombreEr; set => nombreEr = value; }
        public string Cadenas { get => cadenas; set => cadenas = value; }
    }
}
