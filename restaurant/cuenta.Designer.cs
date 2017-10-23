namespace restaurant
{
    partial class cuenta
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelDescripcion = new MaterialSkin.Controls.MaterialLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelNombre = new MaterialSkin.Controls.MaterialLabel();
            this.labelPrecio = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 90);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(453, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Depth = 0;
            this.btnAgregar.Location = new System.Drawing.Point(65, 166);
            this.btnAgregar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Primary = true;
            this.btnAgregar.Size = new System.Drawing.Size(145, 39);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // labelDescripcion
            // 
            this.labelDescripcion.AutoSize = true;
            this.labelDescripcion.Depth = 0;
            this.labelDescripcion.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDescripcion.Location = new System.Drawing.Point(28, 88);
            this.labelDescripcion.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDescripcion.Name = "labelDescripcion";
            this.labelDescripcion.Size = new System.Drawing.Size(89, 19);
            this.labelDescripcion.TabIndex = 2;
            this.labelDescripcion.Text = "Descripcion";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelPrecio);
            this.panel1.Controls.Add(this.labelNombre);
            this.panel1.Controls.Add(this.btnAgregar);
            this.panel1.Controls.Add(this.labelDescripcion);
            this.panel1.Location = new System.Drawing.Point(490, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 222);
            this.panel1.TabIndex = 23;
            this.panel1.Visible = false;
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(12, 428);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(766, 11);
            this.materialDivider1.TabIndex = 24;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 465);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(462, 144);
            this.dataGridView2.TabIndex = 25;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(535, 518);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(175, 39);
            this.btnCerrar.TabIndex = 26;
            this.btnCerrar.Text = "Cerrar cuenta";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Depth = 0;
            this.labelNombre.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelNombre.Location = new System.Drawing.Point(17, 20);
            this.labelNombre.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(118, 19);
            this.labelNombre.TabIndex = 3;
            this.labelNombre.Text = "nombre de plato";
            // 
            // labelPrecio
            // 
            this.labelPrecio.AutoSize = true;
            this.labelPrecio.Depth = 0;
            this.labelPrecio.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPrecio.Location = new System.Drawing.Point(184, 20);
            this.labelPrecio.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelPrecio.Name = "labelPrecio";
            this.labelPrecio.Size = new System.Drawing.Size(51, 19);
            this.labelPrecio.TabIndex = 4;
            this.labelPrecio.Text = "precio";
            // 
            // cuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 638);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "cuenta";
            this.Text = "cuenta";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MaterialSkin.Controls.MaterialRaisedButton btnAgregar;
        private MaterialSkin.Controls.MaterialLabel labelDescripcion;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private MaterialSkin.Controls.MaterialLabel labelPrecio;
        private MaterialSkin.Controls.MaterialLabel labelNombre;
    }
}