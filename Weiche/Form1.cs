using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using Weiche.Properties;

namespace Weiche
{
    public partial class Weichengenerator : Form
    {
        //Define initial texture filenames
        //Motor Texture
        public string motor_texture;
        public string motor_file;
        //Ballast Textures
        public string ballast_file;
        public string ballast_texture;
        //Sleeper Textures
        public string sleeper_file;
        public string sleeper_texture;
        //Embankment Textures
        public string embankment_file;
        public string embankment_texture;
        //Rail Textures
        public string railtop_file;
        public string railtop_texture;
        public string railside_file;
        public string railside_texture;
        //Platform Textures
        public string platform_file;
        public string platform_texture;
        //Fence Textures
        public string fence_file;
        public string fence_texture;
        
        public string launchpath = AppDomain.CurrentDomain.BaseDirectory;
        public Weichengenerator()
        {
            
           
            InitializeComponent();
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
            {
                if (key != null)
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                    var language = key.GetValue("Language");
                    var textureformat = key.GetValue("TextureFormat");
                    
                    if (Convert.ToString(language) == "de")
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                        //German
                    }
                    else if (Convert.ToString(language) == "fr")
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
                        //German
                    }
                    else
                    {
                        //If the registry key isn't what we expect
                        //Return English & reset value
                        key.SetValue("Language", "en");

                    }
                    label_radius.Text = Resources.radius;
                    label_tot.Text = Resources.deviation;
                    radioButton1.Text = Resources.pointswitch;
                    radioButton2.Text = Resources.curve;
                    radioButton3.Text = Resources.straight;
                    label_laenge.Text = Resources.lengthmultiplier;
                    label_segmente.Text = Resources.segments;
                    groupBox1.Text = Resources.advanced;
                    checkBox5.Text = Resources.railtexture;
                    checkBox4.Text = Resources.pngtexture;
                    checkBox3.Text = Resources.invertexture;
                    checkBox2.Text = Resources.embankment;
                    checkBox1.Text = Resources.pointmotor;
                    label_z.Text = Resources.zadjust;
                    folderPath.Text = Resources.path;
                    button.Text = Resources.createbutton;
                    label3.Text = Resources.platformside;
                    label4.Text = Resources.platformheight;
                    label5.Text = Resources.platformwidth_n;
                    label7.Text = Resources.platformwidth_f;
                    label6.Text = Resources.platformramp;
                    label8.Text = Resources.platformfence;
                    radioButton4.Text = Resources.platform;
                    radioButton5.Text = Resources.left;
                    radioButton6.Text = Resources.right;
                    texturebutton.Text = Resources.choosetexture;
                    

