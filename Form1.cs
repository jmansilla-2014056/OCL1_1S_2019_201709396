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

namespace Proyecto1_OLC1
{

    public partial class Form1 : Form
    {
        List<TextControl> listaEntrada = new List<TextControl>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control[] c = listaEntrada.ElementAt(this.tabControl.SelectedIndex).Controls.Find("textEntrada",true);
            RichTextBox richTextBox = c[0] as RichTextBox;
            MessageBox.Show(richTextBox.Text);
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
        }



        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                        textControl.textoEntrada = fileContent;
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
                writer.WriteLine(tc.textoEntrada);
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
                writer.WriteLine(tc.textoEntrada);
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
    }
}
