using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.manejador
{
    public class Thompson
    {
        int i = 0;
        List<Token> er;

        String nameEr; //Nombre de la expresion regular
        Automata raiz; //Contendra el Automata Final (que sera una lista de transiciones)
        Automata determinista;
        public Thompson(List<Token> er, string nameEr)
        {
            this.er = er;
            this.nameEr = nameEr;
            raiz = create();
            raiz.createAlfabeto(er);
            AFD Afd = new AFD(raiz);

            Determinista = Afd.Determinista;
        }

        public void reportar()
        {
            this.raiz.createAlfabeto(er);
            raiz.graficar(NameEr);
        }

        public string NameEr { get => nameEr; set => nameEr = value; }
        public Automata Raiz { get => raiz; set => raiz = value; }
        public Automata Determinista { get => determinista; set => determinista = value; }

        public Automata create()
        {
            switch (er.ElementAt(i).Id)
            {
                case 46: //Operacion .
                    i++;
                    Automata n = create(); //esperamos el primer tramo de mini Automata
                    Automata m = create(); //esperamos el segundo tramo de mini Automata con el que queremos concatenar
                    Automata resultadoConcat = concatenacion(m, n);
                    return resultadoConcat;
                    break;
                case 124: // Operacion |
                    i++;
                    Automata nOr = create(); //esperamos el primer tramo de mini Automata
                    Automata mOr = create(); //esperamos el segundo tramo de mini Automata con el que queremos hacer la union
                    Automata resultado = union(nOr, mOr);
                    return resultado;
                    break;
                case 42: // Operacion *
                    i++;
                    Automata nKleene = create();
                    Automata resultadoK = cerraduraKleene(nKleene);
                    return resultadoK;
                    break;
                case 43: //Operacion +
                    i++;
                    Automata nMas = create(); //Optenemos a
                    Automata nMasWithKleend = cerraduraKleene(nMas); //Optenemos *a
                    Automata resultadoMas = concatenacion(nMasWithKleend, nMas); //Optenemos .a*a
                    return resultadoMas;
                    break;
                case 63: //Operacion ?
                    i++;
                    Automata nInterogacion = create(); //Optenemos a
                    Automata nEpsilon = afnSimple(new Token(999, "ε", "Epsilon", 0, 0)); //Optenemos ε
                    Automata resultadoInterrogacion = union(nInterogacion, nEpsilon); //Optenemos |aε
                    return resultadoInterrogacion;
                    break;
                case 31:
                    Automata AutomataCadena = afnSimple(er.ElementAt(i));
                    return AutomataCadena;
                    break;
                case 30:
                    Automata AutomataId = afnSimple(er.ElementAt(i));
                    return AutomataId;
                    break;
                default:
                    i++;
                    break;
            }
            
            return null;
        }


        public Automata cerraduraKleene(Automata automataFN)
        {
            Automata afn_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            afn_kleene.Estados.Add(nuevoInicio);
            afn_kleene.Inicial = nuevoInicio;

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.Estados.Count; i++)
            {
                Estado tmp = (Estado)automataFN.Estados.ElementAt(i);
                tmp.Id = i + 1;
                afn_kleene.Estados.Add(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automataFN.Estados.Count + 1);
            afn_kleene.Estados.Add(nuevoFin);
            afn_kleene.Aceptacion.Add(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automataFN.Inicial;

            List<Estado> anteriorFin = automataFN.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count; i++)
            {
                anteriorFin.ElementAt(i).Transiciones.Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));
                anteriorFin.ElementAt(i).Transiciones.Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));
            }
            afn_kleene.Alfabeto = automataFN.Alfabeto;
            afn_kleene.LenguajeR = automataFN.LenguajeR;
            return afn_kleene;
        }

        public Automata concatenacion(Automata AFN1, Automata AFN2)
        {

            Automata afn_concat = new Automata();

            //se utiliza como contador para los estados del nuevo automata
            int i = 0;
            //agregar los estados del primer automata
            for (i = 0; i < AFN2.Estados.Count; i++)
            {
                Estado tmp = (Estado)AFN2.Estados.ElementAt(i);
                tmp.Id = i;
                //se define el estado inicial
                if (i == 0)
                {
                    afn_concat.Inicial = tmp ;
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == AFN2.Estados.Count - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < AFN2.Aceptacion.Count; k++)
                    {
                        tmp.Transiciones.Add(new Transicion((Estado)AFN2.Aceptacion.ElementAt(k), AFN1.Inicial, new Token(999, "ε", "Epsilon", 0, 0)));
                    }
                }
                afn_concat.Estados.Add(tmp);

            }
            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < AFN1.Estados.Count; j++)
            {
                Estado tmp = (Estado)AFN1.Estados.ElementAt(j);
                tmp.Id = i;

                //define el ultimo con estado de aceptacion
                if (AFN1.Estados.Count - 1 == j)
                    afn_concat.Aceptacion.Add(tmp);
                afn_concat.Estados.Add(tmp);
                i++;
            }

            HashSet<Token> alfabeto = new HashSet<Token>();
            foreach(Token token in AFN1.Alfabeto)
            {
                alfabeto.Add(token);
            }
            foreach (Token token in AFN2.Alfabeto)
            {
                alfabeto.Add(token);
            }
            afn_concat.Alfabeto = alfabeto;
            afn_concat.LenguajeR = AFN1.LenguajeR+ " " + AFN2.LenguajeR;

            return afn_concat;
        }


        public Automata union(Automata AFN1, Automata AFN2)
        {
            Automata afn_union = new Automata();
            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            //se crea una transicion del nuevo estado inicial al primer automata
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, AFN2.Inicial, new Token(999, "ε","Epsilon",0,0)));

            afn_union.Estados.Add(nuevoInicio);
            afn_union.Inicial = nuevoInicio;
            int i = 0;//llevar el contador del identificador de estados
                      //agregar los estados del segundo automata
            for (i = 0; i < AFN1.Estados.Count; i++)
            {
                Estado tmp = (Estado)AFN1.Estados.ElementAt(i);
                tmp.Id = (i + 1);
                afn_union.Estados.Add(tmp);
            }
            //agregar los estados del primer automata
            for (int j = 0; j < AFN2.Estados.Count; j++)
            {
                Estado tmp = (Estado)AFN2.Estados.ElementAt(j);
                tmp.Id = (i + 1);
                afn_union.Estados.Add(tmp);
                i++;
            }

            //se crea un nuevo estado final
            Estado nuevoFin = new Estado(AFN1.Estados.Count + AFN2.Estados.Count + 1);
            afn_union.Estados.Add(nuevoFin);
            afn_union.Aceptacion.Add(nuevoFin);

            Estado anteriorInicio = AFN1.Inicial;
            List<Estado> anteriorFin = AFN1.Aceptacion;
            List<Estado> anteriorFin2 = AFN2.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));

            // agregar transiciones desde el anterior AFN 1 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin.ElementAt(k).Transiciones.Add(new Transicion(anteriorFin.ElementAt(k),nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));
            // agregar transiciones desde el anterior AFN 2 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin2.ElementAt(k).Transiciones.Add(new Transicion(anteriorFin2.ElementAt(k), nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));

            HashSet<Token> alfabeto = new HashSet<Token>();
            foreach(Token token in AFN1.Alfabeto)
            {
                if(alfabeto.FirstOrDefault(x=> x.Id==token.Id && x.Lexema.Equals(token.Lexema)) == null)
                {
                    alfabeto.Add(token);
                }
                
            }
            
            foreach(Token token in AFN2.Alfabeto)
            {
                if (alfabeto.FirstOrDefault(x => x.Id == token.Id && x.Lexema.Equals(token.Lexema)) == null)
                {
                    alfabeto.Add(token);
                }
            }
            afn_union.Alfabeto = alfabeto;
            afn_union.LenguajeR = AFN1.LenguajeR + " " + AFN2.LenguajeR;
            afn_union.LenguajeR = AFN1.LenguajeR + " " + AFN2.LenguajeR;
            return afn_union;
        }


        public Automata afnSimple(Token simboloRegex)
        {
            Automata automataFN = new Automata();
            //definir los nuevos estados
            Estado inicial = new Estado(0);
            Estado aceptacion = new Estado(1);
            //crear una transicion unica con el simbolo
            Transicion tran = new Transicion(inicial, aceptacion, simboloRegex);
            inicial.Transiciones.Add(tran);
            //agrega los estados creados
            automataFN.Estados.Add(inicial);
            automataFN.Estados.Add(aceptacion);
            //colocar los estados iniciales y de acpetacion
            automataFN.Inicial = inicial;
            automataFN.Aceptacion.Add(aceptacion);
            automataFN.LenguajeR = simboloRegex.Lexema + "";
            i++;
            return automataFN;

        }



    }
}
