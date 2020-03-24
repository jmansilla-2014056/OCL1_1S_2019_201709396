using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Estado
    {
        private AFDEstado inicio;
        private AFDEstado fin;
        private Token simbolo;

        public AFDEstado Inicio { get => inicio; set => inicio = value; }
        public AFDEstado Fin { get => fin; set => fin = value; }
        public Token Simbolo { get => simbolo; set => simbolo = value; }
        

        public Estado(AFDEstado inicio, AFDEstado fin, Token simbolo)
        {
            Inicio = inicio;
            Fin = fin;
            Simbolo = simbolo;

        }
        
        public Estado()
        {

        }

        public string toString()
        {
            return "(" + Inicio.NombreId + "-" + Simbolo + "-" + Fin.NombreId + ")";
        }

        public string dotString()
        {
            return (this.Inicio.NombreId + " -> " + this.Fin.NombreId + " [label=\"" + this.Simbolo.Lexema + "\"];");
        }

    }
}
