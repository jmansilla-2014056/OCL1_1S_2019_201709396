using Proyecto1_OLC1.manejador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.models
{
    public class Automata
    {
        private AFDEstado inicial;
        private List<AFDEstado> aceptacion;
        private List<AFDEstado> estados;

        private HashSet<Token> alfabeto;
        private String[] resultadoRegex;
        private String lenguajeR;

        public AFDEstado Inicial { get => inicial; set => inicial = value; }
        public List<AFDEstado> Aceptacion { get => aceptacion; set => aceptacion = value; }
        public List<AFDEstado> AFDEstados { get => estados; set => estados = value; }
        public HashSet<Token> Alfabeto { get => alfabeto; set => alfabeto = value; }
        public string[] ResultadoRegex { get => resultadoRegex; set => resultadoRegex = value; }
        public string LenguajeR { get => lenguajeR; set => lenguajeR = value; }

        public void createAlfabeto(List<Token> tokens)
        {
            foreach(Token t in tokens)
            {
                if(t.Id == 31 || t.Id == 30 )
                {
                    
                    if(Alfabeto.LastOrDefault(x=> x.Id == t.Id && x.Lexema.Equals(t.Lexema)) == null)
                    {
                        Alfabeto.Add(t);
                    }
                    
                }
            }
        }

        public Automata()
        {
            AFDEstados = new List<AFDEstado>();
            aceptacion = new List<AFDEstado>();
            Alfabeto = new HashSet<Token>();
            ResultadoRegex = new string[3];
        }

        public void obtenerAcpetacion()
        {
            foreach (AFDEstado a in Aceptacion)
            {
                foreach(AFDEstado b in AFDEstados)
                {
                    if(a.NombreId == b.NombreId)
                    {
                        b.final = true;
                    }

                }

            }
        }

        public String graficar(string nombre)
        {
            List<string> ls = new List<string>();
            string texto = "digraph " + nombre +" {\n";
            texto += "\trankdir=LR;" + "\n";

            texto += "\tgraph [label=\"" + nombre + "\", labelloc=t, fontsize=20]; \n";
            texto += "\tnode [style = filled,color = mediumseagreen];";

            foreach(AFDEstado e in this.AFDEstados)
            {
                texto += " " + e.NombreId;
            }

            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";

            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> " + this.Inicial.NombreId + " [label=\"inicio\"];" + "\n";

            Estado transicion = new Estado();
            foreach (AFDEstado e in this.AFDEstados)
            {        
                List<Estado> transiciones = e.Estados;
                foreach(Estado t in transiciones)
                {                   
                    transicion = t;
                    if(!ls.Any(d=> d.Equals(t.dotString())))
                    {
                        ls.Add(t.dotString());
                        texto += "\t" + t.dotString() + "\n";
                    }                       
                }
                             
            }

            foreach (AFDEstado a in Aceptacion)
            {
                texto += a.NombreId + "[shape=doublecircle]";
            }
            
            texto += "}";
           // this.Estados = filtroEstados;

            Console.WriteLine("--------------------------------------" + nombre + "------------------------");
            foreach (AFDEstado e in this.AFDEstados)
            {
                List<Estado> transiciones = e.Estados;
                foreach (Estado t in transiciones)
                {
                    Console.WriteLine(t.dotString());
                }
            }

            Generador g = new Generador();
            return g.graficar(texto, nombre);

        }

    }
}
