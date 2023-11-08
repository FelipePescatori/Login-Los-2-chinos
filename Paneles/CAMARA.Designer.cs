namespace Login_Los_2_chinos
{
    partial class CAMARA
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBuscarCodigoBarra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnApagarCamara = new System.Windows.Forms.Button();
            this.btnEncenderCamara = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.TimerCodigoDeBarra = new System.Windows.Forms.Timer(this.components);
            this.timerCamara = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtBuscarCodigoBarra);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnApagarCamara);
            this.panel1.Controls.Add(this.btnEncenderCamara);
            this.panel1.Location = new System.Drawing.Point(27, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 296);
            this.panel1.TabIndex = 0;
            // 
            // txtBuscarCodigoBarra
            // 
            this.txtBuscarCodigoBarra.Location = new System.Drawing.Point(69, 91);
            this.txtBuscarCodigoBarra.Name = "txtBuscarCodigoBarra";
            this.txtBuscarCodigoBarra.Size = new System.Drawing.Size(149, 22);
            this.txtBuscarCodigoBarra.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(557, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "CAMARA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(337, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 223);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnApagarCamara
            // 
            this.btnApagarCamara.Location = new System.Drawing.Point(38, 236);
            this.btnApagarCamara.Name = "btnApagarCamara";
            this.btnApagarCamara.Size = new System.Drawing.Size(87, 40);
            this.btnApagarCamara.TabIndex = 2;
            this.btnApagarCamara.Text = "Apagar";
            this.btnApagarCamara.UseVisualStyleBackColor = true;
            this.btnApagarCamara.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEncenderCamara
            // 
            this.btnEncenderCamara.Location = new System.Drawing.Point(189, 236);
            this.btnEncenderCamara.Name = "btnEncenderCamara";
            this.btnEncenderCamara.Size = new System.Drawing.Size(93, 44);
            this.btnEncenderCamara.TabIndex = 1;
            this.btnEncenderCamara.Text = "Encender";
            this.btnEncenderCamara.UseVisualStyleBackColor = true;
            this.btnEncenderCamara.Click += new System.EventHandler(this.btnEncender_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSalir);
            this.panel2.Location = new System.Drawing.Point(27, 333);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(937, 164);
            this.panel2.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(101, 56);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(103, 54);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // TimerCodigoDeBarra
            // 
            this.TimerCodigoDeBarra.Interval = 40;
            this.TimerCodigoDeBarra.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerCamara
            // 
            this.timerCamara.Tick += new System.EventHandler(this.timerCamara_Tick);
            // 
            // CAMARA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 543);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CAMARA";
            this.Text = "CAMARA";
            this.Load += new System.EventHandler(this.CAMARA_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnApagarCamara;
        private System.Windows.Forms.Button btnEncenderCamara;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Timer TimerCodigoDeBarra;
        private System.Windows.Forms.TextBox txtBuscarCodigoBarra;
        private System.Windows.Forms.Timer timerCamara;
    }
}