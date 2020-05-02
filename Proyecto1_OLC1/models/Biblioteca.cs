using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Biblioteca
    {
        private int id;
        private string palabra;

        public Biblioteca(int id, string palabra)
        {
            this.Id = id;
            this.Palabra = palabra;
        }

        public int Id { get => id; set => id = value; }
        public string Palabra { get => palabra; set => palabra = value; }
    }
}
