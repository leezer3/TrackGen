namespace Weiche
{
    partial class SteelViaductTexture
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.archbutton = new System.Windows.Forms.Button();
            this.ballastfile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.wallbutton = new System.Windows.Forms.Button();
            this.fencefile = new System.Windows.Forms.TextBox();
            this.okbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(11, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 159);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // archbutton
            // 
            this.archbutton.Location = new System.Drawing.Point(229, 23);
            this.archbutton.Name = "archbutton";
            this.archbutton.Size = new System.Drawing.Size(75, 23);
            this.archbutton.TabIndex = 16;
            this.archbutton.Text = "Choose...";
            this.archbutton.UseVisualStyleBackColor = true;
            this.archbutton.Click += new System.EventHandler(this.archbutton_Click);
            // 
            // ballastfile
            // 
            this.ballastfile.Location = new System.Drawing.Point(11, 25);
            this.ballastfile.Name = "ballastfile";
            this.ballastfile.Size = new System.Drawing.Size(212, 20);
            this.ballastfile.TabIndex = 15;
            this.ballastfile.Text = "Choose a new steel texture....";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Viaduct Textures:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(11, 242);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(212, 54);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // wallbutton
            // 
            this.wallbutton.Location = new System.Drawing.Point(232, 216);
            this.wallbutton.Name = "wallbutton";
            this.wallbutton.Size = new System.Drawing.Size(75, 23);
            this.wallbutton.TabIndex = 20;
            this.wallbutton.Text = "Choose...";
            this.wallbutton.UseVisualStyleBackColor = true;
            this.wallbutton.Click += new System.EventHandler(this.wallbutton_Click);
            // 
            // fencefile
            // 
            this.fencefile.Location = new System.Drawing.Point(11, 216);
            this.fencefile.Name = "fencefile";
            this.fencefile.Size = new System.Drawing.Size(212, 20);
            this.fencefile.TabIndex = 19;
            this.fencefile.Text = "Choose a new fence texture....";
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(229, 302);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 30;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click_1);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(148, 302);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 29;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click_1);
            // 
            // SteelViaductTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 338);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.wallbutton);
            this.Controls.Add(this.fencefile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.archbutton);
            this.Controls.Add(this.ballastfile);
            this.Controls.Add(this.label1);
            this.Name = "SteelViaductTexture";
            this.Text = "Viaduct Textures";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button archbutton;
        private System.Windows.Forms.TextBox ballastfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button wallbutton;
        private System.Windows.Forms.TextBox fencefile;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Button cancelbutton;
    }
}