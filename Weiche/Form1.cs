using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using Weiche.Properties;

namespace Weiche
{
    public enum PlatformType
    {
        LeftLevel,
        LeftRU,
        LeftRD,
        RightLevel,
        RightRU,
        RightRD,
    }
    public partial class Weichengenerator : Form
    {
        //Define initial texture filenames
        //Motor Texture
        public static string motor_texture;
        public static string motor_file;
        //Ballast Textures
        public static string ballast_file;
        public static string ballast_texture;
        //Sleeper Textures
        public static string sleeper_file;
        public static string sleeper_texture;
        //Embankment Textures
        public static string embankment_file;
        public static string embankment_texture;
        //Rail Textures
        public static string railtop_file;
        public static string railtop_texture;
        public static string railside_file;
        public static string railside_texture;
        public static string spez_file;
        public static string spez_texture;
        public static string spezanf_file;
        public static string spezanf_texture;
        //Platforms
        public static string platform_file;
        public static string platform_texture;
        public static double platwidth_near;
        public static double platwidth_far;
        public static double platheight;
        public static double fenceheight;
        public static PlatformType CurrentPlatformType;
        //Fence
        public static string fence_file;
        public static string fence_texture;
        public static bool hasfence;
        //Viaduct Textures
        public static string arch_texture;
        public static string arch_file;
        public static string topwall_texture;
        public static string topwall_file;
        public static string footwalk_texture;
        public static string footwalk_file;
        public static string archis_texture;
        public static string archis_file;
        //Common Values
        public static double radius;
        public static double trackgauge;
        public static double gaugeoffset;
        public static int LiRe;
        public static int LiRe_T;
        public static string name;
        public static string texture_format;
        public static bool EingabeOK;
        public static MathFunctions.Transform trans;
        public static int segmente;
        public static double Abw_tot;
        public static int laenge;
        public static int zmovement;
        
        public static string launchpath = AppDomain.CurrentDomain.BaseDirectory;
        public static string path;

        public static bool noembankment;
        public static bool norailtexture;
        public static bool inverttextures;
        public static bool pointmotor;
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
            if (string.IsNullOrEmpty(arch_file))
            {
                arch_file = ("viaduct1");
                arch_texture = (richTextBox1.Text + "\\Textures\\viaduct1.png");
            }
            if (string.IsNullOrEmpty(topwall_file))
            {
                topwall_file = ("viaduct2");
                topwall_texture = (richTextBox1.Text + "\\Textures\\viaduct2.png");
            }
            if (string.IsNullOrEmpty(footwalk_file))
            {
                footwalk_file = ("viaduct3");
                footwalk_texture = (richTextBox1.Text + "\\Textures\\viaduct3.png");
            }
            if (string.IsNullOrEmpty(archis_file))
            {
                archis_file = ("viaduct4");
                archis_texture = (richTextBox1.Text + "\\Textures\\viaduct4.png");
            }

            spez_file = "SchieneSpez";
            spez_texture = (richTextBox1.Text + "\\Textures\\SchieneSpez.png");
            spezanf_file = "SchieneSpezAnf";
            spezanf_texture = (richTextBox1.Text + "\\Textures\\SchieneSpezAnf.png");

            motor_file = "WeichAntrieb";
            motor_texture = (richTextBox1.Text + "\\Textures\\WeichAntrieb.png");


            EingabeOK = double.TryParse(textBox_radius.Text, out radius);
            if (EingabeOK == false)
            {
                MessageBox.Show("Eingabefehler Radius!");
                return;
            }
            EingabeOK = Int32.TryParse(textBox_segmente.Text, out segmente);
            if (EingabeOK == false)
            {
                MessageBox.Show("Eingabefehler Segmente!");
                return;
            }
            EingabeOK = double.TryParse(trackgauge_inp.Text, out trackgauge);
            if (EingabeOK == false)
            {
                MessageBox.Show("Invalid Track Gauge!");
                return;
            }
            else
            {
                //Is the track gauge standard?
                if (trackgauge != 1.44)
                {
                    gaugeoffset = ((trackgauge - 1.44) / 2);
                }
                else
                {
                    gaugeoffset = 0;
                }
            }
            if (richTextBox1.TextLength == 0)
            {
                MessageBox.Show("Please select a valid path!");
                return;
            }
            else
            {
                path = richTextBox1.Text;
                if (!System.IO.Directory.Exists(path))
                {
                    MessageBox.Show("Please select a valid path!");
                }

            }


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

            Weichengenerator.EingabeOK = int.TryParse(textBox_z.Text, out zmovement);
            if (Weichengenerator.EingabeOK == false)
            {
                MessageBox.Show("Eingabefehler z - Verschiebung!");
                return;
            }

            if (checkBox1.Checked)
            {
                pointmotor = true;
            }
            else
            {
                pointmotor = false;
            }
            if (checkBox2.Checked)
            {
                noembankment = true;
            }
            else
            {
                noembankment = false;
            }
            if (checkBox3.Checked)
            {
                inverttextures = true;
            }
            else
            {
                inverttextures = false;
            }
            if (checkBox5.Checked)
            {
                norailtexture = true;
            }
            else
            {
                norailtexture = false;
            }
            
