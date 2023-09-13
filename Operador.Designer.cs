namespace Login_Los_2_chinos
{
    partial class Operador
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
            this.lblMensajeOperador = new System.Windows.Forms.Label();
            this.lblOperador = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMensajeOperador
            // 
            this.lblMensajeOperador.AutoSize = true;
            this.lblMensajeOperador.BackColor = System.Drawing.SystemColors.Control;
            this.lblMensajeOperador.Location = new System.Drawing.Point(228, 121);
            this.lblMensajeOperador.Name = "lblMensajeOperador";
            this.lblMensajeOperador.Size = new System.Drawing.Size(0, 16);
            this.lblMensajeOperador.TabIndex = 0;
            // 
            // lblOperador
            // 
            this.lblOperador.AutoSize = true;
            this.lblOperador.Location = new System.Drawing.Point(231, 293);
            this.lblOperador.Name = "lblOperador";
            this.lblOperador.Size = new System.Drawing.Size(84, 16);
            this.lblOperador.TabIndex = 1;
            this.lblOperador.Text = "OPERADOR";
            // 
            // Operador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblOperador);
            this.Controls.Add(this.lblMensajeOperador);
            this.Name = "Operador";
            this.Text = "Operador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMensajeOperador;
        private System.Windows.Forms.Label lblOperador;
    }
}