                    if (key.GetValue("TextureFormat") != null)
                    {
                        //Set texture Format
                        if (Convert.ToString(textureformat) == "bmp")
                        {
                            checkBox4.Checked = false;
                        }
                        else
                        {
                            checkBox4.Checked = true;
                        }
                    }
                    else
                    {
                        //Handle case where the key exists, but the subkey does not
                        key.SetValue("TextureFormat", "bmp");
                    }
                }
                else
                {
                    using (var key2 = Registry.CurrentUser.CreateSubKey(@"Software\Trackgen"))
                    {
                        //Write default values to the registry
                        key2.SetValue("Language", "de");
                        key2.SetValue("TextureFormat", "bmp");
                    }
                    

                }
                
            }
            
            richTextBox1.Text = (launchpath);
        }




        


        public static void ConvertAndMove(string launchdir, string inputfile, string outputtex, string filename, string output)
        {
            try
            {
                if (Path.GetExtension(inputfile) == ".bmp")
                {
                    if (outputtex == "bmp")
                    {

                        System.IO.File.Copy(inputfile, launchdir + "\\Output\\" + output + "\\" + filename + "." + outputtex, true);

                    }
                    else
                    {
                        var image1 = System.Drawing.Image.FromFile(@inputfile);
                        image1.Save(@launchdir + "\\Output\\" + output + "\\" + filename + "." + outputtex, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
                else
                {
                    if (outputtex == "png")
                    {
                        System.IO.File.Copy(inputfile, launchdir + "\\Output\\" + output + "\\" + filename + "." + outputtex, true);
                    }
                    else
                    {
                        var image2 = System.Drawing.Image.FromFile(@inputfile);
                        image2.Save(@launchdir + "\\Output\\" + output + "\\" + filename + "." + outputtex, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
            catch
            {
                //Show missing texture error
                MessageBox.Show("The texture:" + "\n" + inputfile + "\n" + "is missing or in an unsupported format!");
            }
        }



        private void button_Click(object sender, EventArgs e)
        {
            string texture_format;

            //Preiminary Checks
            //Have we set textures?
            if (string.IsNullOrEmpty(ballast_file))
            {
                ballast_file = ("Ballast");
                ballast_texture = (richTextBox1.Text + "\\Textures\\ballast.png");
            }

            if (string.IsNullOrEmpty(sleeper_file))
            {
                sleeper_file = ("Sleeper");
                sleeper_texture = (richTextBox1.Text + "\\Textures\\sleeper.png");
            }

            if (string.IsNullOrEmpty(embankment_file))
            {
                embankment_file = ("GrassSeit");
                embankment_texture = (richTextBox1.Text + "\\Textures\\GrassSeit.png");
            }

            if (string.IsNullOrEmpty(railside_file))
            {
                railside_file = ("railSide");
                railside_texture = (richTextBox1.Text + "\\Textures\\railSide.png");
            }

            if (string.IsNullOrEmpty(railtop_file))
            {
                railtop_file = ("railTop");
                railtop_texture = (richTextBox1.Text + "\\Textures\\railTop.png");
            }

            if (string.IsNullOrEmpty(platform_file))
            {
                platform_file = ("platformsurface");
                platform_texture = (richTextBox1.Text + "\\Textures\\platformsurface.png");
            }
            if (string.IsNullOrEmpty(fence_file))
            {
                fence_file = ("fence_18");
                fence_texture = (richTextBox1.Text + "\\Textures\\fence_18.png");
            }

            var spez_file = "SchieneSpez";
            var spez_texture = (richTextBox1.Text + "\\Textures\\SchieneSpez.png");
            var spezanf_file = "SchieneSpezAnf";
            var spezanf_texture = (richTextBox1.Text + "\\Textures\\SchieneSpezAnf.png");

            motor_file = "WeichAntrieb";
            motor_texture = (richTextBox1.Text + "\\Textures\\WeichAntrieb.png");

            //Set texture format
            if (!checkBox4.Checked)
            {
                texture_format = ("bmp");
            }
            else
            {
                texture_format = ("png");
            }
            //Check if the entered path exists
            var pathExists = System.IO.Directory.Exists(richTextBox1.Text);
            if (!pathExists)
            {
                MessageBox.Show("Path does not exist.");
                return;
            }
            //Convert to string/ bool array for passing to new separate functions
            string[] inputstrings = new string[33];
            inputstrings[0] = textBox_radius.Text;
            inputstrings[1] = textBox_segmente.Text;
            inputstrings[2] = trackgauge_inp.Text;
            inputstrings[3] = richTextBox1.Text;
            inputstrings[4] = launchpath;
            inputstrings[5] = ballast_texture;
            inputstrings[6] = ballast_file;
            inputstrings[7] = sleeper_texture;
            inputstrings[8] = sleeper_file;
            inputstrings[9] = railside_texture;
            inputstrings[10] = railside_file;
            inputstrings[11] = railtop_texture;
            inputstrings[12] = railtop_file;
            inputstrings[13] = embankment_texture;
            inputstrings[14] = embankment_file;
            inputstrings[15] = texture_format;
            inputstrings[16] = textBox_tot.Text;
            inputstrings[17] = textBox_laenge.Text;
            inputstrings[18] = textBox_z.Text;
            inputstrings[19] = spez_texture;
            inputstrings[20] = spez_file;
            inputstrings[21] = spezanf_texture;
            inputstrings[22] = spezanf_file;
            inputstrings[23] = motor_texture;
            inputstrings[24] = motor_file;
            inputstrings[25] = platform_texture;
            inputstrings[26] = platform_file;
            inputstrings[27] = fence_texture;
            inputstrings[28] = fence_file;
            inputstrings[29] = textBox_platheight.Text;
            inputstrings[30] = textBox_platwidth_near.Text;
            inputstrings[31] = textBox_platwidth_far.Text;
            inputstrings[32] = fenceheight_tb.Text;
            bool[] inputcheckboxes = new bool[10];
            if (checkBox1.Checked)
            {
                inputcheckboxes[0] = true;
            }
            if (checkBox2.Checked)
            {
                inputcheckboxes[1] = true;
            }
            if (checkBox3.Checked)
            {
                inputcheckboxes[2] = true;
            }
            if (checkBox5.Checked)
            {
                inputcheckboxes[4] = true;
            }
            if (radioButton5.Checked)
            {
                inputcheckboxes[5] = true;
            }
            if (radioButton6.Checked)
            {
                inputcheckboxes[6] = true;
            }
            if (radioButton7.Checked)
            {
                inputcheckboxes[7] = true;
            }
            if (radioButton8.Checked)
            {
                inputcheckboxes[8] = true;
            }
            if (fence_yes.Checked)
            {
                inputcheckboxes[9] = true;
            }
            try
            {
                //Create Switch
                if (radioButton1.Checked == true)
                {
                    Switch.BuildSwitch(inputstrings, inputcheckboxes);
                }

                //Create Curve
                if (radioButton2.Checked == true)
                {
                    
                    CurvedTrack.BuildCurve(inputstrings, inputcheckboxes);
                    
                }

                //Create Straight Track
                if (radioButton3.Checked == true)
                {
                    StraightTrack.BuildStraight(inputstrings, inputcheckboxes);
                }

                //Create Platform
                if (radioButton4.Checked == true)
                {
                    Platforms.BuildPlatform(inputstrings, inputcheckboxes);
                }
                //Create Platform
                if (radioButton10.Checked == true)
                {
                    Viaducts.BuildViaduct(inputstrings, inputcheckboxes);
                }
            }

            //No write permission error!
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have write permission to the selected path.");
                return;
            }












        }

        private void folderPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox1.Text = folderBrowserDialog1.SelectedPath ;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox_tot.ReadOnly = true;
            textBox_laenge.ReadOnly = true;
            textBox_radius.ReadOnly = false;
            checkBox1.Enabled = false;
            checkBox3.Enabled = false;
            textBox_z.ReadOnly = true;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
            textBox_platheight.ReadOnly = true;
            textBox_platwidth_near.ReadOnly = true;
            textBox_platwidth_far.ReadOnly = true;


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox_tot.ReadOnly = true;
            textBox_laenge.ReadOnly = true;
            textBox_radius.ReadOnly = true;
            checkBox1.Enabled = false;
            checkBox2.Enabled = true;
            checkBox5.Enabled = true;
            checkBox3.Enabled = false;
            textBox_z.ReadOnly = true;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
            textBox_platheight.ReadOnly = true;
            textBox_platwidth_near.ReadOnly = true;
            textBox_platwidth_far.ReadOnly = true;
            panel2.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox_tot.ReadOnly = false;
            textBox_laenge.ReadOnly = false;
            textBox_radius.ReadOnly = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox5.Enabled = true;
            checkBox3.Enabled = true;
            textBox_z.ReadOnly = false;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
            textBox_platheight.ReadOnly = true;
            textBox_platwidth_near.ReadOnly = true;
            textBox_platwidth_far.ReadOnly = true;
            panel2.Enabled = false;

        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox4.Checked == false)
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
                {
                    key.SetValue("TextureFormat", "bmp");
                }
            }
            else
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
                {

                    key.SetValue("TextureFormat", "png");
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox_tot.ReadOnly = true;
            textBox_laenge.ReadOnly = true;
            textBox_radius.ReadOnly = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox5.Enabled = false;
            checkBox3.Enabled = false;
            textBox_z.ReadOnly = true;
            radioButton5.Enabled = true;
            radioButton6.Enabled = true;
            textBox_platheight.ReadOnly = false;
            textBox_platwidth_near.ReadOnly = false;
            textBox_platwidth_far.ReadOnly = false;
            panel2.Enabled = true;
        }



        private void texturebutton_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                using (var childform = new platformtexture(launchpath))
                {
                    childform.ShowDialog(this);
                }
            }
            else
            {
                using (var childform = new texturepicker(launchpath))
                {
                    childform.ShowDialog(this);
                }
            }
        }

        public void updateballast(string filename, string texture)
        {
            ballast_file = filename;
            ballast_texture = texture;

        }

        public void updatesleeper(string filename, string texture)
        {
            sleeper_file = filename;
            sleeper_texture = texture;

        }

        public void updateembk(string filename, string texture)
        {
            embankment_file = filename;
            embankment_texture = texture;
        }

        public void updaterailtop(string filename, string texture)
        {
            railtop_file = filename;
            railtop_texture = texture;
        }

        public void updateplatform(string filename, string texture)
        {
            platform_file = filename;
            platform_texture = texture;
        }

        public void updatefence(string filename, string texture)
        {
            fence_file = filename;
            fence_texture = texture;

        }

        private void fence_yes_CheckedChanged(object sender, EventArgs e)
        {
            fenceheight_tb.Text = ("2");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString() == "English")
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
                {
                    key.SetValue("Language", "en");
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Deutsch")
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
                {
                    key.SetValue("Language", "de");
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Français")
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
                {
                    key.SetValue("Language", "fr");
                }
            }
            
                        label_radius.Text = Resources.radius;
                        label_tot.Text = Resources.deviation;
                        radioButton1.Text = Resources.pointswitch;
                        radioButton2.Text = Resources.curve;
                        radioButton3.Text = Resources.straight;
                        label_laenge.Text = Resources.lengthmultiplier;
                        label_segmente.Text = Resources.segments;
                        groupBox1.Text = Resources.advanced;
                        checkBox5.Text = Resources.railtexture;
                        checkBox4.Text = Resources.pngtexture;
                        checkBox3.Text = Resources.invertexture;
                        checkBox2.Text = Resources.embankment;
                        checkBox1.Text = Resources.pointmotor;
                        label_z.Text = Resources.zadjust;
                        folderPath.Text = Resources.path;
                        button.Text = Resources.createbutton;
                        label3.Text = Resources.platformside;
                        label4.Text = Resources.platformheight;
                        label5.Text = Resources.platformwidth_n;
                        label7.Text = Resources.platformwidth_f;
                        label6.Text = Resources.platformramp;
                        label8.Text = Resources.platformfence;
                        radioButton4.Text = Resources.platform;
                        radioButton5.Text = Resources.left;
                        radioButton6.Text = Resources.right;
                        texturebutton.Text = Resources.choosetexture;
        }
    }
}