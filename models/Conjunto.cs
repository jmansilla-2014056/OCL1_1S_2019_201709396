using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Conjunto
    {
        private string nombreConjunto;
        private List<char> caracteres;

        public Conjunto()
        {
            Caracteres = new List<char>();
        }

        public string NombreConjunto { get => nombreConjunto; set => nombreConjunto = value; }
        public List<char> Caracteres { get => caracteres; set => caracteres = value; }
    }
}
