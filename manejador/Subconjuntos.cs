using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_OLC1.manejador
{
    public class Subconjuntos
    {
        HashSet<Token> alfabeto;
        AFDEstado inicio;
        int inicial;
        AFDEstado afdInicial;
        //PILAS Y ARREGLOS PARA EL MANEJO DE DATOS
        List<Estado> estados = new List<Estado>();
        Stack<Estado> estados_pendientes = new Stack<Estado>();
        List<AFDEstado> tablaDeEstados = new List<AFDEstado>();
        Queue<AFDEstado> AFDpendientes = new Queue<AFDEstado>();
        public List<AFD> DescripcionDelAFD = new List<AFD>();
        Automata automata;

        public Subconjuntos(Automata a)
        {
            alfabeto = a.Alfabeto;
            AdaptaEstados(a);
            inicial = a.Inicial.NombreId;
            afdInicial = a.Inicial;
            automata = a;
            Calculo();
        }

        private void AdaptaEstados(Automata a)
        {
            foreach (AFDEstado e in a.AFDEstados)
            {
                List<Estado> transiciones = e.Estados;
                foreach (Estado t in transiciones)
                {
                    estados.Add(t);
                }

            }
        }

        private void Calculo()
        {
            //CERRADURA EPSILON AL ESTADO INICIAL
            Cerradura(null, inicial, new Token(999, "ε", "Epsilon", 0, 0), 1);

            //CALCULAMOS LOS DISTINTOS ESTADOS
            while (AFDpendientes.Count > 0)
            {
                for (int i = 0; i < alfabeto.Count; i++)
                {
                    AFDEstado estadotmp = Mover(AFDpendientes.Peek(), alfabeto.ElementAt(i));

                    //SE VERIFICA QUE EL RESULTADO DE MOVER DIERA UN ESTADO VALIDO (NO POZO)
                    if (estadotmp.llave.Count > 0)
                    {
                        //VERIFICA SI EL ESTADO YA EXISTE
                        if (EstadoPrevio(estadotmp) == false)
                        {
                            AFDEstado result = null;

                            //SI LA LLAVE DEL ESTADO NO EXISTE PREVIAMENTE
                            for (int j = 0; j < estadotmp.intestado.Count; j++)
                            {
                                result = Cerradura(estadotmp, estadotmp.intestado.ElementAt(j), new Token(999, "ε", "Epsilon", 0, 0), estadotmp.NombreId);
                            }
                            if (result != null)
                            {
                                //SE ASIGNA UN NOMBRE UNICO
                                result.NombreId = tablaDeEstados.Count + 1;

                                //SE AGREGA EL ESTADO A LA TABLA
                                tablaDeEstados.Add(result);

                                //SE GUARDA SI EL ESTADO ES FINAL O NO
                                esFinal(result);

                                //SE AGREGA EL ESTADO A LA DESCRIPCION DE ESTADOS
                                DescripcionDelAFD.Add(new AFD(AFDpendientes.Peek(), alfabeto.ElementAt(i), result));
                            }
                        }
                        else
                        {
                            //SI LA LLAVE DEL ESTADO YA SE HABIA CALCULADO EN UN ESTADO PREVIO
                            DescripcionDelAFD.Add(new AFD(AFDpendientes.Peek(), alfabeto.ElementAt(i), NombrePrevio(estadotmp)));
                        }
                    }
                }
                AFDpendientes.Dequeue();
            }
        }

        private AFDEstado NombrePrevio(AFDEstado estadoactual)
        {
            //REVISAR IMPLEMENTACCION
            for (int i = 0; i < tablaDeEstados.Count; i++)
            {
                if (Igual(tablaDeEstados.ElementAt(i).llave, estadoactual.llave))
                {
                    return tablaDeEstados.ElementAt(i);
                }
            }
            return null;
        }


        private bool EstadoPrevio(AFDEstado estadoactual)
        {
            bool existe = false;
            for (int i = 0; i < tablaDeEstados.Count; i++)
            {
                if (Igual(tablaDeEstados.ElementAt(i).llave, estadoactual.llave))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        private bool Igual(List<int> a, List<int> b)
        {
            bool res = false;
            int sum = 0;
            if (a.Count == b.Count)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (a.ElementAt(i) == b.ElementAt(i))
                    {
                        sum++;
                    }
                }
                if (sum == a.Count)
                {
                    res = true;
                }
            }
            return res;
        }

        private AFDEstado Mover(AFDEstado estado, Token simbolo)
        {
            AFDEstado temp = new AFDEstado(tablaDeEstados.Count + 1);
            for (int i = 0; i < estado.intestado.Count; i++)
            {
                for (int j = 0; j < estados.Count; j++)
                {
                    if (estado.intestado.ElementAt(i) == estados.ElementAt(j).Inicio.NombreId)
                    {
                        if (estados.ElementAt(j).Simbolo.Id == simbolo.Id && estados.ElementAt(j).Simbolo.Lexema.Equals(simbolo.Lexema))
                        {
                            if (!temp.intestado.Contains(estados.ElementAt(j).Fin.NombreId))
                            {
                                temp.intestado.Add(estados.ElementAt(j).Fin.NombreId);
                                temp.llave.Add(estados.ElementAt(j).Fin.NombreId);
                            }
                        }
                    }
                }
            }
            temp.llave.Sort();
            return temp;
        }

        private void esFinal(AFDEstado estado)
        {
            estado.final = false;
            for (int i = 0; i < estado.intestado.Count; i++)
            {
                if (automata.Aceptacion.Any(u => u.NombreId == (estado.intestado.ElementAt(i))))
                {
                    estado.final = true;
                }
            }
        }

        private AFDEstado Cerradura(AFDEstado estadoactual, int inicio, Token simbolo, int Nombre)
        {
            AFDEstado estado;
            if (estadoactual == null)
            {
                //CREAR UN NUEVO ESTADO AFD
                estado = new AFDEstado(Nombre);
                estado.intestado.Add(inicio);
                estado.llave.Add(inicio);
                tablaDeEstados.Add(estado);
            }
            else
            {
                estado = estadoactual;
                if (!estado.intestado.Contains(inicio))
                {
                    estado.intestado.Add(inicio);
                }
            }

            for (int i = 0; i < estados.Count; i++)
            {
                if (estados.ElementAt(i).Inicio.NombreId == inicio)
                {
                    //Quizas lleve otra condicion
                    if (estados.ElementAt(i).Simbolo.Id == simbolo.Id && estados.ElementAt(i).Simbolo.Lexema.Equals(simbolo.Lexema))
                    {
                        if (!estados_pendientes.Contains(estados.ElementAt(i)))
                        {
                            estados_pendientes.Push(estados.ElementAt(i));
                        }
                    }
                }
            }
            while (estados_pendientes.Count > 0)
            {
                Cerradura(estado, estados_pendientes.Pop().Fin.NombreId, simbolo, estado.NombreId);
            }
            if (!AFDpendientes.Contains(estado))
            {
                AFDpendientes.Enqueue(estado);
            }
            return estado;
        }


        public String ImprimeFinales()
        {
            String str = "[";
            for (int i = 0; i < tablaDeEstados.Count; i++)
            {
                if (tablaDeEstados.ElementAt(i).final == true)
                {
                    str = str + "'" + tablaDeEstados.ElementAt(i).NombreChar + "',";
                }
            }
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str + "]";

        }

        //IMPRIME LA DESCRIPCION DEL AFD
        public void ImprimeAFD()
        {
            for (int i = 0; i < DescripcionDelAFD.Count; i++)
            {
                System.Windows.Forms.MessageBox.Show(DescripcionDelAFD.ElementAt(i).Descripcion());
                Console.WriteLine(DescripcionDelAFD.ElementAt(i).Descripciongraphviz());
            }

        }

        public String graficar(string nombre)
        {
            nombre = nombre + "_AFD";
            string texto = "digraph " + nombre + " {\n";
            texto += "\trankdir=LR;" + "\n";

            texto += "\tgraph [label=\"" + nombre + "\", labelloc=t, fontsize=20]; \n";
            texto += "\tnode [style = filled,color = mediumseagreen];";
            texto += "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";
            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> " + "A" + " [label=\"inicio\"];" + "\n";

            for (int i = 0; i < DescripcionDelAFD.Count; i++)
            {
                texto += "\n";
                texto += "\t";
                texto += DescripcionDelAFD.ElementAt(i).Descripciongraphviz();
            }

            for (int i = 0; i < tablaDeEstados.Count; i++)
            {
                if (tablaDeEstados.ElementAt(i).final == true)
                {
                    texto += "\n";
                    texto += "\t";
                    texto +=  tablaDeEstados.ElementAt(i).NombreChar + "[shape=doublecircle];";
                }
            }

            texto += "\n }";
            Generador g = new Generador();
            return g.graficar(texto, nombre);

        }


        public String sacaTablas(string nombre)
        {

            SortedSet<string> letras = new SortedSet<string>();
            
            foreach(AFD l in this.DescripcionDelAFD)
            {
                letras.Add(l.inicio.NombreChar);
                letras.Add(l.final.NombreChar);
            }

            AFDEstado[,] tabla = new AFDEstado[letras.Count,alfabeto.Count];

            int x = 0;
            int y = 0;


            foreach(string xx in letras)
            {
                y = 0;
                foreach (Token yy in this.alfabeto)
                {
                    foreach(AFD es in this.DescripcionDelAFD)
                    {
                        if (xx.Equals(es.inicio.NombreChar) && yy.Lexema.Equals(es.simbolo.Lexema) && yy.Id == es.simbolo.Id)
                        {
                            tabla[x, y] = es.final;
                            break;
                        }
                    }
                    y++;
                }
                x++;
            }


            nombre = nombre + "_TABLA";
            string texto = "digraph " + nombre + " {\n";
                texto+="\n graph [ratio=fill];\n" +
                "\n node [label=\"\\N\", fontsize=15, shape=plaintext];\n" +
                "\n arset [label=<\n" +
                "\n <TABLE ALIGN=\"LEFT\">\n" +
                "\n         <TR>\n";
                texto += "              <TD>Estado</TD>\n";
                foreach (Token yy in this.alfabeto)
                {
                    texto += "              <TD>"+ yy.Lexema.Replace(">", "&#62;").Replace("<", "&#60;").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\"", "\\\"")+"</TD>\n";
                }

            texto += "\n    </TR>\n";

            for (int i = 0; i < letras.Count; i++)
            {
                texto += ("    <TR>\n");
                texto += ("        <TD>");

                AFD comprobar = this.DescripcionDelAFD.FirstOrDefault(p => p.final.NombreChar.Equals(letras.ElementAt(i)) && p.final.final == true);
                if(comprobar != null)
                {
                    texto += (letras.ElementAt(i) + "*</TD>\n");
                }
                else
                {
                    texto += (letras.ElementAt(i) + "</TD>\n");
                }
                

                for (int j = 0; j < alfabeto.Count; j++)
                {
                    texto += ("         <TD>");
                    if(tabla[i, j] != null)
                    {
                        texto += tabla[i, j].NombreChar;

                        if (tabla[i, j].final)
                        {
                            texto += "*";
                        }
                    }
                    else
                    {
                        texto += "-";
                    }
                        texto+="</TD>\n";
                }
                texto += ("     </TR>\n");
            }
            texto += " </TABLE>\n" +
                    "    >, ];\n" +
                    "}";

            texto += "\n }";
            Generador g = new Generador();
            return g.graficar(texto, nombre);

        }

    }
}
