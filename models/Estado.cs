using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Estado
    {
        private int id;
        private List<Transicion<string>> transiciones;

        public int Id { get => id; set => id = value; }
        public List<Transicion<string>> Transiciones { get => transiciones; set => transiciones = value; }

        public Estado(int id)
        {
            Id = id;
            Transiciones = new List<Transicion<string>>();
        }
    }


}
