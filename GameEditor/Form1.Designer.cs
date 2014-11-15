namespace GameEditor
{
    partial class Form1
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
            this.game11 = new GameEditor.Game1();
            this.SuspendLayout();
            // 
            // game11
            // 
            this.game11.BackColor = System.Drawing.Color.Black;
            this.game11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.game11.Location = new System.Drawing.Point(66, 12);
            this.game11.Name = "game11";
            this.game11.Size = new System.Drawing.Size(590, 470);
            this.game11.TabIndex = 1;
            this.game11.VSync = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 529);
            this.Controls.Add(this.game11);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Game1 game11;

    }
}

