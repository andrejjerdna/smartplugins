namespace PipeRack
{
    partial class Form_att_column
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_att_column));
            this.SelectYarusCB = new System.Windows.Forms.ComboBox();
            this.selectYarus = new System.Windows.Forms.Label();
            this.RotationCB = new Tekla.Structures.Dialog.UIControls.ImageListComboBox();
            this.Rotation = new System.Windows.Forms.ImageList(this.components);
            this.PlaneCB = new Tekla.Structures.Dialog.UIControls.ImageListComboBox();
            this.Plane = new System.Windows.Forms.ImageList(this.components);
            this.DepthCB = new Tekla.Structures.Dialog.UIControls.ImageListComboBox();
            this.Depth = new System.Windows.Forms.ImageList(this.components);
            this.materialCatalog1 = new Tekla.Structures.Dialog.UIControls.MaterialCatalog();
            this.profileCatalog1 = new Tekla.Structures.Dialog.UIControls.ProfileCatalog();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NomerSborki = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.PrefixSborki = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Class = new System.Windows.Forms.TextBox();
            this.Namen = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.Material = new System.Windows.Forms.TextBox();
            this.Profile = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectYarusCB
            // 
            this.SelectYarusCB.FormattingEnabled = true;
            this.SelectYarusCB.Items.AddRange(new object[] {
            "Колонна 1",
            "Колонна 2",
            "Колонна 3"});
            this.SelectYarusCB.Location = new System.Drawing.Point(144, 177);
            this.SelectYarusCB.Name = "SelectYarusCB";
            this.SelectYarusCB.Size = new System.Drawing.Size(196, 24);
            this.SelectYarusCB.TabIndex = 161;
            this.SelectYarusCB.Text = "Колонна 1";
            // 
            // selectYarus
            // 
            this.selectYarus.Location = new System.Drawing.Point(14, 180);
            this.selectYarus.Name = "selectYarus";
            this.selectYarus.Size = new System.Drawing.Size(120, 21);
            this.selectYarus.TabIndex = 170;
            this.selectYarus.Text = "Выбор колонны";
            this.selectYarus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RotationCB
            // 
            this.RotationCB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RotationCB.BackColor = System.Drawing.Color.Transparent;
            this.RotationCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RotationCB.DefaultValue = "";
            this.RotationCB.HoverColor = System.Drawing.Color.DodgerBlue;
            this.RotationCB.ImageList = this.Rotation;
            this.RotationCB.Location = new System.Drawing.Point(167, 296);
            this.RotationCB.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.RotationCB.MinimumSize = new System.Drawing.Size(70, 56);
            this.RotationCB.Name = "RotationCB";
            this.RotationCB.SelectedIndex = 0;
            this.RotationCB.SelectedItem = ((object)(resources.GetObject("RotationCB.SelectedItem")));
            this.RotationCB.Size = new System.Drawing.Size(70, 56);
            this.RotationCB.TabIndex = 169;
            this.RotationCB.ToolTipText = "";
            // 
            // Rotation
            // 
            this.Rotation.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Rotation.ImageStream")));
            this.Rotation.TransparentColor = System.Drawing.Color.Transparent;
            this.Rotation.Images.SetKeyName(0, "Rotation_top_below.PNG");
            this.Rotation.Images.SetKeyName(1, "Rotation_front_back.PNG");
            // 
            // PlaneCB
            // 
            this.PlaneCB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlaneCB.BackColor = System.Drawing.Color.Transparent;
            this.PlaneCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PlaneCB.DefaultValue = "";
            this.PlaneCB.HoverColor = System.Drawing.Color.DodgerBlue;
            this.PlaneCB.ImageList = this.Plane;
            this.PlaneCB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PlaneCB.Location = new System.Drawing.Point(319, 296);
            this.PlaneCB.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.PlaneCB.MinimumSize = new System.Drawing.Size(70, 56);
            this.PlaneCB.Name = "PlaneCB";
            this.PlaneCB.SelectedIndex = 0;
            this.PlaneCB.SelectedItem = ((object)(resources.GetObject("PlaneCB.SelectedItem")));
            this.PlaneCB.Size = new System.Drawing.Size(70, 56);
            this.PlaneCB.TabIndex = 168;
            this.PlaneCB.ToolTipText = "";
            // 
            // Plane
            // 
            this.Plane.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Plane.ImageStream")));
            this.Plane.TransparentColor = System.Drawing.Color.Transparent;
            this.Plane.Images.SetKeyName(0, "Plane_middle.PNG");
            this.Plane.Images.SetKeyName(1, "Plane_left.PNG");
            this.Plane.Images.SetKeyName(2, "Plane_right.PNG");
            // 
            // DepthCB
            // 
            this.DepthCB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DepthCB.BackColor = System.Drawing.Color.Transparent;
            this.DepthCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DepthCB.DefaultValue = "";
            this.DepthCB.HoverColor = System.Drawing.Color.DodgerBlue;
            this.DepthCB.ImageList = this.Depth;
            this.DepthCB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DepthCB.Location = new System.Drawing.Point(33, 296);
            this.DepthCB.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.DepthCB.MinimumSize = new System.Drawing.Size(70, 56);
            this.DepthCB.Name = "DepthCB";
            this.DepthCB.SelectedIndex = 0;
            this.DepthCB.SelectedItem = ((object)(resources.GetObject("DepthCB.SelectedItem")));
            this.DepthCB.Size = new System.Drawing.Size(70, 56);
            this.DepthCB.TabIndex = 167;
            this.DepthCB.ToolTipText = "";
            // 
            // Depth
            // 
            this.Depth.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Depth.ImageStream")));
            this.Depth.TransparentColor = System.Drawing.Color.Transparent;
            this.Depth.Images.SetKeyName(0, "Depth_front.PNG");
            this.Depth.Images.SetKeyName(1, "Depth_middle.PNG");
            this.Depth.Images.SetKeyName(2, "Depth_behind.PNG");
            // 
            // materialCatalog1
            // 
            this.materialCatalog1.BackColor = System.Drawing.Color.Transparent;
          //  this.materialCatalog1.ButtonText = "Выбрать...";
            this.materialCatalog1.Location = new System.Drawing.Point(353, 74);
            this.materialCatalog1.Margin = new System.Windows.Forms.Padding(5);
            this.materialCatalog1.Name = "materialCatalog1";
            this.materialCatalog1.SelectedMaterial = "";
            this.materialCatalog1.Size = new System.Drawing.Size(117, 33);
            this.materialCatalog1.TabIndex = 166;
            // 
            // profileCatalog1
            // 
            this.profileCatalog1.BackColor = System.Drawing.Color.Transparent;
           // this.profileCatalog1.ButtonText = "Select...";
            this.profileCatalog1.Location = new System.Drawing.Point(351, 30);
            this.profileCatalog1.Margin = new System.Windows.Forms.Padding(5);
            this.profileCatalog1.Name = "profileCatalog1";
            this.profileCatalog1.SelectedProfile = "";
            this.profileCatalog1.Size = new System.Drawing.Size(117, 33);
            this.profileCatalog1.TabIndex = 165;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 388);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 31);
            this.button1.TabIndex = 164;
            this.button1.Text = "Изменить";
            this.button1.UseVisualStyleBackColor = true;
         //   this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(291, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 21);
            this.label4.TabIndex = 163;
            this.label4.Text = "Горизонтально";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(99, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 22);
            this.label3.TabIndex = 162;
            this.label3.Text = "Положение";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NomerSborki
            // 
            this.NomerSborki.Location = new System.Drawing.Point(249, 142);
            this.NomerSborki.Margin = new System.Windows.Forms.Padding(4);
            this.NomerSborki.Name = "NomerSborki";
            this.NomerSborki.Size = new System.Drawing.Size(93, 22);
            this.NomerSborki.TabIndex = 160;
            this.NomerSborki.Text = "2";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(164, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 159;
            this.label2.Text = "Поворот";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(-1, 143);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(140, 20);
            this.label24.TabIndex = 158;
            this.label24.Text = "Нумерация сборок";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PrefixSborki
            // 
            this.PrefixSborki.Location = new System.Drawing.Point(146, 142);
            this.PrefixSborki.Margin = new System.Windows.Forms.Padding(4);
            this.PrefixSborki.Name = "PrefixSborki";
            this.PrefixSborki.Size = new System.Drawing.Size(93, 22);
            this.PrefixSborki.TabIndex = 157;
            this.PrefixSborki.Text = "K";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(13, 111);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 21);
            this.label17.TabIndex = 156;
            this.label17.Text = "Класс";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Class
            // 
            this.Class.Location = new System.Drawing.Point(146, 106);
            this.Class.Margin = new System.Windows.Forms.Padding(4);
            this.Class.Name = "Class";
            this.Class.Size = new System.Drawing.Size(196, 22);
            this.Class.TabIndex = 155;
            this.Class.Text = "2";
            // 
            // Namen
            // 
            this.Namen.Location = new System.Drawing.Point(146, 6);
            this.Namen.Margin = new System.Windows.Forms.Padding(4);
            this.Namen.Name = "Namen";
            this.Namen.Size = new System.Drawing.Size(196, 22);
            this.Namen.TabIndex = 154;
            this.Namen.Text = "Колонна";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(13, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 21);
            this.label18.TabIndex = 153;
            this.label18.Text = "Имя";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(13, 74);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 21);
            this.label20.TabIndex = 152;
            this.label20.Text = "Материал";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Material
            // 
            this.Material.Location = new System.Drawing.Point(146, 70);
            this.Material.Margin = new System.Windows.Forms.Padding(4);
            this.Material.Name = "Material";
            this.Material.Size = new System.Drawing.Size(196, 22);
            this.Material.TabIndex = 151;
            this.Material.Text = "C345";
            // 
            // Profile
            // 
            this.Profile.Location = new System.Drawing.Point(146, 38);
            this.Profile.Margin = new System.Windows.Forms.Padding(4);
            this.Profile.Name = "Profile";
            this.Profile.Size = new System.Drawing.Size(196, 22);
            this.Profile.TabIndex = 150;
            this.Profile.Text = "I30K1_20_93";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(13, 42);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 21);
            this.label21.TabIndex = 149;
            this.label21.Text = "Профиль";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(9, 259);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(131, 21);
            this.label26.TabIndex = 148;
            this.label26.Text = "Вертикально";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_att_column
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 431);
            this.Controls.Add(this.SelectYarusCB);
            this.Controls.Add(this.selectYarus);
            this.Controls.Add(this.RotationCB);
            this.Controls.Add(this.PlaneCB);
            this.Controls.Add(this.DepthCB);
            this.Controls.Add(this.materialCatalog1);
            this.Controls.Add(this.profileCatalog1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NomerSborki);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.PrefixSborki);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Class);
            this.Controls.Add(this.Namen);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.Material);
            this.Controls.Add(this.Profile);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label26);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_att_column";
            this.ShowInTaskbar = false;
            this.Text = "Form_att_column";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectYarusCB;
        private System.Windows.Forms.Label selectYarus;
        private Tekla.Structures.Dialog.UIControls.ImageListComboBox RotationCB;
        private Tekla.Structures.Dialog.UIControls.ImageListComboBox PlaneCB;
        private Tekla.Structures.Dialog.UIControls.ImageListComboBox DepthCB;
        private Tekla.Structures.Dialog.UIControls.MaterialCatalog materialCatalog1;
        private Tekla.Structures.Dialog.UIControls.ProfileCatalog profileCatalog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NomerSborki;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox PrefixSborki;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox Class;
        private System.Windows.Forms.TextBox Namen;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox Material;
        private System.Windows.Forms.TextBox Profile;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ImageList Rotation;
        private System.Windows.Forms.ImageList Plane;
        private System.Windows.Forms.ImageList Depth;
    }
}