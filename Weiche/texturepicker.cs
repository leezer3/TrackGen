using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Weiche
{
    
    public partial class texturepicker : Form
    
    {
        
        public texturepicker(string launchpath)
        {
            InitializeComponent();
            {
                //Set filters for dialogs
                openFileDialog1.Filter = "Image Files (*.bmp, *.png)|*.bmp;*.png";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "Select an Image";
                openFileDialog1.InitialDirectory = launchpath;

                try
                {
                    //Draw Initial Embankment Texture
                    string lp_grass = (launchpath) + "Textures\\Grass_prev.png";
                    pictureBox1.Image = new Bitmap(lp_grass);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                  
                    //Draw Initial Ballast Textures
                    string lp_ballast = (launchpath) + "Textures\\Ballast.png";
                    pictureBox2.Image = new Bitmap(lp_ballast);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                    //Draw Initial Sleeper Texture
                    string lp_sleeper = (launchpath) + "Textures\\Sleeper.png";
                    pictureBox5.Image = new Bitmap(lp_sleeper);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;

                    string lp_railtop = (launchpath) + "Textures\\railTop.png";
                    pictureBox3.Image = new Bitmap(lp_railtop);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading default images!");
                }

            }
        }

        

        private void okbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
        public void ballastbutton_Click(object sender, EventArgs e)
        {
                       
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                try
                {
                   this.pictureBox2.Load(openFileDialog1.FileName);
                   pictureBox2.Load();
                   
                   string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                   Weichengenerator parent = (Weichengenerator)this.Owner;
                   parent.updateballast(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }


        private void sleeperbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    this.pictureBox5.Load(openFileDialog1.FileName);
                    pictureBox5.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatesleeper(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void embankmentbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    this.pictureBox1.Load(openFileDialog1.FileName);
                    pictureBox1.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updateembk(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void railtopbutton_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    this.pictureBox3.Load(openFileDialog1.FileName);
                    pictureBox3.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updaterailtop(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        
    }
    
}
