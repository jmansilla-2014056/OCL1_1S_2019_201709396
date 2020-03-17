using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Transicion
    {
        private Estado inicio;
        private Estado fin;
        private Token simbolo;

        public Estado Inicio { get => inicio; set => inicio = value; }
        public Estado Fin { get => fin; set => fin = value; }
        public Token Simbolo { get => simbolo; set => simbolo = value; }

        public Transicion(Estado inicio, Estado fin, Token simbolo)
        {
            Inicio = inicio;
            Fin = fin;
            Simbolo = simbolo;
        }
        
        public string toString()
        {
            return "(" + Inicio.Id + "-" + Simbolo + "-" + Fin.Id + ")";
        }

        public string dotString()
        {
            return (this.Inicio.Id + " -> " + this.Fin.Id + " [label=\"" + this.Simbolo.Lexema + "\"];");
        }

    }
}
