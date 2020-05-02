using Proyecto1_OLC1.manejador;
using Proyecto1_OLC1.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Proyecto1_OLC1
{

    public partial class Editor : Form
    {
        int contador = 0;
        string concatenar = "";
        List<TextControl> listaEntrada = new List<TextControl>();
        List<Token> listaTokens = new List<Token>();
        List<Biblioteca> listaPalabras = new List<Biblioteca>();
        ManejarToken mt;
        public static List<Trampa> listaErrores = new List<Trampa>();

        public Editor()
        {
            InitializeComponent();
        }      


        public static void AddError(Token t, string espera)
        {
            listaErrores.Add(new Trampa(t.Lexema, espera,t.Fila,t.Columna));
        }


        private void limpieza()
        {
            listaPalabras.Clear();
            listaTokens.Clear();
            listaErrores.Clear();
            listaPalabras.Add(new Biblioteca(1, "CONJ"));
            concatenar = "";
            contador = 0;
        }

        private void reporteXml()
        {          
                using (var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\report.xml"))
                {
                    if (!listaErrores.Any())
                    {
                        TokenGroup tg = new TokenGroup();
                        tg.token = listaTokens.ToArray();
                        XmlSerializer serializer = new XmlSerializer(typeof(TokenGroup));
                        serializer.Serialize(sw, tg);
                        Reportes reportes = new Reportes(mt.Thompsons);
                        reportes.Show();
                    }
                    else
                    {
                        ErrorGroup eg = new ErrorGroup();
                        eg.error = listaErrores.ToArray();
                        XmlSerializer serializer = new XmlSerializer(typeof(ErrorGroup));
                        serializer.Serialize(sw, eg);
                    }
                    System.Diagnostics.Process.Start("chrome", AppDomain.CurrentDomain.BaseDirectory + @"\report.xml");
                }
        }
                
        private void buttonAnalizar_Click(object sender, EventArgs e)
        {
            limpieza();
            generador();
            mt = new ManejarToken();
            mt.parser(listaTokens);
            reporteXml();

        }



        private void generador()
        {

            listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada += " ";       
            char[] analizar = listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada.ToArray();

            for (int x = 0; x < analizar.Length; x++)
            {
                switch (analizar[x])
                {
                    //no importa solo paso al siguiente   
                    case '\n':
                    case ' ':
                    case '\t':
                    case '\r':
                        contador++;
                        x = contador;
                        break;
                    case '>':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], ">", "Mayor", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '<':
                        contador = x;
                        if (analizar[x + 1] == '!'){
                            try{
       
                                contador = contador + 2;
                                comentario();
                                x = contador;
                                break;
                            }
                            catch (Exception p){
                                listaErrores.Add(new Trampa(concatenar, "No pertenece al lenguaje", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                                concatenar = "";
                                contador--;
                            }

                        }
                        else{
                            listaTokens.Add(new Token((int)analizar[x], "<", "Menor", obtenerFila(contador), obtenerColumna(contador)));
                        }
                        contador++;
                        break;
                    case '{':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "{", "Abrir Llaves", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '}':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "}", "Abrir Llaves", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '/':
                        contador = x;
                        if(analizar[x + 1] == '/')
                        {
                            try
                            {
                                contador = contador + 2;
                                comentarioL();
                                x = contador;
                                break;
                            }
                            catch (Exception p)
                            {
                                listaErrores.Add(new Trampa(concatenar, "No pertenece al lenguaje", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                                concatenar = "";
                                contador--;
                            }
                        }
                        else{
                            listaTokens.Add(new Token((int)analizar[x], "/", "barra", obtenerFila(contador), obtenerColumna(contador)));
                        }
                        contador++;
                        break;
                    case '.':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], ".", "Punto", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '"':
                        contador = x;                  
                        contador++;
                        cadena();
                        
                            x = contador;
                        break;
                    case '|':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "|", "OR", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case ',':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], ",", "Coma", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '%':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "%", "Porcentaje", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '+':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "+", "Mas", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '-':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "-", "Menos", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '*':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "*", "Asterisco", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '?':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "?", "Interrogacion", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '~':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], "~", "Virgulilla", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case ':':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], ":", "Dos Puntos", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case ';':
                        contador = x;
                        listaTokens.Add(new Token((int)analizar[x], ";", "Punto y coma", obtenerFila(contador), obtenerColumna(contador)));
                        break;
                    case '\\':
                        contador = x;
                        try {
                            if (analizar[x + 1] == 'n')
                            {
                                contador++;
                                listaTokens.Add(new Token((int)'\n', "\n", "Salto de linea", obtenerFila(contador), obtenerColumna(contador)));
                            }
                            else if (analizar[x + 1] == 't')
                            {
                                contador++;
                                listaTokens.Add(new Token((int)'\t', "\t", "Tabulacion", obtenerFila(contador), obtenerColumna(contador)));
                            }
                            else if (analizar[x + 1] == '\'')
                            {
                                contador++;
                                listaTokens.Add(new Token((int)'\'', "'", "Comilla Simple", obtenerFila(contador), obtenerColumna(contador)));
                            }
                            else if (analizar[x + 1] == '"')
                            {
                                contador++;
                                listaTokens.Add(new Token((int)'"', "\"", "Comilla doble", obtenerFila(contador), obtenerColumna(contador)));
                            }
                            else if (analizar[x + 1] == 'r')
                            {
                                contador++;
                                listaTokens.Add(new Token((int)'\r', "\r", "Tabulacion", obtenerFila(contador), obtenerColumna(contador)));
                            }
                            else
                            {
                                listaTokens.Add(new Token((int)analizar[x], analizar[x].ToString(), "Simbolo", obtenerFila(contador), obtenerColumna(contador)));
                            }
                                x = contador;
                        }catch(Exception p)
                            {
                            listaErrores.Add(new Trampa(concatenar, "Ocurrio un error", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                            contador--;
                        }
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        contador = x;
                        numero();
                        x = contador;
                        break;
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                        contador = x;
                        palabra();
                        x = contador;
                        break;
                    default:
                        if ((int)analizar[x] >= 33 && (int)analizar[x] <= 125)
                        {
                            contador = x;
                            listaTokens.Add(new Token((int)analizar[x], analizar[x].ToString(), "Simbolo", obtenerFila(contador), obtenerColumna(contador)));
                        }
                        else
                        {
                            contador = x;
                            listaErrores.Add(new Trampa(analizar[x].ToString(), "No pertenece al lenguaje", obtenerFila(contador), obtenerColumna(contador)));
                        }
                        break;
                }
            }

        }


        private void palabra()
        {
            try
            {
                if (Char.IsLetter(listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador]) || Char.IsDigit(listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador]))
                {
                    concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador].ToString();
                    contador++;
                    palabra();
                }
                else
                {
                    Biblioteca palabra = null;
                    palabra = listaPalabras.FirstOrDefault(o => o.Palabra.ToLower().Equals(concatenar.ToLower()));
                    if (palabra != null)
                    {
                        listaTokens.Add(new Token(palabra.Id, palabra.Palabra, "Palabra Reservada", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                    }
                    else
                    {
                        listaTokens.Add(new Token(30, concatenar, "Identificador", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                    }
                    concatenar = "";
                    contador--;
                }
            }
            catch (Exception x)
            {
                listaErrores.Add(new Trampa(concatenar, "No pertenece al lenguaje", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                concatenar = "";
                contador--;

            }
        }

        private void numero()
        {
            if (Char.IsDigit(listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador])){
                concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador];
                contador++;
                numero();
            }
            else{
                listaTokens.Add(new Token(30, concatenar, "Numero", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                concatenar = "";
                contador--;
            }
        }

        private void cadena()
        {
            try
            {
                if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador] != '"')
                {
                    concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador].ToString();

                    if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador].ToString().Equals("\\"))
                    {
                        if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador+1].ToString().Equals("\""))
                        {
                            concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador+1].ToString();
                            contador++;
                        }
                    }
                    
                    contador++;
                    cadena();
                }
                else
                {
                    concatenar = concatenar.Replace("\\n", "\n");
                    concatenar = concatenar.Replace("\\t", "\t");
                    concatenar = concatenar.Replace("\\r", "\r");
                    concatenar = concatenar.Replace("\\\"", "\"");
                    listaTokens.Add(new Token(31, concatenar, "Texto", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                    concatenar = "";
                }
            }
            catch (Exception x)
            {
                listaErrores.Add(new Trampa(concatenar, "Ocurrio un error con una cadena", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                concatenar = "";
                contador--;
            }
        }

        private void comentarioL()
        {
            try
            {
                if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador] != '\n')
                {
                    concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador].ToString();
                    contador++;
                    comentarioL();
                }
                else
                {
                    concatenar = "";
                }
            }
            catch (Exception x)
            {
                listaErrores.Add(new Trampa(concatenar, "Ocurrio un error con un comentario", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                concatenar = "";
                contador--;
            }
        }

        private void comentario()
        {
            try
            {
                if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador] != '!')
                {
                    concatenar = concatenar + listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador].ToString();
                    contador++;
                    comentario();
                }
                else
                {
                    if (listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada[contador + 1] == '>')
                    {
                        contador++;
                        concatenar = "";
                    }
                    else
                    {
                        contador++;
                        comentario();
                    }

                }
            }
            catch (Exception x)
            {
                listaErrores.Add(new Trampa(concatenar, "Ocurrio un error en un comentario", obtenerFila(contador - 1), obtenerColumna(contador - 1)));
                concatenar = "";
                contador--;
            }
        }

        private int obtenerFila(int index)
        {
            int x = -1;
            int y = 0;
                foreach (char c in listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada.ToArray()){
                    x++;
                    if (c == '\n' & index > x){
                        y++;
                    }
                }
            return y;
        }

        private int obtenerColumna(int index)
        {
            int x = 0;
            int y = 0;
                foreach (char c in listaEntrada.ElementAt(this.tabControl.SelectedIndex).TextoEntrada.ToArray()){
                x++;
                if (c == '\n' & index + 1 > x){
                    y = x;
                }
            }
            return index - y;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string titulo = Microsoft.VisualBasic.Interaction.InputBox("Titulo","Titulo del diálogo","Nuevo.er");
            TextControl textControl = new TextControl();
            textControl.Nombre = titulo;
            textControl.Ruta = null;
            listaEntrada.Add(textControl);
            TabPage tp = new TabPage(listaEntrada.ElementAt(listaEntrada.Count - 1).Nombre);
            this.tabControl.TabPages.Add(tp);
            tp.Controls.Add(listaEntrada.ElementAt(listaEntrada.Count - 1));
            habilitar();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            habilitar();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.er)|*.er|All er (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;


                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        //Si todo esta bien crear el TextControl con sus atributos sacados del archivos
                        TextControl textControl = new TextControl();
                        textControl.Ruta = filePath;
                        textControl.Nombre = openFileDialog.SafeFileName;
                        textControl.TextoEntrada = fileContent;
                        listaEntrada.Add(textControl);
                        TabPage tp = new TabPage(textControl.Nombre);
                        this.tabControl.TabPages.Add(tp);
                        tp.Controls.Add(listaEntrada.ElementAt(listaEntrada.Count - 1));

                    }
                }
            }


        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            TextControl tc = listaEntrada.ElementAt(this.tabControl.SelectedIndex);
            if (tc.Ruta != null)
            {
                StreamWriter writer = new StreamWriter(tc.Ruta);
                writer.WriteLine(tc.TextoEntrada);
                writer.Dispose();
                writer.Close();
                MessageBox.Show("Se guardaron los cambios");
            }
            else
            {
                guardarComo();
            }
        }

        private void guardarComo()
        {
            TextControl tc = listaEntrada.ElementAt(this.tabControl.SelectedIndex);
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = tc.Nombre;
            save.Filter = "Text File | *.er";
            if(save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.WriteLine(tc.TextoEntrada);
                writer.Dispose();
                writer.Close();
                listaEntrada.ElementAt(this.tabControl.SelectedIndex).Ruta = save.FileName;
                listaEntrada.ElementAt(this.tabControl.SelectedIndex).Nombre = Path.GetFileName(save.FileName);
                this.tabControl.TabPages[this.tabControl.SelectedIndex].Text = Path.GetFileName(save.FileName);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardarComo();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            reporteXml();
        }

        private void habilitar()
        {
            this.buttonAnalizar.Enabled = true;
            this.buttonReport.Enabled = true;
        }

    }
}
