using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{

    public class Trampa
    {
        private string error;
        private string descrip;
        private int fila;
        private int columna;

        public Trampa(string error, string descrip, int fila, int columna)
        {
            this.Error = error;
            this.Descrip = descrip;
            this.Fila = fila;
            this.Columna = columna;
        }

        public string Error { get => error; set => error = value; }
        public string Descrip { get => descrip; set => descrip = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
    }
}
