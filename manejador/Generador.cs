using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;


namespace Proyecto1_OLC1.manejador
{
    class Generador
    {
        String ruta;
        StringBuilder grafo;
        String rtImagen = "";

        public Generador()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Obtiene la ruta del escritorio
        }

        public void generadorDot(String name, String rutaImagen)
        {
            System.IO.File.WriteAllText(name + ".dot", grafo.ToString());
            ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe ");
            startInfo.Arguments = "-Tpng " + name + ".dot -o " + name + ".png ";
            Process.Start(startInfo).WaitForExit();

            //   String comandoDot = "dot.exe -Tpng " + rutaDot + " -o " + rutaImagen + " ";
            //    var comando = string.Format(comandoDot);
            //    var procStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);
            //   var proc = new System.Diagnostics.Process();
            //    proc.StartInfo = procStart;
            //   proc.Start();
            //     proc.WaitForExit();       
        }
        //contenido contiene las lineas
        public void graficar(String contenido, String name)
        {
            grafo = new StringBuilder();
            //   String rutaDot = "\\"+name+".dot";
            //   String rutaImagen = ruta + "\\" + name + ".png";
            //  rtImagen = rutaImagen;
            rtImagen = name + ".png";
            grafo.Append(contenido);
            this.generadorDot(name, "");
            abrirGrafo();
        }

        public String abrirGrafo()
        {
            if (File.Exists(this.rtImagen))
            {
                try
                {
                    System.Diagnostics.Process.Start(this.rtImagen); //Hace que la imagen se abra
                    return this.rtImagen;
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error" + ex);
                    return "";
                }
            }
            else
            {
                Console.WriteLine("Archivo no existe");
                return "";
            }
        }
    }
}

