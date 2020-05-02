using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class AFD
    {
        public AFDEstado inicio;
        public AFDEstado final;
        public Token simbolo;

        public AFD(AFDEstado ini, Token sim, AFDEstado fin)
        {
            inicio = ini;
            simbolo = sim;
            final = fin;
        }

        public String Descripcion()
        {
            //['A','a','B']
            return "['" + inicio.NombreChar + "' , '" + simbolo.Lexema + "' , '" + final.NombreChar + "']";
        }

        public String Descripciongraphviz()
        {
            if(simbolo.Id == 31)
            {
                return inicio.NombreChar + "->" + final.NombreChar + " [label=\"\\\"" + simbolo.Lexema
                    .Replace("\t", "\\\\t")
                    .Replace("\r", "\\\\r")
                    .Replace("\n", "\\\\n")
                    .Replace("\"", "\\\\\"") 
                    + "\\\"\"];\n";
            }
            else
            {
                return inicio.NombreChar + "->" + final.NombreChar + " [label=\"" + simbolo.Lexema + "\"];\n";
            }
           
        }
    }
}
