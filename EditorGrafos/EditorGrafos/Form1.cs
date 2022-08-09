
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
using System.IO;


namespace EditorGrafos
{
    public partial class Form1 : Form
    {
        private int accion, accCamCirc;

        public Graphics g;
        public GraphicsPath gp;

        private Pen egPenLine, egPenDoble;
        private Pen egPen;
        private SolidBrush egBrush, egBrushR;
        Point pf, pf2, pfAnt, pfRes;
        bool band, letraNodo;
        int anchoL, ancho, alto, tam;
        Bitmap bmp;
        Color colorRelleno, colorContorno;
        Rectangle rect;
        CGrafo grafo, grafo2;
        CNodo nodo, nAux, nA, nB;
        CArista auxArista;
        Font letra;
        List<CNodo> Lista;
        Form matrizForm;
        isomorfismo iso;
        bool muestrapeso;
        bool coordNodo;
        List<CNodo> Cam_Circ;
        List<CNodo> CamCirc;
        int cont;
        List<List<int>> caminos;
        List<int[]> combinaciones;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            gp = new GraphicsPath();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            this.BackColor = Color.WhiteSmoke;

            band = false;
            letraNodo = true;
            muestrapeso = false;
            coordNodo = false;
            accion = 0;
            accCamCirc = 0;
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

            Lista = new List<CNodo>();

            grafo = new CGrafo();
            grafo2 = new CGrafo();

            rect = new Rectangle();
            nA = new CNodo();
            nB = new CNodo();
            nodo = new CNodo();
            nAux = new CNodo();
            Cam_Circ = new List<CNodo>();
            CamCirc = new List<CNodo>();

            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            cont = 0;

            caminos = new List<List<int>>();
        }

        private void matriz_Click(object sender, EventArgs e)
        {
            grafo.generaMatriz();
            matrizForm = new Matriz(grafo.matriz, grafo.ListaNodos.Count, grafo.ListaNodos.Count, grafo.ListaNodos);
            matrizForm.Show();
        }

        private void cambiaPeso(CArista arista)
        {
            string peso;
            peso = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Peso", "Peso", arista.GSPeso.ToString(), 100, 100);
            arista.GSPeso = Convert.ToInt32(peso);
        }

        private void picturePrueba_Paint(object sender, PaintEventArgs e)
        {

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

        private void Peso_Click(object sender, EventArgs e)
        {
            if (muestrapeso == true)
            {
                muestrapeso = false;
                Form1_Paint(this, null);
            }
            else
            {
                muestrapeso = true;
                Form1_Paint(this, null);
            }
        }

        private void CoordenadasNodo_Click(object sender, EventArgs e)
        {
            if (coordNodo == true)
            {
                coordNodo = false;
                Form1_Paint(this, null);
            }
            else
            {
                coordNodo = true;
                Form1_Paint(this, null);
            }
        }

        private void insertarPeso_Click(object sender, EventArgs e)
        {
            accion = 6;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string linea;
            int ac = 0, i;
            string sNum = "", sPeso = "", sTam = "", sX = "", sY = "", sNA = "", sNB = "", sDiri = "";
            int num, peso, tam, cont;
            Point pt;
            bool diri = false;
            nodo = new CNodo();

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
            Form1_Paint(this, null);
            //*/
        }

        private void cambiaSent_Click(object sender, EventArgs e)
        {
            if (grafo.dirigido == true)
            {
                accion = 7;
            }
        }

        private void gradoB_Click(object sender, EventArgs e)
        {
            String cad = "";

            grafo.generaGrado();

            foreach (CNodo nod in grafo.ListaNodos)
            {
                if (nod.visible == true)
                    cad += nod.GSNombre.ToString() + ": " + nod.GSGrado.ToString() + ".\n";
            }

            Microsoft.VisualBasic.Interaction.MsgBox(cad);
        }

        private void ListaAdB_Click(object sender, EventArgs e)
        {
            string cad, msg = "";
            int i = grafo.ListaNodos.Count, r, c;
            int[,] mat = new int[grafo.ListaNodos.Count, grafo.ListaNodos.Count];
            CNodo nAux = new CNodo();
            grafo.generaMatriz();
            mat = grafo.matriz;

            for (r = 0; r < i; r++)
            {
                nAux = grafo.ListaNodos.ElementAt<CNodo>(r);
                if (nAux.visible == true)
                {
                    cad = "";
                    if (letraNodo == true)
                        cad += grafo.ListaNodos.ElementAt<CNodo>(r).GSNombre + " -";
                    else
                        cad += grafo.ListaNodos.ElementAt<CNodo>(r).GSNombreNum + " -";

                    for (c = 0; c < i; c++)
                    {
                        if (mat[r, c] == 1 && r != c)
                        {
                            if (letraNodo == true)
                                cad += grafo.ListaNodos.ElementAt<CNodo>(c).GSNombre + " ";
                            else
                                cad += grafo.ListaNodos.ElementAt<CNodo>(c).GSNombreNum + " ";
                        }
                    }
                    msg += cad + "\n";
                }
            }

            Microsoft.VisualBasic.Interaction.MsgBox(msg);
        }

        private void camino_Click(object sender, EventArgs e)
        {
            accion = 8;
            Cam_Circ.Clear();

            switch (accCamCirc)
            {
                case 1:
                    buscaCamino();
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private void buscaCamino()
        {
            grafo.generaRelaciones();
            grafo.generaMatriz();
            string msg = "";
            CamCirc.Clear();
            caminos = new List<List<int>>();
            List<List<int>> hamilton = new List<List<int>>();

            switch (Cam_Circ.Count)
            {
                case 1:
                    switch (accCamCirc)
                    {
                        case 1:
                            caminos = grafo.HayCamino(grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.matriz, grafo.ListaNodos.Count);
                            MessageBox.Show("Circuitos Simples: " + caminos.Count.ToString());
                            break;
                        case 2:
                            caminos = grafo.HayCamino(grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.matriz, grafo.ListaNodos.Count);
                            foreach (var item in caminos)
                            {
                                if (item.Count == grafo.ListaNodos.Count + 1)
                                    hamilton.Add(item);
                            }
                            MessageBox.Show("Circuitos Hamiltonianos: " + hamilton.Count.ToString());
                            if (hamilton.Count > 0)
                            {
                                caminos.Clear();
                                foreach (var item in hamilton)
                                {
                                    caminos.Add(item);
                                }
                            }
                            break;
                        case 3:
                            break;
                    }
                    break;
                case 2:
                    switch (accCamCirc)
                    {
                        case 1:
                            caminos = grafo.HayCamino(grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(1)), grafo.matriz, grafo.ListaNodos.Count);
                            MessageBox.Show("Caminos Simples: " + caminos.Count.ToString());
                            break;
                        case 2:
                            caminos = grafo.HayCamino(grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(1)), grafo.matriz, grafo.ListaNodos.Count);
                            foreach (var item in caminos)
                            {
                                if (item.Count == grafo.ListaNodos.Count)
                                    hamilton.Add(item);
                            }
                            caminos.Clear();
                            caminos = grafo.HayCamino(grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(1)), grafo.ListaNodos.IndexOf(Cam_Circ.ElementAt<CNodo>(0)), grafo.matriz, grafo.ListaNodos.Count);
                            foreach (var item in caminos)
                            {
                                if (item.Count == grafo.ListaNodos.Count)
                                    hamilton.Add(item);
                            }
                            MessageBox.Show("Caminos Hamiltonianos: " + hamilton.Count.ToString());
                            if (hamilton.Count > 0)
                            {
                                caminos.Clear();
                                foreach (var item in hamilton)
                                {
                                    caminos.Add(item);
                                }
                            }
                            else
                            {
                                caminos.Clear();
                            }
                            break;
                        case 3:
                            break;
                    }
                    break;
            }

            CamCirc.Clear();
            Cam_Circ.Clear();
            grafo.resetColor();
            for (int i = 0; i < caminos.Count; i++)
            {
                msg = "";
                msg += (i + 1).ToString() + ": ";
                for (int j = 0; j < caminos[i].Count; j++)
                {
                    if (letraNodo == true)
                    {
                        msg += grafo.ListaNodos.ElementAt<CNodo>(caminos[i][j]).GSNombre.ToString() + " ";
                    }
                    else
                    {
                        msg += caminos[i][j] + " ";
                    }
                }
                msg += "\n";
                int del = 0;
                for (int j = 1; j < caminos[i].Count; j++)
                {
                    grafo.pintaCamino(caminos[i], j);
                    Form1_Paint(this, null);
                    del = 0;
                    while (del < 10000000)
                        del++;
                }
                MessageBox.Show(msg);
                grafo.resetColor();
            }
            buscar.Visible = false;
            grafo.resetColor();
        }

        private void siguiente_Click(object sender, EventArgs e)
        {
            grafo.resetColor();
            if (cont < caminos.Count)
            {
                cont++;
                siguiente.Enabled = true;
                anterior.Enabled = true;
            }
            else
            {
                siguiente.Enabled = false;
                siguiente.Visible = false;
                finalizar.Visible = true;
            }

            if (cont < caminos.Count)
            {
                grafo.pintaCamino(caminos[cont], caminos[cont].Count);
            }
        }

        private void anterior_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case 8:
                    grafo.resetColor();
                    if (cont > 0)
                    {
                        cont--;
                        anterior.Enabled = true;
                        siguiente.Enabled = true;
                    }
                    else
                    {
                        anterior.Enabled = false;
                        siguiente.Enabled = true;
                    }

