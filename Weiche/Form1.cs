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


        public class Transform
        {
            public Transform(int _laenge, double _eigenRadius, int _LiRe, int z)
            {
                laenge = _laenge;
                eigenRadius = _eigenRadius;
                LiRe = _LiRe;
                _z = z;
                radiusOffset = Math.Sqrt(eigenRadius * eigenRadius - 12.5 * 12.5);
            }
            public double X(double x,double z) 
            {
                if (eigenRadius != 0)
                {
                    winkel = Math.Atan((z + _z - 12.5)/radiusOffset);
                    return (radiusOffset-(Math.Cos(winkel) * (eigenRadius - x*LiRe)))*LiRe;
                }
                else
                {
                    return x;
                }
            }
            public double Z(double x,double z) 
            {
                if (eigenRadius != 0)
                {
                    winkel = Math.Atan((z + _z - 12.5) / radiusOffset);
                    return (12.5 + Math.Sin(winkel) * (eigenRadius - x*LiRe));
                }
                else
                {
                    return z + _z;
                }
            }
            int laenge;          // < in Meter
            double eigenRadius;
            double radiusOffset;
            double winkel;
            int LiRe;
            int _z;
        }

        //F�r gebogene tote Kurve mit Abweichung

        //Generate curves using deviation???

        //Paramaaters are as follows:
        //Segment??
        //Radius
        //???
        //Left or right
        public static double Abbiege_x(double z, double radius_tot, double x_Abweich,int LiReT) 
        {
	        var winkel = Math.Asin(z/radius_tot);
            return ((radius_tot - Math.Sqrt(radius_tot * radius_tot - z * z) + Math.Cos(winkel) * x_Abweich) * LiReT);
        }

        public static double Abbiege_z(double z, double radius_tot, double x_Abweich,int LiReT)
        {
            var winkel = Math.Asin(z / radius_tot);
            return (z + Math.Sin(winkel)*x_Abweich*-1);
        }

           

        public static double radius_tot(int laenge, double Abweichung)
        {
            var winkel = Math.Atan(Abweichung / (laenge*25));
            var radius_t = laenge*25 / (Math.Sin(2 * winkel));
            return (radius_t);
        }

        public static double winkel_tot(int laenge, double Abweichung)
        {
            return (Math.Atan(Abweichung / (laenge*25)));
        }

        public static double x_Weichenoefnung(double radius, double z, double Endkoordinate)
        {
            return (radius - Math.Sqrt(radius * radius - (25 - 25 * z / Endkoordinate) * (25 - 25 * z / Endkoordinate)));
        }

        public static void SetTexture(StreamWriter sw, int Faktor,double TexturMulti, int Ausrichtung)
        {
            var b = 0;
            switch (Ausrichtung)
            {
                case 1:
                    //Generic Left
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }
                    break;
                case 2:
                    //Generic Right
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }
                    break;
                case 3:
                    //Generic Up
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }
                    break;
                case 4:
                    //Generic Down
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }
                    break;
                case 5:
                    //Ballast Shoulder Inside R
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},-1,{1:f4},", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }
                    break;
            }
        }

        public static void SetPlatformTexture(StreamWriter sw, int Faktor, double TexturMulti, int Ausrichtung, double platwidth_near, double platwidth_far, double segmente)
        {
            var b = 0;
            switch (Ausrichtung)
            {

                case 1:
                    //Platform Single Mesh
                    
                    var c = 0;
                    for (double i = Faktor; i >= 0; i--)
                    {
                        //5m wide nominal width- Return zero for both
                        if (platwidth_near == 5 && platwidth_far == 5)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, i * TexturMulti / Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.67,", b + 1, i * TexturMulti / Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.79,", b + 2, i * TexturMulti / Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.8,", b + 3, i * TexturMulti / Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.81,", b + 4, i * TexturMulti / Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 5, i * TexturMulti / Faktor);
                        }
                        //Are both widths equal?
                        else
                        {
                            double calculated_texture;
                            if (platwidth_near == platwidth_far)
                            {
                            
                                if (platwidth_far < 5)
                                {
                                    //Calculate texture for below 5m width
                                    calculated_texture = (0.67 - (0.1675 * (platwidth_far)));
                                }
                                else
                                {
                                    //Otherwise return zero and stretch
                                    calculated_texture = 0;
                                }
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2},", b, i * TexturMulti / Faktor, calculated_texture);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.67,", b + 1, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.79,", b + 2, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.8,", b + 3, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.81,", b + 4, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 5, i * TexturMulti / Faktor);
                            }
                                //If platform is wider at far end
                            else if (platwidth_near < platwidth_far)
                            {
                                double far_texture;
                                double near_texture;
                                //Calculate co-ordinates for far end texture
                                if (platwidth_far <= 5)
                                {
                                    //Calculate texture for below 5m width
                                    far_texture = (0.67 - (0.1675 * (platwidth_far -1)));
                                }
                                else
                                {
                                    far_texture = 0;
                                }

                                if (platwidth_near <= 5)
                                {
                                    //Calculate texture for below 5m width
                                    near_texture = (0.67 - (0.1675 * (platwidth_near -1)));
                                }
                                else
                                {
                                    near_texture = 0;
                                }
                                calculated_texture = (((near_texture - far_texture) / segmente) * i);
                                //calculated_texture = ((far_texture - near_texture) / segmente) * i;
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2},", b, i * TexturMulti / Faktor, calculated_texture);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.67,", b + 1, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.79,", b + 2, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.8,", b + 3, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.81,", b + 4, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 5, i * TexturMulti / Faktor);
                            }
                            else
                            {
                                //Platform is wider at near end 
                                double far_texture;
                                double near_texture;
                                //Calculate co-ordinates for far end texture
                                if (platwidth_far < 5)
                                {
                                    //Calculate texture for below 5m width
                                    far_texture = (0.67 - (0.1675 * (platwidth_far - 1)));
                                }
                                else
                                {
                                    far_texture = 0;
                                }

                                if (platwidth_near < 5)
                                {
                                    //Calculate texture for below 5m width
                                    near_texture = (0.67 - (0.1675 * (platwidth_near - 1)));
                                }
                                else
                                {
                                    near_texture = 0;
                                }
                                calculated_texture = (((far_texture - near_texture) / segmente) * c);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2},", b, i * TexturMulti / Faktor,calculated_texture);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.67,", b + 1, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.79,", b + 2, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.8,", b + 3, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.81,", b + 4, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 5, i * TexturMulti / Faktor);
                                c++;
                            }
                        }
                        b = b + 6;
                    }
                    break;
                    
                    case 2:
                    //Platform fence
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b + 1, i * TexturMulti / Faktor);
                        b = b + 2;
                    }

                    break;

            }
        }


        public static void AddFace(StreamWriter sw, int Faktor,int LiRe_T)
        {
                var face = new double[4];
                if (LiRe_T > 0)
                {
                    face[0] = 2;
                    face[1] = 3;
                    face[2] = 1;
                    face[3] = 0;
                }
                else
                {
                    face[0] = 3;
                    face[1] = 2;
                    face[2] = 0;
                    face[3] = 1;
                }


                for (var i = 0; i < Faktor; i++)
                {
                    sw.WriteLine("AddFace,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                    face[0] += 2;
                    face[1] += 2;
                    face[2] += 2;
                    face[3] += 2;
                }
        }

        public static void AddFace2_New(StreamWriter sw, int Faktor, int LiRe_T)
        {
            var face = new double[4];
            if (LiRe_T > 0)
            {
                face[0] = 2;
                face[1] = 3;
                face[2] = 1;
                face[3] = 0;
            }
            else
            {
                face[0] = 3;
                face[1] = 2;
                face[2] = 0;
                face[3] = 1;
            }


            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }

        //New face constructor for platforms
        public static void PlatFace(StreamWriter sw, int Faktor, int LiRe_T)
        {
            var face = new double[20];
            if (LiRe_T > 0)
            {
                //Platform Top P1
                face[0] = 0;
                face[1] = 1;
                face[2] = 7;
                face[3] = 6;
                //Platform Top P2
                face[4] = 1;
                face[5] = 2;
                face[6] = 8;
                face[7] = 7;
                //Toe Front
                face[8] = 2;
                face[9] = 3;
                face[10] = 9;
                face[11] = 8;
                //Toe Bottom
                face[12] = 3;
                face[13] = 4;
                face[14] = 10;
                face[15] = 9;
                //Vertical Drop
                face[16] = 4;
                face[17] = 5;
                face[18] = 11;
                face[19] = 10;
            }
            else
            {
                //Platform Top P1
                face[0] = 6;
                face[1] = 7;
                face[2] = 1;
                face[3] = 0;
                //Platform Top P2
                face[4] = 7;
                face[5] = 8;
                face[6] = 2;
                face[7] = 1;
                //Toe Front
                face[8] = 8;
                face[9] = 9;
                face[10] = 3;
                face[11] = 2;
                //Toe Bottom
                face[12] = 9;
                face[13] = 10;
                face[14] = 4;
                face[15] = 3;
                //Vertical Drop
                face[16] = 10;
                face[17] = 11;
                face[18] = 5;
                face[19] = 4;
            }


            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 6;
                face[1] += 6;
                face[2] += 6;
                face[3] += 6;
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[4], face[5], face[6], face[7]);
                face[4] += 6;
                face[5] += 6;
                face[6] += 6;
                face[7] += 6;
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[8], face[9], face[10], face[11]);
                face[8] += 6;
                face[9] += 6;
                face[10] += 6;
                face[11] += 6;
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[12], face[13], face[14], face[15]);
                face[12] += 6;
                face[13] += 6;
                face[14] += 6;
                face[15] += 6;
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[16], face[17], face[18], face[19]);
                face[16] += 6;
                face[17] += 6;
                face[18] += 6;
                face[19] += 6;
            }
        }

        public static void AddFace2(StreamWriter sw, int Faktor)
        {
            var face = new double[4];

                face[0] = 2;
                face[1] = 3;
                face[2] = 1;
                face[3] = 0;

            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
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
            else if (comboBox1.SelectedItem.ToString() == "Fran�ais")
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