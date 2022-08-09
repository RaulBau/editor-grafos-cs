namespace EditorGrafos
{
    partial class Matriz
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
            this.aceptar = new System.Windows.Forms.Button();
            this.informacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // aceptar
            // 
            this.aceptar.AccessibleName = "Aceptar";
            this.aceptar.Location = new System.Drawing.Point(147, 176);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(75, 24);
            this.aceptar.TabIndex = 0;
            this.aceptar.Text = "Aceptar";
            this.aceptar.UseVisualStyleBackColor = true;
            this.aceptar.Click += new System.EventHandler(this.aceptar_Click);
            // 
            // informacion
            // 
            this.informacion.AccessibleName = "informacion";
            this.informacion.Location = new System.Drawing.Point(199, 12);
            this.informacion.Name = "informacion";
            this.informacion.Size = new System.Drawing.Size(20, 20);
            this.informacion.TabIndex = 1;
            this.informacion.Text = "i";
            this.informacion.UseVisualStyleBackColor = true;
            this.informacion.Click += new System.EventHandler(this.informacion_Click);
            // 
            // Matriz
            // 
            this.AcceptButton = this.aceptar;
            this.AccessibleName = "matrizForma";
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(234, 211);
            this.Controls.Add(this.informacion);
            this.Controls.Add(this.aceptar);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Matriz";
            this.Text = "Matriz";
            this.Load += new System.EventHandler(this.Matriz_Load);
            this.Shown += new System.EventHandler(this.Matriz_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Matriz_Paint);
            this.Resize += new System.EventHandler(this.Matriz_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button informacion;
        public System.Windows.Forms.Button aceptar;
    }
}