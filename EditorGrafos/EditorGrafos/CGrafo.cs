using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EditorGrafos
{
    [Serializable()]

    public class CGrafo
    {
        public List<CNodo> ListaNodos;
        public List<CArista> ListaAristas;
        private Form1 form;
        public CNodo nodo;
        public bool dirigido;
        public int[,] matriz;
        public double[,] mCostos;

        private Matriz m_form;
        private List<CNodo> k, m;

        private List<CNodo> Lk, Lm;


        public CGrafo(Form1 f)
        {
            ListaAristas = new List<CArista>();
            ListaNodos = new List<CNodo>();
            form = f;
            nodo = new CNodo();
            dirigido = false;

            m_form = new Matriz();
            k = new List<CNodo>();
            m = new List<CNodo>();
        }


        public CGrafo()
        {
            ListaAristas = new List<CArista>();
            ListaNodos = new List<CNodo>();
            nodo = new CNodo();
            dirigido = false;

            m_form = new Matriz();
            k = new List<CNodo>();
            m = new List<CNodo>();
        }

        public int Count
        {
            get { return ListaNodos.Count; }
        }

        public void copiaGrafo(CGrafo g)
        {
            this.ListaAristas.Clear();
            this.ListaNodos.Clear();
            foreach (var item in g.ListaNodos)
            {
                this.ListaNodos.Add(item);
            }

            foreach (var item in g.ListaAristas)
            {
                this.ListaAristas.Add(item);
            }
        }

        public void fucionaNodos(CNodo n)
        {
            CNodo nAux = new CNodo();
            //CNodo aux = new CNodo();

            nAux = this.buscaNodoPoint(n.GSCentro);
            if (nAux == n || nAux==null)
                return;

            foreach (var item in ListaAristas)
            {
                if(item.GSOrigen!=item.GSDestino)
                {
                    if (item.GSOrigen == nAux && item.GSDestino != n)
                        item.GSOrigen = n;
                    if (item.GSDestino == nAux && item.GSOrigen != n)
                        item.GSDestino = n;                    
                }
            }
            //this.ListaNodos.Remove(nAux);
            this.eliminaNodo(nAux, false);
        }

        public bool IsomorfismoManual(CGrafo g1, CGrafo g2)
        {
            g1.generaMatriz();
            g2.generaMatriz();
            List<int[]> pesosU = CalculaPeso(g1.matriz, g1.Count);
            List<int[]> pesosV = CalculaPeso(g2.matriz, g2.Count);
            /*
            Archivo arc = new Archivo();
            arc.Abrete();
            */
            //StreamWriter stw = new StreamWriter("matrizManual.txt");
            StreamWriter stw = new StreamWriter(@"matrizManual.txt", true);
            if (stw == null)
            {
                MessageBox.Show("No se creo un archivo para guardar.");
                return false;
            }
            else
            {
                MessageBox.Show("Se creo un archivo.");
            }

            if (MatricesIguales(g1.matriz, g2.matriz, g1.Count))
            {
                //arc.Iguales(g1, g2);
                //arc.Cierrate();
                stw.Close();
                return true;
            }
            
            stw.WriteLine("Grafo U");
            GuardaMatriz(g1.matriz, g1.Count, stw);
            stw.WriteLine("Grafo V");
            GuardaMatriz(g2.matriz, g2.Count, stw);
            stw.WriteLine("Algoritmo Botello/Manual");
            
            for (int i = 0; i < g1.Count - 1; i++)
                for (int j = i + 1; j < g1.Count; j++)
                    if (pesosU[0][i] == pesosV[0][j])//comparando los pesos de las columnas
                    {
                        g2.matriz = Intercambia(i, j, g1.Count, g2.matriz);
                        //arc.CambiandoPos(g2, i, j);
                        if (MatricesIguales(g1.matriz, g2.matriz, g1.Count))
                        {
                            stw.WriteLine("V = U");
                            stw.Close();
                            //arc.Cierrate();
                            return true;
                        }
                        pesosV = CalculaPeso(g2.matriz, g2.Count);
                    }
            stw.WriteLine("El algoritmo no funciono.");
            stw.Close();
            //arc.Cierrate();
            return false;
        }

        public int[,] Intercambia(int index1, int index2, int count, int[,] V)
        {
            int[] temp = new int[count];
            for (int i = 0; i < count; i++)//respaldando la columna de v
                temp[i] = V[i, index1];

            for (int i = 0; i < count; i++)//col
                V[i, index1] = V[i, index2];
            for (int i = 0; i < count; i++)//
                V[i, index2] = temp[i];
            //m_form.MuestraMatriz(V, count);
            for (int i = 0; i < count; i++)//respaldando el renglon de v
                temp[i] = V[index1, i];
            //MessageBox.Show("cr: " + temp[i]);
            for (int i = 0; i < count; i++)//ren2 a ren1
                V[index1, i] = V[index2, i];
            //m_form.MuestraMatriz(V, count);
            for (int i = 0; i < count; i++)//temp a ren2
                V[index2, i] = temp[i];
            //m_form.MuestraMatriz(V, count);
            return V;
        }

        //retorna una lista de dos arreglos de int, con los pesos de renglon y columna
        public List<int[]> CalculaPeso(int[,] MR, int count)
        {
            int[] pesoC = new int[count], pesoR = new int[count];
            List<int[]> pesos = new List<int[]>();

            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                {
                    pesoR[i] += MR[i, j];
                    pesoC[j] += MR[j, i];
                }
            pesos.Add(pesoR);
            pesos.Add(pesoC);
            return pesos;
        }

        public bool PruebaPermutacion(int[,] M1, int[,] M2, int[] permutacion)
        {
            for (int i = 0; i < permutacion.Length; i++)
                for (int j = 0; j < permutacion.Length; j++)
                    if (M1[i, j] != M2[permutacion[i], permutacion[j]]) return false;
            return true;
        }

        public bool FuerzaBruta(int[,] g1, int[,] g2, int count)
        {
            /*
            if (count > 14)
                MessageBox.Show("Demasiados nodos, ¿Me quiéres matar?");
            */
            long[] fact = { 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800,
                           39916800, 479001600, 6227020800, 87178291200};//cantidad maxima de veces a permutar
            int[] indexp = new int[count]; //indices a permutar
            //creando el arreglo de indices
            for (int i = 0; i < count; indexp[i] = i, i++) ;

            /*
            Archivo arc = new Archivo();
            arc.Abrete();
            */
            //StreamWriter stw = new StreamWriter("matrizFuerzaBruta.txt");
            StreamWriter stw = new StreamWriter(@"matrizFuerzaBruta.txt", true);
            if (stw == null)
            {
                MessageBox.Show("No se creo un archivo.");
                return false;
            }
            else
            {
                MessageBox.Show("Se creo un archivo.");
            }
            //si las matrices son iguales, sin permutar nada, return
            if (MatricesIguales(g1, g2, count))
            {
                //arc.Iguales(g1, g2, count);
                //arc.Cierrate();
                stw.Close();
                return true;
            }
            stw.WriteLine("Matriz A.");
            GuardaMatriz(g1, count, stw);
            stw.WriteLine("Matriz B.");
            GuardaMatriz(g2, count, stw);
            stw.WriteLine("Empezando permutacion de matriz B, para B==A.");

            //creamos una permutacion y la probamos
            for (int i = 0; i < fact[count]; i++)
            {
                NextPermutation(indexp);
                //stw.WriteLine("Probando Permutacion de Matriz B.");
                //GuardaMatriz(g2, indexp[i], stw);
                if (PruebaPermutacion(g1, g2, indexp))
                {
                    stw.WriteLine("Permutacion correcta encontrada, A=B");
                    stw.WriteLine("Matriz A igual al la ultima permutacion.");
                    GuardaMatriz(g1, count, stw);
                    //arc.Cierrate();
                    stw.Close();
                    return true;
                }
            }
            //si se terminaron de probar todas las permutaciones y ninguna fue igual
            stw.WriteLine("Ninguna permutacion funciono.");
            stw.Close();
            //arc.Cierrate();
            return false;
        }

        private static bool NextPermutation(int[] numList)
        {
            /* 
            Knuths 
            1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation. 
            2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l. 
            3. Swap a[j] with a[l]. 
            4. Reverse the sequence from a[j + 1] up to and including the final element a[n]. 
            */
            var largestIndex = -1;
            for (var i = numList.Length - 2; i >= 0; i--)
            {
                if (numList[i] < numList[i + 1])
                {
                    largestIndex = i;
                    break;
                }
            }
            if (largestIndex < 0) return false;
            var largestIndex2 = -1;
            for (var i = numList.Length - 1; i >= 0; i--)
            {
                if (numList[largestIndex] < numList[i])
                {
                    largestIndex2 = i;
                    break;
                }
            }
            var tmp = numList[largestIndex];
            numList[largestIndex] = numList[largestIndex2];
            numList[largestIndex2] = tmp;
            for (int i = largestIndex + 1, j = numList.Length - 1; i < j; i++, j--)
            {
                tmp = numList[i];
                numList[i] = numList[j];
                numList[j] = tmp;
            }
            return true;
        }

        public bool MatricesIguales(int[,] u, int[,] v, int count)
        {
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    if (u[i, j] != v[i, j])
                        return false;
            return true;
        }

        //B = P • A • Pt
        public bool IsomorfismoMatrizPermutada(CGrafo g1, CGrafo g2)
        {
            g1.generaMatriz();
            g2.generaMatriz();
            int[,] A = g1.matriz, B = g2.matriz, P = new int[g1.Count, g1.Count], T = new int[g1.Count, g1.Count];
            int count = g1.Count;

            //StreamWriter stw = new StreamWriter("matriz.txt");
            StreamWriter stw = new StreamWriter(@"matriz.txt",true);
            if (stw == null)
            {
                MessageBox.Show("No se creo un archivo.");
                return false;
            }
            else
            {
                MessageBox.Show("Se creo un archivo.");
            }
            
            if (MatricesIguales(A, B, count))
            {
                //arc.Iguales(g1, g2);
                //arc.Cierrate();
                stw.Close();
                return true;
            }

            List<int[]> pesos = CalculaPeso(A, count);
            List<int[]> pesos2 = CalculaPeso(B, count);
            List<int> f = pesos[0].ToList();
            List<int> g = pesos2[0].ToList();
            List<int> mper = new List<int>();
            for (int i = 0; i < count; i++)//inicializando las matrices con 0s
                for (int j = 0; j < count; j++)
                {
                    P[i, j] = 0;
                    T[i, j] = 0;
                }
            for (int i = 0; i < f.Count; i++)//creando la permutacion
                for (int j = 0; j < g.Count; j++)
                    if (f[i] == g[j])
                        if (!mper.Contains(j))
                        {
                            P[i, j] = 1;
                            T[j, i] = 1;
                            mper.Add(j);
                            break;
                        }
            
            stw.WriteLine("Matriz de permutacion.");
            GuardaMatriz(P, count, stw);
            stw.WriteLine("Matriz de permutacion transpuesta.");
            GuardaMatriz(T, count, stw);
            
            return (CreaMatricezPermutadas(P, T, A, B, count, stw, f));
        }

        public bool CreaMatricezPermutadas(int[,] P, int[,] T, int[,] A, int[,] B, int count, StreamWriter stw, List<int> f)
        {
            long[] fact = { 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800,
                           39916800, 479001600, 6227020800, 87178291200};
            int[] indices = new int[count];
            for (int i = 0; i < count; indices[i] = i, i++) ;
            int[,] res = new int[count, count];
            /*int rep = 0;
            for (int k = 0; k < fact[count]; k++)
                for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                {
                    if (i != j && f[i] == f[j])
                    {
                        int[] per = { i, j};
                        NextPermutation(per);
                        indices[i] = per[0];
                        indices[j] = per[1];
                        res = FotmulaPAPT(P, A, T, count, indices, arc);
                        if (MatricesIguales(res, B, count))
                            return true;
                    }
                }*/
            for (int i = 0; i < fact[count]; i++)
            {
                NextPermutation(indices);
                res = FotmulaPAPT(P, A, T, count, indices, stw);
                stw.WriteLine("P*A*PT");
                GuardaMatriz(res, count, stw);
                if (MatricesIguales(B, res, count))
                {
                    stw.WriteLine("Funciona B = P * A * PT");
                    stw.WriteLine("**- A -**");
                    GuardaMatriz(B, count, stw);
                    stw.WriteLine("--P * A *PT--");
                    GuardaMatriz(res, count, stw);
                    stw.Close();
                    return true;
                }
            }
            stw.WriteLine("No funciono.");
            stw.Close();
            return false;
        }
        
        public void GuardaMatriz(int[,] mat, int c, StreamWriter stw)
        {
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    stw.Write(mat[i, j].ToString() + " ");
                }
                stw.WriteLine();
            }
        }

        public int[,] FotmulaPAPT(int[,] p, int[,] a, int[,] t, int count, int[] indices, StreamWriter stw)
        {
            int[,] P = new int[count, count];
            int[,] T = new int[count, count];

            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    P[i, j] = p[indices[i], indices[j]];
            stw.WriteLine("Nuevas matrices permutadas");
            stw.WriteLine("**- P -**");
            GuardaMatriz(P, count, stw);
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    T[i, j] = P[j, i];
            stw.WriteLine("**- T -**");
            GuardaMatriz(T, count, stw);
            int[,] PA = Matriz_Mult(P, a, count);
            int[,] PAT = Matriz_Mult(PA, T, count);
            return PAT;
        }

        public int[,] Matriz_Mult(int[,] A, int[,] B, int count)
        {
            int temp = 0;
            int[,] kHasil = new int[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    temp = 0;
                    for (int k = 0; k < count; k++)
                    {
                        temp += A[i, k] * B[k, j];
                    }
                    kHasil[i, j] = temp;
                }
            }
            return kHasil;
        }

        public void resetRevisado()
        {
            foreach (var item in ListaNodos)
            {
                item.revisado = false;
            }
        }

        public CArista eliminaAristaMR(int r, int c)
        {
            CNodo nodoR = this.ListaNodos.ElementAt<CNodo>(r);
            CNodo nodoC = this.ListaNodos.ElementAt<CNodo>(c);
            int i, j;
            CArista auxAri=new CArista();
            auxAri = null;
            int elimina = -1;
            
            foreach (CArista ari in ListaAristas)
            {

                if (this.dirigido == true)
                {
                    if (ari.GSDiri == true)
                    {
                        foreach (var item in ListaAristas)
                        {
                            if (item.GSOrigen == nodoR && item.GSDestino == nodoC)
                            {
                                elimina = this.ListaAristas.IndexOf(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in ListaAristas)
                        {
                            if ((item.GSOrigen == nodoR && item.GSDestino == nodoC) || (item.GSOrigen == nodoC && item.GSDestino == nodoR))
                            {
                                elimina = this.ListaAristas.IndexOf(item);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in ListaAristas)
                    {
                        if (item!=null && ((item.GSOrigen == nodoR && item.GSDestino == nodoC) || (item.GSOrigen == nodoC && item.GSDestino == nodoR)))
                        {
                            elimina = this.ListaAristas.IndexOf(item);
                        }
                    }
                }
            }

            if (elimina != -1)
            {
                auxAri = this.ListaAristas.ElementAt<CArista>(elimina);
                this.ListaAristas.RemoveAt(elimina);
            }

            this.generaMatriz();
            return auxAri;
        }

        private void agregaAristaMR(CArista arista)
        {
            this.ListaAristas.Add(arista);
            this.generaMatriz();
        }

        public List<int>adyacentes(CNodo n)
        {
            List<int> ad = new List<int>();
            CNodo nAux = new CNodo();
            this.generaMatriz();

            int nod = 0;
            nod = this.ListaNodos.IndexOf(n);
            for (int i = 0; i < this.Count; i++)
            {
                nAux = ListaNodos.ElementAt<CNodo>(i);
                if (matriz[nod, i] == 1 && nAux.visible==true)
                {
                    ad.Add(i);
                }
            }

            /*
            string msg = "";
            msg += n.GSNombre + "\n";
            foreach (var item in ad)
            {
                msg += item.ToString()+" ";
            }
            MessageBox.Show(msg);
            */

            return ad;
        }

        public int Aristas(CNodo n)
        {
            int a = 0;
            CNodo nAux = new CNodo();
            this.generaMatriz();

            int nod = 0;
            nod = this.ListaNodos.IndexOf(n);
            for (int i = 0; i < this.Count; i++)
            {
                nAux = this.ListaNodos.ElementAt<CNodo>(i);
                if(matriz[nod,i]==1 && nAux.visible==true)
                {
                    a++;
                }
            }

            return a;
        }

        public bool esPuente(CNodo u, CNodo v)
        {
            bool puente = false;
            int cont, cont2;
            int r, c;
            CArista auxA = new CArista();

            r = this.ListaNodos.IndexOf(u);
            c = this.ListaNodos.IndexOf(v);

            this.generaMatriz();
            cont = conteoProfundidad(u);
            auxA=this.eliminaAristaMR(r, c);
            this.generaMatriz();

            this.resetRevisado();
            cont2 = conteoProfundidad(u);
            this.agregaAristaMR(auxA);
            this.generaMatriz();
            if(cont>cont2)
            {
                puente = true;
            }


            return puente;
        }

        public int conteoProfundidad(CNodo v)
        {
            int cont;
            int i;

            cont = 1;
            v.revisado = true;
            i=ListaNodos.IndexOf(v);
            List<int> rela = new List<int>();

            rela = this.adyacentes(v);
            foreach (var rel in rela)
            {
                if(matriz[i,rel]==1)
                {
                    if(ListaNodos.ElementAt<CNodo>(rel).revisado==false)
                    {
                        cont = cont + conteoProfundidad(ListaNodos.ElementAt<CNodo>(rel));
                    }
                }
            }

            return cont;
        }

        public void resetGrafo()
        {
            this.ListaAristas.Clear();
            this.ListaNodos.Clear();
            nodo.resetCNodo();
        }

        public void addNodo(CNodo a)
        {
            ListaNodos.Add(a);
        }

        public void addArista(CNodo a, CNodo b)
        {
            ListaAristas.Add(new CArista(a, b));
        }

        public void addArista(CNodo a, CNodo b, int p)
        {
            ListaAristas.Add(new CArista(a, b, p));
        }

        public void addArista(CNodo a, CNodo b, int p, bool d)
        {
            ListaAristas.Add(new CArista(a, b, p, d));
        }

        //busca un camino entre el indice del nodo n1 y n2 de un grafo
        public List<List<int>> HayCamino(int n1, int n2, int[,] MR, int count)
        {
            List<List<int>> caminos = new List<List<int>>();
            for (int i = 0; i < count; i++)
                if (MR[n1, i] == 1)
                    CreaCamino(caminos, new List<int> { n1 }, i, n2, MR, count);

            if (n1 == n2)//para circuitos
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
            return caminos;
        }

        public void resetColor()
        {
            foreach (var item in ListaNodos)
            {
                item.resetColor();
            }
            if (ListaAristas.Count > 0)
            {
                foreach (var item in ListaAristas)
                {
                    item.resetColor();
                }
            }
        }

        public void resetVisible()
        {
            foreach (var item in ListaNodos)
            {
                item.visible = true;
            }
        }

        public void CreaCamino(List<List<int>> caminos, List<int> camino, int n1, int n2, int[,] MR, int count)
        {
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
            for (int i = 0; i < count; i++)
                if (MR[n1, i] == 1)
                {
                    List<int> c = new List<int>();
                    for (int j = 0; j < camino.Count; j++)
                        c.Add(camino[j]);
                    CreaCamino(caminos, c, i, n2, MR, count);
                }
        }


        public void generaRelaciones()
        {
            foreach (var nod in ListaNodos)
            {
                if (nod.visible==true)
                {
                    nod.LRel.Clear();
                    nod.revisado = false;
                }
            }

            if (dirigido == false)
            {
                foreach (var ari in ListaAristas)
                {
                    if(ari!=null)
                    if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                    {
                        ari.GSOrigen.añadeRelacion(ari);
                        ari.GSDestino.añadeRelacion(ari.GSOrigen, ari.GSPeso, ari.GSDiri);
                    }
                }
            }
            else
            {
                foreach (var ari in ListaAristas)
                {
                    if(ari!=null)
                    if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                    {
                        if (ari.GSDiri == true)
                        {
                            ari.GSOrigen.añadeRelacion(ari);
                        }
                        else
                        {
                            ari.GSOrigen.añadeRelacion(ari);
                            ari.GSDestino.añadeRelacion(ari.GSOrigen, ari.GSPeso, ari.GSDiri);
                        }
                    }
                }
            }

        }

        public CNodo buscaNodoPointUp(Point p)
        {
            double dist;
            CNodo n = new CNodo();
            CNodo nod = new CNodo();
            n = null;

            //foreach (CNodo nod in this.ListaNodos)
            for (int i = ListaNodos.Count-1; i >=0; i--)
            {
                nod = ListaNodos.ElementAt<CNodo>(i);
                if (nod.visible == true)
                {
                    dist = Math.Sqrt(Math.Pow(p.X - nod.GSCentro.X, 2) + Math.Pow(p.Y - nod.GSCentro.Y, 2));
                    if (nod.GSRect.Contains(p))
                    {
                        if (dist <= (nod.GSRect.Width / 2))
                        {
                            n = nod;
                            break;
                        }
                    }
                }
            }

            return n;
        }

        public CNodo buscaNodoPoint(Point p)
        {
            double dist;
            CNodo n = new CNodo();
            n = null;

            foreach (CNodo nod in this.ListaNodos)
            {
                if (nod.visible == true)
                {
                    dist = Math.Sqrt(Math.Pow(p.X - nod.GSCentro.X, 2) + Math.Pow(p.Y - nod.GSCentro.Y, 2));
                    if (nod.GSRect.Contains(p))
                    {
                        if (dist <= (nod.GSRect.Width / 2))
                        {
                            n = nod;
                            break;
                        }
                    }
                }
            }

            return n;
        }

        public void MoverNodo(CNodo n)
        {
            foreach (CNodo nodo in ListaNodos)
            {
                if (nodo.visible == true)
                {
                    if (nodo.GSNombre == n.GSNombre)
                    {
                        nodo.GSCentro = n.GSCentro;
                        nodo.actualizaRectNodo(n.GSCentro);
                        break;
                    }
                }
            }
        }

        public void eliminaNodo(CNodo nodo, bool band)
        {
            int i=0;

            foreach (CArista ari in this.ListaAristas)
            {
                if (ari.GSOrigen.GSNombre == nodo.GSNombre||ari.GSDestino.GSNombre == nodo.GSNombre)
                {
                    ListaAristas.RemoveAt(i);
                    this.eliminaNodo(nodo, false);
                    break;
                }
                i++;
            }
            i = 0;
            if (band == false)
            {
                foreach (CNodo nod in this.ListaNodos)
                {
                    if (nod.GSNombre == nodo.GSNombre)
                    {
                        ListaNodos.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
        }

        public void eliminaNodoLogic(CNodo nodo, bool band)
        {
            int i = 0;

            nodo.visible = false;
            /*
            foreach (CArista ari in this.ListaAristas)
            {
                if (ari.GSOrigen.GSNombre == nodo.GSNombre || ari.GSDestino.GSNombre == nodo.GSNombre)
                {
                    ListaAristas.RemoveAt(i);
                    this.eliminaNodo(nodo, false);
                    break;
                }
                i++;
            }
            i = 0;
            if (band == false)
            {
                foreach (CNodo nod in this.ListaNodos)
                {
                    if (nod.GSNombre == nodo.GSNombre)
                    {
                        ListaNodos.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            */
        }

        public void generaMatrizCostos()
        {
            int i, j;
            mCostos = new double[ListaNodos.Count, ListaNodos.Count];

            for (i = 0; i < ListaNodos.Count; i++)
            {
                for (j = 0; j < ListaNodos.Count; j++)
                {
                    mCostos[i, j] = double.PositiveInfinity;
                    //---------------------------------------------------------------------------------
                }
            }

            foreach (CArista ari in ListaAristas)
            {
                if (ari != null)
                {
                    if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                    {
                        if (this.dirigido == true)
                        {
                            if (ari.GSDiri == true)
                            {
                                //------------
                                mCostos[ListaNodos.IndexOf(ari.GSOrigen), ListaNodos.IndexOf(ari.GSDestino)] = ari.GSPeso;
                                //------------
                            }
                            else
                            {
                                mCostos[ListaNodos.IndexOf(ari.GSOrigen), ListaNodos.IndexOf(ari.GSDestino)] = ari.GSPeso;
                                mCostos[ListaNodos.IndexOf(ari.GSDestino), ListaNodos.IndexOf(ari.GSOrigen)] = ari.GSPeso;
                            }
                        }
                        else
                        {
                            mCostos[ListaNodos.IndexOf(ari.GSOrigen), ListaNodos.IndexOf(ari.GSDestino)] = ari.GSPeso;
                            mCostos[ListaNodos.IndexOf(ari.GSDestino), ListaNodos.IndexOf(ari.GSOrigen)] = ari.GSPeso;
                        }
                    }
                }
            }

            for (i = 0; i < ListaNodos.Count; i++)
            {
                mCostos[i, i] = 0;
            }
        }

        public void generaMatriz()
        {
            int i, j;
            int contR, ContC;
            matriz = new int[ListaNodos.Count, ListaNodos.Count];

            for (i = 0; i < ListaNodos.Count; i++)
            {
                for (j = 0; j < ListaNodos.Count; j++)
                {
                    matriz[i,j] = 0;
                }
            }


            foreach (CArista ari in ListaAristas)
            {
                contR = 0;
                ContC = 0;
                if (ari != null)
                {
                    if(ari.GSOrigen.visible==true&&ari.GSDestino.visible==true)
                    {
                        if (this.dirigido == true)
                        {
                            if (ari.GSDiri == true)
                            {
                                foreach (CNodo nod in ListaNodos)
                                {
                                    if (ari.GSOrigen.GSNombre == nod.GSNombre)
                                    {
                                        break;
                                    }
                                    contR++;
                                }
                                foreach (CNodo nod in ListaNodos)
                                {
                                    if (ari.GSDestino.GSNombre == nod.GSNombre)
                                    {
                                        break;
                                    }
                                    ContC++;
                                }
                                matriz[contR, ContC] = 1;
                            }
                            else
                            {
                                foreach (CNodo nod in ListaNodos)
                                {
                                    if (ari.GSOrigen.GSNombre == nod.GSNombre)
                                    {
                                        break;
                                    }
                                    contR++;
                                }
                                foreach (CNodo nod in ListaNodos)
                                {
                                    if (ari.GSDestino.GSNombre == nod.GSNombre)
                                    {
                                        break;
                                    }
                                    ContC++;
                                }
                                matriz[ContC, contR] = 1;
                                matriz[contR, ContC] = 1;
                            }
                        }
                        else
                        {
                            foreach (CNodo nod in ListaNodos)
                            {
                                if (ari.GSOrigen.GSNombre == nod.GSNombre)
                                {
                                    break;
                                }
                                contR++;
                            }
                            foreach (CNodo nod in ListaNodos)
                            {
                                if (ari.GSDestino.GSNombre == nod.GSNombre)
                                {
                                    break;
                                }
                                ContC++;
                            }
                            matriz[ContC, contR] = 1;
                            matriz[contR, ContC] = 1;
                        }
                    }
                }
            }
        }

        public void generaGrado()
        {
            foreach (CNodo nod in ListaNodos)
            {
                nod.GSGrado = 0;
            }

            if (this.dirigido == true)
            {
                foreach (CArista ari in ListaAristas)
                {
                    if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                    {
                        ari.GSOrigen.incrementaGrado();
                        if (ari.GSDiri == false)
                        {
                            ari.GSDestino.incrementaGrado();
                        }
                    }
                }
            }
            else
            {
                foreach (CArista ari in ListaAristas)
                {
                    if (ari.GSOrigen.visible == true && ari.GSDestino.visible == true)
                    {
                        ari.GSOrigen.incrementaGrado();
                        ari.GSDestino.incrementaGrado();
                    }
                }
            }
        }

        public void pintaCamino(List<int> camino, int ult)
        {
            CArista ari;
            /*
            foreach (var item in camino)
            {
                this.ListaNodos.ElementAt<CNodo>(item).GSColor = Color.Aqua;
            }
            */
            for (int i = 0; i <= ult; i++)
            {
                this.ListaNodos.ElementAt<CNodo>(camino[i]).GSColor = Color.Aqua;
            }
            int del = 0;
            while (del < 100000000)
                del++;
            for (int i = 1; i <= ult; i++)
            {
                ari=new CArista(this.ListaNodos.ElementAt<CNodo>(camino[i - 1]), this.ListaNodos.ElementAt<CNodo>(camino[i]));
                //this.ListaAristas.ElementAt<CArista>( ListaAristas.IndexOf<CArista>(ari) ).GSColor = Color.Aqua;
                foreach (var item in ListaAristas)
                {
                    if((item.GSOrigen==ari.GSOrigen && item.GSDestino==ari.GSDestino) || (item.GSOrigen == ari.GSDestino && item.GSDestino == ari.GSOrigen))
                    {
                        item.GSColor = Color.Aqua;
                        break;
                    }
                }
            }
        }

        public void pintaCamino(List<CArista> camino, int ult)
        {
            /*
            foreach (var item in camino)
            {
                this.ListaNodos.ElementAt<CNodo>(item).GSColor = Color.Aqua;
            }
            */

            foreach (var ari in camino)
            {
                ari.GSOrigen.GSColor = Color.Aqua;
                ari.GSDestino.GSColor = Color.Aqua;
                ari.GSColor = Color.Aqua;
                for (double del = 0; del < 100000000; del++) ;
            }
        }

        public void AdyacenciaLista()
        {
            generaMatriz();
            foreach (var item in ListaNodos)
            {
                item.ARel = new List<CArista>();
            }

            for (int i = 0; i < ListaNodos.Count; i++)
            {
                for (int j = 0; j < ListaNodos.Count; j++)
                {
                    if(matriz[i,j]==1)
                    {
                        ListaNodos[i].ARel.Add(new CArista(ListaNodos[i], ListaNodos[j]));
                    }
                }
            }

        }

        public bool hayCiclo(List<CArista> la, CNodo Origen)
        {
            bool ciclo = false;
            CNodo Prev, Actual;

            if (Origen == la[0].GSOrigen)
            {
                Actual = Origen;
                Prev = null;
            }
            else
            {
                Actual = la[0].GSDestino;
                Prev = null;
            }

            foreach (CArista ax in la)
            {
                if (ax.GSOrigen == Actual)
                {
                    Actual = ax.GSDestino;
                    Prev = ax.GSOrigen;
                }
                else
                {
                    Prev = ax.GSDestino;
                    Actual = ax.GSOrigen;
                }
            }

            if (Actual == Origen)
            {
                ciclo = true;
            }

            return ciclo;
        }

        public void pintaArbol(List<List<int>> arbol)
        {
            for (int i = 0; i < arbol.Count; i++)
            {
                for (int j = 0; j < arbol[i].Count; j++)
                {
                    this.ListaNodos.ElementAt<CNodo>(arbol[i][j]).GSColor = Color.Aqua;
                }
            }
        }

        public void cambiaID(Button b)
        {
            char c = 'A';
            int i = 0;
            if (this.ListaNodos.Count != 0 && this.ListaNodos.ElementAt<CNodo>(0).GSNombre == c.ToString())
            {
                b.Text = "0";
                foreach (CNodo nod in this.ListaNodos)
                {
                    nod.GSNombre = i.ToString();
                    i++;
                }
            }
            else
            {
                c = '0';
                if (this.ListaNodos.Count != 0 && this.ListaNodos.ElementAt<CNodo>(0).GSNombre == c.ToString())
                {
                    c = 'A';
                    b.Text = "A";
                    foreach (CNodo nod in this.ListaNodos)
                    {
                        nod.GSNombre = c.ToString();
                        c++;
                    }
                }
            }
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

        public List<List<int>> MatrizLL()
        {
            List<List<int>> mll = new List<List<int>>();

            generaMatriz();

            for (int i = 0; i < ListaNodos.Count; i++)
            {
                mll.Add(new List<int>());
                for (int j = 0; j < ListaNodos.Count; j++)
                {
                    mll[i].Add(matriz[i, j]);
                }
            }

            string s = "";

            foreach (var ll in mll)
            {
                foreach (var l in ll)
                {
                    s += " " + l;
                }
                s += "\n";
            }
            MessageBox.Show(s);

            return mll;
        }
    }
}
