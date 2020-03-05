namespace Proyecto1_OLC1
{
    partial class TextControl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textEntrada = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textEntrada
            // 
            this.textEntrada.AccessibleName = "textEntrada";
            this.textEntrada.Location = new System.Drawing.Point(3, 3);
            this.textEntrada.Name = "textEntrada";
            this.textEntrada.Size = new System.Drawing.Size(517, 406);
            this.textEntrada.TabIndex = 0;
            this.textEntrada.Text = "";
            this.textEntrada.WordWrap = false;
            // 
            // TextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEntrada);
            this.Name = "TextControl";
            this.Size = new System.Drawing.Size(526, 412);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox textEntrada;
    }
}
