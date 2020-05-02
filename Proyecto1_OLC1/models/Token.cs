using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Proyecto1_OLC1.models
{
    [XmlRoot("Token")]

    public class Token
    {
        private int id;
        private string lexema;
        private string tipo;
        private int fila;
        private int columna;

        public Token(int id, string lexema, string tipo, int fila, int columna)
        {
            this.id = id;
            this.lexema = lexema;
            this.tipo = tipo;
            this.fila = fila;
            this.columna = columna;
        }

        public Token(){

            }
        
        public int Id { get => id; set => id = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
    }
}
