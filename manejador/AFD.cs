using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.manejador
{
    public class AFD
    {
        private Automata determinista;
        private Simulacion simulacion;
        private readonly Dictionary<int, SortedSet<Nodo>> resultadoFollowPos;

        public Automata Determinista { get => determinista; set => determinista = value; }

        public AFD(Automata a)
        {
            resultadoFollowPos = new Dictionary<int, SortedSet<Nodo>>();
            simulacion = new Simulacion();
            conversionAFN(a);
        }

        public void conversionAFN(Automata afn)
        {
            Automata automata = new Automata();
            Queue<HashSet<Estado>> cola = new Queue<HashSet<Estado>>();
            Estado inicial = new Estado(0);
            automata.Inicial = inicial;
            automata.Estados.Add(inicial);

            HashSet<Estado> arrayInicial = simulacion.eClosure(afn.Inicial);
            foreach (Estado aceptacion in afn.Aceptacion)
            {
                if (arrayInicial.Contains(aceptacion))
                {
                    automata.Aceptacion.Add(inicial);
                }
                    
            }
           
            cola.Enqueue(arrayInicial);
            List<HashSet<Estado>> temporal = new List<HashSet<Estado>>();
            int indexInicio = 0;
            while (cola.Count>0)
            {
                HashSet<Estado> actual = cola.Dequeue();
               
                foreach(Token simbolo in afn.Alfabeto)
                {
                    HashSet<Estado> move_result = simulacion.move(actual, simbolo);
                    HashSet<Estado> resultado = new HashSet<Estado>();
                        
                    foreach(Estado e in move_result)
                    {
                        foreach (Estado estate in simulacion.eClosure(e))
                        {
                            //if (resultado.FirstOrDefault(x => x.Id == estate.Id) == null)
                            //{
                                resultado.Add(estate);
                   
                            //}
                        }                    
                    }
                    Estado anterior = automata.Estados.ElementAt(indexInicio);
                    if (contenido(temporal,resultado))
                    {
                        List<Estado> array_viejo = automata.Estados;
                        Estado estado_viejo = anterior;
                        Estado estado_siguiente = array_viejo.ElementAt(temporal.IndexOf(resultado) + 1);
                        estado_viejo.Transiciones.Add(new Transicion(estado_viejo, estado_siguiente, simbolo));
                    }
                    else
                    {
                         
                        temporal.Add(resultado);
                        cola.Enqueue(resultado);
                                               
                        Estado nuevo = new Estado(temporal.IndexOf(resultado) + 1);
                        anterior.Transiciones.Add(new Transicion(anterior, nuevo, simbolo));
                        automata.Estados.Add(nuevo);

                        foreach(Estado aceptacion in afn.Aceptacion)
                        {
                            if (resultado.Contains(aceptacion))
                            {
                                automata.Aceptacion.Add(nuevo);
                            }
                        }
                    }

                }
                indexInicio++;
            }
            DateTime fecha = DateTime.Now;
            automata.graficar(fecha.ToString().Replace("/", "").Replace(":","").Replace(" ",""));
            this.Determinista = automata;
            this.Determinista.Alfabeto = afn.Alfabeto;

        }

        public bool contenido(List<HashSet<Estado>> temporal, HashSet<Estado> resultado)
        {

            foreach(HashSet<Estado> o in temporal)
            {
                if (busqueda(o, resultado))
                {
                    return true;
                }
            }

            return false;

        }

        public bool busqueda(HashSet<Estado> x, HashSet<Estado> y)
        {
            List<int> temporali = new List<int>();
            List<int> resultadoi = new List<int>();
            foreach(Estado a in x)
            {
                temporali.Add(a.Id);
            }
            foreach(Estado b in y)
            {
                resultadoi.Add(b.Id);
            }

            return temporali.Intersect(resultadoi).Count() == temporali.Count();

        }

        
    }
}
