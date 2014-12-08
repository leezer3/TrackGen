using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Weiche
{
    public partial class ViaductTexture : Form
    {
        public ViaductTexture(string launchpath)
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image Files (*.bmp, *.png)|*.bmp;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Select an Image";
            openFileDialog1.InitialDirectory = launchpath;

            try
            {
                
                string lp_arch = (launchpath) + "Textures\\viaduct1.png";
                pictureBox1.Image = new Bitmap(lp_arch);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                string lp_wall = (launchpath) + "Textures\\viaduct2.png";
                pictureBox2.Image = new Bitmap(lp_wall);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                string lp_footwalk = (launchpath) + "Textures\\viaduct3.png";
                pictureBox3.Image = new Bitmap(lp_footwalk);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                string lp_arch_is = (launchpath) + "Textures\\viaduct4.png";
                pictureBox4.Image = new Bitmap(lp_arch_is);
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading default images!");
            }


        }
        
        private void archbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox1.Load(openFileDialog1.FileName);
                    pictureBox1.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatetexture(filename, openFileDialog1.FileName,7);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void wallbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox2.Load(openFileDialog1.FileName);
                    pictureBox2.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatetexture(filename, openFileDialog1.FileName, 8);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void footwalkbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox2.Load(openFileDialog1.FileName);
                    pictureBox2.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatetexture(filename, openFileDialog1.FileName, 9);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void archisbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox2.Load(openFileDialog1.FileName);
                    pictureBox2.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatetexture(filename, openFileDialog1.FileName, 10);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void okbutton_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelbutton_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
