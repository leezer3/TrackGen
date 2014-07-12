namespace Weiche
{
    partial class platformtexture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(platformtexture));
            this.label1 = new System.Windows.Forms.Label();
            this.ballastfile = new System.Windows.Forms.TextBox();
            this.platformbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.fencefile = new System.Windows.Forms.TextBox();
            this.fencebutton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Platform Texture:";
            // 
            // ballastfile
            // 
            this.ballastfile.Location = new System.Drawing.Point(12, 29);
            this.ballastfile.Name = "ballastfile";
            this.ballastfile.Size = new System.Drawing.Size(212, 20);
            this.ballastfile.TabIndex = 4;
            this.ballastfile.Text = "Choose a new platform texture....";
            // 
            // platformbutton
            // 
            this.platformbutton.Location = new System.Drawing.Point(230, 27);
            this.platformbutton.Name = "platformbutton";
            this.platformbutton.Size = new System.Drawing.Size(75, 23);
            this.platformbutton.TabIndex = 5;
            this.platformbutton.Text = "Choose...";
            this.platformbutton.UseVisualStyleBackColor = true;
            this.platformbutton.Click += new System.EventHandler(this.platformbutton_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 159);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(149, 418);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 14;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(230, 418);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 15;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // fencefile
            // 
            this.fencefile.Location = new System.Drawing.Point(12, 220);
            this.fencefile.Name = "fencefile";
            this.fencefile.Size = new System.Drawing.Size(212, 20);
            this.fencefile.TabIndex = 16;
            this.fencefile.Text = "Choose a new fence texture....";
            // 
            // fencebutton
            // 
            this.fencebutton.Location = new System.Drawing.Point(233, 220);
            this.fencebutton.Name = "fencebutton";
            this.fencebutton.Size = new System.Drawing.Size(75, 23);
            this.fencebutton.TabIndex = 17;
            this.fencebutton.Text = "Choose...";
            this.fencebutton.UseVisualStyleBackColor = true;
            this.fencebutton.Click += new System.EventHandler(this.fencebutton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 246);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(212, 159);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // platformtexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 452);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.fencebutton);
            this.Controls.Add(this.fencefile);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.platformbutton);
            this.Controls.Add(this.ballastfile);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "platformtexture";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ballastfile;
        private System.Windows.Forms.Button platformbutton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.TextBox fencefile;
        private System.Windows.Forms.Button fencebutton;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}