using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_OLC1.manejador
{
    public class AFNtoAFD
    {
        private Automata determinista;
        private Simulacion simulacion;
        private HashSet<AFDEstado> global;
        public Automata Determinista { get => determinista; set => determinista = value; }

        public AFNtoAFD(Automata a)
        {
            simulacion = new Simulacion();
            conversionAFN(a);
        }

        public void conversionAFN(Automata afn)
        {
            List<Estado> limpize = new List<Estado>();

            foreach(AFDEstado estado in afn.AFDEstados)
            {
                foreach(Estado trans in estado.Estados)
                {
                    limpize.Add(trans);
                }
            }

            afn.AFDEstados = new List<AFDEstado>();

          
            Automata automata = new Automata();
            Queue<HashSet<AFDEstado>> cola = new Queue<HashSet<AFDEstado>>();
            AFDEstado inicial = new AFDEstado(0);
            automata.Inicial = inicial;
            automata.AFDEstados.Add(inicial);

            HashSet<AFDEstado> arrayInicial = simulacion.eClosure(afn.Inicial);
            foreach (AFDEstado aceptacion in afn.Aceptacion)
            {
                if (arrayInicial.Contains(aceptacion))
                {
                    automata.Aceptacion.Add(inicial);
                }
                    
            }
            
            cola.Enqueue(arrayInicial);
            List<HashSet<AFDEstado>> temporal = new List<HashSet<AFDEstado>>();
            int indexInicio = 0;
            while (cola.Count>0)
            {
                HashSet<AFDEstado> actual = cola.Dequeue();
               
                foreach(Token simbolo in afn.Alfabeto)
                {
                    HashSet<AFDEstado> move_result = simulacion.move(actual, (Token)simbolo);
                    HashSet<AFDEstado> resultado = new HashSet<AFDEstado>();
                        
                    foreach(AFDEstado e in move_result)
                    {
                        foreach (AFDEstado estate in simulacion.eClosure(e))
                        {
                            //if (resultado.FirstOrDefault(x => x.Id == estate.Id) == null)
                            //{
                                resultado.Add(estate);
                   
                            //}
                        }                    
                    }
                    AFDEstado anterior = automata.AFDEstados.ElementAt(indexInicio);

                    if (contenido(temporal, resultado))
                    {

                        List<AFDEstado> array_viejo = automata.AFDEstados;
                        AFDEstado estado_viejo = anterior;
                        int indexor = temporal.FindIndex(x => x.ToString().Equals(global.ToString())) + 1;
                        AFDEstado estado_siguiente = array_viejo.ElementAt(indexor);
                        estado_viejo.Estados.Add(new Estado(estado_viejo, estado_siguiente, simbolo));
                    }
                    else
                    {
                        
                        temporal.Add(resultado);
                        cola.Enqueue(resultado);
                        int indexor = temporal.FindIndex(x => x.ToString().Equals(resultado.ToString())) + 1;
                        AFDEstado nuevo = new AFDEstado(indexor);
                        
                        anterior.Estados.Add(new Estado(anterior, nuevo, simbolo));
                        automata.AFDEstados.Add(nuevo);

                         foreach(AFDEstado aceptacion in afn.Aceptacion)
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
            automata.graficar(fecha.ToString().Replace("/", "").Replace(":","").Replace(" ","")+"111111111");
            this.Determinista = automata;
            this.Determinista.Alfabeto = afn.Alfabeto;
            

        }

        public bool contenido(List<HashSet<AFDEstado>> temporal, HashSet<AFDEstado> resultado)
        {

            foreach(HashSet<AFDEstado> o in temporal)
            {
                if (busqueda(o, resultado))
                {
                    return true;
                }
            }

            return false;

        }



        public bool busqueda(HashSet<AFDEstado> x, HashSet<AFDEstado> y)
        {
            List<int> temporali = new List<int>();
            List<int> resultadoi = new List<int>();
            foreach(AFDEstado a in x)
            {
                temporali.Add(a.NombreId);
            }
            foreach(AFDEstado b in y)
            {
                resultadoi.Add(b.NombreId);
            }
            global = y;
            return temporali.Intersect(resultadoi).Count() == temporali.Count();

        }

        
    }
}
