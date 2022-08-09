using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EditorGrafos
{
    public partial class isomorfismo : Form
    {
        public CGrafo grafo;
        CNodo nodo, nAux, nA, nB;
        CArista auxArista;
        public Graphics g;
        public GraphicsPath gp;

        private int accion;
        private Pen egPenLine, egPenDoble;
        private Pen egPen;
        private SolidBrush egBrush, egBrushR;
        Color colorRelleno, colorContorno;
        Font letra;
        Bitmap bmp;
        bool band, letraNodo, muestrapeso, coordNodo;
        Point pf, pf2, pfAnt, pfRes;
        int anchoL, ancho, alto, tam;
        Form matrizForm;

        /*public isomorfismo()
        {
            InitializeComponent();
        }*/

        public isomorfismo()
        {
            //grafo = graf;
            grafo = new CGrafo();

            InitializeComponent();
        }

        private void isomorfismo_Load(object sender, EventArgs e)
        {
            nodo = new CNodo();
            nAux = new CNodo();
            nA = new CNodo();
            nB = new CNodo();
            auxArista = new CArista();


            g = CreateGraphics();
            gp = new GraphicsPath();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            band = false;
            letraNodo = true;
            muestrapeso = false;
            coordNodo = false;
            accion = 0;
            colorContorno = Color.Black;
            colorRelleno = BackColor;


            g.SmoothingMode = SmoothingMode.AntiAlias;
            band = false;
            letraNodo = true;
            muestrapeso = false;
            coordNodo = false;
            accion = 0;
            colorContorno = Color.Black;
            colorRelleno = BackColor;
            anchoL = 1;
            tam = 50;
            egBrush = new SolidBrush(colorContorno);
            egBrushR = new SolidBrush(colorRelleno);
            egPen = new Pen(egBrush, anchoL);

            egPenLine = new Pen(egBrush, anchoL);
            egPenDoble = new Pen(egBrush, anchoL);
            GraphicsPath egPathLine = new GraphicsPath();
            egPathLine.AddLine(new Point(0, 0), new Point(4, -4));
            egPathLine.AddLine(new Point(0, 0), new Point(-4, -4));

            egPenLine.CustomEndCap = new CustomLineCap(null, egPathLine);

            egPenDoble.CustomEndCap = new CustomLineCap(null, egPathLine);
            egPathLine.Reverse();
            egPenDoble.CustomStartCap = new CustomLineCap(null, egPathLine);

            pf = new Point();
            pf2 = new Point();
            pfAnt = new Point();
            pfRes = new Point();
            pfAnt = pf;
            letra = new Font("Arial", 10);

            grafo = new CGrafo();           
            nA = new CNodo();
            nB = new CNodo();
            nodo = new CNodo();
            nAux = new CNodo();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        public CGrafo getGrafo()
        {
            return grafo;
        }

        private void matriz_Click(object sender, EventArgs e)
        {
            grafo.generaMatriz();
            matrizForm = new Matriz(grafo.matriz, grafo.ListaNodos.Count, grafo.ListaNodos.Count);
            matrizForm.Show();
        }

        private void cambiaSentido(CArista ari)
        {
            ari.cambiaSentido();
        }

        private CArista click_Arista(Point p)
        {
            CArista auxAri = new CArista();

            GraphicsPath path = new GraphicsPath();

            auxAri = null;

            foreach (CArista ari in grafo.ListaAristas)
            {

                if (ari.GSOrigen != ari.GSDestino)
                {
                    path.AddLine(ari.GSOrigen.GSCentro, ari.GSDestino.GSCentro);
                }
                else
                {
                    path.AddArc(ari.GSOrigen.GSRect.Location.X - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSRect.Location.Y - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSTam / 2, ari.GSOrigen.GSTam / 2, 30, 340);
                }


                if (grafo.dirigido == true)
                {
                    if (path.IsOutlineVisible(p, egPenLine))
                    {
                        auxAri = ari;
                        break;
                    }
                    /*
                    else
                    {
                        if (path.IsOutlineVisible(p, egPenDoble))
                        {
                            auxAri = ari;
                            break;
                        }
                    }
                    */

                }
                else
                {
                    if (path.IsOutlineVisible(p, egPen))
                    {
                        auxAri = ari;
                        break;
                    }
                }
            }

            return auxAri;
        }

        private void cambiaSent_Click(object sender, EventArgs e)
        {
            if (grafo.dirigido == true)
            {
                accion = 7;
            }
        }

        private void cambiaPeso(CArista arista)
        {
            string peso;
            peso = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Peso", "Peso", arista.GSPeso.ToString(), 100, 100);
            arista.GSPeso = Convert.ToInt32(peso);
        }

        private void eliminaArista(CArista arista)
        {
            int i = 0;

            foreach (CArista ari in grafo.ListaAristas)
            {
                if (ari.GSOrigen == arista.GSOrigen && ari.GSDestino == arista.GSDestino)
                {
                    //grafo.ListaAristas.RemoveAt(i);
                    grafo.ListaAristas.Remove(ari);
                    break;
                }
                i++;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string linea;
            int ac = 0, i;
            string sNum = "", sPeso = "", sTam = "", sX = "", sY = "", sNA = "", sNB = "", sDiri = "";
            int num, peso, tam, cont;
            Point pt;
            bool diri = false;

            grafo.resetGrafo();

            using (StreamReader archivo = new StreamReader(openFileDialog1.FileName))
            {
                while (!archivo.EndOfStream)
                {
                    linea = archivo.ReadLine();
                    switch (linea)
                    {
                        case "nombre":
                            linea = archivo.ReadLine();
                            ac = 1;
                            break;
                        case "matriz":
                            ac = 2;
                            break;
                        case "nodos":
                            linea = archivo.ReadLine();
                            ac = 3;
                            break;
                        case "aristas":
                            linea = archivo.ReadLine();
                            ac = 4;
                            break;
                    }
                    sNum = ""; sPeso = ""; sTam = ""; sX = ""; sY = ""; cont = 0;
                    sNA = ""; sNB = ""; sPeso = ""; sDiri = "";
                    for (i = 0; i < linea.Length; i++)
                    {
                        switch (ac)
                        {
                            case 1:
                                if (linea[i] != '-')
                                {
                                    sNum += linea[i].ToString();
                                }
                                else
                                {
                                    num = (Convert.ToInt32(sNum));
                                    grafo.ListaNodos.Add(new CNodo(num));
                                    sNum = "";
                                }
                                break;
                            case 2:
                                break;
                            case 3:
                                switch (cont)
                                {
                                    case 0:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sNum += linea[i].ToString();
                                        }
                                        break;
                                    case 1:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sTam += linea[i].ToString();
                                        }
                                        break;
                                    case 2:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sX += linea[i].ToString();
                                        }
                                        break;
                                    case 3:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sY += linea[i].ToString();
                                        }
                                        break;
                                }
                                break;
                            case 4:
                                switch (cont)
                                {
                                    case 0:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sNA += linea[i].ToString();
                                        }
                                        break;
                                    case 1:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sNB += linea[i].ToString();
                                        }
                                        break;
                                    case 2:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sPeso += linea[i].ToString();
                                        }
                                        break;
                                    case 3:
                                        if (linea[i] == '-')
                                            cont++;
                                        else
                                        {
                                            sDiri += linea[i].ToString();
                                        }
                                        break;
                                }
                                break;
                        }
                    }

                    switch (ac)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            foreach (CNodo n in grafo.ListaNodos)
                            {
                                if (sNum == n.GSNombreNum)
                                {
                                    tam = Convert.ToInt32(sTam);
                                    n.GSTam = tam;
                                    pt = new Point(Convert.ToInt32(sX), Convert.ToInt32(sY));
                                    n.GSCentro = pt;
                                    n.actualizaRectNodo(pt);
                                    sNum = ""; sPeso = ""; sTam = ""; sX = ""; sY = "";
                                    break;
                                }
                            }
                            break;
                        case 4:
                            foreach (CNodo n in grafo.ListaNodos)
                            {
                                if (sNA == n.GSNombreNum)
                                {
                                    nA = n;
                                    break;
                                }
                            }
                            foreach (CNodo n in grafo.ListaNodos)
                            {
                                if (sNB == n.GSNombreNum)
                                {
                                    nB = n;
                                    break;
                                }
                            }

                            switch (sDiri)
                            {
                                case "True":
                                    diri = true;
                                    break;
                                case "False":
                                    diri = false;
                                    break;
                            }

                            peso = Convert.ToInt32(sPeso);
                            grafo.addArista(nA, nB, peso, diri);
                            sNA = ""; sNB = ""; sPeso = "";
                            break;
                    }
                }
                archivo.Close();
            }
            nodo.incCNodo();
            isomorfismo_Paint(this, null);
            //*/
        }

        private void isomorfismo_Paint(object sender, PaintEventArgs e)
        {
            Graphics pagina = CreateGraphics();
            CArista auxA = new CArista();
            pagina.SmoothingMode = SmoothingMode.AntiAlias;
            g = Graphics.FromImage(bmp);
            g.Clear(BackColor);

            double tg, atg;
            int a, b, x, y;
            Point p, p2;

            gp.Reset();
            if (band)
            {
                switch (accion)
                {
                    case 1://Dibuja Nodo
                        g.DrawEllipse(egPen, nAux.GSRect);
                        g.FillEllipse(egBrushR, nAux.GSRect);
                        if (letraNodo == true)
                            g.DrawString(nAux.GSNombre, letra, egBrush, nAux.GSCentro);
                        else
                            g.DrawString(nAux.GSNombreNum, letra, egBrush, nAux.GSCentro);
                        break;
                    case 2://Dibuja Arista
                        if (grafo.dirigido == true)
                        {
                            g.DrawLine(egPenLine, pf.X, pf.Y, pf2.X, pf2.Y);
                        }
                        else
                        {
                            g.DrawLine(egPen, pf.X, pf.Y, pf2.X, pf2.Y);
                        }
                        break;
                }
                band = false;
            }

            //if (fig == 3)
            {
                foreach (CArista ari in grafo.ListaAristas)
                {
                    if (grafo.dirigido == true)
                    {
                        if (ari.GSOrigen != ari.GSDestino)
                        {
                            tg = (double)(ari.GSOrigen.GSCentro.Y - ari.GSDestino.GSCentro.Y) / (ari.GSDestino.GSCentro.X - ari.GSOrigen.GSCentro.X);
                            atg = Math.Atan(tg);
                            a = (int)((ari.GSDestino.GSTam * .53) * Math.Cos(atg));
                            b = (int)((ari.GSDestino.GSTam * .53) * Math.Sin(atg));

                            if (ari.GSOrigen.GSCentro.X < ari.GSDestino.GSCentro.X)
                            {
                                a *= -1;
                                b *= -1;
                            }

                            p = new Point(ari.GSDestino.GSCentro.X + a, ari.GSDestino.GSCentro.Y - b);

                            if (ari.GSDiri == true)
                            {
                                g.DrawLine(egPenLine, ari.GSOrigen.GSCentro, p);
                            }
                            else
                            {
                                auxA.GSOrigen = ari.GSDestino;
                                auxA.GSDestino = ari.GSOrigen;

                                tg = (double)(auxA.GSOrigen.GSCentro.Y - auxA.GSDestino.GSCentro.Y) / (auxA.GSDestino.GSCentro.X - auxA.GSOrigen.GSCentro.X);
                                atg = Math.Atan(tg);
                                a = (int)((auxA.GSDestino.GSTam * .53) * Math.Cos(atg));
                                b = (int)((auxA.GSDestino.GSTam * .53) * Math.Sin(atg));

                                if (auxA.GSOrigen.GSCentro.X < auxA.GSDestino.GSCentro.X)
                                {
                                    a *= -1;
                                    b *= -1;
                                }
                                p2 = new Point(auxA.GSDestino.GSCentro.X + a, auxA.GSDestino.GSCentro.Y - b);

                                g.DrawLine(egPenDoble, p2, p);
                            }
                        }
                        else
                        {
                            g.DrawArc(egPenLine, ari.GSOrigen.GSRect.Location.X - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSRect.Location.Y - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSTam / 2, ari.GSOrigen.GSTam / 2, 30, 310);
                        }
                    }
                    else
                    {
                        if (ari.GSOrigen != ari.GSDestino)
                        {
                            g.DrawLine(egPen, ari.GSOrigen.GSCentro, ari.GSDestino.GSCentro);
                        }
                        else
                        {
                            g.DrawArc(egPen, ari.GSOrigen.GSRect.Location.X - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSRect.Location.Y - ari.GSOrigen.GSTam / 6, ari.GSOrigen.GSTam / 2, ari.GSOrigen.GSTam / 2, 30, 340);
                        }
                    }

                    if (muestrapeso == true)
                    {
                        if (ari.GSOrigen != ari.GSDestino)
                        {
                            x = ari.GSOrigen.GSCentro.X + ((ari.GSDestino.GSCentro.X - ari.GSOrigen.GSCentro.X) / 2);
                            y = ari.GSOrigen.GSCentro.Y + ((ari.GSDestino.GSCentro.Y - ari.GSOrigen.GSCentro.Y) / 2);
                            g.DrawString(ari.GSPeso.ToString(), letra, egBrush, x, y);
                        }
                        else
                        {
                            x = ari.GSOrigen.GSRect.Location.X - ari.GSOrigen.GSTam / 6;
                            y = ari.GSOrigen.GSRect.Location.Y - ari.GSOrigen.GSTam / 6;
                            g.DrawString(ari.GSPeso.ToString(), letra, egBrush, x, y);
                        }
                    }
                }


                foreach (CNodo nod in grafo.ListaNodos)
                {
                    g.FillEllipse(egBrushR, nod.GSRect);
                    g.DrawEllipse(egPen, nod.GSRect);
                    if (letraNodo == true)
                        g.DrawString(nod.GSNombre, letra, egBrush, nod.GSCentro);
                    else
                        g.DrawString(nod.GSNombreNum, letra, egBrush, nod.GSCentro);

                    if (coordNodo == true)
                    {
                        g.DrawString("x" + nod.GSCentro.X.ToString(), letra, egBrush, nod.GSCentro.X - (float)(nod.GSTam * .4), nod.GSCentro.Y - nod.GSTam / 3);
                        g.DrawString(nod.GSCentro.Y.ToString(), letra, egBrush, nod.GSCentro.X - (float)(nod.GSTam * .4), nod.GSCentro.Y - ((int)letra.Size / 8));
                    }
                }
            }

            pagina.DrawImage(bmp, 0, 0);
        }

        private void archivo_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "guardarGrafo":
                    archivo.Invalidate();
                    saveFileDialog1.Filter = "Grafo|*.grafo";
                    saveFileDialog1.ShowDialog();
                    break;
                case "abrirGrafo":
                    archivo.Invalidate();
                    openFileDialog1.Filter = "Grafo|*.grafo";
                    openFileDialog1.ShowDialog();
                    break;
            }
        }

        private void isomorfismo_MouseDown(object sender, MouseEventArgs e)
        {
            switch (accion)
            {
                case 1:
                    if (grafo.buscaNodoPoint(e.Location) == null)
                    {
                        nodo = new CNodo(e.Location, tam);
                        nAux = nodo;
                        grafo.addNodo(nAux);
                        band = true;
                        isomorfismo_Paint(this, null);
                    }
                    break;
                case 2:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        nA = nAux;
                        pf2 = e.Location;
                        pf = (grafo.buscaNodoPoint(e.Location)).GSCentro;
                        band = true;
                    }
                    break;
                case 3:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        nodo = grafo.buscaNodoPoint(e.Location);
                    }
                    break;
                case 4:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        grafo.eliminaNodo(nAux, false);
                        isomorfismo_Paint(this, null);
                    }
                    break;
                case 5:
                    if ((auxArista = click_Arista(e.Location)) != null)
                    {
                        eliminaArista(auxArista);
                    }
                    isomorfismo_Paint(this, null);
                    break;
                case 6:
                    if ((auxArista = click_Arista(e.Location)) != null)
                    {
                        cambiaPeso(auxArista);
                    }
                    break;
                case 7:
                    if ((auxArista = click_Arista(e.Location)) != null)
                    {
                        cambiaSentido(auxArista);
                        isomorfismo_Paint(this, null);
                    }
                    break;
            }
        }

        private void Acciones_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "MoverNodo":
                    accion = 3;
                    break;
                case "BorrarNodo":
                    accion = 4;
                    break;
                case "BorrarArista":
                    accion = 5;
                    break;
            }
        }

        private void isomorfismo_MouseUp(object sender, MouseEventArgs e)
        {
            switch (accion)
            {
                case 1:
                    isomorfismo_Paint(this, null);
                    break;
                case 2:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        pf2 = nAux.GSCentro;
                        if (pf != pf2 || pf == pf2)
                        {
                            nB = nAux;
                            auxArista = new CArista(nA, nB);
                            grafo.addArista(nA, nB);
                            pfRes = pf2;
                            band = true;
                            isomorfismo_Paint(this, null);
                            nAux = null;
                        }
                        else
                        {
                            g.Clear(BackColor);
                            band = false;
                            isomorfismo_Paint(this, null);
                        }
                    }
                    else
                    {
                        band = false;
                        isomorfismo_Paint(this, null);
                    }
                    break;
                case 3:
                    nAux = null;
                    nodo = null;
                    break;
                case 4:
                        isomorfismo_Paint(this, null);
                    break;
            }
        }

        private void isomorfismo_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left.Equals(e.Button))
            {
                switch (accion)
                {
                    case 2:
                        if (band == true)
                        {
                            pf2 = e.Location;
                            isomorfismo_Paint(this, null);
                            band = true;
                        }
                        break;
                    case 3:
                        if (nodo != null)
                        {
                            pf = e.Location;
                            nodo.GSCentro = pf;
                            nodo.actualizaRectNodo(pf);
                            isomorfismo_Paint(this, null);
                            band = true;
                        }
                        break;
                }
            }
        }

        private void Insertar_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Nodo":
                    accion = 1;
                    break;
                case "Arista":
                    accion = 2;
                    break;
            }
        }

        private void cambio_Click(object sender, EventArgs e)
        {
            if (letraNodo == true)
            {
                cambio.Text = "0";
                letraNodo = false;
            }
            else
            {
                cambio.Text = "A";
                letraNodo = true;
            }
        }

        private void Dirigido_Click(object sender, EventArgs e)
        {
            Pen aux = new Pen(egBrush, anchoL);
            if (grafo.dirigido == false)
            {
                grafo.dirigido = true;
                Dirigido.Width = 32;
                cambiaSent.Visible = true;
            }
            else
            {
                grafo.dirigido = false;
                Dirigido.Width = 75;
                cambiaSent.Visible = false;
            }
            isomorfismo_Paint(this, null);
        }

        private void insertarPeso_Click(object sender, EventArgs e)
        {
            accion = 6;
        }

        private void Peso_Click(object sender, EventArgs e)
        {
            if (muestrapeso == true)
            {
                muestrapeso = false;
                Peso.Width = 75;
                Peso.Text = "Peso";
                insertarPeso.Visible = false;
                isomorfismo_Paint(this, null);
            }
            else
            {
                muestrapeso = true;
                Peso.Width = 32;
                Peso.Text = "P";
                insertarPeso.Visible = true;
                isomorfismo_Paint(this, null);
            }
        }

    }
}
