using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class AFDEstado

    {
        private int nombreId;
        private List<Estado> estados;
        public List<int> intestado = new List<int>();
        public List<int> llave = new List<int>();
        public bool final;

        public int NombreId { get => nombreId; set => nombreId = value; }
        public List<Estado> Estados { get => estados; set => estados = value; }


        public AFDEstado(int id)
        {
            NombreId = id;
            Estados = new List<Estado>();
        }

        public String NombreChar
        {
            get { return ((char)(NombreId + 64)).ToString(); }
        }

    }


}
