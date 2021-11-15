using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace cdzmemoria
{
    public partial class Form1 : Form
    {
        int movimentos, cliques, cartasEncontradas, tagIndex;
        Image[] img = new Image[12];
        List<string> lista = new List<string>();
        int[] tags = new int[2];
        public Form1()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {
            lblMovimentos.Text = movimentos.ToString();
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                int tagIndex = int.Parse(String.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = Properties.Resources.verso1;
                item.Enabled = true;
            }
            RandomP();
        }

        private void RandomP()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                Random rdn = new Random();
                int[] xP = { 19, 141, 263, 385, 507, 629, 751, 873 };
                int[] yP = { 26, 176, 326 };

            Repete:
                var x = xP[rdn.Next(0, xP.Length)];
                var y = yP[rdn.Next(0, yP.Length)];

                string verificacao = x.ToString() + y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(x, y);
                    lista.Add(verificacao);
                }
            }
        }

        private void clickImagens(object sender, EventArgs e)
        {
            bool parEncontrado = false;

            PictureBox pic = (PictureBox)sender;
            cliques++;
            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if (cliques == 1)
            {
                tags[0] = int.Parse(String.Format("{0}", pic.Tag));
            }
            else if (cliques == 2)
            {
                movimentos++;
                lblMovimentos.Text = movimentos.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = checkPar();
                desvirar(parEncontrado);
            }
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool checkPar()
        {
            cliques = 0;
            if (tags[0] == tags[1]) { return true; } else { return false; }
        }

        private void desvirar(bool check)
        {
            Thread.Sleep(500);
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if(int.Parse(String.Format("{0}", item.Tag))== tags[0] ||
                   int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;
                    }
                    else 
                    {
                        item.Image = Properties.Resources.verso1;
                        item.Refresh();
                    }
                }
            }
            finalJogo();
        }

        private void finalJogo()
        {
            if(cartasEncontradas== img.Length*2)
            {
                MessageBox.Show("Parabéns! Você terminou o jogo com " + movimentos.ToString() + " movimentos.");
                DialogResult msg = MessageBox.Show("Deseja continuar jogando?", "Caixa e pergunta", MessageBoxButtons.YesNo);

                if(msg == DialogResult.Yes)
                {
                    cliques = 0; movimentos = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();
                }
                else if(msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigada por jogar!");
                    Application.Exit();
                }
            }
        }
    }
}
