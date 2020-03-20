using Proyecto1_OLC1.manejador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_OLC1
{
    public partial class Reportes : Form
    {
        List<Thompson> listathompsons = new List<Thompson>();
        public Reportes(List<Thompson> thompsons)
        {
            InitializeComponent();
            listathompsons = thompsons;
            foreach (Thompson t in thompsons)
            {
                this.comboER.Items.Add(t.NameEr);
            }
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            Thompson thompson = listathompsons.FindLast(x => x.NameEr.Equals(this.comboER.SelectedItem.ToString()));
            if (thompson != null)
            {
                thompson.reportar();
              //  thompson.Determinista.graficar("AFD_"+thompson.NameEr);
            }
        }
    }
}
