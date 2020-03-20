using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Nodo : IComparable<Nodo>
    {
        private Nodo izquierda, derecha;
        private bool isLeft;
        private int id;
        private string regex;
        private int numeroNodo;

        public Nodo Izquierda { get => izquierda; set => izquierda = value; }
        public Nodo Derecha { get => derecha; set => derecha = value; }
        public bool IsLeft { get => isLeft; set => isLeft = value; }
        public int Id { get => id; set => id = value; }
        public string Regex { get => regex; set => regex = value; }
        public int NumeroNodo { get => numeroNodo; set => numeroNodo = value; }

        public int CompareTo(Nodo other)
        {
            return NumeroNodo.CompareTo(other.NumeroNodo);
        }
    }
}
