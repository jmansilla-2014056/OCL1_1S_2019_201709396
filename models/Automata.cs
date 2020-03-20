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
        private Estado inicial;
        private List<Estado> aceptacion;
        private List<Estado> estados;

        private HashSet<Token> alfabeto;
        private String[] resultadoRegex;
        private String lenguajeR;

        public Estado Inicial { get => inicial; set => inicial = value; }
        public List<Estado> Aceptacion { get => aceptacion; set => aceptacion = value; }
        public List<Estado> Estados { get => estados; set => estados = value; }
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
            Estados = new List<Estado>();
            aceptacion = new List<Estado>();
            Alfabeto = new HashSet<Token>();
            ResultadoRegex = new string[3];
        }

        public void graficar(string nombre)
        {
            string texto = "digraph " + nombre +" {\n";
            texto += "\trankdir=LR;" + "\n";

            texto += "\tgraph [label=\"" + nombre + "\", labelloc=t, fontsize=20]; \n";
            texto += "\tnode [style = filled,color = mediumseagreen];";

            foreach(Estado e in this.Estados)
            {
                texto += " " + e.Id;
            }

            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";

            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> " + this.Inicial.Id + " [label=\"inicio\"];" + "\n";
            List<string> duplicados = new List<string>();
            List<Estado> filtroEstados = new List<Estado>();
            List<Transicion> filtroTransiciones = new List<Transicion>();
            Transicion transicion = new Transicion();
            foreach (Estado e in this.Estados)
            {
                Estado ee = e;             
                List<Transicion> transiciones = e.Transiciones;
                foreach(Transicion t in transiciones)
                {
                    if(duplicados.Find(x => x.Equals(t.dotString())) == null)
                    {
                        transicion = t;
                        filtroTransiciones.Add(t);
                        duplicados.Add(t.dotString());
                        texto += "\t" + t.dotString() + "\n";
                    }
                    
                }
               
                ee.Transiciones = filtroTransiciones;
                filtroEstados.Add(ee);
                filtroTransiciones = new List<Transicion>();
            }
            foreach(Estado a in Aceptacion)
            {
                texto += a.Id + "[shape=doublecircle]";
            }
            
            texto += "}";
            this.Estados = filtroEstados;

            Console.WriteLine("--------------------------------------" + nombre + "------------------------");
            foreach (Estado e in this.Estados)
            {
                List<Transicion> transiciones = e.Transiciones;
                foreach (Transicion t in transiciones)
                {
                    Console.WriteLine(t.dotString());
                }
            }

            Generador g = new Generador();
            g.graficar(texto, nombre);

        }

    }
}
