using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EditorGrafos
{
    public class CArista
    {
        private int peso;
        private string Nombre;
        private CNodo Origen;
        private CNodo Destino;
        private Color color, colorResp;
        private Rectangle rect;
        private bool nDiri;

        public CArista()
        {

        }

        public String GSNombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        public CNodo GSOrigen
        {
            get { return Origen; }
            set { Origen = value; }
        }

        public CNodo GSDestino
        {
            get { return Destino; }
            set { Destino = value; }
        }

        public Color GSColor
        {
            get { return color; }
            set { color = value; }
        }

        public int GSPeso
        {
            get { return peso; }
            set { peso = value; }
        }

        public bool GSDiri
        {
            get { return nDiri; }
            set { nDiri = value; }
        }

        public CArista(CNodo a, CNodo b)
        {
            Origen = a;
            Destino = b;
            peso = 0;
            GSDiri = true;
            colorResp = color = Color.Black;
        }

        public CArista(CNodo a, CNodo b, int p)
        {
            Origen = a;
            Destino = b;
            peso = p;
            nDiri = true;
            colorResp = color = Color.Black;
        }

        public CArista(CNodo a, CNodo b, int p, bool d)
        {
            Origen = a;
            Destino = b;
            peso = p;
            nDiri = d;
            colorResp = color = Color.Black;
        }

        public CArista(CNodo b, int p, bool d)
        {
            Origen = null;
            Destino = b;
            peso = p;
            nDiri = d;
            colorResp = color = Color.Black;
        }

        public void cambiaSentido()
        {
            if (nDiri == true)
                nDiri = false;
            else
                nDiri = true;
        }

        public void resetColor()
        {
            color = colorResp;
        }
       
    }
}
