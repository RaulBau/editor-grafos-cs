using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EditorGrafos
{
    public class CNodo
    {
        static char nombre = 'A';
        static int numNomb = 0;

        private string Nombre;
        private string NombreNum;
        private Point centro;
        private int NombreNumInt;
        private Rectangle rect;
        private int tam;
        private Color color, colorResp;
        public bool revisado;
        public bool CC, visible;
        private int grado;
        public List<CNodo> LRel;
        public List<CArista> ARel;

        public CNodo()
        {

        }

        public CNodo(Point pt, int tam)
        {
            this.tam = tam;
            Nombre = nombre.ToString();
            NombreNum = numNomb.ToString();
            NombreNumInt = numNomb;
            nombre++;
            numNomb++;
            colorResp = Color.LightGray;
            color = Color.LightGray;
            centro.X = pt.X;// -(tam / 2);
            centro.Y = pt.Y;// -(tam / 2);
            rect = new Rectangle(centro.X - (tam / 2), centro.Y - (tam / 2), tam, tam);
            revisado = true;
            CC = false;
            grado = 0;
            LRel = new List<CNodo>();
            ARel = new List<CArista>();
            visible = true;
        }

        public void resetColor()
        {
            color = colorResp;
        }

        public void resetCNodo()
        {
            nombre = 'A';
            numNomb = 0;
        }

        public void incCNodo()
        {
            nombre++;
            numNomb++;
        }

        public CNodo(int num)
        {
            while (num != numNomb)
            {
                nombre++;
                numNomb++;
            }
            colorResp = Color.LightGray;
            color = Color.LightGray;
            LRel = new List<CNodo>();
            NombreNum = numNomb.ToString();
            NombreNumInt = numNomb;
            Nombre = nombre.ToString();
            visible = true;
        }

        public Color GSColor
        {
            get { return color; }
            set { color = value; }
        }

        public String GSNombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        public String GSNombreNum
        {
            get { return NombreNum; }
            set { NombreNum = value; }
        }

        public int GSNombreNumInt
        {
            get { return NombreNumInt; }
            set { NombreNumInt = value; }
        }

        public Point GSCentro
        {
            get { return centro; }
            set { centro = value; }
        }

        public Rectangle GSRect
        {
            get { return rect; }
            set { rect = value; }
        }

        public int GSTam
        {
            get { return tam; }
            set { tam = value; }
        }

        public int GSGrado
        {
            get { return grado; }
            set { grado = value; }
        }

        public void actualizaRectNodo(Point cen)
        {
            this.centro = cen;
            this.rect.Y = centro.Y - (tam / 2);
            this.rect.X = centro.X - (tam / 2);
            this.rect.Width = tam;
            this.rect.Height = tam;
        }

        public void añadeRelacion(CArista arista)
        {
            LRel.Add(arista.GSDestino);
        }

        public void añadeRelacion(CNodo Destino, int peso, bool diri)
        {
            LRel.Add(Destino);
        }

        public void incrementaGrado()
        {
            grado++;
        }
    }
}
