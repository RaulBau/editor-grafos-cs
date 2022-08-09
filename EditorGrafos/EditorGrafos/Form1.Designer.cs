namespace EditorGrafos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivo = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarGrafo = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirGrafo = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarAristaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caminosCircuitos = new System.Windows.Forms.ToolStripMenuItem();
            this.simplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hamiltonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fleuryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hierholzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grafosPlanos = new System.Windows.Forms.ToolStripMenuItem();
            this.grafosColoreadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kuraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caminoMasCortoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dijkstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floydToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arboles = new System.Windows.Forms.ToolStripMenuItem();
            this.primToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kruskalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warshallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isomorfismoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isomorfismoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GuardarGrafoB = new System.Windows.Forms.ToolStripButton();
            this.AbrirGrafoB = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.InsertarNodo = new System.Windows.Forms.ToolStripButton();
            this.InsertarArista = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.MoverNodoB = new System.Windows.Forms.ToolStripButton();
            this.BorrarNodoB = new System.Windows.Forms.ToolStripButton();
            this.BorrarAristaB = new System.Windows.Forms.ToolStripButton();
            this.cambio = new System.Windows.Forms.Button();
            this.Dirigido = new System.Windows.Forms.Button();
            this.matriz = new System.Windows.Forms.Button();
            this.Peso = new System.Windows.Forms.Button();
            this.CoordenadasNodo = new System.Windows.Forms.Button();
            this.insertarPeso = new System.Windows.Forms.Button();
            this.cambiaSent = new System.Windows.Forms.Button();
            this.gradoB = new System.Windows.Forms.Button();
            this.ListaAdB = new System.Windows.Forms.Button();
            this.isomorfB = new System.Windows.Forms.Button();
            this.buscar = new System.Windows.Forms.Button();
            this.siguiente = new System.Windows.Forms.Button();
            this.anterior = new System.Windows.Forms.Button();
            this.finalizar = new System.Windows.Forms.Button();
            this.FuerzaBr = new System.Windows.Forms.Button();
            this.Botello = new System.Windows.Forms.Button();
            this.permutaciones = new System.Windows.Forms.Button();
            this.accionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moverNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarAristaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "grafo";
            this.saveFileDialog1.Filter = "\"Archivos Grafo|*.grafo\"";
            this.saveFileDialog1.Title = "Guardar Grafo";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "grafo";
            this.openFileDialog1.Filter = "\"Archivos Grafo|*.grafo\"";
            this.openFileDialog1.Title = "Abrir Grafo";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivo,
            this.insertarToolStripMenuItem,
            this.accionesToolStripMenuItem,
            this.caminosCircuitos,
            this.grafosPlanos,
            this.caminoMasCortoToolStripMenuItem,
            this.arboles,
            this.isomorfismoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivo
            // 
            this.archivo.AccessibleName = "archivo";
            this.archivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guardarGrafo,
            this.abrirGrafo});
            this.archivo.Name = "archivo";
            this.archivo.Size = new System.Drawing.Size(60, 20);
            this.archivo.Text = "Archivo";
            this.archivo.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.archivo_Click);
            // 
            // guardarGrafo
            // 
            this.guardarGrafo.AccessibleName = "guardarGrafo";
            this.guardarGrafo.Name = "guardarGrafo";
            this.guardarGrafo.Size = new System.Drawing.Size(148, 22);
            this.guardarGrafo.Text = "Guardar Grafo";
            // 
            // abrirGrafo
            // 
            this.abrirGrafo.AccessibleName = "abrirGrafo";
            this.abrirGrafo.Name = "abrirGrafo";
            this.abrirGrafo.Size = new System.Drawing.Size(148, 22);
            this.abrirGrafo.Text = "Abrir Grafo";
            // 
            // insertarToolStripMenuItem
            // 
            this.insertarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertarNodoToolStripMenuItem,
            this.insertarAristaToolStripMenuItem});
            this.insertarToolStripMenuItem.Name = "insertarToolStripMenuItem";
            this.insertarToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.insertarToolStripMenuItem.Text = "Insertar";
            this.insertarToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Insertar_Click);
            // 
            // insertarNodoToolStripMenuItem
            // 
            this.insertarNodoToolStripMenuItem.AccessibleName = "Nodo";
            this.insertarNodoToolStripMenuItem.Name = "insertarNodoToolStripMenuItem";
            this.insertarNodoToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.insertarNodoToolStripMenuItem.Text = "Insertar Nodo";
            // 
            // insertarAristaToolStripMenuItem
            // 
            this.insertarAristaToolStripMenuItem.AccessibleName = "Arista";
            this.insertarAristaToolStripMenuItem.Name = "insertarAristaToolStripMenuItem";
            this.insertarAristaToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.insertarAristaToolStripMenuItem.Text = "Insertar Arista";
            // 
            // caminosCircuitos
            // 
            this.caminosCircuitos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simplesToolStripMenuItem,
            this.hamiltonToolStripMenuItem,
            this.eulerToolStripMenuItem});
            this.caminosCircuitos.Name = "caminosCircuitos";
            this.caminosCircuitos.Size = new System.Drawing.Size(118, 20);
            this.caminosCircuitos.Text = "Caminos/Circuitos";
            this.caminosCircuitos.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.caminosCircuitos_DropDownItemClicked);
            // 
            // simplesToolStripMenuItem
            // 
            this.simplesToolStripMenuItem.AccessibleName = "Simples";
            this.simplesToolStripMenuItem.Name = "simplesToolStripMenuItem";
            this.simplesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.simplesToolStripMenuItem.Text = "Simples";
            // 
            // hamiltonToolStripMenuItem
            // 
            this.hamiltonToolStripMenuItem.AccessibleName = "Hamilton";
            this.hamiltonToolStripMenuItem.Name = "hamiltonToolStripMenuItem";
            this.hamiltonToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.hamiltonToolStripMenuItem.Text = "Hamilton";
            // 
            // eulerToolStripMenuItem
            // 
            this.eulerToolStripMenuItem.AccessibleName = "Euler";
            this.eulerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fleuryToolStripMenuItem,
            this.hierholzerToolStripMenuItem});
            this.eulerToolStripMenuItem.Name = "eulerToolStripMenuItem";
            this.eulerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eulerToolStripMenuItem.Text = "Euler";
            this.eulerToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.eulerToolStripMenuItem_DropDownItemClicked);
            this.eulerToolStripMenuItem.Click += new System.EventHandler(this.eulerToolStripMenuItem_Click);
            // 
            // fleuryToolStripMenuItem
            // 
            this.fleuryToolStripMenuItem.AccessibleName = "Fleury";
            this.fleuryToolStripMenuItem.Name = "fleuryToolStripMenuItem";
            this.fleuryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fleuryToolStripMenuItem.Text = "Fleury";
            this.fleuryToolStripMenuItem.Visible = false;
            // 
            // hierholzerToolStripMenuItem
            // 
            this.hierholzerToolStripMenuItem.AccessibleName = "Hierholzer";
            this.hierholzerToolStripMenuItem.Name = "hierholzerToolStripMenuItem";
            this.hierholzerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hierholzerToolStripMenuItem.Text = "Hierholzer";
            this.hierholzerToolStripMenuItem.Visible = false;
            // 
            // grafosPlanos
            // 
            this.grafosPlanos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grafosColoreadosToolStripMenuItem,
            this.kuraToolStripMenuItem});
            this.grafosPlanos.Name = "grafosPlanos";
            this.grafosPlanos.Size = new System.Drawing.Size(91, 20);
            this.grafosPlanos.Text = "Grafos Planos";
            this.grafosPlanos.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.grafosPlanos_DropDownItemClicked);
            // 
            // grafosColoreadosToolStripMenuItem
            // 
            this.grafosColoreadosToolStripMenuItem.AccessibleName = "grafosColoreados";
            this.grafosColoreadosToolStripMenuItem.Name = "grafosColoreadosToolStripMenuItem";
            this.grafosColoreadosToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.grafosColoreadosToolStripMenuItem.Text = "Grafos Coloreados";
            // 
            // kuraToolStripMenuItem
            // 
            this.kuraToolStripMenuItem.AccessibleName = "Kura";
            this.kuraToolStripMenuItem.Name = "kuraToolStripMenuItem";
            this.kuraToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.kuraToolStripMenuItem.Text = "Kura";
            // 
            // caminoMasCortoToolStripMenuItem
            // 
            this.caminoMasCortoToolStripMenuItem.AccessibleName = "camMasCorto";
            this.caminoMasCortoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dijkstraToolStripMenuItem,
            this.floydToolStripMenuItem});
            this.caminoMasCortoToolStripMenuItem.Name = "caminoMasCortoToolStripMenuItem";
            this.caminoMasCortoToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.caminoMasCortoToolStripMenuItem.Text = "Camino mas Corto";
            this.caminoMasCortoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.caminoMasCortoToolStripMenuItem_DropDownItemClicked);
            // 
            // dijkstraToolStripMenuItem
            // 
            this.dijkstraToolStripMenuItem.AccessibleName = "D";
            this.dijkstraToolStripMenuItem.Name = "dijkstraToolStripMenuItem";
            this.dijkstraToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.dijkstraToolStripMenuItem.Text = "Dijkstra";
            // 
            // floydToolStripMenuItem
            // 
            this.floydToolStripMenuItem.AccessibleName = "F";
            this.floydToolStripMenuItem.Name = "floydToolStripMenuItem";
            this.floydToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.floydToolStripMenuItem.Text = "Floyd";
            // 
            // arboles
            // 
            this.arboles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primToolStripMenuItem,
            this.kruskalToolStripMenuItem,
            this.warshallToolStripMenuItem});
            this.arboles.Name = "arboles";
            this.arboles.Size = new System.Drawing.Size(59, 20);
            this.arboles.Text = "Arboles";
            this.arboles.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.arboles_click);
            // 
            // primToolStripMenuItem
            // 
            this.primToolStripMenuItem.AccessibleName = "Prim";
            this.primToolStripMenuItem.Name = "primToolStripMenuItem";
            this.primToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.primToolStripMenuItem.Text = "Prim";
            // 
            // kruskalToolStripMenuItem
            // 
            this.kruskalToolStripMenuItem.AccessibleName = "Kruskal";
            this.kruskalToolStripMenuItem.Name = "kruskalToolStripMenuItem";
            this.kruskalToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.kruskalToolStripMenuItem.Text = "Kruskal";
            // 
            // warshallToolStripMenuItem
            // 
            this.warshallToolStripMenuItem.AccessibleName = "Warshall";
            this.warshallToolStripMenuItem.Name = "warshallToolStripMenuItem";
            this.warshallToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.warshallToolStripMenuItem.Text = "Warshall";
            // 
            // isomorfismoToolStripMenuItem
            // 
            this.isomorfismoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isomorfismoToolStripMenuItem1});
            this.isomorfismoToolStripMenuItem.Name = "isomorfismoToolStripMenuItem";
            this.isomorfismoToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.isomorfismoToolStripMenuItem.Text = "Isomorfismo";
            // 
            // isomorfismoToolStripMenuItem1
            // 
            this.isomorfismoToolStripMenuItem1.Name = "isomorfismoToolStripMenuItem1";
            this.isomorfismoToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.isomorfismoToolStripMenuItem1.Text = "Isomorfismo";
            this.isomorfismoToolStripMenuItem1.Click += new System.EventHandler(this.isomorfismoToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GuardarGrafoB,
            this.AbrirGrafoB});
            this.toolStrip1.Location = new System.Drawing.Point(9, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(58, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.archivo_Click);
            // 
            // GuardarGrafoB
            // 
            this.GuardarGrafoB.AccessibleName = "guardarGrafo";
            this.GuardarGrafoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GuardarGrafoB.Image = ((System.Drawing.Image)(resources.GetObject("GuardarGrafoB.Image")));
            this.GuardarGrafoB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GuardarGrafoB.Name = "GuardarGrafoB";
            this.GuardarGrafoB.Size = new System.Drawing.Size(23, 22);
            this.GuardarGrafoB.Text = "Guardar Grafo";
            // 
            // AbrirGrafoB
            // 
            this.AbrirGrafoB.AccessibleName = "abrirGrafo";
            this.AbrirGrafoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AbrirGrafoB.Image = ((System.Drawing.Image)(resources.GetObject("AbrirGrafoB.Image")));
            this.AbrirGrafoB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AbrirGrafoB.Name = "AbrirGrafoB";
            this.AbrirGrafoB.Size = new System.Drawing.Size(23, 22);
            this.AbrirGrafoB.Text = "Abrir Grafo";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsertarNodo,
            this.InsertarArista});
            this.toolStrip2.Location = new System.Drawing.Point(67, 24);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(58, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            this.toolStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Insertar_Click);
            // 
            // InsertarNodo
            // 
            this.InsertarNodo.AccessibleName = "Nodo";
            this.InsertarNodo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertarNodo.Image = ((System.Drawing.Image)(resources.GetObject("InsertarNodo.Image")));
            this.InsertarNodo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertarNodo.Name = "InsertarNodo";
            this.InsertarNodo.Size = new System.Drawing.Size(23, 22);
            this.InsertarNodo.Text = "Insertar Nodo";
            // 
            // InsertarArista
            // 
            this.InsertarArista.AccessibleName = "Arista";
            this.InsertarArista.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertarArista.Image = ((System.Drawing.Image)(resources.GetObject("InsertarArista.Image")));
            this.InsertarArista.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertarArista.Name = "InsertarArista";
            this.InsertarArista.Size = new System.Drawing.Size(23, 22);
            this.InsertarArista.Text = "Insertar Arista";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoverNodoB,
            this.BorrarNodoB,
            this.BorrarAristaB});
            this.toolStrip3.Location = new System.Drawing.Point(125, 24);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(81, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            this.toolStrip3.Visible = false;
            this.toolStrip3.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Acciones_Click);
            // 
            // MoverNodoB
            // 
            this.MoverNodoB.AccessibleName = "MoverNodo";
            this.MoverNodoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoverNodoB.Image = ((System.Drawing.Image)(resources.GetObject("MoverNodoB.Image")));
            this.MoverNodoB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoverNodoB.Name = "MoverNodoB";
            this.MoverNodoB.Size = new System.Drawing.Size(23, 22);
            this.MoverNodoB.Text = "Mover Nodo";
            // 
            // BorrarNodoB
            // 
            this.BorrarNodoB.AccessibleName = "BorrarNodo";
            this.BorrarNodoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BorrarNodoB.Image = ((System.Drawing.Image)(resources.GetObject("BorrarNodoB.Image")));
            this.BorrarNodoB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BorrarNodoB.Name = "BorrarNodoB";
            this.BorrarNodoB.Size = new System.Drawing.Size(23, 22);
            this.BorrarNodoB.Text = "Borrar Nodo";
            // 
            // BorrarAristaB
            // 
            this.BorrarAristaB.AccessibleName = "BorrarArista";
            this.BorrarAristaB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BorrarAristaB.Image = ((System.Drawing.Image)(resources.GetObject("BorrarAristaB.Image")));
            this.BorrarAristaB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BorrarAristaB.Name = "BorrarAristaB";
            this.BorrarAristaB.Size = new System.Drawing.Size(23, 22);
            this.BorrarAristaB.Text = "toolStripButton1";
            this.BorrarAristaB.ToolTipText = "BorrarArista";
            // 
            // cambio
            // 
            this.cambio.AccessibleName = "cambioID";
            this.cambio.Location = new System.Drawing.Point(765, 27);
            this.cambio.Name = "cambio";
            this.cambio.Size = new System.Drawing.Size(23, 23);
            this.cambio.TabIndex = 4;
            this.cambio.Text = "A";
            this.cambio.UseVisualStyleBackColor = true;
            this.cambio.Click += new System.EventHandler(this.cambio_Click);
            // 
            // Dirigido
            // 
            this.Dirigido.Location = new System.Drawing.Point(656, 27);
            this.Dirigido.Name = "Dirigido";
            this.Dirigido.Size = new System.Drawing.Size(52, 23);
            this.Dirigido.TabIndex = 6;
            this.Dirigido.Text = "Dirigido";
            this.Dirigido.UseVisualStyleBackColor = true;
            this.Dirigido.Click += new System.EventHandler(this.Dirigido_Click);
            // 
            // matriz
            // 
            this.matriz.AccessibleName = "creaMatriz";
            this.matriz.Location = new System.Drawing.Point(714, 27);
            this.matriz.Name = "matriz";
            this.matriz.Size = new System.Drawing.Size(45, 23);
            this.matriz.TabIndex = 7;
            this.matriz.Text = "Matriz";
            this.matriz.UseVisualStyleBackColor = true;
            this.matriz.Click += new System.EventHandler(this.matriz_Click);
            // 
            // Peso
            // 
            this.Peso.AccessibleName = "MostrarPeso";
            this.Peso.Location = new System.Drawing.Point(610, 27);
            this.Peso.Name = "Peso";
            this.Peso.Size = new System.Drawing.Size(40, 23);
            this.Peso.TabIndex = 8;
            this.Peso.Text = "Peso";
            this.Peso.UseVisualStyleBackColor = true;
            this.Peso.Click += new System.EventHandler(this.Peso_Click);
            // 
            // CoordenadasNodo
            // 
            this.CoordenadasNodo.AccessibleName = "DatosNodo";
            this.CoordenadasNodo.Location = new System.Drawing.Point(547, 28);
            this.CoordenadasNodo.Name = "CoordenadasNodo";
            this.CoordenadasNodo.Size = new System.Drawing.Size(57, 23);
            this.CoordenadasNodo.TabIndex = 9;
            this.CoordenadasNodo.Text = "Coord. N";
            this.CoordenadasNodo.UseVisualStyleBackColor = true;
            this.CoordenadasNodo.Click += new System.EventHandler(this.CoordenadasNodo_Click);
            // 
            // insertarPeso
            // 
            this.insertarPeso.AccessibleName = "cambiarPeso";
            this.insertarPeso.Location = new System.Drawing.Point(466, 28);
            this.insertarPeso.Name = "insertarPeso";
            this.insertarPeso.Size = new System.Drawing.Size(75, 23);
            this.insertarPeso.TabIndex = 10;
            this.insertarPeso.Text = "Inser. Peso";
            this.insertarPeso.UseVisualStyleBackColor = true;
            this.insertarPeso.Click += new System.EventHandler(this.insertarPeso_Click);
            // 
            // cambiaSent
            // 
            this.cambiaSent.AccessibleName = "cambiaSentido";
            this.cambiaSent.Location = new System.Drawing.Point(656, 56);
            this.cambiaSent.Name = "cambiaSent";
            this.cambiaSent.Size = new System.Drawing.Size(52, 23);
            this.cambiaSent.TabIndex = 11;
            this.cambiaSent.Text = "Cambia";
            this.cambiaSent.UseVisualStyleBackColor = true;
            this.cambiaSent.Visible = false;
            this.cambiaSent.Click += new System.EventHandler(this.cambiaSent_Click);
            // 
            // gradoB
            // 
            this.gradoB.AccessibleName = "grado";
            this.gradoB.Location = new System.Drawing.Point(415, 28);
            this.gradoB.Name = "gradoB";
            this.gradoB.Size = new System.Drawing.Size(45, 23);
            this.gradoB.TabIndex = 12;
            this.gradoB.Text = "Grado";
            this.gradoB.UseVisualStyleBackColor = true;
            this.gradoB.Click += new System.EventHandler(this.gradoB_Click);
            // 
            // ListaAdB
            // 
            this.ListaAdB.AccessibleDescription = "Lista de Adyasencia";
            this.ListaAdB.AccessibleName = "listaAd";
            this.ListaAdB.Location = new System.Drawing.Point(372, 28);
            this.ListaAdB.Name = "ListaAdB";
            this.ListaAdB.Size = new System.Drawing.Size(37, 23);
            this.ListaAdB.TabIndex = 13;
            this.ListaAdB.Text = "Lista";
            this.ListaAdB.UseVisualStyleBackColor = true;
            this.ListaAdB.Click += new System.EventHandler(this.ListaAdB_Click);
            // 
            // isomorfB
            // 
            this.isomorfB.AccessibleName = "isoB";
            this.isomorfB.Location = new System.Drawing.Point(314, 28);
            this.isomorfB.Name = "isomorfB";
            this.isomorfB.Size = new System.Drawing.Size(52, 23);
            this.isomorfB.TabIndex = 14;
            this.isomorfB.Text = "Isomorf.";
            this.isomorfB.UseVisualStyleBackColor = true;
            this.isomorfB.Click += new System.EventHandler(this.isomorfB_Click);
            // 
            // buscar
            // 
            this.buscar.AccessibleName = "buscar";
            this.buscar.Location = new System.Drawing.Point(733, 386);
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(55, 23);
            this.buscar.TabIndex = 16;
            this.buscar.Text = "Buscar";
            this.buscar.UseVisualStyleBackColor = true;
            this.buscar.Visible = false;
            this.buscar.Click += new System.EventHandler(this.buscar_Click);
            // 
            // siguiente
            // 
            this.siguiente.Location = new System.Drawing.Point(713, 415);
            this.siguiente.Name = "siguiente";
            this.siguiente.Size = new System.Drawing.Size(75, 23);
            this.siguiente.TabIndex = 17;
            this.siguiente.Text = "Siguente";
            this.siguiente.UseVisualStyleBackColor = true;
            this.siguiente.Visible = false;
            this.siguiente.Click += new System.EventHandler(this.siguiente_Click);
            // 
            // anterior
            // 
            this.anterior.Location = new System.Drawing.Point(632, 415);
            this.anterior.Name = "anterior";
            this.anterior.Size = new System.Drawing.Size(75, 23);
            this.anterior.TabIndex = 18;
            this.anterior.Text = "Anterior";
            this.anterior.UseVisualStyleBackColor = true;
            this.anterior.Visible = false;
            this.anterior.Click += new System.EventHandler(this.anterior_Click);
            // 
            // finalizar
            // 
            this.finalizar.Location = new System.Drawing.Point(713, 415);
            this.finalizar.Name = "finalizar";
            this.finalizar.Size = new System.Drawing.Size(75, 23);
            this.finalizar.TabIndex = 19;
            this.finalizar.Text = "Finalizar";
            this.finalizar.UseVisualStyleBackColor = true;
            this.finalizar.Visible = false;
            this.finalizar.Click += new System.EventHandler(this.finalizar_Click);
            // 
            // FuerzaBr
            // 
            this.FuerzaBr.Location = new System.Drawing.Point(315, 56);
            this.FuerzaBr.Name = "FuerzaBr";
            this.FuerzaBr.Size = new System.Drawing.Size(51, 23);
            this.FuerzaBr.TabIndex = 20;
            this.FuerzaBr.Text = "Fuerza B";
            this.FuerzaBr.UseVisualStyleBackColor = true;
            this.FuerzaBr.Visible = false;
            this.FuerzaBr.Click += new System.EventHandler(this.FuerzaBr_Click);
            // 
            // Botello
            // 
            this.Botello.Location = new System.Drawing.Point(315, 86);
            this.Botello.Name = "Botello";
            this.Botello.Size = new System.Drawing.Size(51, 23);
            this.Botello.TabIndex = 21;
            this.Botello.Text = "Botello";
            this.Botello.UseVisualStyleBackColor = true;
            this.Botello.Visible = false;
            this.Botello.Click += new System.EventHandler(this.Botello_Click);
            // 
            // permutaciones
            // 
            this.permutaciones.Location = new System.Drawing.Point(315, 116);
            this.permutaciones.Name = "permutaciones";
            this.permutaciones.Size = new System.Drawing.Size(51, 23);
            this.permutaciones.TabIndex = 22;
            this.permutaciones.Text = "Permuta";
            this.permutaciones.UseVisualStyleBackColor = true;
            this.permutaciones.Visible = false;
            this.permutaciones.Click += new System.EventHandler(this.permutaciones_Click);
            // 
            // accionesToolStripMenuItem
            // 
            this.accionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moverNodoToolStripMenuItem,
            this.borrarNodoToolStripMenuItem,
            this.borrarAristaToolStripMenuItem});
            this.accionesToolStripMenuItem.Name = "accionesToolStripMenuItem";
            this.accionesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.accionesToolStripMenuItem.Text = "Acciones";
            this.accionesToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Acciones_Click);
            // 
            // moverNodoToolStripMenuItem
            // 
            this.moverNodoToolStripMenuItem.AccessibleName = "MoverNodo";
            this.moverNodoToolStripMenuItem.Name = "moverNodoToolStripMenuItem";
            this.moverNodoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moverNodoToolStripMenuItem.Text = "Mover Nodo";
            // 
            // borrarNodoToolStripMenuItem
            // 
            this.borrarNodoToolStripMenuItem.AccessibleName = "BorrarNodo";
            this.borrarNodoToolStripMenuItem.Name = "borrarNodoToolStripMenuItem";
            this.borrarNodoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.borrarNodoToolStripMenuItem.Text = "Borrar Nodo";
            // 
            // borrarAristaToolStripMenuItem
            // 
            this.borrarAristaToolStripMenuItem.AccessibleName = "BorrarArista";
            this.borrarAristaToolStripMenuItem.Name = "borrarAristaToolStripMenuItem";
            this.borrarAristaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.borrarAristaToolStripMenuItem.Text = "Borrar Arista";
            // 
            // Form1
            // 
            this.AccessibleName = "Dirigido";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.permutaciones);
            this.Controls.Add(this.Botello);
            this.Controls.Add(this.FuerzaBr);
            this.Controls.Add(this.finalizar);
            this.Controls.Add(this.anterior);
            this.Controls.Add(this.siguiente);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.isomorfB);
            this.Controls.Add(this.ListaAdB);
            this.Controls.Add(this.gradoB);
            this.Controls.Add(this.cambiaSent);
            this.Controls.Add(this.insertarPeso);
            this.Controls.Add(this.CoordenadasNodo);
            this.Controls.Add(this.Peso);
            this.Controls.Add(this.matriz);
            this.Controls.Add(this.Dirigido);
            this.Controls.Add(this.cambio);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Editor de Grafos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivo;
        private System.Windows.Forms.ToolStripMenuItem guardarGrafo;
        private System.Windows.Forms.ToolStripMenuItem abrirGrafo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton GuardarGrafoB;
        private System.Windows.Forms.ToolStripButton AbrirGrafoB;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton InsertarNodo;
        private System.Windows.Forms.ToolStripButton InsertarArista;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton MoverNodoB;
        private System.Windows.Forms.ToolStripButton BorrarNodoB;
        private System.Windows.Forms.Button cambio;
        private System.Windows.Forms.ToolStripMenuItem insertarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertarAristaToolStripMenuItem;
        private System.Windows.Forms.Button Dirigido;
        private System.Windows.Forms.Button matriz;
        private System.Windows.Forms.ToolStripButton BorrarAristaB;
        private System.Windows.Forms.Button Peso;
        private System.Windows.Forms.Button CoordenadasNodo;
        private System.Windows.Forms.Button insertarPeso;
        private System.Windows.Forms.Button cambiaSent;
        private System.Windows.Forms.Button gradoB;
        private System.Windows.Forms.Button ListaAdB;
        private System.Windows.Forms.Button isomorfB;
        private System.Windows.Forms.Button buscar;
        private System.Windows.Forms.Button siguiente;
        private System.Windows.Forms.Button anterior;
        private System.Windows.Forms.Button finalizar;
        private System.Windows.Forms.Button FuerzaBr;
        private System.Windows.Forms.Button Botello;
        private System.Windows.Forms.Button permutaciones;
        private System.Windows.Forms.ToolStripMenuItem caminosCircuitos;
        private System.Windows.Forms.ToolStripMenuItem simplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hamiltonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fleuryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hierholzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grafosPlanos;
        private System.Windows.Forms.ToolStripMenuItem grafosColoreadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kuraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caminoMasCortoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dijkstraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floydToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arboles;
        private System.Windows.Forms.ToolStripMenuItem primToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kruskalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warshallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isomorfismoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isomorfismoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem accionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moverNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarAristaToolStripMenuItem;
    }
}

