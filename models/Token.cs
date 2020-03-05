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

    //    [XmlAttribute(DataType = "int", AttributeName = "Id")]
        public int Id { get => id; set => id = value; }
    //    [XmlAttribute(DataType = "string", AttributeName = "Lexema")]
        public string Lexema { get => lexema; set => lexema = value; }
    //    [XmlAttribute(DataType = "string", AttributeName = "Tipo")]
        public string Tipo { get => tipo; set => tipo = value; }
    //    [XmlAttribute(DataType = "int", AttributeName = "fila")]
        public int Fila { get => fila; set => fila = value; }
    //    [XmlAttribute(DataType = "int", AttributeName = "columna")]
        public int Columna { get => columna; set => columna = value; }
    }
}
