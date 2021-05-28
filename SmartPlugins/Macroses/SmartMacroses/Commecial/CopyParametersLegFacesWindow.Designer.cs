
namespace SmartMacroses
{
    partial class CopyParametersLegFacesWindow
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
            this.Delta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Number = new System.Windows.Forms.TextBox();
            this.Flip = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Delta
            // 
            this.Delta.Location = new System.Drawing.Point(170, 6);
            this.Delta.Name = "Delta";
            this.Delta.Size = new System.Drawing.Size(100, 20);
            this.Delta.TabIndex = 0;
            this.Delta.TextChanged += new System.EventHandler(this.Delta_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(293, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "ПРИМЕНИТЬ К ВЫБРАННЫМ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дополнительное смещение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Перенести стержни:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Порядковый номер слоя:";
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(170, 58);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(100, 20);
            this.Number.TabIndex = 5;
            this.Number.TextChanged += new System.EventHandler(this.Number_TextChanged);
            // 
            // Flip
            // 
            this.Flip.FormattingEnabled = true;
            this.Flip.Items.AddRange(new object[] {
            "Нет",
            "Да"});
            this.Flip.Location = new System.Drawing.Point(170, 32);
            this.Flip.Name = "Flip";
            this.Flip.Size = new System.Drawing.Size(100, 21);
            this.Flip.TabIndex = 7;
            this.Flip.SelectedIndexChanged += new System.EventHandler(this.Flip_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Image = global::SmartMacroses.Properties.Resources.add_black_24dp;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(276, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 72);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 113);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(293, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // CopyParametersLegFacesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 148);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Flip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Delta);
            this.MaximumSize = new System.Drawing.Size(336, 187);
            this.MinimumSize = new System.Drawing.Size(336, 187);
            this.Name = "CopyParametersLegFacesWindow";
            this.Text = "CopyParametersLegFaces";
            this.Load += new System.EventHandler(this.CopyParametersLegFacesWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Delta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.ComboBox Flip;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}