namespace Proyecto1_OLC1
{
    partial class Reportes
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
            this.comboER = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonG = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboER
            // 
            this.comboER.FormattingEnabled = true;
            this.comboER.Location = new System.Drawing.Point(93, 76);
            this.comboER.Name = "comboER";
            this.comboER.Size = new System.Drawing.Size(155, 21);
            this.comboER.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(45, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(780, 303);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // buttonG
            // 
            this.buttonG.Location = new System.Drawing.Point(286, 59);
            this.buttonG.Name = "buttonG";
            this.buttonG.Size = new System.Drawing.Size(173, 52);
            this.buttonG.TabIndex = 3;
            this.buttonG.Text = "Graficar AFN";
            this.buttonG.UseVisualStyleBackColor = true;
            this.buttonG.Click += new System.EventHandler(this.buttonG_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(487, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 52);
            this.button1.TabIndex = 4;
            this.button1.Text = "Graficar AFN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonG);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboER);
            this.Name = "Reportes";
            this.Text = "Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboER;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonG;
        private System.Windows.Forms.Button button1;
    }
}