using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.manejador
{
    public class Simulacion
    {
        public HashSet<AFDEstado> eClosure(AFDEstado eClosureEstado)
        {

            Stack<AFDEstado> pilaClosure = new Stack<AFDEstado>();
            AFDEstado actual = eClosureEstado;
            
            HashSet<AFDEstado> resultado = new HashSet<AFDEstado>();

            pilaClosure.Push(actual);
            while (pilaClosure.Count>0)
            {
                actual = pilaClosure.Pop();

                foreach (Estado t in actual.Estados)
                {

                    if (t.Simbolo.Id == 999 && !resultado.Contains(t.Fin))
                    {

                        resultado.Add(t.Fin);
                        pilaClosure.Push(t.Fin);
                    }
                }
            }
            resultado.Add(eClosureEstado); //la operacion e-Closure debe tener el estado aplicado
            return resultado;
        }

        public HashSet<AFDEstado> move(HashSet<AFDEstado> estados, Token simbolo)
        {
            Console.WriteLine("//////////////////////////////////////////////////");
            HashSet<AFDEstado> alcanzados = new HashSet<AFDEstado>();
            IEnumerator<AFDEstado> iterator = estados.GetEnumerator();
            while (iterator.MoveNext())
            {    
                foreach (Estado t in iterator.Current.Estados)
                {
                    AFDEstado siguiente = t.Fin;
                    Token simb = t.Simbolo;
                    if(simb.Id == simbolo.Id && simb.Lexema.Equals(simbolo.Lexema))
                    {                       
                            alcanzados.Add(siguiente);
                            Console.WriteLine("a0");                        
                    }
                    else
                    {
                            Console.WriteLine("a1");
                    }
                }
            }
            return alcanzados;

        }
            
    }
}
