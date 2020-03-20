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
        public HashSet<Estado> eClosure(Estado eClosureEstado)
        {
            Stack<Estado> pilaClosure = new Stack<Estado>();
            Estado actual = eClosureEstado;
            
            HashSet<Estado> resultado = new HashSet<Estado>();

            pilaClosure.Push(actual);
            while (pilaClosure.Count>0)
            {
                actual = pilaClosure.Pop();

                foreach (Transicion t in actual.Transiciones)
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

        public HashSet<Estado> move(HashSet<Estado> estados, Token simbolo)
        {
            HashSet<Estado> alcanzados = new HashSet<Estado>();
            IEnumerator<Estado> iterator = estados.GetEnumerator();
            while (iterator.MoveNext())
            {    
                foreach (Transicion t in iterator.Current.Transiciones)
                {
                    Estado siguiente = t.Fin;
                    Token simb = t.Simbolo;
                    if(simb.Id == simbolo.Id && simb.Lexema.Equals(simbolo.Lexema))
                    {
                        if(alcanzados.FirstOrDefault(x=> x.Id == siguiente.Id) == null)
                        {
                            alcanzados.Add(siguiente);
                        }
                        
                    }
                }
            }
            return alcanzados;

        }
            
    }
}
