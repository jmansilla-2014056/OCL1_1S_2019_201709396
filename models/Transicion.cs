using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Transicion<T>
    {
        private Estado inicio;
        private Estado fin;
        private Token simbolo;
        private T llave;

        public Estado Inicio { get => inicio; set => inicio = value; }
        public Estado Fin { get => fin; set => fin = value; }
        public Token Simbolo { get => simbolo; set => simbolo = value; }
        private T Llave { get => llave; set => llave = value; }

        public Transicion(Estado inicio, Estado fin, Token simbolo)
        {
            Inicio = inicio;
            Fin = fin;
            Simbolo = simbolo;
            llave = (T)Convert.ChangeType((string)Simbolo.Id.ToString()+Simbolo.Lexema.ToString(), typeof(T));
        }
        
        public Transicion()
        {

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
