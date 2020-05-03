using Proyecto1_OLC1.manejador;
using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_OLC1
{
    public partial class Reportes : Form
    {
        List<Thompson> listathompsons = new List<Thompson>();
        List<Validar> validaciones = new List<Validar>();
        string capturar = null;
        public Thompson seleccionado;
        public Reportes(List<Thompson> thompsons)
        {
            validaciones = ManejarToken.validaciones;
            InitializeComponent();
            listathompsons = thompsons;
            foreach (Thompson t in thompsons)
            {
                this.comboER.Items.Add(t.NameEr);
                this.comboER.SelectedIndex = 0;
            }
            
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            Thompson thompson = listathompsons.FindLast(x => x.NameEr.Equals(this.comboER.SelectedItem.ToString()));
            if (thompson != null)
            {
                seleccionado = thompson;
                Image image = Image.FromFile(thompson.reportar());
                while (true)
                {
                    if(image != null)
                    {
                        break;
                    }
                }
                Thread.Sleep(1000);
                Clipboard.SetImage(image);
                this.richTextBox2.ReadOnly = false;
                this.richTextBox2.Clear();
                Thread.Sleep(1000);
                this.richTextBox2.Paste();
                this.richTextBox2.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Thompson thompson = listathompsons.FindLast(x => x.NameEr.Equals(this.comboER.SelectedItem.ToString()));
            if (thompson != null)
            {
                seleccionado = thompson;
                Image image = Image.FromFile(thompson.Deterministas.graficar(thompson.NameEr));
                while (true)
                {
                    if (image != null)
                    {
                        break;
                    }
                }
                // this.pictureBox1.Image = image;
                Thread.Sleep(1000);
                Clipboard.SetImage(image);
                this.richTextBox2.ReadOnly = false;
                this.richTextBox2.Clear();
                Thread.Sleep(1000);
                this.richTextBox2.Paste();
                this.richTextBox2.ReadOnly = true;
            }
            
        }

        private void comboValidaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboER_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Thompson thompson = listathompsons.FindLast(x => x.NameEr.Equals(this.comboER.SelectedItem.ToString()));
            if (thompson != null)
            {
                seleccionado = thompson;
                Image image = Image.FromFile(thompson.Deterministas.sacaTablas(thompson.NameEr));
                while (true)
                {
                    if (image != null)
                    {
                        break;
                    }
                }
                // this.pictureBox1.Image = image;
                Thread.Sleep(1000);
                Clipboard.SetImage(image);
                this.richTextBox2.ReadOnly = false;
                this.richTextBox2.Clear();
                Thread.Sleep(1000);
                this.richTextBox2.Paste(); 
                this.richTextBox2.ReadOnly = true;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Thompson thompson = listathompsons.FindLast(x => x.NameEr.Equals(this.comboER.SelectedItem.ToString()));
            if (thompson != null)
            {
                seleccionado = thompson;
                foreach (Validar v in ManejarToken.validaciones)
                {
                    if (v.NombreEr.Equals(comboER.SelectedItem.ToString()))
                    {
                        richTextBox1.Text += "-----------------------------------------------------------------" + "\n";
                        richTextBox1.Text += "CADENA:" + v.Cadenas + "\n RESULTADO:" + Cadena(v.Cadenas, seleccionado.Deterministas.DescripcionDelAFD).ToString() + "\n";
                        richTextBox1.Text += capturar;
                    }
                }
            }
        }


        public void enviar(string s)
        {
                capturar = s;
        }

        public bool Cadena(String cadena, List<AFD> AFD)
        {
            capturar = null;
            //Se utilizan 2 variables
            //cont define el caracter en el que vamos
            //basecont es una variable de seguridad para detectar cuando una estado no tiene transiciones
            int cont = 0, basecont = AFD.Count;

            //obtenemos el estado inicial del AFD
            AFDEstado actual = AFD.ElementAt(0).inicio;

            //analizamos para cada caracter en la cadena
            while (cont < cadena.Length)
            {
                basecont = AFD.Count;
                //analizamos para cada transicion en el AFD
                for (int i = 0; i < AFD.Count; i++)
                {
                    //obtenemos el caracter a analizar
                    char c = cadena[cont];

                    // Cadena
                    if (AFD.ElementAt(i).simbolo.Id == 31)
                    {
                        bool bandera1 = true;
                        char[] literal = AFD.ElementAt(i).simbolo.Lexema.ToCharArray();
                        cont--;
                        foreach(char d in literal)
                        {
                            cont++;
                            try
                            {
                                c = cadena[cont];
                            }
                            catch(Exception q)
                            {
                                enviar("\n Error no se alcazo a completar el lexema ->" + AFD.ElementAt(i).simbolo.Lexema + "\n");
                                return false;
                            }
                            if (!(d==c))
                            {
                                bandera1 = false;
                                break;
                            }
                        }

                        //Checa si el estado y el simbolo actual coincide con el AFD
                        if (AFD.ElementAt(i).inicio == actual && bandera1)
                        {
                            actual = AFD.ElementAt(i).final;
                            cont++;
                            basecont = AFD.Count;
                            if (cont == cadena.Length)
                            {
                                enviar("\n EL proceso termino en el estado:" + actual.NombreChar + "\n");
                                return actual.final;
                            }
                        }
                        else
                        {
                            enviar("\n Error de cadena o conjunto No.Caracter:"+  cont +" Estado:"  + actual.NombreChar + "\n");
                            if (cont == 10)
                            {
                                Console.WriteLine("mixca");
                            }
                        }
                    }


                    // identificador
                    else if(AFD.ElementAt(i).simbolo.Id == 30)
                    {

                        bool bandera = true;
                        List<Conjunto> conj = this.seleccionado.conjuntos;

                        Conjunto conjunto = conj.FirstOrDefault(x => x.NombreConjunto.Equals(AFD.ElementAt(i).simbolo.Lexema));

                        if (conjunto != null)
                        {
                            bandera = conjunto.Caracteres.Contains(c);
                        }
                        else
                        {                  
                            bandera = false;
                        }

                        if (AFD.ElementAt(i).inicio == actual && bandera)
                        {
                            actual = AFD.ElementAt(i).final;
                            cont++;
                            basecont = AFD.Count;
                            if (cont == cadena.Length)
                            { 
                                enviar("\n EL proceso termino en el estado:" + actual.NombreChar + "\n");
                                return actual.final;
                            }
                        }
                        else
                        {                     
                            enviar("\n Error de cadena o conjunto No.Caracter:" + cont + " Estado:" + actual.NombreChar + "\n");
                            if (cont == 10)
                            {
                                Console.WriteLine("mixca");
                            }
                        }
                    }
                    
                    basecont--;
                    if (basecont == 0)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        

    }
}