                    if (cont >= 0)
                    {
                        grafo.pintaCamino(caminos[cont], caminos[cont].Count);
                    }
                    break;
            }

        }

        private void FuerzaBr_Click(object sender, EventArgs e)
        {
            string msg = "";

            grafo2 = iso.getGrafo();

            grafo.generaMatriz();
            grafo2.generaMatriz();

            foreach (var item in grafo2.ListaNodos)
            {
                msg += item.GSNombre + " \n";
            }

            MessageBox.Show(msg);

            if (grafo.Count == grafo2.Count && grafo.FuerzaBruta(grafo.matriz, grafo2.matriz, grafo.Count))
            {
                msg += "Isomorfico por Fuerza Bruta\n";
            }
            else
            {
                msg += "No es Isomorfico por Fuerza Bruta\n";
            }
            MessageBox.Show(msg);
            //FuerzaBr.Visible = false;
            //Botello.Visible = false;
        }

        private void Botello_Click(object sender, EventArgs e)
        {
            string msg = "";

            grafo2 = iso.getGrafo();

            grafo.generaMatriz();
            grafo2.generaMatriz();

            if (grafo.Count == grafo2.Count && grafo.IsomorfismoManual(grafo, grafo2))
            {
                msg += "Es Isomorfico por el algoritmo del Manual\n";
            }
            else
            {
                msg += "No es Isomorfico por el algoritmo del Manual\n";
            }

            MessageBox.Show(msg);
            //Botello.Visible = false;
            //FuerzaBr.Visible = false;
        }

        private void permutaciones_Click(object sender, EventArgs e)
        {
            string msg = "";
            grafo2 = iso.getGrafo();

            grafo.generaMatriz();
            grafo2.generaMatriz();
            if (grafo.Count == grafo2.Count && grafo.IsomorfismoMatrizPermutada(grafo, grafo2))
            {
                msg += "El grafo es isomorfico por Matriz Permutada\n";
            }
            else
            {
                msg += "El grafo no es isomorfico por Matriz Permutada\n";
            }

            MessageBox.Show(msg);
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case 8:
                    buscaCamino();
                    break;
                case 9:
                    grafo.resetColor();
                    Form1_Paint(this, null);
                    Coloreados(nodo, grafo);
                    Cam_Circ.Clear();
                    break;
                case 10:

                    break;
                case 11:
                    dijkstra();
                    break;
                case 12:
                    floyd();
                    break;
                case 13:
                    Isomor_Click(sender, e);
                    break;
                case 14:
                    Euler(Cam_Circ[0]);
                    break;
            }
        }

        public List<List<int>> fleury(CGrafo grafo, CNodo inicial)
        {
            grafo.generaMatriz();
            grafo.resetRevisado();
            List<int> ad = new List<int>();
            List<List<int>> caminos = new List<List<int>>();
            CNodo aux = new CNodo();

            if (grafo.Count > 0)
            {
                for (int i = 0; i < grafo.Count; i++)
                {
                    //grafo.
                    aux = grafo.ListaNodos.ElementAt<CNodo>(i);
                    caminosFleury(grafo, caminos, new List<int> { i }, aux, i, i, grafo.ListaAristas.Count);
                }
            }

            //if (n1 == n2)//para circuitos
            {
                int limpiando = caminos.Count;
                while (limpiando != 0)
                {
                    for (int i = 0; i < caminos.Count; i++)
                        if (caminos[i].Count < 4)
                        {
                            caminos.RemoveAt(i);
                            break;
                        }
                    limpiando--;
                }
            }

            string msg = "";
            foreach (var item in caminos)
            {
                foreach (var i in item)
                {
                    msg += i.ToString() + " ";
                }
                msg += "\n";
            }
            MessageBox.Show(msg);
            return caminos;
        }

        public void caminosFleury(CGrafo grafo, List<List<int>> caminos, List<int> camino, CNodo inicial, int n1, int n2, int contA)
        {
            //---------------------------
            /*
            if (camino.Contains(n1))
            {
                if (n1 == n2)
                {
                    camino.Add(n1);
                    caminos.Add(camino);
                    return;
                }
                camino.Add(n1);
                return;
            }
            camino.Add(n1);
            if (n1 == n2)
            {
                caminos.Add(camino);
                return;
            }
            */
            //---------------------------


            List<int> ad = new List<int>();
            CNodo aux = new CNodo();
            int r = -1, c = -1;
            CGrafo copia = new CGrafo();
            copia.copiaGrafo(grafo);

            if (contA != 0)
            {
                if (grafo.Aristas(inicial) != 0)
                {
                    ad = grafo.adyacentes(inicial);
                    foreach (var item in ad)
                    {
                        aux = grafo.ListaNodos.ElementAt<CNodo>(item);
                        for (int del = 0; del < 100000000; del++) ;
                        if (grafo.Aristas(inicial) == 1 || grafo.esPuente(inicial, aux) == false)
                        {
                            camino.Add(grafo.ListaNodos.IndexOf(aux));
                            r = grafo.ListaNodos.IndexOf(inicial);
                            c = grafo.ListaNodos.IndexOf(aux);

                            //MessageBox.Show("Eliminando arista:" + r.ToString() + "-" + c.ToString());
                            grafo.eliminaAristaMR(r, c);
                            Form1_Paint(this, null);
                            //---------------
                            List<int> cl = new List<int>();
                            for (int j = 0; j < camino.Count; j++)
                                cl.Add(camino[j]);
                            //---------------
                            //grafo.copiaGrafo(copia);
                            caminosFleury(grafo, caminos, cl, aux, r, n2, grafo.ListaAristas.Count);
                        }
                        //grafo.copiaGrafo(copia);
                    }
                }
            }
        }

        private void eulerToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accion = 8;
            Cam_Circ.Clear();

            switch (e.ClickedItem.AccessibleName)
            {
                case "Fleury":
                    accCamCirc = 3;
                    if (grafo.Count > 0)
                        fleury(grafo, grafo.ListaNodos.ElementAt<CNodo>(0));
                    break;
                case "Hierholzer":
                    accCamCirc = 4;
                    break;
            }
        }

        private void grafosPlanos_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "grafosColoreados":
                    accion = 9;
                    break;
                case "Kura":
                    accion = 10;
                    break;
            }
        }

        private void Coloreados(CNodo inicio, CGrafo grafo)
        {
            List<Color> colores = new List<Color> { Color.Blue, Color.Gray, Color.Red,
                                                    Color.Green, Color.Purple, Color.Yellow,
                                                    Color.Cyan, Color.Olive, Color.Pink,
                                                    Color.Tomato, Color.Fuchsia, Color.Honeydew};
            List<int> ad = new List<int>();

            grafo.resetRevisado();
            Colorea(inicio, colores, grafo);
        }

        public void Colorea(CNodo nodo, List<Color> colores, CGrafo grafo)
        {
            List<int> ad = new List<int>();
            List<Color> colAd = new List<Color>();
            CNodo colNod = new CNodo();

            if (nodo.revisado == true)
            {
                return;
            }
            else
            {
                nodo.revisado = true;
                ad = grafo.adyacentes(nodo);
                foreach (var ady in ad)
                {
                    colNod = grafo.ListaNodos.ElementAt<CNodo>(ady);
                    if (nodo != colNod && colNod.revisado == true)
                        colAd.Add(colNod.GSColor);
                }

                if (colAd.Count == 0)
                {
                    nodo.GSColor = colores.ElementAt<Color>(0);
                }
                else
                {
                    foreach (var item in colores)
                    {
                        if (colAd.Contains<Color>(item) == false)
                        {
                            nodo.GSColor = item;
                            break;
                        }
                    }
                }
                Form1_Paint(this, null);
                for (int del = 0; del < 50000000; del++) ;
                foreach (var item in ad)
                {
                    colNod = grafo.ListaNodos.ElementAt<CNodo>(item);
                    Colorea(colNod, colores, grafo);
                }

            }
        }

        private void caminosCircuitos_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accion = 8;
            Cam_Circ.Clear();
            switch (e.ClickedItem.AccessibleName)
            {
                case "Simples":
                    accCamCirc = 1;
                    break;
                case "Hamilton":
                    accCamCirc = 2;
                    break;
            }
        }

        private void finalizar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case 8:
                    cont = 0;
                    finalizar.Visible = false;
                    buscar.Visible = false;
                    grafo.resetColor();
                    Form1_Paint(this, null);
                    Cam_Circ.Clear();
                    break;
                case 9:
                    finalizar.Visible = false;
                    buscar.Visible = false;
                    grafo.resetColor();
                    Form1_Paint(this, null);
                    Cam_Circ.Clear();
                    break;
                case 11:
                    finalizar.Visible = false;
                    buscar.Visible = false;
                    grafo.resetColor();
                    Form1_Paint(this, null);
                    Cam_Circ.Clear();
                    break;
                case 12:
                    finalizar.Visible = false;
                    buscar.Visible = false;
                    grafo.resetColor();
                    Form1_Paint(this, null);
                    Cam_Circ.Clear();
                    break;
                case 13:
                    finalizar.Visible = false;
                    buscar.Visible = false;
                    iso.Close();
                    break;
            }
        }

        private void isomorfB_Click(object sender, EventArgs e)
        {
            grafo.generaMatriz();
            iso = new isomorfismo();
            iso.Show();
            FuerzaBr.Visible = true;
            Botello.Visible = true;
            permutaciones.Visible = true;

            /*
            if(iso.FormClosing())
            {
                FuerzaBr.Visible = true;
                Botello.Visible = true;
            }
            */
        }

        private void cambiaSentido(CArista ari)
        {
            ari.cambiaSentido();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            int i = grafo.ListaNodos.Count, r, c;
            int init = 20;
            int X = init, Y = init;

            grafo.generaMatriz();

            using (StreamWriter archivo = new StreamWriter(saveFileDialog1.FileName))
            {
                archivo.Write("nombre");
                archivo.WriteLine();
                foreach (CNodo nod in grafo.ListaNodos)
                {
                    archivo.Write(nod.GSNombreNum.ToString() + "-");
                }
                archivo.WriteLine();
                archivo.Write("matriz");
                archivo.WriteLine();
                for (r = 0; r < i; r++)
                {
                    for (c = 0; c < i; c++)
                    {
                        archivo.Write(grafo.matriz[r, c].ToString());
                        archivo.Write("-");
                    }
                    archivo.WriteLine();
                }

                archivo.Write("nodos");
                archivo.WriteLine();
                foreach (CNodo nod in grafo.ListaNodos)
                {
                    archivo.Write(nod.GSNombreNum.ToString() + "-" + nod.GSTam.ToString() + "-" + nod.GSCentro.X.ToString() + "-" + nod.GSCentro.Y.ToString());
                    archivo.WriteLine();
                }

                archivo.Write("aristas");
                archivo.WriteLine();
                foreach (CArista ari in grafo.ListaAristas)
                {
                    archivo.Write(ari.GSOrigen.GSNombreNum + "-" + ari.GSDestino.GSNombreNum + "-" + ari.GSPeso.ToString() + "-" + ari.GSDiri.ToString());
                    archivo.WriteLine();
                }
            }
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

        private void Form1_Paint(object sender, PaintEventArgs e)
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
                    if (ari != null)
                        if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                        {
                            if (ari == null)
                                break;
                            egBrush.Color = ari.GSColor;
                            egPen.Brush = egBrush;
                            egPenLine.Brush = egBrush;
                            egPenDoble.Brush = egBrush;

                            egBrush.Color = colorContorno;
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
                }
                egBrush.Color = colorContorno;
                egPen.Brush = egBrush;
                egPenLine.Brush = egBrush;
                egPenDoble.Brush = egBrush;


                foreach (CNodo nod in grafo.ListaNodos)
                {
                    if (nod.visible == true)
                    {
                        egBrushR.Color = nod.GSColor;

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
            }
            egBrush.Color = colorContorno;
            pagina.DrawImage(bmp, 0, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
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
                        Form1_Paint(this, null);
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
                        Form1_Paint(this, null);
                    }
                    break;
                case 5:
                    if ((auxArista = click_Arista(e.Location)) != null)
                    {
                        eliminaArista(auxArista);
                    }
                    Form1_Paint(this, null);
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
                        Form1_Paint(this, null);
                    }
                    break;
                case 8:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        buscar.Text = "Buscar";
                        finalizar.Visible = true;
                        nodo = grafo.buscaNodoPoint(e.Location);

                        if (nodo.CC == false && Cam_Circ.Count < 2)
                        {
                            nodo.GSColor = Color.Aquamarine;
                            nodo.CC = true;
                            Cam_Circ.Add(nodo);
                            buscar.Visible = true;
                        }
                        else
                        {
                            if (nodo.CC == true)
                            {
                                Cam_Circ.Remove(nodo);
                                nodo.CC = false;
                                nodo.resetColor();
                                if (Cam_Circ.Count == 0)
                                {
                                    buscar.Visible = false;
                                }
                            }
                        }

                        Form1_Paint(this, null);
                    }
                    break;
                case 9:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        buscar.Text = "Colorea";
                        finalizar.Visible = true;
                        nodo = grafo.buscaNodoPoint(e.Location);

                        if (nodo.CC == false && Cam_Circ.Count < 1)
                        {
                            nodo.GSColor = Color.Aquamarine;
                            nodo.CC = true;
                            Cam_Circ.Add(nodo);
                            buscar.Visible = true;
                        }
                        else
                        {
                            if (nodo.CC == true)
                            {
                                Cam_Circ.Remove(nodo);
                                nodo.CC = false;
                                nodo.resetColor();
                                if (Cam_Circ.Count == 0)
                                {
                                    buscar.Visible = false;
                                }
                            }
                        }

                        Form1_Paint(this, null);
                    }
                    break;
                case 10:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        nodo = grafo.buscaNodoPoint(e.Location);
                    }
                    break;
                case 11:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        buscar.Text = "Buscar";
                        finalizar.Visible = true;
                        nodo = grafo.buscaNodoPoint(e.Location);

                        if (nodo.CC == false && Cam_Circ.Count < 2)
                        {
                            nodo.GSColor = Color.Aquamarine;
                            nodo.CC = true;
                            Cam_Circ.Add(nodo);
                            if (Cam_Circ.Count == 2)
                            {
                                buscar.Visible = true;
                            }
                        }
                        else
                        {
                            if (nodo.CC == true)
                            {
                                Cam_Circ.Remove(nodo);
                                nodo.CC = false;
                                nodo.resetColor();
                                if (Cam_Circ.Count < 2)
                                {
                                    buscar.Visible = false;
                                }
                            }
                        }
                        Form1_Paint(this, null);
                    }
                    break;
                case 12:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        buscar.Text = "Buscar";
                        finalizar.Visible = true;
                        nodo = grafo.buscaNodoPoint(e.Location);

                        if (nodo.CC == false && Cam_Circ.Count < 2)
                        {
                            nodo.GSColor = Color.Aquamarine;
                            nodo.CC = true;
                            Cam_Circ.Add(nodo);
                            if (Cam_Circ.Count == 2)
                            {
                                buscar.Visible = true;
                            }
                        }
                        else
                        {
                            if (nodo.CC == true)
                            {
                                Cam_Circ.Remove(nodo);
                                nodo.CC = false;
                                nodo.resetColor();
                                if (Cam_Circ.Count < 2)
                                {
                                    buscar.Visible = false;
                                }
                            }
                        }
                        Form1_Paint(this, null);
                    }
                    break;
                case 14:
                    if ((nAux = grafo.buscaNodoPoint(e.Location)) != null)
                    {
                        buscar.Text = "Buscar";
                        finalizar.Visible = true;
                        nodo = grafo.buscaNodoPoint(e.Location);

                        if (nodo.CC == false && Cam_Circ.Count < 2)
                        {
                            nodo.GSColor = Color.Aquamarine;
                            nodo.CC = true;
                            Cam_Circ.Add(nodo);
                            if (Cam_Circ.Count == 1)
                            {
                                buscar.Visible = true;
                            }
                        }
                        else
                        {
                            if (nodo.CC == true)
                            {
                                Cam_Circ.Remove(nodo);
                                nodo.CC = false;
                                nodo.resetColor();
                                if (Cam_Circ.Count < 1)
                                {
                                    buscar.Visible = false;
                                }
                            }
                        }
                        Form1_Paint(this, null);
                    }
                    break;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (accion)
            {
                case 1:
                    Form1_Paint(this, null);
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
                            Form1_Paint(this, null);
                            nAux = null;
                        }
                        else
                        {
                            g.Clear(BackColor);
                            band = false;
                            Form1_Paint(this, null);
                        }
                    }
                    else
                    {
                        band = false;
                        Form1_Paint(this, null);
                    }
                    break;
                case 3:
                    nAux = null;
                    nodo = null;
                    break;
                case 4:
                    Form1_Paint(this, null);
                    break;
                case 10:
                    if ((nAux = grafo.buscaNodoPointUp(e.Location)) != null)
                    {
                        nodo = grafo.buscaNodoPointUp(e.Location);
                        grafo.fucionaNodos(nodo);
                        nodo = null;
                        Form1_Paint(this, null);
                        if (kuratowsky() == true)
                        {
                            MessageBox.Show("Es plano");
                        }
                        else
                        {
                            MessageBox.Show("No es plano");
                        }
                        Form1_Paint(this, null);
                    }
                    break;
            }
        }

        private void generaCaminoFloydIter(int ini, int fin, double[,] P)
        {
            bool end = false;
            List<int> cam = new List<int>();
            int resp = ini;
            //double inicio = ini, final = fin;

            while (!end)
            {
                /*
                cam.Add(Convert.ToInt16(inicio));
                if(P[Convert.ToInt16(inicio), Convert.ToInt16(final)]==final)
                {
                    cam.Add(Convert.ToInt16(final));
                    end = true;
                }
                else
                {
                    inicio = P[ Convert.ToInt16(inicio), Convert.ToInt16(final)];
                }
                */
                cam.Add(ini);
                if (P[ini, fin] == fin)
                {
                    cam.Add(fin);
                    end = true;
                }
                else
                {
                    ini = Convert.ToInt16(P[ini, fin]);
                    //cam.Add(ini);
                }
            }
            ini = resp;
            caminos.Add(cam);
        }

        private void generaCaminoFloyd(int i, int j, List<int> camino, double[,] P)
        {
            double k = P[i, j];
            //MessageBox.Show(k.ToString());

            /*
            if (k == 0)
                return;
            */

            if (k == j)// || k == 0)
            {
                camino.Add(j);
                caminos.Add(camino);
                return;
            }
            else
            {
                foreach (var nod in grafo.ListaNodos)
                {
                    if (nod.GSNombreNumInt == k)//&&nod.GSNombreNumInt!=i)
                    {
                        camino.Add(nod.GSNombreNumInt);
                        generaCaminoFloyd(nod.GSNombreNumInt, j, camino, P);
                        break;
                    }
                }


                /*
                generaCaminoFloyd(i, Convert.ToInt16(k), camino, P);
                camino.Add(Convert.ToInt16(k));
                generaCaminoFloyd(Convert.ToInt16(k), j, camino, P);
                */
            }
        }

        private void floyd()
        {
            List<int> cam = new List<int>();
            string msg = "", msgP = "";
            double[,] A, P;
            grafo.generaMatrizCostos();
            A = grafo.mCostos;
            P = new double[grafo.ListaNodos.Count, grafo.ListaNodos.Count];

            for (int i = 0; i < grafo.ListaNodos.Count; i++)
            {
                for (int j = 0; j < grafo.ListaNodos.Count; j++)
                {
                    msg += A[i, j].ToString() + "\t";
                    P[i, j] = j;
                    msgP += j.ToString() + "\t";
                }
                msg += "\n";
                msgP += "\n";
            }
            MessageBox.Show(msg + "\n" + msgP);

            //---------------------------------------------------------------
            //Algoritmo
            for (int k = 0; k < grafo.ListaNodos.Count; k++)
            {
                for (int i = 0; i < grafo.ListaNodos.Count; i++)
                {
                    for (int j = 0; j < grafo.ListaNodos.Count; j++)
                    {
                        if (((A[i, k] + A[k, j]) < A[i, j]))// && i != k && j != k)
                        {
                            A[i, j] = A[i, k] + A[k, j];
                            P[i, j] = k;
                        }
                    }
                }
            }
            //----------------------------------------------------------------

            msg = "";
            msgP = "";
            for (int i = 0; i < grafo.ListaNodos.Count; i++)
            {
                for (int j = 0; j < grafo.ListaNodos.Count; j++)
                {
                    msg += A[i, j].ToString() + "\t";
                    msgP += P[i, j].ToString() + "\t";
                }
                msg += "\n";
                msgP += "\n";
            }
            MessageBox.Show(msg);
            MessageBox.Show(msgP);

            caminos.Clear();
            //generaCaminoFloyd(Cam_Circ[0].GSNombreNumInt, Cam_Circ[1].GSNombreNumInt, cam, P);
            //generaCaminoFloydIter(Cam_Circ[0].GSNombreNumInt, Cam_Circ[1].GSNombreNumInt, P);
            dijkstra();


            msg = "";
            for (int i = 0; i < caminos.Count; i++)
            {
                for (int j = 0; j < caminos[i].Count; j++)
                {
                    msg += caminos[i][j].ToString() + ", ";
                }
                msg += "\n";
            }
            //MessageBox.Show(msg);

            caminos.Clear();
        }

        private void dijkstra()
        {
            string msg = "";//, msgV = "V: ";
            CNodo W = new CNodo();
            List<CNodo> S = new List<CNodo>();
            List<CNodo> V = new List<CNodo>();
            int[] P = new int[grafo.ListaNodos.Count];
            double[] D = new double[grafo.ListaNodos.Count];
            double[,] C = new double[grafo.ListaNodos.Count, grafo.ListaNodos.Count];
            int[,] mAd = new int[grafo.ListaNodos.Count, grafo.ListaNodos.Count];
            List<int> cam = new List<int>();

            grafo.generaMatrizCostos();
            grafo.generaMatriz();
            C = grafo.mCostos;
            W = Cam_Circ[0];
            mAd = grafo.matriz;
            P[0] = W.GSNombreNumInt;

            for (int i = 0; i < grafo.ListaNodos.Count; i++)
            {
                V.Add(grafo.ListaNodos[i]);
            }
            for (int i = 0; i < grafo.ListaNodos.Count; i++)
            {
                D[i] = C[W.GSNombreNumInt, i];
                P[i] = -1;
            }

            //------------------------------------------------------------------------
            if (accion == 11)
            {
                msg += "D: ";
                for (int i = 0; i < D.GetLength(0); i++)
                    msg += D[i].ToString() + " ";

                msg += "\n" + "V: ";
                for (int i = 0; i < V.Count; i++)
                    msg += V[i].GSNombreNumInt.ToString() + " ";
            }
            //------------------------------------------------------------------------
            S.Add(W);
            V.Remove(W);
            //------------------------------------------------------------------------
            if (accion == 11)
            {
                msg += "\n" + "W: " + W.GSNombreNumInt.ToString();

                msg += "\n" + "V-W: ";
                for (int i = 0; i < V.Count; i++)
                    msg += V[i].GSNombreNumInt.ToString() + " ";
                MessageBox.Show(msg);
            }
            //------------------------------------------------------------------------
            for (int i = 0; i < grafo.ListaNodos.Count - 1; i++)
            {
                W = V[V.Count - 1];
                for (int j = 0; j < V.Count; j++)
                {
                    if (D[V[j].GSNombreNumInt] < D[W.GSNombreNumInt])// && D[W.GSNombreNumInt] != 0)
                        W = V[j];
                }
                S.Add(W);
                V.Remove(W);
                foreach (var v in V)
                {
                    if (mAd[W.GSNombreNumInt, v.GSNombreNumInt] == 1)
                    {
                        D[v.GSNombreNumInt] = Math.Min(D[v.GSNombreNumInt], D[W.GSNombreNumInt] + C[W.GSNombreNumInt, v.GSNombreNumInt]);
                        P[v.GSNombreNumInt] = W.GSNombreNumInt;
                        /*
                        if (D[v.GSNombreNumInt] > D[W.GSNombreNumInt] + C[W.GSNombreNumInt, v.GSNombreNumInt]) {
                            D[v.GSNombreNumInt] = D[W.GSNombreNumInt] + C[W.GSNombreNumInt, v.GSNombreNumInt];
                            P[v.GSNombreNumInt] = W.GSNombreNumInt;
                        }
                        */
                    }
                }
            }

            //-------------------------------------------------------------------------
            if (accion == 11)
            {
                msg = "D: ";
                string msgV = "V: ";
                string msgP = "P: ";
                for (int i = 0; i < D.GetLength(0); i++)
                {
                    msg += "[" + D[i].ToString() + "]" + " ";
                    msgV += S[i].GSNombreNumInt.ToString() + " ";
                    msgP += P[i].ToString() + " ";
                }
                msg += "\nCamino mas corto de ";
                if (letraNodo == true)
                    msg += Cam_Circ[0].GSNombre + " a " + Cam_Circ[1].GSNombre + " es de " + D[Cam_Circ[1].GSNombreNumInt] + ".\n";
                else
                    msg += Cam_Circ[0].GSNombreNumInt + " a " + Cam_Circ[1].GSNombreNumInt + " es de " + D[Cam_Circ[1].GSNombreNumInt] + ".\n";

                //MessageBox.Show(msg);
                calculaCaminoDF(D[Cam_Circ[1].GSNombreNumInt], msg);
            }
            //-------------------------------------------------------------------------
            //Accion==12
            if (accion == 12)
            {
                msg = "";
                //string msgV = "V: ";
                //string msgP = "P: ";
                /*
                for (int i = 0; i < D.GetLength(0); i++)
                {
                    msg += "[" + D[i].ToString() + "]" + " ";
                    msgV += S[i].GSNombreNumInt.ToString() + " ";
                    msgP += P[i].ToString() + " ";
                }
                */
                msg += "\nCamino mas corto de ";
                if (letraNodo == true)
                    msg += Cam_Circ[0].GSNombre + " a " + Cam_Circ[1].GSNombre + " es de " + D[Cam_Circ[1].GSNombreNumInt] + ".\n";
                else
                    msg += Cam_Circ[0].GSNombreNumInt + " a " + Cam_Circ[1].GSNombreNumInt + " es de " + D[Cam_Circ[1].GSNombreNumInt] + ".\n";

                //MessageBox.Show(msg);
                calculaCaminoDF(D[Cam_Circ[1].GSNombreNumInt], msg);
            }

        }

        private void calculaCaminoDF(double valor, string msg)
        {
            double[,] mC = new double[grafo.ListaNodos.Count, grafo.ListaNodos.Count];
            grafo.generaMatriz();
            grafo.generaMatrizCostos();
            mC = grafo.mCostos;
            List<int> cam = new List<int>();
            List<int> camResp = new List<int>();
            double costo = 0, menor = double.PositiveInfinity;
            caminos.Clear();
            caminos = grafo.HayCamino(Cam_Circ[0].GSNombreNumInt, Cam_Circ[1].GSNombreNumInt, grafo.matriz, grafo.ListaNodos.Count);
            for (int i = 0; i < Cam_Circ.Count; i++)
                cam.Add(Cam_Circ[i].GSNombreNumInt);

            for (int i = 0; i < caminos.Count; i++)
            {
                if (caminos[i][0] == Cam_Circ[0].GSNombreNumInt && caminos[i][caminos[i].Count - 1] == Cam_Circ[1].GSNombreNumInt)
                {
                    //costo += mC[caminos[i][0], caminos[i][1]];
                    costo = 0;
                    for (int j = 1; j < caminos[i].Count; j++)
                    {
                        costo += mC[caminos[i][j - 1], caminos[i][j]];
                    }
                    menor = Math.Min(menor, costo);
                    if (menor == costo && menor == valor)
                    {
                        for (int j = 1; j < caminos[i].Count; j++)
                        {
                            grafo.pintaCamino(caminos[i], j);
                            camResp = caminos[i];
                            /*
                            caminos.Clear();
                            caminos.Add(camResp);
                            */
                            Form1_Paint(this, null);
                            int del = 0;
                            while (del < 10000000)
                                del++;
                        }
                        MessageBox.Show(msg);
                        grafo.resetColor();
                        grafo.pintaCamino(cam, Cam_Circ.Count - 1);
                        Form1_Paint(this, null);
                    }
                }
            }
        }

        private void caminoMasCortoToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "D"://Dijkstra
                    accion = 11;
                    if (Cam_Circ.Count != 2)
                        Cam_Circ.Clear();
                    caminos.Clear();
                    break;
                case "F"://Floyd
                    accion = 12;
                    if (Cam_Circ.Count != 2)
                        Cam_Circ.Clear();
                    caminos.Clear();
                    break;
            }
        }

        private double[,] Prim(double[,] matriz)
        {
            bool[] marcados = new bool[grafo.ListaNodos.Count];
            CNodo vertice = grafo.ListaNodos[0];

            return Prim_Rec(matriz, marcados, vertice, new double[grafo.ListaNodos.Count, grafo.ListaNodos.Count]);
        }

        private double[,] Prim_Rec(double[,] matriz, bool[] marcados, CNodo vertice, double[,] final)
        {
            marcados[grafo.ListaNodos.IndexOf(vertice)] = true;
            double aux = -1;
            List<int> lista = new List<int>();

            if (!TodosMarcados(marcados))
            { //Mientras que no todos estén marcados
                for (int i = 0; i < marcados.Length; i++)
                { //Recorremos sólo las filas de los nodos marcados
                    if (marcados[i])
                    {
                        for (int j = 0; j < matriz.GetLength(0); j++)
                        {
                            if (matriz[i, j] >= 0)
                            {        //Si la arista existe
                                if (!marcados[j])
                                {         //Si el nodo no ha sido marcado antes
                                    if (aux == -1)
                                    {        //Esto sólo se hace una vez
                                        aux = matriz[i, j];
                                    }
                                    else
                                    {
                                        aux = Convert.ToDouble(Math.Min(aux, matriz[i, j])); //Encontramos la arista mínima
                                    }
                                }
                            }
                        }
                    }
                }
                //Aquí buscamos el nodo correspondiente a esa arista mínima (aux)
                for (int i = 0; i < marcados.Length; i++)
                {
                    if (marcados[i])
                    {
                        for (int j = 0; j < matriz.GetLength(0); j++)
                        {
                            if (matriz[i, j] == aux)
                            {
                                if (!marcados[j])
                                { //Si no ha sido marcado antes
                                    final[i, j] = aux; //Se llena la matriz final con el valor
                                    final[j, i] = aux;//Se llena la matriz final con el valor
                                                      //return AlgPrim(Matriz, marcados, ListaVertices.get(j), Final); //se llama de nuevo al método con
                                                      //el nodo a marcar
                                    lista.Add(i);
                                    lista.Add(j);
                                    caminos.Add(lista);

                                    return Prim_Rec(matriz, marcados, grafo.ListaNodos[j], final);
                                }
                            }
                        }
                    }
                }
            }
            return final;
        }

        public bool TodosMarcados(bool[] vertice)
        {
            foreach (var b in vertice)
            {
                if (!b)
                {
                    return b;
                }
            }
            return true;
        }

        public double[,] Kruskal()
        {
            List<int> lista = new List<int>();
            double[,] arbol = new double[grafo.ListaNodos.Count, grafo.ListaNodos.Count];
            grafo.generaMatrizCostos();
            double[,] ady = grafo.mCostos;
            int[] pert = new int[grafo.ListaNodos.Count];

            for (int i = 0; i < grafo.ListaNodos.Count; i++)
            {
                //arbol[i] = new double[grafo.ListaNodos.Count];
                pert[i] = i;
            }

            int nodoA = 0;
            int nodoB = 0;
            int arcos = 1;
            while (arcos < grafo.ListaNodos.Count)
            {
                double min = double.PositiveInfinity;
                for (int i = 0; i < grafo.ListaNodos.Count; i++)
                    for (int j = 0; j < grafo.ListaNodos.Count; j++)
                        if (min > ady[i, j] && ady[i, j] != 0 && pert[i] != pert[j])
                        {
                            min = ady[i, j];
                            nodoA = i;
                            nodoB = j;
                        }

                if (pert[nodoA] != pert[nodoB])
                {
                    lista = new List<int>();
                    arbol[nodoA, nodoB] = min;
                    arbol[nodoB, nodoA] = min;

                    //lista.Clear();

                    /*
                    lista.Add(nodoA);
                    lista.Add(nodoB);
                    caminos.Add(lista);
                    */

                    int temp = pert[nodoB];
                    pert[nodoB] = pert[nodoA];
                    for (int k = 0; k < grafo.ListaNodos.Count; k++)
                        if (pert[k] == temp)
                            pert[k] = pert[nodoA];

                    arcos++;

                    lista.Add(nodoA);
                    lista.Add(nodoB);
                    caminos.Add(lista);
                }
            }

            return arbol;
        }

        public int[,] Warshall()
        {
            int count = grafo.ListaNodos.Count;
            int[,] clausura = new int[count, count];
            grafo.generaMatriz();
            clausura = grafo.matriz;
            List<int> l = new List<int>();
            caminos.Clear();
            CArista ari;
            CGrafo resp = new CGrafo();
            resp.copiaGrafo(grafo);

            for (int k = 0; k < count; k++)
            {
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (clausura[i, j] == 0)
                        {
                            clausura[i, j] = clausura[i, k] * clausura[k, j];
                            if (clausura[i, j] == 1)
                            {
                                //l.Clear();
                                l = new List<int>();
                                l.Add(i);
                                l.Add(j);
                                caminos.Add(l);
                                //ari = new CArista(new CNodo(i), new CNodo(j));
                                ari = new CArista(grafo.ListaNodos[i], grafo.ListaNodos[j], 0, true);
                                ari.GSColor = Color.Red;
                                grafo.ListaAristas.Add(ari);
                                for (double d = 0; d < 10000; d++) ;
                                Form1_Paint(this, null);
                                /*
                                l = new List<int>();
                                l.Add(j);
                                l.Add(i);
                                caminos.Add(l);
                                */
                            }
                        }
                    }
                }
            }
            //grafo.copiaGrafo(resp);
            return clausura;

        }

        private void arboles_click(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = "";
            double max = 0;
            caminos.Clear();
            CGrafo resp = new CGrafo();
            resp.copiaGrafo(grafo);
            bool dibuja = false;
            switch (e.ClickedItem.AccessibleName)
            {
                case "Prim":
                    MessageBox.Show("Prim");
                    grafo.generaMatrizCostos();
                    grafo.generaMatriz();
                    double[,] p = Prim(grafo.mCostos);
                    for (int i = 0; i < p.GetLength(0); i++)
                    {
                        for (int j = 0; j < p.GetLength(1); j++)
                        {
                            msg += p[i, j].ToString() + ", ";
                        }
                        msg += "\n";
                    }
                    max = 100000000;
                    dibuja = true;
                    break;
                case "Kruskal":
                    MessageBox.Show("Kruskal");
                    double[,] k = Kruskal();
                    for (int i = 0; i < k.GetLength(0); i++)
                    {
                        for (int j = 0; j < k.GetLength(1); j++)
                        {
                            msg += k[i, j].ToString() + ", ";
                        }
                        msg += "\n";
                    }
                    max = 100000000;
                    dibuja = true;
                    break;
                case "Warshall":
                    MessageBox.Show("Warshall");
                    int[,] w = Warshall();
                    for (int i = 0; i < w.GetLength(0); i++)
                    {
                        for (int j = 0; j < w.GetLength(1); j++)
                        {
                            msg += w[i, j].ToString() + ", ";
                        }
                        msg += "\n";
                    }
                    max = 100000;
                    break;
            }
            double del = 0;
            MessageBox.Show(caminos.Count.ToString());
            if (dibuja == true)
                for (int i = 0; i < caminos.Count; i++)
                {
                    grafo.pintaCamino(caminos[i], caminos[i].Count - 1);
                    Form1_Paint(this, null);
                    for (del = 0; del < max; del++) ;

                }
            MessageBox.Show(msg);
            grafo.copiaGrafo(resp);
            grafo.resetColor();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left.Equals(e.Button))
            {
                switch (accion)
                {
                    case 2:
                        if (band == true)
                        {
                            pf2 = e.Location;
                            Form1_Paint(this, null);
                            band = true;
                        }
                        break;
                    case 3:
                        if (nodo != null)
                        {
                            pf = e.Location;
                            nodo.GSCentro = pf;
                            nodo.actualizaRectNodo(pf);
                            Form1_Paint(this, null);
                            band = true;
                        }
                        break;
                    case 10:
                        if (nodo != null)
                        {
                            pf = e.Location;
                            nodo.GSCentro = pf;
                            nodo.actualizaRectNodo(pf);
                            Form1_Paint(this, null);
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
                cambiaSent.Visible = true;
            }
            else
            {
                grafo.dirigido = false;
                cambiaSent.Visible = false;
            }
            Form1_Paint(this, null);
        }

        public bool kuratowsky()
        {
            int cuantos_restar = 0;
            bool avr = true;
            MessageBox.Show("Grafos k33");
            //K3 3
            cuantos_restar = 0;
            for (int i = grafo.Count; i > 6; i--)
            {
                cuantos_restar++;
            }
            MessageBox.Show("G.count: " + grafo.Count.ToString() + "\n" +
                            "restar: " + cuantos_restar.ToString());

            int[] v = new int[cuantos_restar];
            CGrafo respaldo = new CGrafo();

            CGrafo k5 = new CGrafo();
            CGrafo k33 = new CGrafo();
            string msg = "";

            k5 = grafok5();
            k33 = grafok33();

            respaldo.copiaGrafo(grafo);
            combinaciones = new List<int[]>();
            grafo.generaMatriz();
            genera_combinaciones(v, grafo.Count, cuantos_restar, cuantos_restar);

            MessageBox.Show("C: " + combinaciones.Count.ToString());
            foreach (var combinacion in combinaciones)
            {
                grafo.copiaGrafo(respaldo);
                grafo.resetVisible();
                msg = "";
                foreach (var a in combinacion)
                {
                    for (int n = 0; n < grafo.ListaNodos.Count; n++)
                    {
                        if (grafo.ListaNodos[n].GSNombreNumInt == a)
                        {
                            grafo.eliminaNodo(grafo.ListaNodos[n], false);
                        }
                    }
                }
                grafo.generaMatriz();
                /*
                Form1_Paint(this, null);
                for (int i = 0; i < 10000; i++) ;
                */
                //MessageBox.Show("");
                if (isomorfismo(grafo, k33) == true)
                {
                    Form1_Paint(this, null);
                    for (int i = 0; i < 10000000; i++) ;
                    //MessageBox.Show("isomorfico con el grafo K3,3");
                    avr = false;
                }
                grafo.copiaGrafo(respaldo);
            }

            grafo.copiaGrafo(respaldo);
            MessageBox.Show("Grafos k55");
            //K5
            cuantos_restar = 0;
            for (int i = grafo.Count; i > 5; i--)
            {
                cuantos_restar++;
            }
            MessageBox.Show("G.count: " + grafo.Count.ToString() + "\n" +
                            "restar: " + cuantos_restar.ToString());

            v = new int[cuantos_restar];

            combinaciones = new List<int[]>();
            grafo.generaMatriz();
            genera_combinaciones(v, grafo.Count, cuantos_restar, cuantos_restar);

            MessageBox.Show("C: " + combinaciones.Count.ToString());
            //for (int i = 0; i < combinaciones.Count; i++)
            foreach (var combinacion in combinaciones)
            {
                grafo.copiaGrafo(respaldo);
                grafo.resetVisible();
                msg = "";
                foreach (var a in combinacion)
                {
                    for (int n = 0; n < grafo.ListaNodos.Count; n++)
                    {
                        if (grafo.ListaNodos[n].GSNombreNumInt == a)
                        {
                            grafo.eliminaNodo(grafo.ListaNodos[n], false);
                        }
                    }
                }
                grafo.generaMatriz();
                //MessageBox.Show("");
                if (isomorfismo(grafo, k5) == true)
                {
                    Form1_Paint(this, null);
                    for (int i = 0; i < 10000000; i++) ;
                    //MessageBox.Show("isomorfico con el Grafo K5");
                    avr = false;
                }
                grafo.copiaGrafo(respaldo);
            }

            grafo.copiaGrafo(respaldo);
            return avr;
        }

        public void genera_combinaciones(int[] v, int n, int m, int cm)
        {
            int i, j;
            if (m != 0)
            {
                for (i = m, j = 0; i <= n; ++i, j++)
                {
                    v[m - 1] = grafo.ListaNodos[i - 1].GSNombreNumInt;
                    genera_combinaciones(v, i - 1, m - 1, cm);
                }
            }
            else
            {
                int[] vt = new int[cm];
                for (int k = 0; k < cm; k++)
                {
                    vt[k] = v[k];
                }
                combinaciones.Add(vt);
                //combinaciones.Add(v);
                string msgm = "Combi: ";
                foreach (var item in vt)
                {
                    msgm += item.ToString();
                }
                //MessageBox.Show(msgm);
            }
        }


        public bool isomorfismo(CGrafo g1, CGrafo g2)
        {
            if (g1 != null && g2 != null)
            {
                if (g1.ListaNodos.Count == g2.ListaNodos.Count)
                {
                    int tam = g1.ListaNodos.Count;
                    int indiceM1 = 0;
                    int indiceM2 = 1;
                    g1.generaMatriz();
                    int[,] matrizGrafo1 = g1.matriz;
                    g2.generaMatriz();
                    int[,] matrizGrafo2 = g2.matriz;
                    if (comparaMatriz(matrizGrafo1, matrizGrafo2, tam))
                        return true;
                    else
                        do
                        {
                            if (suma_columna(matrizGrafo1, indiceM1, tam) == suma_columna(matrizGrafo2, indiceM2, tam))
                            {

                                if (compara_listas(suma_unos(matrizGrafo1, indiceM1, tam), suma_unos(matrizGrafo2, indiceM2, tam), tam))
                                {
                                    intercambiaFilasyColumnas(matrizGrafo2, indiceM1, indiceM2, tam);
                                    if (comparaMatriz(matrizGrafo1, matrizGrafo2, tam))
                                        return true;
                                    else
                                    {
                                        indiceM1 += 1;
                                        indiceM2 = indiceM1 + 1;
                                    }
                                }
                                else
                                    indiceM2 += 1;
                            }
                            else
                                indiceM2 += 1;
                            if (indiceM2 == tam)
                                indiceM2 = 0;
                        } while (indiceM1 != indiceM2);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        private void intercambiaFilasyColumnas(int[,] matrizGrafo, int indice1, int indice2, int tam)
        {
            try
            {
                int[] aux = new int[tam];

                for (int i = 0; i < tam; i++)
                {
                    aux[i] = matrizGrafo[indice1, i];
                    matrizGrafo[indice1, i] = matrizGrafo[indice2, i];
                    matrizGrafo[indice2, i] = aux[i];
                }

                for (int i = 0; i < tam; i++)
                {
                    aux[i] = matrizGrafo[i, indice1];
                    matrizGrafo[i, indice1] = matrizGrafo[i, indice2];
                    matrizGrafo[i, indice2] = aux[i];
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public int suma_columna(int[,] matriz, int indice, int tamMatriz)
        {
            int suma = 0;
            for (int i = 0; i < tamMatriz; i++)
            {
                suma += matriz[indice, i];
            }
            return suma;
        }

        public List<int> suma_unos(int[,] U, int indice, int tamMatriz)
        {
            List<int> suma = new List<int>();
            for (int i = 0; i < tamMatriz; i++)
            {
                if (U[indice, i] != 0)
                {
                    suma.Add(suma_renglon(U, i, tamMatriz));
                }
                else
                {
                    suma.Add(0);
                }
            }
            return suma;
        }

        public int suma_renglon(int[,] matriz, int indice, int tamMatriz)
        {
            int suma = 0;
            for (int i = 0; i < tamMatriz; i++)
            {
                suma += matriz[i, indice];
            }
            return suma;
        }

        public bool compara_listas(List<int> lista1, List<int> lista2, int tam)
        {
            lista1.Sort();
            lista2.Sort();
            for (int i = 0; i < tam; i++)
            {
                if (lista1[i] != lista2[i])
                    return false;
            }

            return true;
        }

        public bool comparaMatriz(int[,] matriz1, int[,] matriz2, int tamMatriz)
        {
            for (int i = 0; i < tamMatriz; i++)
            {
                for (int j = 0; j < tamMatriz; j++)
                {
                    if (matriz1[i, j] != matriz2[i, j])
                        return false;
                }
            }
            return true;
        }

        private CGrafo grafok5()
        {
            string linea;
            int ac = 0, i;
            string sNum = "", sPeso = "", sTam = "", sX = "", sY = "", sNA = "", sNB = "", sDiri = "";
            int num, peso, tam, cont;
            Point pt;
            bool diri = false;
            nodo = new CNodo();
            CGrafo grafo = new CGrafo();
            grafo.resetGrafo();

            using (StreamReader archivo = new StreamReader("gk55.grafo"))
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
            //*/
            return grafo;
        }

        private CGrafo grafok33()
        {
            string linea;
            int ac = 0, i;
            string sNum = "", sPeso = "", sTam = "", sX = "", sY = "", sNA = "", sNB = "", sDiri = "";
            int num, peso, tam, cont;
            Point pt;
            bool diri = false;
            nodo = new CNodo();
            CGrafo grafo = new CGrafo();
            grafo.resetGrafo();

            using (StreamReader archivo = new StreamReader("gk33.grafo"))
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
            //*/
            return grafo;
        }

        #region Isomorfismo V2

        private void isomorfismoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buscar.Visible = true;
            finalizar.Visible = true;

            iso = new isomorfismo();
            accion = 13;
            iso.Show();
            //Isomor_Click(sender, e);
        }

        private void Isomor_Click(object sender, EventArgs e)
        {
            //textBoxIso.Clear();
            //if (!OtroGrafo.Enabled)
            if (iso.Visible == true)
            {
                List<List<int>> Matriz1 = grafo.MatrizLL();
                List<List<int>> Matriz2 = iso.grafo.MatrizLL();
                string s = null;
                Boolean f = false, b = false, mp = false, mpt = false;
                List<CNodo> nodosVisitados = new List<CNodo>();
                if (Matriz1.Count == Matriz2.Count)
                {
                    //Fuerza Bruta
                    nodosVisitados.Clear();
                    f = FuerzaBruta(nodosVisitados, grafo.ListaNodos[0], 0);
                    if (f)
                    {
                        s += "Son Isomorficos por el metodo de Fuerza Bruta";
                        s += "\r\n";
                    }
                    else
                    {
                        s += "No Son Isomorficos por el metodo de Fuerza Bruta";
                        s += "\r\n";
                    }

                    //Botello
                    b = BotelloV2(0, Matriz1, Matriz2);
                    if (b)
                    {
                        s += "Son Isomorficos por el metodo de Botello";
                        s += "\r\n";
                    }
                    else
                    {
                        s += "No Son Isomorficos por el metodo de Botello";
                        s += "\r\n";
                    }

                    //Matrices
                    mp = Permutacion(Matriz1, Matriz2);
                    if (mp)
                    {
                        s += "Son Isomorficos por el metodo de Permutacion";
                        s += "\r\n";
                    }
                    else
                    {
                        s += "No Son Isomorficos por el metodo de de Permutacion";
                        s += "\r\n";
                    }

                    mpt = PermutacionTranspuesta(Matriz1, Matriz2);
                    if (mpt)
                    {
                        s += "Son Isomorficos por el metodo de Permutacion transpuesta";
                        s += "\r\n";
                    }
                    else
                    {
                        s += "No Son Isomorficos por el metodo de de Permutacion transpuesta";
                        s += "\r\n";
                    }
                }
                else
                {
                    s += "No son Isomorficos";
                }

                //textBoxIso.Text = s;
                MessageBox.Show(s);
            }
        }

        private Boolean FuerzaBruta(List<CNodo> nodosVisitados, CNodo n, int i)
        {
            Boolean res = true;
            foreach (var item in nodosVisitados)
            {
                if (n == item)
                    res = false;
            }
            if (res)
            {
                foreach (var item in iso.grafo.ListaNodos)//Form2.nodos)
                {
                    //if (n.conec.Count == item.conec.Count)
                    if (n.GSGrado == item.GSGrado)
                    {
                        if (nodosVisitados.Count < grafo.ListaNodos.Count * 2)
                        {
                            nodosVisitados.Add(n);
                            nodosVisitados.Add(item);
                            i++;
                            if (i < grafo.ListaNodos.Count)
                                res = FuerzaBruta(nodosVisitados, grafo.ListaNodos[i], i);
                        }
                    }
                }
            }
            if (nodosVisitados.Count == 0)
                res = false;
            return res;
        }

        private Boolean BotelloV2(int i, List<List<int>> matriz_u, List<List<int>> matriz_v)
        {
            string s = "Botello";
            s += "\r\n";
            s += "Matriz U";
            s += "\r\n";
            for (int m1 = 0; m1 < matriz_u.Count; m1++)
            {
                for (int m2 = 0; m2 < matriz_u.Count; m2++)
                {
                    s += " " + matriz_u[m1][m2].ToString();
                }
                s += "\r\n";
            }
            s += "Matriz V";
            s += "\r\n";
            for (int m1 = 0; m1 < matriz_v.Count; m1++)
            {
                for (int m2 = 0; m2 < matriz_v.Count; m2++)
                {
                    s += " " + matriz_v[m1][m2].ToString();
                }
                s += "\r\n";
            }
            Boolean res = true;
            if (i == matriz_u.Count)
            {
                res = false;
            }
            else
            {
                int peso_col_u = 0;
                List<int> peso_ren_u = new List<int>();

                for (int c = 0; c < matriz_u.Count; c++)
                {
                    peso_col_u += matriz_u[i][c];
                    if (matriz_u[i][c] == 1)
                    {
                        int temp = 0;
                        for (int r = 0; r < matriz_u[i].Count; r++)
                        {
                            temp += matriz_u[r][c];
                        }
                        peso_ren_u.Add(temp);
                    }
                }

                int j = -1;
                for (int c = i + 1; c < matriz_v.Count; c++)
                {
                    int peso_col_v = 0;
                    for (int r = 0; r < matriz_v.Count; r++)
                    {
                        peso_col_v += matriz_v[c][r];
                    }

                    if (peso_col_v == peso_col_u)
                    {
                        List<int> peso_ren_v = new List<int>();
                        for (int t = 0; t < matriz_v.Count; t++)
                        {
                            if (matriz_v[c][t] == 1)
                            {
                                int temp = 0;
                                for (int r = 0; r < matriz_v.Count; r++)
                                {
                                    temp += matriz_v[r][t];
                                }
                                peso_ren_v.Add(temp);
                            }
                        }
                        for (int t = 0; t < peso_ren_u.Count; t++)
                        {
                            int eliminar = -1;
                            for (int t2 = 0; t2 < peso_ren_v.Count; t2++)
                            {
                                if (peso_ren_u[t] == peso_ren_v[t2])
                                {
                                    eliminar = t2;
                                    break;
                                }
                            }
                            if (eliminar != -1)
                            {
                                peso_ren_v.RemoveAt(eliminar);
                            }
                        }
                        if (peso_ren_v.Count == 0)
                        {
                            j = c;
                        }
                    }
                }

                if (j != -1)
                {
                    s += "Cambio de columna " + i + " por " + j;
                    s += "\r\n";
                    List<List<int>> temp = new List<List<int>>();
                    for (int c = 0; c < matriz_v.Count; c++)
                    {
                        temp.Add(new List<int>());
                        for (int r = 0; r < matriz_v.Count; r++)
                        {
                            if (c == j)
                                temp[c].Add(matriz_v[i][r]);
                            else if (c == i)
                                temp[c].Add(matriz_v[j][r]);
                            else
                                temp[c].Add(matriz_v[c][r]);
                            s += " " + matriz_v[c][r].ToString();
                        }
                        s += "\r\n";
                    }
                    s += "Cambio de renglon " + j + " por " + i;
                    s += "\r\n";
                    for (int c = 0; c < matriz_v.Count; c++)
                    {
                        for (int r = 0; r < matriz_v.Count; r++)
                        {
                            if (r == j)
                            {
                                matriz_v[c][r] = temp[c][i];

                            }
                            else if (r == i)
                            {
                                matriz_v[c][r] = temp[c][j];

                            }
                            else
                            {
                                matriz_v[c][r] = temp[c][r];


                            }
                            s += " " + matriz_v[c][r].ToString();
                        }
                        s += "\r\n";
                    }
                }
                for (int c = 0; c < matriz_u.Count && res; c++)
                {
                    for (int r = 0; r < matriz_u.Count; r++)
                    {
                        if (matriz_u[c][r] != matriz_v[c][r])
                        {
                            res = false;
                            break;
                        }
                    }
                }
                if (!res)
                {
                    res = BotelloV2(i + 1, matriz_u, matriz_v);
                }
            }
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta + "\\Iso_Botello.txt";
            File.WriteAllText(ruta, s);
            return res;
        }

        private Boolean Permutacion(List<List<int>> Matriz1, List<List<int>> Matriz2)
        {
            string s = "Permutacion";
            s += "\r\n";
            bool res = false;
            List<List<int>> MatrizP = devuelvePermutativo(Matriz1, Matriz2);

            if (Matriz1.Count == Matriz2.Count && MatrizP != null)
            {
                List<List<int>> Mx1 = multiplicaMatrices(Matriz1, MatrizP);
                List<List<int>> Mx2 = multiplicaMatrices(MatrizP, Matriz2);
                if (comparaMatrices(Mx1, Mx2))
                    res = true;
                else
                    res = false;
                s += "Matriz 1 * Matriz P";
                s += "\r\n";
                for (int m1 = 0; m1 < Matriz1.Count; m1++)
                {
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Matriz1[m1][m2].ToString();
                    }
                    s += " x ";
                    for (int m3 = 0; m3 < Matriz1.Count; m3++)
                    {
                        s += " " + MatrizP[m1][m3].ToString();
                    }
                    s += " = ";
                    for (int m4 = 0; m4 < Matriz1.Count; m4++)
                    {
                        s += " " + Mx1[m1][m4].ToString();
                    }
                    s += "\r\n";
                }
                s += "Matriz P * Matriz 2";
                s += "\r\n";
                for (int m1 = 0; m1 < Matriz1.Count; m1++)
                {
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + MatrizP[m1][m2].ToString();
                    }
                    s += " x ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Matriz2[m1][m2].ToString();
                    }
                    s += " = ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Mx2[m1][m2].ToString();
                    }
                    s += "\r\n";
                }
            }
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta + "\\Iso_Permutacion.txt";
            File.WriteAllText(ruta, s);
            return res;
        }

        private Boolean PermutacionTranspuesta(List<List<int>> Matriz1, List<List<int>> Matriz2)
        {
            string s = "Permutacion Transpuesta";
            s += "\r\n";
            bool res = false;
            List<List<int>> MatrizP = devuelvePermutativo(Matriz1, Matriz2);

            if (Matriz1.Count == Matriz2.Count && MatrizP != null)
            {
                List<List<int>> MatrizT = devuelveTranspuesta(MatrizP);
                List<List<int>> Px1 = multiplicaMatrices(MatrizP, Matriz1);
                List<List<int>> Px1T = multiplicaMatrices(Px1, MatrizT);
                if (comparaMatrices(Matriz2, Px1T))
                    res = true;
                else
                    res = false;
                s += "Matriz 2 = Matriz P * Matriz 1 * Matriz T";
                s += "\r\n";
                for (int m1 = 0; m1 < Matriz1.Count; m1++)
                {
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Matriz2[m1][m2].ToString();
                    }
                    s += " = ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + MatrizP[m1][m2].ToString();
                    }
                    s += " * ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Matriz1[m1][m2].ToString();
                    }
                    s += " * ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + MatrizT[m1][m2].ToString();
                    }
                    s += " = ";
                    for (int m2 = 0; m2 < Matriz1.Count; m2++)
                    {
                        s += " " + Px1T[m1][m2].ToString();
                    }
                    s += "\r\n";
                }
            }
            string ruta = Directory.GetCurrentDirectory();
            ruta = ruta + "\\Iso_Perm_Transpuesta.txt";
            File.WriteAllText(ruta, s);
            return res;
        }

        public List<List<int>> devuelvePermutativo(List<List<int>> m1, List<List<int>> m2)
        {
            List<List<int>> res = new List<List<int>>();
            List<int> pesom1 = new List<int>();
            List<int> pesom2 = new List<int>();
            for (int i = 0; i < m1.Count; i++)
            {
                pesom1.Add(0);
                pesom2.Add(0);
                res.Add(new List<int>());
                for (int j = 0; j < m1.Count; j++)
                {
                    res[i].Add(0);
                    pesom1[i] += m1[i][j];
                    pesom2[i] += m2[i][j];
                }
            }
            List<int> temp = new List<int>();
            for (int i = 0; i < pesom1.Count; i++)
            {
                for (int j = 0; j < pesom2.Count; j++)
                {
                    if (pesom1[i] == pesom2[j] && !temp.Contains(j))
                    {
                        res[i][j] = 1;
                        temp.Add(j);
                        break;
                    }
                }
            }

            if (temp.Count != m1.Count)
            {
                res = null;
            }

            return res;
        }

        public List<List<int>> multiplicaMatrices(List<List<int>> m1, List<List<int>> m2)
        {
            List<List<int>> res = new List<List<int>>();
            for (int i = 0; i < m1.Count; i++)
            {
                res.Add(new List<int>());
                for (int j = 0; j < m1.Count; j++)
                {
                    res[i].Add(0);
                    for (int k = 0; k < m1.Count; k++)
                    {
                        res[i][j] += m1[i][k] * m2[k][j];
                    }
                }
            }
            return res;
        }

        public Boolean comparaMatrices(List<List<int>> m1, List<List<int>> m2)
        {
            Boolean res = true;
            for (int i = 0; i < m1.Count && res; i++)
            {
                for (int j = 0; j < m1.Count; j++)
                {
                    if (m1[i][j] != m2[i][j])
                    {
                        res = false;
                        break;
                    }
                }
            }
            return res;
        }

        public List<List<int>> devuelveTranspuesta(List<List<int>> m1)
        {
            List<List<int>> res = new List<List<int>>();
            for (int i = 0; i < m1.Count; i++)
            {
                res.Add(new List<int>());
                for (int j = 0; j < m1.Count; j++)
                {
                    res[i].Add(m1[j][i]);
                }
            }
            return res;

        }


        #endregion

        #region Euler

        private void eulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accion = 14;
        }

        private void Euler(CNodo Punto)
        {
            List<List<CArista>> Camino = new List<List<CArista>>();
            List<CArista> Road = new List<CArista>();
            List<int> l = new List<int>();
            grafo.AdyacenciaLista();

            EulerRecursivo(Punto, Camino, Road);
            if (Camino.Count > 0)
            {
                foreach (List<CArista> la in Camino)
                {
                    MessageBox.Show("");
                    for (int i = 0; i < la.Count; i++)
                    {
                        grafo.pintaCamino(la, i);
                        Form1_Paint(this, null);
                    }
                    if ((Punto == la[la.Count - 1].GSOrigen || Punto == la[la.Count - 1].GSDestino) && grafo.hayCiclo(la, Punto))
                    {
                        MessageBox.Show("Es un circuito euler " + stringLista(la));
                    }
                    else
                    {
                        MessageBox.Show("Es un camino euler " + stringLista(la));
                    }
                    grafo.resetColor();
                }
            }
            else
            {
                MessageBox.Show("No hay camino de euler en " + Punto.GSNombreNum);
            }
            Camino.Clear();
            Road.Clear();
        }

        private void EulerRecursivo(CNodo A, List<List<CArista>> Recorrido, List<CArista> ActualRoad)
        {
            MessageBox.Show("recursivo");
            List<CArista> Road = new List<CArista>();
            copiaListaArista(Road, ActualRoad);
            if (Road.Count == grafo.ListaAristas.Count && !Recorrido.Contains(Road))
            {
                añadeRecorrido(Recorrido, Road);
            }
            else
            {
                foreach (CArista ax in A.ARel)
                {
                    if (!Road.Contains(ax))
                    {
                        Road.Add(ax);
                        if (ax.GSOrigen == A)
                        {
                            EulerRecursivo(ax.GSDestino, Recorrido, Road);
                        }
                        else
                        {
                            EulerRecursivo(ax.GSOrigen, Recorrido, Road);
                        }
                    }

                    copiaListaArista(Road, ActualRoad);
                }
            }
        }

        private void copiaListaArista(List<CArista> Copia, List<CArista> OG)
        {
            Copia.Clear();
            foreach (CArista ax in OG)
            {
                Copia.Add(ax);
            }
        }

        private void añadeRecorrido(List<List<CArista>> Recorrido, List<CArista> Road)
        {
            List<CArista> Aux = new List<CArista>();
            copiaListaArista(Aux, Road);
            Recorrido.Add(Aux);
        }

        private string stringLista(List<CArista> LA)
        {
            string s = "Camino : ";
            foreach (CArista a in LA)
            {
                s += a.GSOrigen.GSNombreNum + "," + a.GSDestino.GSNombreNum + "\n";
                s += "\t";
            }
            return s;
        }
        /*
            string msg = "";
            camino_euler = new List<List<CArista>>();
            //grafo.AdyacenciaLista(grafo.dirigido);
            grafo.generaRelaciones();
            Euler(nodo_camino_ini);
            if (camino_euler.Count > 0)
            {
                msg += "Caminos de euler: " + camino_euler.Count + "\n";
                foreach (List<CArista> cam in camino_euler)
                {
                    nodo_aux_eu = new CNodo();
                    msg += "Camino:\n";
                    for (int i = 0; i < cam.Count; i++)
                    {
                        msg += "|arista:";
                        msg += cam[i].GSOrigen.GSNombreNumInt.ToString();
                        msg += "-";
                        msg += cam[i].GSDestino.GSNombreNumInt.ToString();
                        msg += "|>";
                    }

                    msg += ".\n";
                }
            }
        }

        public void Euler(CNodo Punto)
        {
            caminote_euler = new List<List<CArista>>();
            List<CArista> Road = new List<CArista>();

            EulerRecursivo(Punto, caminote_euler, Road);
            if (caminote_euler.Count <= 0)
            {
                MessageBox.Show("No hay camino de euler en " + Punto.GSNombreNumInt.ToString());
            }

            foreach (List<CArista> ar in caminote_euler)
            {
                camino_euler.Add(ar);
            }

            caminote_euler.Clear();
            Road.Clear();

            //Microsoft.VisualBasic.Interaction.MsgBox("terminado");
        }
        public void EulerRecursivo(CNodo A, List<List<CArista>> Recorrido, List<CArista> ActualRoad)
        {
            List<CArista> Road = new List<CArista>();
            copiaListaArista(Road, ActualRoad);
            if (Road.Count == grafo.ListaAristas.Count && !Recorrido.Contains(Road))
            {
                //Microsoft.VisualBasic.Interaction.MsgBox("primer if");
                añadeRecorrido(Recorrido, Road);
            }
            else
            {
                foreach (CArista ax in A.ARel)
                {
                    if (!Road.Contains(ax))
                    {
                        //Microsoft.VisualBasic.Interaction.MsgBox("segundo if");
                        Road.Add(ax);
                        if (ax.GSOrigen == A)
                        {
                            //Microsoft.VisualBasic.Interaction.MsgBox("tercer if");
                            EulerRecursivo(ax.GSDestino, Recorrido, Road);
                        }
                        else
                        {
                            //Microsoft.VisualBasic.Interaction.MsgBox("else del tercer if");
                            EulerRecursivo(ax.GSOrigen, Recorrido, Road);
                        }
                    }

                    copiaListaArista(Road, ActualRoad);
                }
            }
        }
        public void copiaListaArista(List<CArista> Copia, List<CArista> OG)
        {
            Copia.Clear();
            foreach (CArista ax in OG)
            {
                Copia.Add(ax);
            }
        }

        public void añadeRecorrido(List<List<CArista>> Recorrido, List<CArista> Road)
        {
            List<CArista> Aux = new List<CArista>();
            copiaListaArista(Aux, Road);
            Recorrido.Add(Aux);
        }
        public void AdyacenciaLista(bool b)
        {
            foreach (CNodo nx in lista_nodos)
            {
                foreach (CArista ax in lista_aristas)
                {

                    if ((ax.origen_public == nx(ax.destino_public == nx && ((true && true)(false && false)))) && !nx.ARel.Contains(ax))
                    {
                        nx.ARel.Add(ax);
                    }
                }
            }
        }
        */
        #endregion

    }
}
