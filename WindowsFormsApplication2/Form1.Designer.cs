namespace WindowsFormsApplication2
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.sekunde = new System.Windows.Forms.Timer(this.components);
            this.senderzeigen = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.tveingabe = new System.Windows.Forms.Timer(this.components);
            this.uhreingabe = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.radioeingabe = new System.Windows.Forms.Timer(this.components);
            this.Rsenderzeigen = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // sekunde
            // 
            this.sekunde.Enabled = true;
            this.sekunde.Interval = 1000;
            this.sekunde.Tick += new System.EventHandler(this.sekunde_Tick);
            // 
            // senderzeigen
            // 
            this.senderzeigen.Interval = 5000;
            this.senderzeigen.Tick += new System.EventHandler(this.senderzeigen_Tick);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.MidnightBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("!PaulMaul", 70F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Orange;
            this.label6.Location = new System.Drawing.Point(12, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 119);
            this.label6.TabIndex = 5;
            this.label6.Text = "VIVA";
            this.label6.Visible = false;
            // 
            // tveingabe
            // 
            this.tveingabe.Interval = 4000;
            this.tveingabe.Tick += new System.EventHandler(this.tveingabe_Tick);
            // 
            // uhreingabe
            // 
            this.uhreingabe.Interval = 4000;
            this.uhreingabe.Tick += new System.EventHandler(this.uhreingabe_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 1500;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // radioeingabe
            // 
            this.radioeingabe.Interval = 4000;
            this.radioeingabe.Tick += new System.EventHandler(this.radioeingabe_Tick);
            // 
            // Rsenderzeigen
            // 
            this.Rsenderzeigen.Interval = 5000;
            this.Rsenderzeigen.Tick += new System.EventHandler(this.Rsenderanzeigen_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(796, 507);
            this.Controls.Add(this.label6);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer sekunde;
        private System.Windows.Forms.Timer senderzeigen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer tveingabe;
        private System.Windows.Forms.Timer uhreingabe;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer radioeingabe;
        private System.Windows.Forms.Timer Rsenderzeigen;

    }
}

