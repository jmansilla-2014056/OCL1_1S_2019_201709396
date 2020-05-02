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
            this.buttonG = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboER
            // 
            this.comboER.FormattingEnabled = true;
            this.comboER.Location = new System.Drawing.Point(24, 29);
            this.comboER.Name = "comboER";
            this.comboER.Size = new System.Drawing.Size(155, 21);
            this.comboER.TabIndex = 0;
            this.comboER.SelectedIndexChanged += new System.EventHandler(this.comboER_SelectedIndexChanged);
            // 
            // buttonG
            // 
            this.buttonG.Location = new System.Drawing.Point(202, 12);
            this.buttonG.Name = "buttonG";
            this.buttonG.Size = new System.Drawing.Size(173, 52);
            this.buttonG.TabIndex = 3;
            this.buttonG.Text = "Graficar AFN";
            this.buttonG.UseVisualStyleBackColor = true;
            this.buttonG.Click += new System.EventHandler(this.buttonG_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(381, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 52);
            this.button1.TabIndex = 4;
            this.button1.Text = "Graficar AFD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(24, 388);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(888, 182);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(560, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 52);
            this.button2.TabIndex = 7;
            this.button2.Text = "Graficar Tabla";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(24, 70);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(888, 300);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(739, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 52);
            this.button3.TabIndex = 9;
            this.button3.Text = "Validar Lexemas";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(950, 582);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonG);
            this.Controls.Add(this.comboER);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboER;
        private System.Windows.Forms.Button buttonG;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button3;
    }
}