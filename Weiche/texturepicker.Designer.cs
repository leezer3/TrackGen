namespace Weiche
{
    partial class texturepicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(texturepicker));
            this.cancelbutton = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.ballastlabel = new System.Windows.Forms.Label();
            this.ballastfile = new System.Windows.Forms.TextBox();
            this.ballastbutton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sleeperbutton = new System.Windows.Forms.Button();
            this.sleeperfile = new System.Windows.Forms.TextBox();
            this.embankmentfile = new System.Windows.Forms.TextBox();
            this.embankmentbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.railtopfile = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.railtopbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(153, 507);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 0;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(234, 507);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 1;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // ballastlabel
            // 
            this.ballastlabel.AutoSize = true;
            this.ballastlabel.Location = new System.Drawing.Point(13, 13);
            this.ballastlabel.Name = "ballastlabel";
            this.ballastlabel.Size = new System.Drawing.Size(80, 13);
            this.ballastlabel.TabIndex = 2;
            this.ballastlabel.Text = "Ballast Texture:";
            // 
            // ballastfile
            // 
            this.ballastfile.Location = new System.Drawing.Point(16, 30);
            this.ballastfile.Name = "ballastfile";
            this.ballastfile.Size = new System.Drawing.Size(212, 20);
            this.ballastfile.TabIndex = 3;
            this.ballastfile.Text = "Choose a new ballast texture...";
            // 
            // ballastbutton
            // 
            this.ballastbutton.Location = new System.Drawing.Point(234, 30);
            this.ballastbutton.Name = "ballastbutton";
            this.ballastbutton.Size = new System.Drawing.Size(75, 23);
            this.ballastbutton.TabIndex = 4;
            this.ballastbutton.Text = "Choose...";
            this.ballastbutton.UseVisualStyleBackColor = true;
            this.ballastbutton.Click += new System.EventHandler(this.ballastbutton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sleeper Texture:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Embankment Texture:";
            // 
            // sleeperbutton
            // 
            this.sleeperbutton.Location = new System.Drawing.Point(238, 179);
            this.sleeperbutton.Name = "sleeperbutton";
            this.sleeperbutton.Size = new System.Drawing.Size(75, 23);
            this.sleeperbutton.TabIndex = 7;
            this.sleeperbutton.Text = "Choose...";
            this.sleeperbutton.UseVisualStyleBackColor = true;
            this.sleeperbutton.Click += new System.EventHandler(this.sleeperbutton_Click);
            // 
            // sleeperfile
            // 
            this.sleeperfile.Location = new System.Drawing.Point(16, 179);
            this.sleeperfile.Name = "sleeperfile";
            this.sleeperfile.Size = new System.Drawing.Size(212, 20);
            this.sleeperfile.TabIndex = 6;
            this.sleeperfile.Text = "Choose a new sleeper texture...";
            // 
            // embankmentfile
            // 
            this.embankmentfile.Location = new System.Drawing.Point(16, 323);
            this.embankmentfile.Name = "embankmentfile";
            this.embankmentfile.Size = new System.Drawing.Size(212, 20);
            this.embankmentfile.TabIndex = 9;
            this.embankmentfile.Text = "Choose a new embankment texture...";
            // 
            // embankmentbutton
            // 
            this.embankmentbutton.Location = new System.Drawing.Point(234, 323);
            this.embankmentbutton.Name = "embankmentbutton";
            this.embankmentbutton.Size = new System.Drawing.Size(75, 23);
            this.embankmentbutton.TabIndex = 10;
            this.embankmentbutton.Text = "Choose...";
            this.embankmentbutton.UseVisualStyleBackColor = true;
            this.embankmentbutton.Click += new System.EventHandler(this.embankmentbutton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 347);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 100);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(16, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(16, 206);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(200, 100);
            this.pictureBox5.TabIndex = 13;
            this.pictureBox5.TabStop = false;
            // 
            // railtopfile
            // 
            this.railtopfile.Location = new System.Drawing.Point(16, 453);
            this.railtopfile.Name = "railtopfile";
            this.railtopfile.Size = new System.Drawing.Size(212, 20);
            this.railtopfile.TabIndex = 14;
            this.railtopfile.Text = "Choose a new railtop texture...";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(16, 479);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(26, 22);
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // railtopbutton
            // 
            this.railtopbutton.Location = new System.Drawing.Point(234, 453);
            this.railtopbutton.Name = "railtopbutton";
            this.railtopbutton.Size = new System.Drawing.Size(75, 23);
            this.railtopbutton.TabIndex = 16;
            this.railtopbutton.Text = "Choose...";
            this.railtopbutton.UseVisualStyleBackColor = true;
            this.railtopbutton.Click += new System.EventHandler(this.railtopbutton_Click_1);
            // 
            // texturepicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 538);
            this.Controls.Add(this.railtopbutton);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.railtopfile);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.embankmentbutton);
            this.Controls.Add(this.embankmentfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sleeperbutton);
            this.Controls.Add(this.sleeperfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ballastbutton);
            this.Controls.Add(this.ballastfile);
            this.Controls.Add(this.ballastlabel);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.cancelbutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "texturepicker";
            this.Text = "Texture Options- Track";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Label ballastlabel;
        private System.Windows.Forms.TextBox ballastfile;
        private System.Windows.Forms.Button ballastbutton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sleeperbutton;
        private System.Windows.Forms.TextBox sleeperfile;
        private System.Windows.Forms.TextBox embankmentfile;
        private System.Windows.Forms.Button embankmentbutton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.TextBox railtopfile;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button railtopbutton;
    }
}