using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Weiche
{
    public partial class platformtexture : Form
    {
        public platformtexture(string launchpath)
        {
            InitializeComponent();

            openFileDialog1.Filter = "Image Files (*.bmp, *.png)|*.bmp;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Select an Image";
            openFileDialog1.InitialDirectory = launchpath;

            try
            {
                //Draw Initial Embankment Texture
                string lp_platform = (launchpath) + "Textures\\platformsurface.png";
                pictureBox1.Image = new Bitmap(lp_platform);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                string lp_fence = (launchpath) + "Textures\\fence_18.png";
                pictureBox2.Image = new Bitmap(lp_fence);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading default images!");
            }
        }



        public void platformbutton_Click_1(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox1.Load(openFileDialog1.FileName);
                    pictureBox1.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updateplatform(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        public void fencebutton_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    this.pictureBox2.Load(openFileDialog1.FileName);
                    pictureBox2.Load();

                    string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    Weichengenerator parent = (Weichengenerator)this.Owner;
                    parent.updatefence(filename, openFileDialog1.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }


}
