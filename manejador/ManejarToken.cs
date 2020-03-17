using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_OLC1.manejador
{
    class ManejarToken
    {
        List<Token> listaTokens = new List<Token>();
        List<Conjunto> listaConjuntos = new List<Conjunto>();
        List<Token> filtro = new List<Token>();
        List<Thompson> thompsons = new List<Thompson>();
        Conjunto conj = new Conjunto();
        

        char caracter = new Char();
        int x = -1;
        String nombreEr = "";
        String nombreConj = "";

        public void parser(List<Token> lt)
        {
            listaTokens = lt;
            conjOrID(consumir());


        }

        public Token consumir()
        {
           
                x++;
                if (x <= listaTokens.Count-1)
                {
                    return listaTokens.ElementAt(x);
                }
                else
                {
                    Token t = listaTokens.ElementAt(x - 1);
                    t.Id = 0;
                    return t;
                }
            
        }

        void addRange(Token t)
        {
            char c = caracter;
            if (t.Lexema.Length == 1)
            {
                c = t.Lexema[0];
            }
            else
            {
                c = t.Lexema[0];
                Editor.AddError(t, "se esperaba un char");
            }

  
            if (!(caracter < c))
            {
                char temp = caracter;
                caracter = c;
                c = temp;
            }
            for(int x = (int)caracter; x <= (int)c; x++)
            {
                conj.Caracteres.Add((char)x);
            }
        }

        void addChar(Token t)
        {
            if (t.Lexema.Length == 1)
            {
                conj.Caracteres.Add(t.Lexema[0]);
            }
            else
            {
                conj.Caracteres.Add(t.Lexema[0]);
                Editor.AddError(t, "se esperaba un char");
            }
        }

        //Que venga conjunto o Id
        void conjOrID(Token t)
        {
            if (t.Id == 0)
            {
                return;
            }
            else if (t.Id == 1)
            {
                dosPuntos(consumir());
            }
            else if (t.Id == 32)
            {
                this.nombreEr = t.Lexema;
                guionOrdosPuntos(consumir());
            }
            
            else
            {
                Editor.AddError(t, "Se esperaba CONJ o Identificador");
            }
        }

        //Entra en id se espera guion o dos puntos
        void guionOrdosPuntos(Token t)
        {
            if (t.Id == 45)
            {
                mayorQue(consumir());
            }else if(t.Id == 58)
            {
                texto(consumir());
            }
            else
            {
                Editor.AddError(t, "espera guion o dos puntos");
            }
        }

        void mayorQue(Token t)
        {
            if(t.Id == 62)
            {
                expresion(consumir());
            }
            else
            {
                Editor.AddError(t, "se espera >");
            }
        }

        //Se espera . * | ? +  o abrir llaves o ;
        void expresion(Token t)
        {
            if (t.Id==46 || t.Id == 42 || t.Id == 124 || t.Id == 63 ||  t.Id == 43 || t.Id==31)
            {
                filtro.Add(t);
                expresion(consumir());
            }else if (t.Id == 123)
            {
                idConj(consumir());
            }else if (t.Id == 59)
            {
                Thompson thompson = new Thompson(filtro,nombreEr);
                thompsons.Add(thompson);
                nombreEr = "";
                filtro = new List<Token>();
                conjOrID(consumir());
            }
            else
            {
                Editor.AddError(t, "Se espera . * | ? +  o abrir llaves o ;");
            }
        }

        //despues de llaves se espera identificador
        void idConj(Token t)
        {
            if(t.Id == 32)
            {
                filtro.Add(t);
                llavesCerrar(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaba un identificador");
            }
        }

        void llavesCerrar(Token t)
        {
            if(t.Id == 125)
            {
                expresion(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban llaves que cierran");
            }
        }

        void texto(Token t)
        {
            if(t.Id == 31)
            {
                puntoComa(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaba una cadena");
            }
        }

        void puntoComa(Token t)
        {
            if(t.Id == 59)
            {
                conjOrID(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaba ;");
            }
        }

        //Entro a Conj se esperan pos puntos
        void dosPuntos(Token t)
        {
            if (t.Id == 58)
            {
                id(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban :");
            }
        }

        //Entran a 2 puntos se espera id
        void id(Token t)
        {
            if (t.Id == 32)
            {
                this.nombreConj = t.Lexema;
                guion(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban id");
            }
        }

        //Entra en id se espera guion
        void guion(Token t)
        {
            if (t.Id == 45)
            {
                mayor(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban -");
            }
        }

        //Entra en guion se espera mayor >
        void mayor(Token t)
        {
            if (t.Id == 62)
            {
                idOrNumOrSymbol(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban >");
            }
        }

        //Entra en > se espera Identificador o Numero o Simbolo
        void idOrNumOrSymbol(Token t)
        {
            caracter = t.Lexema[0];
            addChar(t);
            //Id
            if (t.Id == 32)
            {
                VirOrComaOrPunto0(consumir());
            }
            //Numero
            else if (t.Id == 30)
            {
                VirOrComaOrPunto1(consumir());
            }
            else if (t.Id >= 33 && t.Id <= 125)
            //Simbolo
            {
                VirOrComaOrPunto2(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban id o numero o simbolos validos");
            }

        }

        //Entra en Id se espeta coma o virgulilla o punto y coma
        void VirOrComaOrPunto0(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                finalConj();             
            }
            //Viene ,?  
            else if (t.Id == 44)
            {
                id1(consumir());
            }
            //Viene ~?
            else if (t.Id == 126)
            {
                id2(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban >");
            }
        }

        //Entra en numero se espera virgulilla o otro numero o punto y coma
        void VirOrComaOrPunto1(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                finalConj();                
            }
            //Viene ,?   
            else if (t.Id == 44)
            {
                num1(consumir());
                
            }
            //Viene ~?
            else if (t.Id == 126)
            {
                
                num2(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban >");
            }
        }     
       
        //Entran en simbolo se espera punto y coma o coma o virgulilla
        void VirOrComaOrPunto2(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                finalConj();
            }
            //Viene ,?    
            else if (t.Id == 44)
            {
                symbol1(consumir());               
            }
            //Viene ~?
            else if (t.Id == 126)
            {
                symbol2(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban >");
            }
        }
       
        void comaOrPunto0(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                conjOrID(consumir());
            }
            //Viene ,?    
            else if (t.Id == 44)
            {
                id1(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban , o ;");
            }
        }

        void comaOrPunto1(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                conjOrID(consumir());
                //Viene ,?    
            }
            else if (t.Id == 44)
            {
                num1(consumir());

            }
            else
            {
                Editor.AddError(t, "Se esperaban , o ;");
            }
        }

        void comaOrPunto2(Token t)
        {
            //viene punto y coma?
            if (t.Id == 59)
            {
                conjOrID(consumir());
                //Viene ,?    
            }
            else if (t.Id == 44)
            {
                symbol1(consumir());

            }
            else
            {
                Editor.AddError(t, "Se esperaban , o ;");
            }
        }

        void id1(Token t)
        {
            addChar(t);
            if (t.Id == 32)
            {
                comaOrPunto0(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban id");
            }
        }

        void id2(Token t)
        {
            addRange(t);
            if (t.Id == 32)
            {
                puntoYcoma(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban id");
            }
        }

        void num1(Token t)
        {
            addChar(t);
            if (t.Id == 30)
            {
                comaOrPunto1(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban numero");
            }
        }

        void num2(Token t)
        {
            addRange(t);
            if (t.Id == 30)
            {
                puntoYcoma(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban numero");
            }
        }

        void symbol1(Token t)
        {
            addChar(t);
            if (t.Id >= 33 && t.Id <= 125)
            {
                comaOrPunto2(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban simbolo valido");
            }
        }

        void symbol2(Token t)
        {
            addRange(t);
            if (t.Id >= 33 && t.Id <= 125)
            {
                puntoYcoma(consumir());
            }
            else
            {
                Editor.AddError(t, "Se esperaban simbolo valido");
            }
        }

        void puntoYcoma(Token t)
        {
            if (t.Id == 59)
            {
                finalConj();
            }
            else
            {
                Editor.AddError(t, "Se esperaba ;");
            }
        }

        void finalConj()
        {
            conj.NombreConjunto = this.nombreConj;
            listaConjuntos.Add(conj);
            caracter = new Char();
            conj = new Conjunto();
            conjOrID(consumir());
        }

    }
}
