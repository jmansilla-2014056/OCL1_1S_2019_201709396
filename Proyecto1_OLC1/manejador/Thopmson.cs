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
        public List<Conjunto> conjuntos;

        String nameEr; //Nombre de la expresion regular
        Automata raiz; //Contendra el Automata Final (que sera una lista de transiciones)
        Subconjuntos deterministas;
        public Thompson(List<Token> er, string nameEr, List<Conjunto> lc)
        {

            foreach (Token t in er)
            {
                t.Fila = 0;
                t.Columna = 0;
                t.Tipo = "";
            }

            this.er = er;
            this.nameEr = nameEr;
            this.conjuntos = lc;
            raiz = create();
            raiz.createAlfabeto(er);
            raiz.obtenerAcpetacion();
            //  AFNtoAFD Afd = new AFNtoAFD(raiz);

            //  Determinista = Afd.Determinista;
            Subconjuntos s = new Subconjuntos(raiz);

            this.Deterministas = s;

        }

        public String reportar()
        {
            this.raiz.createAlfabeto(er);
            return raiz.graficar(NameEr);
        }

        public string NameEr { get => nameEr; set => nameEr = value; }
        public Automata Raiz { get => raiz; set => raiz = value; }
        public Subconjuntos Deterministas { get => deterministas; set => deterministas = value; }

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
                    Automata nMas = create();
                  //  Automata nMasWithKleend = concatenacionMas(nMas); //Optenemos *a               
                    //Optenemos .a*a
                    return concatenacion(nMas, cerraduraKleene(nMas));
                    break;
                case 63: //Operacion ?
                    i++;
                    Automata nInterogacion = create(); //Optenemos a
                    Automata nEpsilon = afnSimple(new Token(999, "ε", "Epsilon", 0, 0)); //Optenemos ε
                    i--;
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
            AFDEstado nuevoInicio = new AFDEstado(0);
            afn_kleene.AFDEstados.Add(nuevoInicio);
            afn_kleene.Inicial = nuevoInicio;

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.AFDEstados.Count; i++)
            {
                AFDEstado tmp = (AFDEstado)automataFN.AFDEstados.ElementAt(i);
                tmp.NombreId = i + 1;
                afn_kleene.AFDEstados.Add(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            AFDEstado nuevoFin = new AFDEstado(automataFN.AFDEstados.Count + 1);
            afn_kleene.AFDEstados.Add(nuevoFin);
            afn_kleene.Aceptacion.Add(nuevoFin);

            //definir estados clave para realizar la cerraduras
            AFDEstado anteriorInicio = automataFN.Inicial;

            List<AFDEstado> anteriorFin = automataFN.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Estados.Add(new Estado(nuevoInicio, anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));
            nuevoInicio.Estados.Add(new Estado(nuevoInicio, nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count; i++)
            {
                anteriorFin.ElementAt(i).Estados.Add(new Estado(anteriorFin.ElementAt(i), anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));
                anteriorFin.ElementAt(i).Estados.Add(new Estado(anteriorFin.ElementAt(i), nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));
            }
            afn_kleene.Alfabeto = automataFN.Alfabeto;
            afn_kleene.LenguajeR = automataFN.LenguajeR;
            return afn_kleene;
        }



        public Automata concatenacionMas(Automata AFN2)
        {

            Automata AFN1 = new Automata();


            AFN1.Inicial = new AFDEstado(AFN2.Inicial.NombreId  + AFN2.AFDEstados.Count);
            
            AFN1.Inicial.Estados.AddRange(AFN2.Inicial.Estados);

            //    AFN1.Alfabeto = AFN2.alfabeto;


            foreach (AFDEstado e in AFN2.AFDEstados)
            {
                AFDEstado aFDEstado = new AFDEstado(e.NombreId + AFN2.AFDEstados.Count);
                List<Estado> nuevo = new List<Estado>();
                List<Estado> transiciones = e.Estados;

                foreach (Estado t in transiciones)
                {
                    Estado x = new Estado();
                    x.Simbolo = t.Simbolo;

                    x.Fin = new AFDEstado(t.Fin.NombreId + AFN2.AFDEstados.Count);
                    x.Inicio = new AFDEstado(t.Inicio.NombreId + AFN2.AFDEstados.Count);

                    x.Fin.Estados.AddRange(t.Fin.Estados);
                    x.Inicio.Estados.AddRange(t.Inicio.Estados);
                    if (x.Inicio.NombreId <= AFN2.AFDEstados.Count - 1)
                    {
                        x.Inicio.NombreId += t.Inicio.NombreId + AFN2.AFDEstados.Count - 1;
                    }
                    if (x.Fin.NombreId <= AFN2.AFDEstados.Count - 1)
                    {
                        x.Fin.NombreId += t.Fin.NombreId + AFN2.AFDEstados.Count - 1;
                    }
                    nuevo.Add(x);
                }

                aFDEstado.Estados = nuevo;
                AFN1.estados.Add(aFDEstado);
            }

            foreach (AFDEstado r in AFN2.AFDEstados)
            {
                r.NombreId--;
            }

            Automata n = cerraduraKleene(AFN2);

            foreach (AFDEstado x in AFN2.AFDEstados)
            {
                List<Estado> transiciones = x.Estados;

                foreach (Estado t in transiciones)
                {
                    if(t.Fin.NombreId > AFN2.AFDEstados.Count)
                    {
                        t.Fin.NombreId--;
                    }
                }
            }
                
            
            Automata afn_concat = new Automata();
            return concatenacion(AFN1, AFN2);
            //se utiliza como contador para los estados del nuevo automata
      
        }



        public Automata concatenacion(Automata AFN1, Automata AFN2)
        {

            Automata afn_concat = new Automata();

            //se utiliza como contador para los estados del nuevo automata
            int i = 0;
            //agregar los estados del primer automata
            for (i = 0; i < AFN2.AFDEstados.Count; i++)
            {
                AFDEstado tmp = (AFDEstado)AFN2.AFDEstados.ElementAt(i);
                tmp.NombreId = i;
                //se define el estado inicial
                if (i == 0)
                {
                    afn_concat.Inicial = tmp;
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == AFN2.AFDEstados.Count - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < AFN2.Aceptacion.Count; k++)
                    {
                        tmp.Estados.Add(new Estado((AFDEstado)AFN2.Aceptacion.ElementAt(k), AFN1.Inicial, new Token(999, "ε", "Epsilon", 0, 0)));
                    }
                }
                afn_concat.AFDEstados.Add(tmp);

            }
            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < AFN1.AFDEstados.Count; j++)
            {
                AFDEstado tmp = (AFDEstado)AFN1.AFDEstados.ElementAt(j);
                tmp.NombreId = i;

                //define el ultimo con estado de aceptacion
                if (AFN1.AFDEstados.Count - 1 == j)
                    afn_concat.Aceptacion.Add(tmp);
                afn_concat.AFDEstados.Add(tmp);
                i++;
            }

            HashSet<Token> alfabeto = new HashSet<Token>();
            foreach (Token token in AFN1.Alfabeto)
            {
                alfabeto.Add(token);
            }
            foreach (Token token in AFN2.Alfabeto)
            {
                alfabeto.Add(token);
            }
            afn_concat.Alfabeto = alfabeto;
            afn_concat.LenguajeR = AFN1.LenguajeR + " " + AFN2.LenguajeR;

            return afn_concat;
        }


        public Automata union(Automata AFN1, Automata AFN2)
        {
            Automata afn_union = new Automata();
            //se crea un nuevo estado inicial
            AFDEstado nuevoInicio = new AFDEstado(0);
            //se crea una transicion del nuevo estado inicial al primer automata
            nuevoInicio.Estados.Add(new Estado(nuevoInicio, AFN2.Inicial, new Token(999, "ε", "Epsilon", 0, 0)));

            afn_union.AFDEstados.Add(nuevoInicio);
            afn_union.Inicial = nuevoInicio;
            int i = 0;//llevar el contador del identificador de estados
                      //agregar los estados del segundo automata
            for (i = 0; i < AFN1.AFDEstados.Count; i++)
            {
                AFDEstado tmp = (AFDEstado)AFN1.AFDEstados.ElementAt(i);
                tmp.NombreId = (i + 1);
                afn_union.AFDEstados.Add(tmp);
            }
            //agregar los estados del primer automata
            for (int j = 0; j < AFN2.AFDEstados.Count; j++)
            {
                AFDEstado tmp = (AFDEstado)AFN2.AFDEstados.ElementAt(j);
                tmp.NombreId = (i + 1);
                afn_union.AFDEstados.Add(tmp);
                i++;
            }

            //se crea un nuevo estado final
            AFDEstado nuevoFin = new AFDEstado(AFN1.AFDEstados.Count + AFN2.AFDEstados.Count + 1);
            afn_union.AFDEstados.Add(nuevoFin);
            afn_union.Aceptacion.Add(nuevoFin);

            AFDEstado anteriorInicio = AFN1.Inicial;
            List<AFDEstado> anteriorFin = AFN1.Aceptacion;
            List<AFDEstado> anteriorFin2 = AFN2.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Estados.Add(new Estado(nuevoInicio, anteriorInicio, new Token(999, "ε", "Epsilon", 0, 0)));

            // agregar transiciones desde el anterior AFN 1 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin.ElementAt(k).Estados.Add(new Estado(anteriorFin.ElementAt(k), nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));
            // agregar transiciones desde el anterior AFN 2 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin2.ElementAt(k).Estados.Add(new Estado(anteriorFin2.ElementAt(k), nuevoFin, new Token(999, "ε", "Epsilon", 0, 0)));

            HashSet<Token> alfabeto = new HashSet<Token>();
            foreach (Token token in AFN1.Alfabeto)
            {
                if (alfabeto.FirstOrDefault(x => x.Id == token.Id && x.Lexema.Equals(token.Lexema)) == null)
                {
                    alfabeto.Add(token);
                }

            }

            foreach (Token token in AFN2.Alfabeto)
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
            AFDEstado inicial = new AFDEstado(0);
            AFDEstado aceptacion = new AFDEstado(1);
            //crear una transicion unica con el simbolo
            Estado tran = new Estado(inicial, aceptacion, simboloRegex);
            inicial.Estados.Add(tran);
            //agrega los estados creados
            automataFN.AFDEstados.Add(inicial);
            automataFN.AFDEstados.Add(aceptacion);
            //colocar los estados iniciales y de acpetacion
            automataFN.Inicial = inicial;
            automataFN.Aceptacion.Add(aceptacion);
            automataFN.LenguajeR = simboloRegex.Lexema + "";
            i++;
            return automataFN;

        }

    }
}