            try
            {
                //Create Switch
                if (radioButton1.Checked == true)
                {
                    //Check secondary deviation is a valid number
                    EingabeOK = double.TryParse(textBox_tot.Text, out Abw_tot);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler Abweichung!");
                        return;
                    }

                    //Check length is a valid number
                    //TODO: Check length is no more than 200m
                    EingabeOK = int.TryParse(textBox_laenge.Text, out laenge);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler Weichenlänge!");
                        return;
                    }
                    //Check Z-Movement is valid
                    EingabeOK = int.TryParse(textBox_z.Text, out zmovement);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler z - Verschiebung!");
                        return;
                    }
                    Switch.BuildSwitch();
                }

                //Create Curve
                if (radioButton2.Checked == true)
                {
                    //Check radius
                    if ((radius <= 49) && (radius >= -49))
                    {
                        MessageBox.Show("Radius should be greater than 50m!");
                        return;
                    }
                    CurvedTrack.BuildCurve();
                    
                }

                //Create Straight Track
                if (radioButton3.Checked == true)
                {
                    StraightTrack.BuildStraight();
                }

                //Create Platform
                if (radioButton4.Checked == true)
                {
                    //Check radius
                    if ((radius != 0) && (radius <= 49) && (radius >= -49))
                    {
                        MessageBox.Show("Radius for platforms should either be 0 for Straight or greater than 50m!");
                        return;
                    }
                    //Check platform widths are valid numbers
                    EingabeOK = double.TryParse(textBox_platwidth_near.Text, out platwidth_near);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid Platform Width (Near)!");
                        return;
                    }
                    EingabeOK = double.TryParse(textBox_platwidth_far.Text, out platwidth_far);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid Platform Width (Far)!");
                        return;
                    }
                    //Check platform height is a valid number
                    EingabeOK = double.TryParse(textBox_platheight.Text, out platheight);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid Platform Height!");
                        return;
                    }
                    //Parse fence height into number
                    if (fence_yes.Checked == true)
                    {
                        EingabeOK = double.TryParse(fenceheight_tb.Text, out fenceheight);
                        if (EingabeOK == false)
                        {
                            MessageBox.Show("Invalid Fence Height!");
                            return;
                        }
                        if (fenceheight == 0)
                        {
                            MessageBox.Show("Fence Height should not be zero!");
                            return;
                        }
                        hasfence = true;
                    }
                    else
                    {
                        hasfence = false;
                    }
                    //Define L/R & RU/RD
                    if (radioButton5.Checked == true)
                    {
                        if (radioButton7.Checked == true)
                        {
                            CurrentPlatformType = PlatformType.LeftLevel;
                        }
                        else if (radioButton8.Checked == true)
                        {
                            CurrentPlatformType = PlatformType.LeftRU;
                        }
                        else
                        {
                            CurrentPlatformType = PlatformType.LeftRD;
                        }
                    }
                    else if (radioButton6.Checked == true)
                    {
                        if (radioButton7.Checked == true)
                        {
                            CurrentPlatformType = PlatformType.RightLevel;
                        }
                        else if (radioButton8.Checked == true)
                        {
                            CurrentPlatformType = PlatformType.RightRU;
                        }
                        else
                        {
                            CurrentPlatformType = PlatformType.RightRD;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a left or right-sided platform!");
                        return;
                    }
                    Platforms.BuildPlatform();
                }
                //Create Viaduct
                if (radioButton10.Checked == true)
                {
                    //Check radius
                    if ((radius != 0) && (radius <= 49) && (radius >= -49))
                    {
                        MessageBox.Show("Radius for viaducts should either be 0 for Straight or greater than 50m!");
                        return;
                    }
                    //Check platform widths are over 1m
                    if (platwidth_near < 1 | platwidth_far < 1)
                    {
                        MessageBox.Show("Minimum platform width is 1m!");
                        return;
                    }
                    Viaducts.BuildViaduct();
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
            else if (radioButton10.Checked == true)
            {
                using (var childform = new ViaductTexture(launchpath))
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

        public void updatetexture(string filename, string texture, int key)
        {
            switch (key)
            {
                case 1:
                    //Ballast
                    ballast_file = filename;
                    ballast_texture = texture;
                    break;
                case 2:
                    //Sleeper
                    sleeper_file = filename;
                    sleeper_texture = texture;
                    break;
                case 3:
                    //Embankment
                    embankment_file = filename;
                    embankment_texture = texture;
                    break;
                case 4:
                    //Railtop
                    railtop_file = filename;
                    railtop_texture = texture;
                    break;
                case 5:
                    //Platform surface
                    platform_file = filename;
                    platform_texture = texture;
                    break;
                case 6:
                    //Fence
                    fence_file = filename;
                    fence_texture = texture;
                    break;
                case 7:
                    //Viaduct Arch
                    arch_file = filename;
                    arch_texture = texture;
                    break;
                case 8:
                    //Viaduct top wall
                    topwall_file = filename;
                    topwall_texture = texture;
                    break;
                case 9:
                    //Viaduct footwalk
                    footwalk_file = filename;
                    footwalk_texture = texture;
                    break;
                case 10:
                    archis_file = filename;
                    archis_texture = filename;
                    //Viaduct Arch IS
                    break;
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