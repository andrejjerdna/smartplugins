namespace PipeRack
{
    partial class FormRedaсtion
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
            this.NameOfRack = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TypeAtt = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NomerYarusa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NomerProleta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TypeOfElements = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ShagRamTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GetShagRam = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameOfRack
            // 
            this.NameOfRack.FormattingEnabled = true;
            this.NameOfRack.Location = new System.Drawing.Point(197, 32);
            this.NameOfRack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NameOfRack.Name = "NameOfRack";
            this.NameOfRack.Size = new System.Drawing.Size(259, 24);
            this.NameOfRack.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название эстакады";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 81);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 353);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.New);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.TypeAtt);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.NomerYarusa);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.NomerProleta);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.TypeOfElements);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(459, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Атрибуты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(159, 98);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(240, 21);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "(если не выбрано - левый ярус)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Правый ярус";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 270);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 31);
            this.button2.TabIndex = 10;
            this.button2.Text = "Изменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(159, 226);
            this.New.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(259, 22);
            this.New.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Новое значение";
            // 
            // TypeAtt
            // 
            this.TypeAtt.FormattingEnabled = true;
            this.TypeAtt.Items.AddRange(new object[] {
            "Класс",
            "Имя",
            "Профиль",
            "Материал",
            "Префикс сборки",
            "Номер сборки",
            "Положение вертикально",
            "Положение горизонтально",
            "Положение поворот"});
            this.TypeAtt.Location = new System.Drawing.Point(159, 161);
            this.TypeAtt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TypeAtt.Name = "TypeAtt";
            this.TypeAtt.Size = new System.Drawing.Size(259, 24);
            this.TypeAtt.TabIndex = 7;
            this.TypeAtt.Text = "Класс";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Атрибут";
            // 
            // NomerYarusa
            // 
            this.NomerYarusa.Location = new System.Drawing.Point(179, 133);
            this.NomerYarusa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NomerYarusa.Name = "NomerYarusa";
            this.NomerYarusa.Size = new System.Drawing.Size(239, 22);
            this.NomerYarusa.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "№№ яруса (колонны)";
            // 
            // NomerProleta
            // 
            this.NomerProleta.Location = new System.Drawing.Point(159, 62);
            this.NomerProleta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NomerProleta.Name = "NomerProleta";
            this.NomerProleta.Size = new System.Drawing.Size(259, 22);
            this.NomerProleta.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "№№ пролета";
            // 
            // TypeOfElements
            // 
            this.TypeOfElements.FormattingEnabled = true;
            this.TypeOfElements.Items.AddRange(new object[] {
            "Колонны",
            "Траверсы яруса",
            "Продольные балки",
            "Траверсы в пролете",
            "Стойки"});
            this.TypeOfElements.Location = new System.Drawing.Point(159, 31);
            this.TypeOfElements.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TypeOfElements.Name = "TypeOfElements";
            this.TypeOfElements.Size = new System.Drawing.Size(259, 24);
            this.TypeOfElements.TabIndex = 1;
            this.TypeOfElements.Text = "Колонны";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Тип элемента";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GetShagRam);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.ShagRamTB);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(459, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Расстояние между рамами";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 441);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(230, 197);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 38);
            this.button3.TabIndex = 0;
            this.button3.Text = "Изменить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // ShagRamTB
            // 
            this.ShagRamTB.Location = new System.Drawing.Point(134, 127);
            this.ShagRamTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShagRamTB.Name = "ShagRamTB";
            this.ShagRamTB.Size = new System.Drawing.Size(259, 22);
            this.ShagRamTB.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "Шаг рам";
            // 
            // GetShagRam
            // 
            this.GetShagRam.Location = new System.Drawing.Point(80, 197);
            this.GetShagRam.Name = "GetShagRam";
            this.GetShagRam.Size = new System.Drawing.Size(144, 38);
            this.GetShagRam.TabIndex = 6;
            this.GetShagRam.Text = "Получить";
            this.GetShagRam.UseVisualStyleBackColor = true;
            this.GetShagRam.Click += new System.EventHandler(this.GetShagRam_Click);
            // 
            // FormRedaсtion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 482);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameOfRack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormRedaсtion";
            this.Text = "FormRedaсtion";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox NameOfRack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeOfElements;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox TypeAtt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NomerYarusa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NomerProleta;
        private System.Windows.Forms.TextBox New;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button GetShagRam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ShagRamTB;
        private System.Windows.Forms.Button button3;
    }
}