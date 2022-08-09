using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorGrafos
{
    public partial class Matriz : Form
    {
        private int[,] m;
        public int i, j;
        Graphics g;
        public Font letra;
        SolidBrush brocha;
        Bitmap bmp1, bmp2;
        List<CNodo> listaNodos;

        private List<CNodo> Lk, Lm;

        public Matriz()
        {
            InitializeComponent();
        }

        private void Matriz_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            letra = new Font("Arial", 12);
            brocha = new SolidBrush(Color.Black);

            bmp1 = new Bitmap(ClientSize.Width, ClientSize.Height);

            Lk = new List<CNodo>();
            Lm = new List<CNodo>();
        }

        public Matriz(int [,]mat, int i, int j)
        {
            this.i = i;
            this.j = j;
            m = new int[i,j];            
            m = mat;

            InitializeComponent();
        }

        public Matriz(int[,] mat, int i, int j, List<CNodo> list)
        {
            this.i = i;
            this.j = j;
            m = new int[i, j];
            m = mat;
            listaNodos = list;

            InitializeComponent();
        }

        public int[] EsBiparitoKM(List<CNodo> nodos, int[,] MR)
        {
            int[] km = { 0, 0 };
            if (!EsBipartito(nodos, MR))
                return km;
            for (int i = 0; i < Lk.Count; i++)
                for (int j = 0; j < Lm.Count; j++)
                    if (!SeRelacionan(Lk[i], Lm[j]))
                        return km;
            km[0] = Lk.Count;
            km[1] = Lm.Count;
            return km;
        }

        public bool EsBipartito(List<CNodo> nodos, int[,] MR)
        {
            if (nodos.Count < 2)
                return false;
            int suma;
            for (int i = 0; i < nodos.Count; i++)
            {
                suma = 0;
                for (int j = 0; j < nodos.Count; j++)
                {
                    if (i == j && MR[i, j] == 1)
                        return false;
                    suma += MR[i, j];
                }
                if (suma == 0)
                    return false;
            }
            List<CNodo> l1 = new List<CNodo>();
            List<CNodo> l2 = new List<CNodo>();
            int index;
            bool b1, b2;
            l1.Add(nodos[0]);
            while (l1.Count + l2.Count < nodos.Count)
            {
                b1 = b2 = false;
                for (int i = 0; i < l1.Count; i++)//agregando relaciones de nodos de lista1 a lista2
                    for (int j = 0; j < l1[i].LRel.Count; j++)
                    {
                        index = IndiceNodo(nodos, l1[i].LRel[j]);
                        if (!l2.Contains(nodos[index]))
                        {
                            //MessageBox.Show("metiendo a "+nodos[index].ID+" a l2");
                            l2.Add(nodos[index]);
                            b1 = true;
                        }
                    }
                for (int k = 0; k < l2.Count; k++)//agregando relaciones de nodos de lista2 a lista1
                    for (int l = 0; l < l2[k].LRel.Count; l++)
                    {
                        index = IndiceNodo(nodos, l2[k].LRel[l]);
                        if (!l1.Contains(nodos[index]))
                        {
                            //MessageBox.Show("metiendo a " + nodos[index].ID + " a l1");
                            l1.Add(nodos[index]);
                            b2 = true;
                        }
                    }
                if (b1 == false && b2 == false)//si no se agregaron nodos, agrega uno "valido" a la lista1
                    for (int i = 1; i < nodos.Count; i++)
                        if (!l1.Contains(nodos[i]) && !l2.Contains(nodos[i]))
                        {
                            if (!SeRelacionan(nodos[i], nodos[0]))
                            {
                                l1.Add(nodos[i]);
                                //MessageBox.Show("metiendo extra a " + nodos[i].ID + " a l1");
                                i = 1000;
                            }
                        }
            }
            for (int i = 0; i < l1.Count; i++)
                for (int j = 0; j < l1.Count; j++)
                    if (SeRelacionan(l1[i], l1[j]))
                        return false;
            for (int i = 0; i < l2.Count; i++)
                for (int j = 0; j < l2.Count; j++)
                    if (SeRelacionan(l2[i], l2[j]))
                        return false;
            Lk = l1;
            Lm = l2;
            return true;
        }

        public bool SeRelacionan(CNodo n1, CNodo n2)
        {
            if (IndiceNodo(n1.LRel, n2) >= 0 || IndiceNodo(n2.LRel, n1) >= 0)
                return true;
            return false;
        }

        public int IndiceNodo(List<CNodo> nodos, CNodo n)
        {
            return nodos.FindIndex(nodo => nodo.GSNombre == n.GSNombre);
        }

        public bool EsCiclo(List<CNodo> nodos, int[,] MR)
        {
            int suma;
            for (int i = 0; i < nodos.Count; i++)
            {
                suma = 0;
                for (int j = 0; j < nodos.Count; j++)
                    suma += MR[i, j];
                if (suma > 2)
                    return false;
            }
            for (int i = 0; i < nodos.Count;)
            {
                if (nodos[i].LRel.Count == 0)
                    return false;
                for (int j = 0; j < nodos[i].LRel.Count; j++)
                    i = IndiceNodo(nodos, nodos[i].LRel[j]);
                if (nodos[i] == nodos[0])
                    return true;
            }
            return false;
        }

        private void Matriz_Shown(object sender, EventArgs e)
        {
            int tamX, tamY;

            /*
            tamX = (int)(letra.Size + (letra.Size / 8)) * i;
            tamY = (int)(letra.Size + (letra.Size / 8)) * i;
            SetClientSizeCore(tamX, tamY);
            */
            
            g.Clear(BackColor);
            Matriz_Paint(this, null);
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Matriz_Resize(object sender, EventArgs e)
        {

            //aceptar.SetBounds(this.ClientRectangle.Right - (aceptar.Width)-10, aceptar.Location.Y, aceptar.Width, aceptar.Left );
            /*
            bmp2 = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics pagina = Graphics.FromImage(bmp2);

            pagina.DrawImage(bmp1, 0, 0);
            bmp1.Dispose();
            bmp1 = (Bitmap)bmp2.Clone();
            Matriz_Paint(this, null);
            */
        }
        
        private void informacion_Click(object sender, EventArgs e)
        {
            int r, c;
            int orejas = 0, cont;
            bool d = true;
            string msg = "";


            for (r = 0; r < i; r++)
            {
                if(m[r,r]==1)
                {
                    orejas++;
                }
            }

            for (r = 0; r < i; r++)
            {
                for (c = r; c < i; c++)
                {
                    d = (m[r, c] == m[c, r]);
                    if (d == false)
                        break;
                }
                if (d == false)
                    break;
            }

            msg += "El grafo tiene " + orejas.ToString() + " orejas.\n";

            if (d == false)
            {
                msg += "El grafo es dirigido.\n";
            }
            else
            {
                msg += "El grafo no es dirigido.\n";
            }

            cont = 0;
            for (r = 0; r < i; r++)
            {
                for (c = r + 1; c < i; c++)
                {
                    if (m[r, c] == 1 || m[c, r] == 1)
                        cont++;
                }
            }

            if (cont == (i * (i - 1)) / 2)
            {
                msg += "El grafo es completo K" + cont.ToString() + ".\n";
            }
            else
            {
                msg += "El grafo no es completo.\n";
            }
            if(EsBipartito(listaNodos, m))
            {
                int[] m2= { 0, 0};
                m2 = EsBiparitoKM(listaNodos, m);
                msg += "Es Bipartito. "+"K: "+m2[0].ToString()+". M: " + m2[0].ToString() +"\n";
            }
            else
            {
                msg += "No es Bipartito. \n";
            }

            if( EsCiclo(listaNodos, m) )
            {
                msg += "Es Ciclico. \n";
            }
            else
            {
                msg += "No es Ciclico. \n";
            }
            //MessageBox.Show(msg,);
            Microsoft.VisualBasic.Interaction.MsgBox(msg);
        }

        private void Matriz_Paint(object sender, PaintEventArgs e)
        {
            int r, c;
            int init = 20;
            int X=init, Y=init;

            for (r = 0; r < i; r++)
            {
                for (c = 0; c < i; c++)
                {
                    g.DrawString( m[r,c].ToString(), letra, brocha, X, Y);
                    //textoM.Text += m[r, c].ToString();
                    X += (int)letra.Size;
                }
                X = init;
                Y += (int)(letra.Size + (letra.Size / 8));
            }
        }       
    }
}
