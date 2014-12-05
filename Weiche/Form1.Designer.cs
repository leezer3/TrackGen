namespace Weiche
{
    partial class Weichengenerator
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
            this.label_radius = new System.Windows.Forms.Label();
            this.label_tot = new System.Windows.Forms.Label();
            this.label_laenge = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.textBox_radius = new System.Windows.Forms.TextBox();
            this.textBox_tot = new System.Windows.Forms.TextBox();
            this.textBox_laenge = new System.Windows.Forms.TextBox();
            this.folderPath = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.textBox_segmente = new System.Windows.Forms.TextBox();
            this.label_segmente = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackgauge_inp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_z = new System.Windows.Forms.TextBox();
            this.label_z = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_platheight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_platwidth_near = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_platwidth_far = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.texturebutton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fence_no = new System.Windows.Forms.RadioButton();
            this.fence_yes = new System.Windows.Forms.RadioButton();
            this.fenceheight_tb = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_radius
            // 
            this.label_radius.AutoSize = true;
            this.label_radius.Location = new System.Drawing.Point(12, 58);
            this.label_radius.Name = "label_radius";
            this.label_radius.Size = new System.Drawing.Size(219, 13);
            this.label_radius.TabIndex = 0;
            this.label_radius.Text = "Radius/Abweichung der eigenen Spur (in m) ";
            // 
            // label_tot
            // 
            this.label_tot.AutoSize = true;
            this.label_tot.Location = new System.Drawing.Point(12, 84);
            this.label_tot.Name = "label_tot";
            this.label_tot.Size = new System.Drawing.Size(154, 13);
            this.label_tot.TabIndex = 1;
            this.label_tot.Text = "Abweichung der Totspur (in m) ";
            // 
            // label_laenge
            // 
            this.label_laenge.AutoSize = true;
            this.label_laenge.Location = new System.Drawing.Point(12, 110);
            this.label_laenge.Name = "label_laenge";
            this.label_laenge.Size = new System.Drawing.Size(139, 13);
            this.label_laenge.TabIndex = 2;
            this.label_laenge.Text = "Weichenlänge (25,50,75,..) ";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(290, 523);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(106, 26);
            this.button.TabIndex = 9;
            this.button.Text = "Erstellen";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // textBox_radius
            // 
            this.textBox_radius.Location = new System.Drawing.Point(278, 55);
            this.textBox_radius.Name = "textBox_radius";
            this.textBox_radius.Size = new System.Drawing.Size(118, 20);
            this.textBox_radius.TabIndex = 3;
            this.textBox_radius.Text = "0";
            // 
            // textBox_tot
            // 
            this.textBox_tot.Location = new System.Drawing.Point(278, 81);
            this.textBox_tot.Name = "textBox_tot";
            this.textBox_tot.Size = new System.Drawing.Size(118, 20);
            this.textBox_tot.TabIndex = 4;
            this.textBox_tot.Text = "2";
            // 
            // textBox_laenge
            // 
            this.textBox_laenge.Location = new System.Drawing.Point(278, 107);
            this.textBox_laenge.Name = "textBox_laenge";
            this.textBox_laenge.Size = new System.Drawing.Size(118, 20);
            this.textBox_laenge.TabIndex = 5;
            this.textBox_laenge.Text = "1";
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(354, 454);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(42, 26);
            this.folderPath.TabIndex = 8;
            this.folderPath.Text = "Pfad";
            this.folderPath.UseVisualStyleBackColor = true;
            this.folderPath.Click += new System.EventHandler(this.folderPath_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 454);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(336, 20);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(19, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Weiche";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioButton2.Location = new System.Drawing.Point(117, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Kurve";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioButton3.Location = new System.Drawing.Point(207, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(60, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Gerade";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // textBox_segmente
            // 
            this.textBox_segmente.Location = new System.Drawing.Point(278, 133);
            this.textBox_segmente.Name = "textBox_segmente";
            this.textBox_segmente.Size = new System.Drawing.Size(118, 20);
            this.textBox_segmente.TabIndex = 6;
            this.textBox_segmente.Text = "20";
            // 
            // label_segmente
            // 
            this.label_segmente.AutoSize = true;
            this.label_segmente.Location = new System.Drawing.Point(12, 136);
            this.label_segmente.Name = "label_segmente";
            this.label_segmente.Size = new System.Drawing.Size(90, 13);
            this.label_segmente.TabIndex = 13;
            this.label_segmente.Text = "Anzahl Segmente";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.trackgauge_inp);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 89);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Erweiterte Optionen";
            // 
            // trackgauge_inp
            // 
            this.trackgauge_inp.Location = new System.Drawing.Point(132, 63);
            this.trackgauge_inp.Name = "trackgauge_inp";
            this.trackgauge_inp.Size = new System.Drawing.Size(100, 20);
            this.trackgauge_inp.TabIndex = 6;
            this.trackgauge_inp.Text = "1.44";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Track Gauge (in m)";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(132, 42);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(126, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "Nicht Textur Schiene";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 42);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(82, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "PNG Textur";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged_1);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(258, 19);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(109, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Textur Invertieren";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(132, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(103, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Ohne Böschung";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Weichenantrieb";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(-3, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(481, 42);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_z
            // 
            this.textBox_z.Location = new System.Drawing.Point(278, 158);
            this.textBox_z.Name = "textBox_z";
            this.textBox_z.Size = new System.Drawing.Size(118, 20);
            this.textBox_z.TabIndex = 17;
            this.textBox_z.Text = "0";
            // 
            // label_z
            // 
            this.label_z.AutoSize = true;
            this.label_z.Location = new System.Drawing.Point(12, 158);
            this.label_z.Name = "label_z";
            this.label_z.Size = new System.Drawing.Size(86, 13);
            this.label_z.TabIndex = 18;
            this.label_z.Text = "z - Verschiebung";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(16, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "v 1.5 ALPHA 11  © N.Busch and C. Lees 2008- 2014";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(9, 486);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Language:";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioButton4.Location = new System.Drawing.Point(311, 12);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(63, 17);
            this.radioButton4.TabIndex = 23;
            this.radioButton4.Text = "Platform";
            this.radioButton4.UseVisualStyleBackColor = false;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Platform Side";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Location = new System.Drawing.Point(278, 184);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(121, 22);
            this.panel1.TabIndex = 26;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Enabled = false;
            this.radioButton6.Location = new System.Drawing.Point(50, -1);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(50, 17);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Right";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Enabled = false;
            this.radioButton5.Location = new System.Drawing.Point(0, -1);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(43, 17);
            this.radioButton5.TabIndex = 0;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Left";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Platform Height (in m)";
            // 
            // textBox_platheight
            // 
            this.textBox_platheight.Location = new System.Drawing.Point(278, 212);
            this.textBox_platheight.Name = "textBox_platheight";
            this.textBox_platheight.ReadOnly = true;
            this.textBox_platheight.Size = new System.Drawing.Size(118, 20);
            this.textBox_platheight.TabIndex = 28;
            this.textBox_platheight.Text = "1.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Platform Width (in m) - Near End";
            // 
            // textBox_platwidth_near
            // 
            this.textBox_platwidth_near.Location = new System.Drawing.Point(278, 241);
            this.textBox_platwidth_near.Name = "textBox_platwidth_near";
            this.textBox_platwidth_near.ReadOnly = true;
            this.textBox_platwidth_near.Size = new System.Drawing.Size(118, 20);
            this.textBox_platwidth_near.TabIndex = 30;
            this.textBox_platwidth_near.Text = "5";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton9);
            this.panel2.Controls.Add(this.radioButton8);
            this.panel2.Controls.Add(this.radioButton7);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(225, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 27);
            this.panel2.TabIndex = 31;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(96, 4);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(53, 17);
            this.radioButton9.TabIndex = 2;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "Down";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(57, 4);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(39, 17);
            this.radioButton8.TabIndex = 1;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "Up";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(4, 4);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(51, 17);
            this.radioButton7.TabIndex = 0;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "None";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Platform Ramp";
            // 
            // textBox_platwidth_far
            // 
            this.textBox_platwidth_far.Location = new System.Drawing.Point(278, 272);
            this.textBox_platwidth_far.Name = "textBox_platwidth_far";
            this.textBox_platwidth_far.ReadOnly = true;
            this.textBox_platwidth_far.Size = new System.Drawing.Size(118, 20);
            this.textBox_platwidth_far.TabIndex = 34;
            this.textBox_platwidth_far.Text = "5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 272);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Platform Width (in m) - Far End";
            // 
            // texturebutton
            // 
            this.texturebutton.Location = new System.Drawing.Point(229, 486);
            this.texturebutton.Name = "texturebutton";
            this.texturebutton.Size = new System.Drawing.Size(168, 23);
            this.texturebutton.TabIndex = 35;
            this.texturebutton.Text = "Choose Textures...";
            this.texturebutton.UseVisualStyleBackColor = true;
            this.texturebutton.Click += new System.EventHandler(this.texturebutton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 326);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Platform Fence";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.fence_no);
            this.panel3.Controls.Add(this.fence_yes);
            this.panel3.Location = new System.Drawing.Point(184, 328);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(96, 25);
            this.panel3.TabIndex = 37;
            // 
            // fence_no
            // 
            this.fence_no.AutoSize = true;
            this.fence_no.Checked = true;
            this.fence_no.Location = new System.Drawing.Point(45, 0);
            this.fence_no.Name = "fence_no";
            this.fence_no.Size = new System.Drawing.Size(39, 17);
            this.fence_no.TabIndex = 1;
            this.fence_no.TabStop = true;
            this.fence_no.Text = "No";
            this.fence_no.UseVisualStyleBackColor = true;
            // 
            // fence_yes
            // 
            this.fence_yes.AutoSize = true;
            this.fence_yes.Location = new System.Drawing.Point(4, 0);
            this.fence_yes.Name = "fence_yes";
            this.fence_yes.Size = new System.Drawing.Size(43, 17);
            this.fence_yes.TabIndex = 0;
            this.fence_yes.Text = "Yes";
            this.fence_yes.UseVisualStyleBackColor = true;
            this.fence_yes.CheckedChanged += new System.EventHandler(this.fence_yes_CheckedChanged);
            // 
            // fenceheight_tb
            // 
            this.fenceheight_tb.Location = new System.Drawing.Point(274, 327);
            this.fenceheight_tb.Name = "fenceheight_tb";
            this.fenceheight_tb.Size = new System.Drawing.Size(118, 20);
            this.fenceheight_tb.TabIndex = 38;
            this.fenceheight_tb.Text = "Height (in m)";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "English",
            "Deutsch",
            "Français"});
            this.comboBox1.Location = new System.Drawing.Point(12, 502);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 40;
            this.comboBox1.Text = "English";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Weichengenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(417, 560);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.fenceheight_tb);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.texturebutton);
            this.Controls.Add(this.textBox_platwidth_far);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox_platwidth_near);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_platheight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_z);
            this.Controls.Add(this.label_z);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_segmente);
            this.Controls.Add(this.label_segmente);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.textBox_laenge);
            this.Controls.Add(this.textBox_tot);
            this.Controls.Add(this.textBox_radius);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label_laenge);
            this.Controls.Add(this.label_tot);
            this.Controls.Add(this.label_radius);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Weichengenerator";
            this.Text = "TrackGen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_radius;
        private System.Windows.Forms.Label label_tot;
        private System.Windows.Forms.Label label_laenge;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.TextBox textBox_radius;
        private System.Windows.Forms.TextBox textBox_tot;
        private System.Windows.Forms.TextBox textBox_laenge;
        private System.Windows.Forms.Button folderPath;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox textBox_segmente;
        private System.Windows.Forms.Label label_segmente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox_z;
        private System.Windows.Forms.Label label_z;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_platheight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_platwidth_near;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_platwidth_far;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button texturebutton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton fence_no;
        private System.Windows.Forms.RadioButton fence_yes;
        private System.Windows.Forms.TextBox fenceheight_tb;
        private System.Windows.Forms.TextBox trackgauge_inp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;


    }
}

