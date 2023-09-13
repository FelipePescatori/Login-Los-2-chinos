namespace Login_Los_2_chinos
{
    partial class FromModificar
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
            this.dtgAdminArticulos = new System.Windows.Forms.DataGridView();
            this.btnVer = new System.Windows.Forms.Button();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtArticuloid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.txtCompra = new System.Windows.Forms.TextBox();
            this.txtVenta = new System.Windows.Forms.TextBox();
            this.txtPresentacion = new System.Windows.Forms.TextBox();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdminArticulos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgAdminArticulos
            // 
            this.dtgAdminArticulos.BackgroundColor = System.Drawing.Color.White;
            this.dtgAdminArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAdminArticulos.Location = new System.Drawing.Point(421, 111);
            this.dtgAdminArticulos.Name = "dtgAdminArticulos";
            this.dtgAdminArticulos.RowHeadersWidth = 51;
            this.dtgAdminArticulos.RowTemplate.Height = 24;
            this.dtgAdminArticulos.Size = new System.Drawing.Size(742, 161);
            this.dtgAdminArticulos.TabIndex = 0;
            this.dtgAdminArticulos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAdminArticulos_CellClick);
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(874, 296);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(206, 48);
            this.btnVer.TabIndex = 1;
            this.btnVer.Text = "VER";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Unispace", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.Location = new System.Drawing.Point(51, 92);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(63, 15);
            this.lblDetalle.TabIndex = 2;
            this.lblDetalle.Text = "Detalle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Unispace", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Presentacion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Unispace", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Precio Venta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Unispace", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Precio Compra";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtArticuloid);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnModificar);
            this.panel1.Controls.Add(this.txtCompra);
            this.panel1.Controls.Add(this.txtVenta);
            this.panel1.Controls.Add(this.txtPresentacion);
            this.panel1.Controls.Add(this.txtDetalle);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblDetalle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 452);
            this.panel1.TabIndex = 6;
            // 
            // txtArticuloid
            // 
            this.txtArticuloid.Location = new System.Drawing.Point(125, 48);
            this.txtArticuloid.Name = "txtArticuloid";
            this.txtArticuloid.Size = new System.Drawing.Size(212, 22);
            this.txtArticuloid.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Unispace", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "ArticuloID";
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(125, 262);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(124, 38);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // txtCompra
            // 
            this.txtCompra.Location = new System.Drawing.Point(125, 196);
            this.txtCompra.Name = "txtCompra";
            this.txtCompra.Size = new System.Drawing.Size(212, 22);
            this.txtCompra.TabIndex = 9;
            // 
            // txtVenta
            // 
            this.txtVenta.Location = new System.Drawing.Point(125, 159);
            this.txtVenta.Name = "txtVenta";
            this.txtVenta.Size = new System.Drawing.Size(212, 22);
            this.txtVenta.TabIndex = 8;
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Location = new System.Drawing.Point(125, 122);
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(212, 22);
            this.txtPresentacion.TabIndex = 7;
            // 
            // txtDetalle
            // 
            this.txtDetalle.Location = new System.Drawing.Point(126, 85);
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.Size = new System.Drawing.Size(212, 22);
            this.txtDetalle.TabIndex = 6;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(538, 358);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(206, 48);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FromModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 450);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.dtgAdminArticulos);
            this.Name = "FromModificar";
            this.Text = "FormAdministrarArticulos";
            this.Load += new System.EventHandler(this.AdmArticulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdminArticulos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgAdminArticulos;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.TextBox txtCompra;
        private System.Windows.Forms.TextBox txtVenta;
        private System.Windows.Forms.TextBox txtPresentacion;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.TextBox txtArticuloid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalir;
    }
}