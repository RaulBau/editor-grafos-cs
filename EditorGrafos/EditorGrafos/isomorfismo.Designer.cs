namespace EditorGrafos
{
    partial class isomorfismo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.insertarPeso = new System.Windows.Forms.Button();
            this.Peso = new System.Windows.Forms.Button();
            this.cambiaSent = new System.Windows.Forms.Button();
            this.matriz = new System.Windows.Forms.Button();
            this.Dirigido = new System.Windows.Forms.Button();
            this.cambio = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.archivo = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarGrafo = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirGrafo = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertarAristaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moverNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarAristaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.insertarPeso);
            this.groupBox1.Controls.Add(this.Peso);
            this.groupBox1.Controls.Add(this.cambiaSent);
            this.groupBox1.Controls.Add(this.matriz);
            this.groupBox1.Controls.Add(this.Dirigido);
            this.groupBox1.Controls.Add(this.cambio);
            this.groupBox1.Location = new System.Drawing.Point(0, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 375);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // insertarPeso
            // 
            this.insertarPeso.AccessibleDescription = "Insertar Peso";
            this.insertarPeso.Location = new System.Drawing.Point(44, 106);
            this.insertarPeso.Name = "insertarPeso";
            this.insertarPeso.Size = new System.Drawing.Size(37, 23);
            this.insertarPeso.TabIndex = 6;
            this.insertarPeso.Text = "Ins.";
            this.insertarPeso.UseVisualStyleBackColor = true;
            this.insertarPeso.Visible = false;
            this.insertarPeso.Click += new System.EventHandler(this.insertarPeso_Click);
            // 
            // Peso
            // 
            this.Peso.Location = new System.Drawing.Point(6, 106);
            this.Peso.Name = "Peso";
            this.Peso.Size = new System.Drawing.Size(75, 23);
            this.Peso.TabIndex = 8;
            this.Peso.Text = "Peso";
            this.Peso.UseVisualStyleBackColor = true;
            this.Peso.Click += new System.EventHandler(this.Peso_Click);
            // 
            // cambiaSent
            // 
            this.cambiaSent.Location = new System.Drawing.Point(44, 48);
            this.cambiaSent.Name = "cambiaSent";
            this.cambiaSent.Size = new System.Drawing.Size(37, 23);
            this.cambiaSent.TabIndex = 6;
            this.cambiaSent.Text = "Cambia";
            this.cambiaSent.UseVisualStyleBackColor = true;
            this.cambiaSent.Visible = false;
            this.cambiaSent.Click += new System.EventHandler(this.cambiaSent_Click);
            // 
            // matriz
            // 
            this.matriz.Location = new System.Drawing.Point(6, 77);
            this.matriz.Name = "matriz";
            this.matriz.Size = new System.Drawing.Size(75, 23);
            this.matriz.TabIndex = 7;
            this.matriz.Text = "Matriz";
            this.matriz.UseVisualStyleBackColor = true;
            this.matriz.Click += new System.EventHandler(this.matriz_Click);
            // 
            // Dirigido
            // 
            this.Dirigido.Location = new System.Drawing.Point(6, 48);
            this.Dirigido.Name = "Dirigido";
            this.Dirigido.Size = new System.Drawing.Size(75, 23);
            this.Dirigido.TabIndex = 6;
            this.Dirigido.Text = "Dirigido";
            this.Dirigido.UseVisualStyleBackColor = true;
            this.Dirigido.Click += new System.EventHandler(this.Dirigido_Click);
            // 
            // cambio
            // 
            this.cambio.Location = new System.Drawing.Point(6, 19);
            this.cambio.Name = "cambio";
            this.cambio.Size = new System.Drawing.Size(75, 23);
            this.cambio.TabIndex = 0;
            this.cambio.Text = "A";
            this.cambio.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivo,
            this.insertarToolStripMenuItem,
            this.accionesToolStripMenuItem});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // archivo
            // 
            this.archivo.AccessibleName = "archivo";
            this.archivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guardarGrafo,
            this.abrirGrafo});
            this.archivo.Name = "archivo";
            this.archivo.Size = new System.Drawing.Size(60, 25);
            this.archivo.Text = "Archivo";
            this.archivo.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.archivo_Click);
            // 
            // guardarGrafo
            // 
            this.guardarGrafo.AccessibleName = "guardarGrafo";
            this.guardarGrafo.Name = "guardarGrafo";
            this.guardarGrafo.Size = new System.Drawing.Size(180, 22);
            this.guardarGrafo.Text = "Guardar Grafo";
            // 
            // abrirGrafo
            // 
            this.abrirGrafo.AccessibleName = "abrirGrafo";
            this.abrirGrafo.Name = "abrirGrafo";
            this.abrirGrafo.Size = new System.Drawing.Size(180, 22);
            this.abrirGrafo.Text = "Abrir Grafo";
            // 
            // insertarToolStripMenuItem
            // 
            this.insertarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertarNodoToolStripMenuItem,
            this.insertarAristaToolStripMenuItem});
            this.insertarToolStripMenuItem.Name = "insertarToolStripMenuItem";
            this.insertarToolStripMenuItem.Size = new System.Drawing.Size(58, 25);
            this.insertarToolStripMenuItem.Text = "Insertar";
            this.insertarToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Insertar_Click);
            // 
            // insertarNodoToolStripMenuItem
            // 
            this.insertarNodoToolStripMenuItem.AccessibleName = "Nodo";
            this.insertarNodoToolStripMenuItem.Name = "insertarNodoToolStripMenuItem";
            this.insertarNodoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insertarNodoToolStripMenuItem.Text = "Insertar Nodo";
            // 
            // insertarAristaToolStripMenuItem
            // 
            this.insertarAristaToolStripMenuItem.AccessibleName = "Arista";
            this.insertarAristaToolStripMenuItem.Name = "insertarAristaToolStripMenuItem";
            this.insertarAristaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insertarAristaToolStripMenuItem.Text = "Insertar Arista";
            // 
            // accionesToolStripMenuItem
            // 
            this.accionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moverNodoToolStripMenuItem,
            this.borrarNodoToolStripMenuItem,
            this.borrarAristaToolStripMenuItem});
            this.accionesToolStripMenuItem.Name = "accionesToolStripMenuItem";
            this.accionesToolStripMenuItem.Size = new System.Drawing.Size(67, 25);
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
            // isomorfismo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 434);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "isomorfismo";
            this.Text = "isomorfismo";
            this.Load += new System.EventHandler(this.isomorfismo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.isomorfismo_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.isomorfismo_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.isomorfismo_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.isomorfismo_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cambio;
        private System.Windows.Forms.Button Dirigido;
        private System.Windows.Forms.Button cambiaSent;
        private System.Windows.Forms.Button matriz;
        private System.Windows.Forms.Button Peso;
        private System.Windows.Forms.Button insertarPeso;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivo;
        private System.Windows.Forms.ToolStripMenuItem guardarGrafo;
        private System.Windows.Forms.ToolStripMenuItem abrirGrafo;
        private System.Windows.Forms.ToolStripMenuItem insertarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertarAristaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moverNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarAristaToolStripMenuItem;
    }
}