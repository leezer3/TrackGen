using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Web;
using Microsoft.Win32;

namespace Weiche
{
    public partial class Weichengenerator : Form
    {
        //Define initial texture filenames
        
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
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            InitializeComponent();
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen", true))
            {
                if (key != null)
                {
                    var language = key.GetValue("Language");
                    var textureformat = key.GetValue("TextureFormat");
                    //Set Language
                    if (Convert.ToString(language) == "en")
                    {
                        //English
                        label_radius.Text = "Curve radius for player's track OR deviation (In meters)";
                        label_tot.Text = "Deviation for diverging track (In meters)";
                        radioButton1.Text = "Switch";
                        radioButton2.Text = "Curve";
                        radioButton3.Text = "Straight";
                        label_laenge.Text = "Switch Length Multiplication (25,50,75,..) ";
                        label_segmente.Text = "Number of segments";
                        groupBox1.Text = "Advanced options";
                        checkBox5.Text = "Do not texture rails";
                        checkBox4.Text = "Use PNG textures";
                        checkBox3.Text = "Invert textures";
                        checkBox2.Text = "Without embankment";
                        checkBox1.Text = "Add point motor";
                        label_z.Text = "Z - adjustment";
                        folderPath.Text = "Path";
                        button.Text = "Create";
                        label3.Text = "Platform Side";
                        label4.Text = "Platform height (in m)";
                        label5.Text = "Platform Width (Near)";
                        label7.Text = "Plarform Width (Far)";
                        label6.Text = "Platform Ramp";
                        label8.Text = "Platform Fence";
                        texturebutton.Text = "Choose Textures...";
                        radioButton5.Text = "Left";
                        radioButton6.Text = "Right";
                    }
                    if (Convert.ToString(language) == "de")
                    {
                        //German
                        label_radius.Text = "Radius/Abweichung der eigenen Spur (in m) ";
                        label_tot.Text = "Abweichung der Totspur (in m) ";
                        radioButton1.Text = "Weiche";
                        radioButton2.Text = "Kurve";
                        radioButton3.Text = "Gerade";
                        label_laenge.Text = "Weichenlänge (25,50,75,..) ";
                        label_segmente.Text = "Anzahl Segmente";
                        groupBox1.Text = "Erweiterte Optionen";
                        checkBox5.Text = "Nicht Textur Schiene";
                        checkBox4.Text = "PNG Textur";
                        checkBox3.Text = "Textur Invertieren";
                        checkBox2.Text = "Ohne Böschung";
                        checkBox1.Text = "Weichenantrieb";
                        label_z.Text = "z - Verschiebung";
                        folderPath.Text = "Pfad";
                        button.Text = "Erstellen";
                        label3.Text = "Bahnsteigseite";
                        label4.Text = "Plattformhöhe (in m)";
                        label5.Text = "Plattformbreite (Nahe)";
                        label7.Text = "Plattformbreite (Weit)";
                        label6.Text = "Plattform Rampe";
                        label8.Text = "Plattform Zaun";
                        texturebutton.Text = "Texturen Wählen...";
                        radioButton5.Text = "Links";
                        radioButton6.Text = "Recht";
                    }
                    else
                    {
                        //If the registry key isn't what we expect
                        //Return English & reset value
                        key.SetValue("Language", "en");
                        label_radius.Text = "Curve radius for player's track OR deviation (In meters)";
                        label_tot.Text = "Deviation for diverging track (In meters)";
                        radioButton1.Text = "Switch";
                        radioButton2.Text = "Curve";
                        radioButton3.Text = "Straight";
                        label_laenge.Text = "Switch Length Multiplication (25,50,75,..) ";
                        label_segmente.Text = "Number of segments";
                        groupBox1.Text = "Advanced options";
                        checkBox5.Text = "Do not texture rails";
                        checkBox4.Text = "Use PNG textures";
                        checkBox3.Text = "Invert textures";
                        checkBox2.Text = "Without embankment";
                        checkBox1.Text = "Add point motor";
                        label_z.Text = "Z - adjustment";
                        folderPath.Text = "Path";
                        button.Text = "Create";
                        label3.Text = "Platform Side";
                        label4.Text = "Platform height (in m)";
                        label5.Text = "Platform Width (Near)";
                        label7.Text = "Plarform Width (Far)";
                        label6.Text = "Platform Ramp";
                        label8.Text = "Platform Fence";
                        texturebutton.Text = "Choose Textures...";
                        radioButton5.Text = "Left";
                        radioButton6.Text = "Right";
                    }

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
            int laenge;          ///< in Meter
            double eigenRadius;
            double radiusOffset;
            double winkel;
            int LiRe;
            int _z;
        }

        //Für gebogene tote Kurve mit Abweichung

        //Generate curves using deviation???
        static double Abbiege_x(double z, double radius_tot, double x_Abweich,int LiReT) 
        {
	        double winkel = Math.Asin(z/radius_tot);
            return ((radius_tot - Math.Sqrt(radius_tot * radius_tot - z * z) + Math.Cos(winkel) * x_Abweich) * LiReT);
        }

        static double Abbiege_z(double z, double radius_tot, double x_Abweich,int LiReT)
        {
            double winkel = Math.Asin(z / radius_tot);
            return (z + Math.Sin(winkel)*x_Abweich*-1);
        }

           

        static double radius_tot(int laenge, double Abweichung)
        {
            double winkel = Math.Atan(Abweichung / (laenge*25));
            double radius_t = laenge*25 / (Math.Sin(2 * winkel));
            return (radius_t);
        }

        static double winkel_tot(int laenge, double Abweichung)
        {
            return (Math.Atan(Abweichung / (laenge*25)));
        }

        static double x_Weichenoefnung(double radius, double z, double Endkoordinate)
        {
            return (radius - Math.Sqrt(radius * radius - (25 - 25 * z / Endkoordinate) * (25 - 25 * z / Endkoordinate)));
        }

        static void SetTexture(StreamWriter sw, int Faktor,double TexturMulti, int Ausrichtung)
        {
            int b = 0;
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

        static void SetPlatformTexture(StreamWriter sw, int Faktor, double TexturMulti, int Ausrichtung, double platwidth_near, double platwidth_far, double segmente)
        {
            int b = 0;
            switch (Ausrichtung)
            {

                case 1:
                    //Platform Single Mesh
                    
                    int c = 0;
                    double calculated_texture = 0;
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
                        else if (platwidth_near == platwidth_far)
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
                            double far_texture = 0;
                            double near_texture= 0;
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
                            double far_texture = 0;
                            double near_texture = 0;
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


        static void AddFace(StreamWriter sw, int Faktor,int LiRe_T)
        {
                double[] face = new double[4];
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


                for (int i = 0; i < Faktor; i++)
                {
                    sw.WriteLine("AddFace,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                    face[0] += 2;
                    face[1] += 2;
                    face[2] += 2;
                    face[3] += 2;
                }
        }

        static void AddFace2_New(StreamWriter sw, int Faktor, int LiRe_T)
        {
            double[] face = new double[4];
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


            for (int i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }

        //New face constructor for platforms
        static void PlatFace(StreamWriter sw, int Faktor, int LiRe_T)
        {
            double[] face = new double[20];
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


            for (int i = 0; i < Faktor; i++)
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

        static void AddFace2(StreamWriter sw, int Faktor)
        {
            double[] face = new double[4];

                face[0] = 2;
                face[1] = 3;
                face[2] = 1;
                face[3] = 0;

            for (int i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }



        static void ConvertAndMove(string launchdir, string inputfile, string outputtex, string filename, string output)
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
                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(@inputfile);
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
                        System.Drawing.Image image2 = System.Drawing.Image.FromFile(@inputfile);
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
            double radius;
            double Abw_tot;
            double radiusT;
            int laenge;
            int LiRe = 1;
            int LiRe_T = 1;
            bool EingabeOK;
            double segmente;
            double trackgauge;
            double gaugeoffset;
            double platheight;
            double platwidth_near;
            double platwidth_far;
            double platwidth = 0;
            double fenceheight = 0;
            double[] face = new double[4];
            int z;
            string name;
            string texture_format;
            
            Transform trans;

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

            string spez_file = "SchieneSpez";
            string spez_texture = (richTextBox1.Text + "\\Textures\\SchieneSpez.png");
            string spezanf_file = "SchieneSpezAnf";
            string spezanf_texture = (richTextBox1.Text + "\\Textures\\SchieneSpezAnf.png");

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
            bool pathExists = System.IO.Directory.Exists(richTextBox1.Text);
            if (!pathExists)
            {
                MessageBox.Show("Path does not exist.");
                return;
            }

            
            try
            {
                //Create Switch
                if (radioButton1.Checked == true)
                {

                    //Switches


                    //Initialise

                    //Check radius/ deviation is a valid number
                    EingabeOK = double.TryParse(textBox_radius.Text, out radius);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid Radius!");
                        return;
                    }

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

                    //Check segments are a valid number 
                    
                    //TODO: Check for sensible number of segments
                    //Assume a max of 50 segments per 25m
                    EingabeOK = double.TryParse(textBox_segmente.Text, out segmente);
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
                            //We aren't standard gauge
                            //Now calculate the deviation
                            
                        }
                        else
                        {
                            gaugeoffset = 0;
                        }
                    }

                    //Create Output directory
                    if (!System.IO.Directory.Exists(richTextBox1.Text + "\\Output\\Tracks"))
                    {
                        System.IO.Directory.CreateDirectory(richTextBox1.Text + "\\Output\\Tracks");
                    }


                    //Check path is valid
                    if (richTextBox1.TextLength == 0)
                    {
                        MessageBox.Show("Geben sie einen Pfad an!");
                        return;
                    }

                    //Check Z-Movement is valid
                    EingabeOK = int.TryParse(textBox_z.Text, out z);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler z - Verschiebung!");
                        return;
                    }

                    if (z != 0)
                    {
                        name = (richTextBox1.Text) + "\\Output\\Tracks\\W" + radius + "m_" + Abw_tot + "m_" + 25 * laenge + "m_" + z + "z.csv";
                    }
                    else
                    {
                        name = (richTextBox1.Text) + "\\Output\\Tracks\\W" + radius + "m_" + Abw_tot + "m_" + 25 * laenge + "m.csv";
                    }


                    //Main Textures
                    string outputtype = "Tracks";
                    ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                    ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);
                    
                    //Spez Textures
                    ConvertAndMove(launchpath, spez_texture, texture_format, spez_file, outputtype);
                    ConvertAndMove(launchpath, spezanf_texture, texture_format, spezanf_file, outputtype);

                    //Embankment Texture
                    if (checkBox2.Checked == false)
                    {
                        ConvertAndMove(launchpath, embankment_texture, texture_format, embankment_file, outputtype);
                    }

                    //Rail Textures
                    if (checkBox5.Checked == false)
                    {
                        ConvertAndMove(launchpath, railside_texture, texture_format, railside_file, outputtype);
                        ConvertAndMove(launchpath, railtop_texture, texture_format, railtop_file, outputtype);
                    }

                    //LiRe Definition
                    if (radius < 0)
                    {
                        LiRe = -1;
                        radius = radius * -1;
                    }
                    if (Abw_tot < 0)
                    {
                        LiRe_T = -1;
                        Abw_tot = Abw_tot * -1;
                    }

                    //Convert deviation to radius
                    if (radius < 50 && radius > 0)
                    {
                        radius = 12.5 * 25 * laenge * laenge / radius;
                    }

                    trans = new Transform(laenge, radius, LiRe, z);
                    

                    //Calculate Frog

                    radiusT = radius_tot(laenge, Abw_tot);

                    // Co-ordinates 1- Top of switch
                    /*
                    double[] K1 = new double[2];
                    K1[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                    K1[0] = (0.72 + gaugeoffset) * LiRe_T;

                     * Did we need to move these???
                     * Try below.
                     */
                    double[] K1 = new double[2];
                    K1[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                    K1[0] = (0.72 + gaugeoffset) * LiRe_T;

                    // Co-ordinates 2- Left End
                    double[] K2 = new double[2];
                    K2[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset) - 0.15) * (radiusT - (0.72 + gaugeoffset) - 0.15));
                    K2[0] = (radiusT - Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - K2[1] * K2[1])) * LiRe_T;

                    //Co-ordinate 3- Right End
                    double[] K3 = new double[2];
                    K3[1] = K2[1] + Math.Tan(winkel_tot(laenge, Abw_tot)) * (K2[0] * LiRe_T - (0.72 + gaugeoffset));
                    K3[0] = (0.72 + gaugeoffset) * LiRe_T;

                    //Co-ordinates 4- Toe of point
                    double[] K4 = new double[2];
                    K4[1] = K3[1];
                    K4[0] = (K2[0] - K3[0]) / 2 + K3[0];
                    
                    //Write out to CSV

                    using (StreamWriter sw = new StreamWriter(name))
                    {
                        sw.WriteLine(";c by Nils Busch");
                        sw.WriteLine(";Created with Weiche");
                        sw.WriteLine(";Radius: {0}", radius);
                        sw.WriteLine(";Abweichung Totspur: {0}", Abw_tot);
                        sw.WriteLine(";Radius Totspur: {0}", radiusT);

                        sw.WriteLine("\r\r\nCreateMeshBuilder");       //Herzstück 1 Hälfte
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K1[0], K1[1]), trans.Z(K1[0], K1[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K2[0], K2[1]), trans.Z(K2[0], K2[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K4[0], K4[1]), trans.Z(K4[0], K4[1]));

                        if (LiRe_T >= 0)
                        {
                            sw.WriteLine("AddFace,0,2,1,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,0,1,2,");
                        }

                        sw.WriteLine("GenerateNormals,");

                        if (!checkBox5.Checked)
                        {
                            
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            sw.WriteLine("SetTextureCoordinates,0,0,1,");
                            sw.WriteLine("SetTextureCoordinates,1,0,0,");
                            sw.WriteLine("SetTextureCoordinates,2,0.5,0,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }
                        sw.WriteLine("\r\r\nCreateMeshBuilder");       //Herzstück 2 Hälfte
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K4[0], K4[1]), trans.Z(K4[0], K4[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K3[0], K3[1]), trans.Z(K3[0], K3[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K1[0], K1[1]), trans.Z(K1[0], K1[1]));

                        if (LiRe_T >= 0)
                        {
                            sw.WriteLine("AddFace,0,2,1,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,0,1,2,");
                        }

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            sw.WriteLine("SetTextureCoordinates,0,0.5,0,");
                            sw.WriteLine("SetTextureCoordinates,1,0.9,0,");
                            sw.WriteLine("SetTextureCoordinates,2,0.9,1,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }

                        // Schienen oberhalb des Herzstücks
                        // Schiene Totspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K2[0] - 0.06 * LiRe_T, K2[1]), trans.Z(K2[0], K2[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K2[0], K2[1]), trans.Z(K2[0], K2[1]));

                        double j = 0;
                        while (j < K3[1])
                        {
                            j = j + 25 / segmente;
                        }
                        j = j + 25 / segmente;

                        int a = 0;
                        for (; j <= 25 * laenge; j = j + 25 / segmente, a++)
                        {

                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }



                        // Schiene Spielerspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K3[0] + 0.06 * LiRe_T, K3[1]), trans.Z(K3[0] + 0.06 * LiRe_T, K3[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K3[0], K3[1]), trans.Z(K3[0], K3[1]));

                        j = 0;
                        while (j < K3[1])
                        {
                            j = j + 25 / segmente;
                        }
                        j = j + 25 / segmente;

                        a = 0;
                        for (; j <= 25 * laenge; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.78 + gaugeoffset) * LiRe_T, j), trans.Z((0.78 + gaugeoffset) * LiRe_T, j));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.72 + gaugeoffset) * LiRe_T, j), trans.Z((0.72 + gaugeoffset) * LiRe_T, j));
                        }

                        AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }



                        //Gerade Spielerspur Vorne
                        //It's the frog that's the problem....
                        double[] K5 = new double[2];
                        K5[0] = (0.72 + gaugeoffset) * LiRe_T;
                        K5[1] = Math.Sqrt((radiusT + (0.67 + gaugeoffset)) * (radiusT + (0.67 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                        double[] K6 = new double[2];
                        K6[0] = (0.78 + gaugeoffset) * LiRe_T;
                        K6[1] = Math.Sqrt((radiusT + (0.61 + gaugeoffset)) * (radiusT + (0.61 + gaugeoffset)) - (radiusT - (0.78 + gaugeoffset)) * (radiusT - (0.78 + gaugeoffset)));

                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Gerade Spielerspur Vorne");
                        a = 0;
                        for (int i = 0; (25 / segmente) * i <= K6[1]; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.72+ gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((0.72 +gaugeoffset) * LiRe_T, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.78+ gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((0.78 +gaugeoffset) * LiRe_T, (25 / segmente) * i));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K5[0], K5[1]), trans.Z(K5[0], K5[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K6[0], K6[1]), trans.Z(K6[0], K6[1]));

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }



                        //Totspur vorne

                        double[] K7 = new double[2];
                        K7[0] = (0.61+ gaugeoffset) * LiRe_T;
                        K7[1] = Math.Sqrt((radiusT + (0.78+ gaugeoffset)) * (radiusT + (0.78+ gaugeoffset)) - (radiusT - (0.61+ gaugeoffset)) * (radiusT - (0.61+ gaugeoffset)));
                        double[] K8 = new double[2];
                        K8[0] = (0.67 + gaugeoffset) * LiRe_T;
                        K8[1] = Math.Sqrt((radiusT + (0.72+ gaugeoffset)) * (radiusT + (0.72+ gaugeoffset)) - (radiusT - (0.67+ gaugeoffset)) * (radiusT - (0.67+ gaugeoffset)));
                        double[] K11 = new double[2];
                        K11[1] = Math.Sqrt((radiusT + (0.78+ gaugeoffset)) * (radiusT + (0.78+ gaugeoffset)) - (radiusT + 0) * (radiusT + 0));
                        K11[0] = 0;
                        double[] K12 = new double[2];
                        K12[0] = Abbiege_x(K11[1], radiusT, (-0.72 - gaugeoffset), LiRe_T);
                        K12[1] = Abbiege_z(Math.Sqrt((radiusT + (0.78+ gaugeoffset)) * (radiusT + (0.78+ gaugeoffset)) - (radiusT + 0) * (radiusT + 0)), radiusT, (-0.72- gaugeoffset), LiRe_T);


                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Totspur vorne");

                        a = 1;
                        for (double i = 0; i <= K11[1]; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-0.72- gaugeoffset), LiRe_T) + x_Weichenoefnung(4200, i, K11[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.72- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-0.72- gaugeoffset), LiRe_T) + x_Weichenoefnung(4200, i, K11[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.72- gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-0.78- gaugeoffset), LiRe_T) + x_Weichenoefnung(3000, i, K11[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.78- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-0.78- gaugeoffset), LiRe_T) + x_Weichenoefnung(3000, i, K11[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.78- gaugeoffset), LiRe_T)));
                        }



                        j = 0;
                        while (j < K11[1])
                        {
                            j = j + 25 / segmente;
                        }

                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K12[0], K12[1]), trans.Z(K12[0], K12[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K11[0], K11[1]), trans.Z(K11[0], K11[1]));
                        for (; j <= K7[1]; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.72- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.72- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.72- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.72- gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.78- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.78- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.78- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.78- gaugeoffset), LiRe_T)));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K8[0], K8[1]), trans.Z(K8[0], K8[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K7[0], K7[1]), trans.Z(K7[0], K7[1]));

                        AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }

                        //Flügelschiene Kurve Spielerspur

                        double[] K18 = new double[2];
                        K18[0] = (0.78+ gaugeoffset) * LiRe_T;
                        K18[1] = Math.Sqrt((radiusT + (0.67+ gaugeoffset)) * (radiusT + (0.67+ gaugeoffset)) - (radiusT - (0.78+ gaugeoffset)) * (radiusT - (0.78+ gaugeoffset)));

                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Fluegelschiene Totspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K5[0], K5[1]), trans.Z(K5[0], K5[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K18[0], K18[1]), trans.Z(K18[0], K18[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K6[0], K6[1]), trans.Z(K6[0], K6[1]));
                        if (LiRe_T < 0)
                        {
                            sw.WriteLine("AddFace,2,1,0,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,0,1,2,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            sw.WriteLine("SetTextureCoordinates,0,0,1,");
                            sw.WriteLine("SetTextureCoordinates,1,0,0,");
                            sw.WriteLine("SetTextureCoordinates,2,1,1,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Flügelschiene Spielerspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K18[0], K18[1]), trans.Z(K18[0], K18[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K6[0], K6[1]), trans.Z(K6[0], K6[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K3[1], radiusT, (-0.62- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.62- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K3[1], radiusT, (-0.62- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.62- gaugeoffset), LiRe_T)));
                        a = 1;
                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                            sw.WriteLine("SetColor,169,140,114,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }


                        //Flügelschiene Gerade Totspur

                        double[] K17 = new double[2];
                        K17[0] = (0.67+ gaugeoffset) * LiRe_T;
                        K17[1] = Math.Sqrt((radiusT + (0.78+ gaugeoffset)) * (radiusT + (0.78+ gaugeoffset)) - (radiusT - (0.67+ gaugeoffset)) * (radiusT - (0.67+ gaugeoffset)));

                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Fluegelschiene Totspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K7[0], K7[1]), trans.Z(K7[0], K7[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K17[0], K17[1]), trans.Z(K17[0], K17[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K8[0], K8[1]), trans.Z(K8[0], K8[1]));
                        if (LiRe_T < 0)
                        {
                            sw.WriteLine("AddFace,2,1,0,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,0,1,2,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {

                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            sw.WriteLine("SetTextureCoordinates,0,1,1,");
                            sw.WriteLine("SetTextureCoordinates,1,0,0,");
                            sw.WriteLine("SetTextureCoordinates,2,0,1,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Fluegelschiene Spielerspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K7[0], K7[1]), trans.Z(K7[0], K7[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K17[0], K17[1]), trans.Z(K17[0], K17[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.62+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)), trans.Z((0.62+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.67+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)), trans.Z((0.67+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.67- gaugeoffset), LiRe_T)));
                        a = 1;
                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                            sw.WriteLine("SetColor,169,140,114,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }



                        //Gerade Schiene Außen

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Gerade Schiene Aussen");
                        for (int i = 0; i <= segmente * laenge; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.72- gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((-0.72- gaugeoffset) * LiRe_T, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.78- gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((-0.78- gaugeoffset) * LiRe_T, (25 / segmente) * i));
                        }

                        AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }

                        //Radlenker Spielerspur
                        

                        //Do we need to move the 1.65 numbers here???


                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Radlenker Spielerspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] - 1.65), trans.Z((-0.66- gaugeoffset) * LiRe_T, K1[1] - 1.65));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.62- gaugeoffset) * LiRe_T, K1[1] - 1.65), trans.Z((-0.62- gaugeoffset) * LiRe_T, K1[1] - 1.65));
                        a = 1;
                        for (double i = -1.5; i <= 1.5; i = i + 1, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.68- gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.68- gaugeoffset) * LiRe_T, K1[1] + i));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64- gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.64- gaugeoffset) * LiRe_T, K1[1] + i));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] + 1.65), trans.Z((-0.66 - gaugeoffset) * LiRe_T, K1[1] + 1.65));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.62 - gaugeoffset) * LiRe_T, K1[1] + 1.65), trans.Z((-0.62 - gaugeoffset) * LiRe_T, K1[1] + 1.65));

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                            sw.WriteLine("SetColor,169,140,114,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }


                        //Radlenker Totspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Radlenker Totspur");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(Abbiege_x(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)),
                            trans.Z(Abbiege_x(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(Abbiege_x(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)),
                            trans.Z(Abbiege_x(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)));
                        a = 1;
                        for (double i = -1.5; i <= 1.5; i = i + 1, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                                trans.X(Abbiege_x(K1[1] + i, radiusT, (0.68+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.68+ gaugeoffset), LiRe_T)),
                                trans.Z(Abbiege_x(K1[1] + i, radiusT, (0.68+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.68+ gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                                trans.X(Abbiege_x(K1[1] + i, radiusT, (0.64+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.64+ gaugeoffset), LiRe_T)),
                                trans.Z(Abbiege_x(K1[1] + i, radiusT, (0.64+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.64+ gaugeoffset), LiRe_T)));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(Abbiege_x(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)),
                            trans.Z(Abbiege_x(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(Abbiege_x(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)),
                            trans.Z(Abbiege_x(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)));

                        AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                            sw.WriteLine("SetColor,169,140,114,");
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }


                        // Abbiegende Schine Außen(Totspur)

                        a = -1;

                        sw.WriteLine("\r\r\nCreateMeshBuilder; Abbiegende Schiene Aussen (Totspur)");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0.001,{1:f4},", trans.X(Abbiege_x(i, radiusT, (0.72+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.72+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (0.72+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.72+ gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},0.001,{1:f4},", trans.X(Abbiege_x(i, radiusT, (0.78+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.78+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (0.78+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.78+ gaugeoffset), LiRe_T)));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }

                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }



                        ////////////////////////////////////////
                        ///////////SchienenMittelteil //////////
                        ////////////////////////////////////////






                        //Railside Gerade Schiene Außen

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.74- gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i), trans.Z((-0.74- gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.74- gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i), trans.Z((-0.74- gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Railside Abbiegende Schine Außen(Totspur)

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(i, radiusT, (0.74+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.74+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (0.74+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.74+ gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (0.74+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.74+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (0.74+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (0.74+ gaugeoffset), LiRe_T)));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        // Railside Hinter Herzstück - Spielerspur

                        double[] K13 = new double[2];
                        K13[0] = (0.74+ gaugeoffset) * LiRe_T;
                        K13[1] = Math.Sqrt((radiusT + (0.74+ gaugeoffset)) * (radiusT + (0.74+ gaugeoffset)) - (radiusT - (0.74+ gaugeoffset)) * (radiusT - (0.74+ gaugeoffset)));

                        j = 0;
                        while (j < K13[1])
                        {
                            j = j + 25 / segmente;
                        }
                        a = 0;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K13[0], K13[1]), trans.Z(K13[0], K13[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K13[0], K13[1]), trans.Z(K13[0], K13[1]));
                        for (; j <= 25 * laenge; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74+ gaugeoffset) * LiRe_T, j), trans.Z((0.74+ gaugeoffset) * LiRe_T, j));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74+ gaugeoffset) * LiRe_T, j), trans.Z((0.74+ gaugeoffset) * LiRe_T, j));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }
                        // Railside Hinter Herzstück - Totspur

                        j = 0;
                        while (j < K13[1])
                        {
                            j = j + 25 / segmente;
                        }
                        a = 0;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K13[0], K13[1]), trans.Z(K13[0], K13[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K13[0], K13[1]), trans.Z(K13[0], K13[1]));

                        for (; j <= 25 * laenge; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        // Railside Vor Herzstück - Spielerspur

                        double[] K15 = new double[2];
                        K15[0] = (0.74+ gaugeoffset) * LiRe_T;
                        K15[1] = Math.Sqrt((radiusT + (0.65+ gaugeoffset)) * (radiusT + (0.65+ gaugeoffset)) - (radiusT - (0.74+ gaugeoffset)) * (radiusT - (0.74+ gaugeoffset)));

                        a = 0;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (j = 0; j <= K15[1]; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74+ gaugeoffset) * LiRe_T, j), trans.Z((0.74+ gaugeoffset) * LiRe_T, j));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74+ gaugeoffset) * LiRe_T, j), trans.Z((0.74+ gaugeoffset) * LiRe_T, j));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Flügelrailside Totspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T), Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)));
                        a = 1;
                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }


                        //Flügelrailside Gerade Spielerspur

                        double[] K14 = new double[2];
                        K14[0] = (0.65+ gaugeoffset) * LiRe_T;
                        K14[1] = Math.Sqrt((radiusT + (0.74+ gaugeoffset)) * (radiusT + (0.74+ gaugeoffset)) - (radiusT - (0.65+ gaugeoffset)) * (radiusT - (0.65+ gaugeoffset)));

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.65+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.65+ gaugeoffset), LiRe_T)), trans.Z((0.65+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.65+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)), trans.Z((0.65+ gaugeoffset) * LiRe_T, Abbiege_z(K3[1], radiusT, (-0.65- gaugeoffset), LiRe_T)));
                        a = 1;
                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //TotspurRailside vorne

                        double[] K16 = new double[2];
                        K16[1] = Math.Sqrt((radiusT + (0.74+ gaugeoffset)) * (radiusT + (0.74+ gaugeoffset)) - (radiusT) * (radiusT));
                        K16[0] = 0;


                        sw.WriteLine("\r\r\nCreateMeshBuilder");

                        a = 1;
                        for (double i = 0; i <= K16[1]; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-0.74- gaugeoffset), LiRe_T) + x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-0.74- gaugeoffset), LiRe_T) + x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-0.74- gaugeoffset), LiRe_T) + x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-0.74- gaugeoffset), LiRe_T) + x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, Abbiege_z(i, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                        }

                        j = 0;
                        while (j < K16[1])
                        {
                            j = j + 25 / segmente;
                        }

                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K16[0], K16[1]), trans.Z(K16[0], K16[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K16[0], K16[1]), trans.Z(K16[0], K16[1]));
                        for (; j <= K14[1]; j = j + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(j, radiusT, (-0.74- gaugeoffset), LiRe_T), Abbiege_z(j, radiusT, (-0.74- gaugeoffset), LiRe_T)));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Radlenker Railside Spielerspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64- gaugeoffset) * LiRe_T, K1[1] - (1.65+ gaugeoffset)), trans.Z((-0.64- gaugeoffset) * LiRe_T, K1[1] - 1.65));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] - (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] - 1.65));
                        a = 1;
                        for (double i = -1.5; i <= 1.5; i = i + 1, a++)
                        {
                            sw.WriteLine("AddVertex,{0},0,{1:f4},", trans.X((-0.66- gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.66- gaugeoffset) * LiRe_T, K1[1] + i));
                            sw.WriteLine("AddVertex,{0},-0.15,{1:f4},", trans.X((-0.66- gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.66- gaugeoffset) * LiRe_T, K1[1] + i));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] + (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] + 1.65));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] + (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] + 1.65));

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Radlenker Railside Totspur

                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                        a = 1;
                        for (double i = -1.5; i <= 1.5; i = i + 1, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T), Abbiege_z(K1[1] + i, radiusT, (0.66+ gaugeoffset), LiRe_T)));
                        }
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railSide.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);

                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }




                        //Ballast, sleepers & embankments
                        //
                        //

                        //Ballast Right

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((-2.8- gaugeoffset) * LiRe_T, i), trans.Z((-2.8- gaugeoffset) * LiRe_T, i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3- gaugeoffset) * LiRe_T, i), trans.Z((-1.3- gaugeoffset) * LiRe_T, i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        
                        sw.WriteLine("LoadTexture,ballast.{0:f4},", texture_format);
                        SetTexture(sw, a, 10 * laenge, 2);

                        // Ballast Left

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (1.3+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (1.3+ gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X(Abbiege_x(i, radiusT, (2.8+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (2.8+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (2.8+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (2.8+ gaugeoffset), LiRe_T)));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,ballast.{0:f4},", texture_format);
                        SetTexture(sw, a, 10 * laenge, 1);

                        if (!checkBox2.Checked)
                        {

                            // Embankment Right

                            a = -1;
                            sw.WriteLine("\r\r\nCreateMeshBuilder");
                            for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((-3.6- gaugeoffset) * LiRe_T, i), trans.Z((-3.6- gaugeoffset) * LiRe_T, i));
                                sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((-2.5- gaugeoffset) * LiRe_T, i), trans.Z((-2.5- gaugeoffset) * LiRe_T, i));
                            }

                            AddFace(sw, a, LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", embankment_file, texture_format);
                            SetTexture(sw, a, 3 * laenge, 1);


                            // Embankment Left

                            a = -1;
                            sw.WriteLine("\r\r\nCreateMeshBuilder");
                            for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X(Abbiege_x(i, radiusT, (2.5+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (2.5+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (2.5+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (2.5+ gaugeoffset), LiRe_T)));
                                sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X(Abbiege_x(i, radiusT, (3.6+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (3.6+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (3.6+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (3.6+ gaugeoffset), LiRe_T)));
                            }

                            AddFace(sw, a, LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", embankment_file, texture_format);
                            SetTexture(sw, a, 3 * laenge, 2);
                        }

                        //Sleepers for left branch
                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Sleepers L Branch");

                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            //New
                            double newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            double newsleepers1 = trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);               
                           

                            
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3- gaugeoffset) * LiRe_T, i), trans.Z((-1.3- gaugeoffset) * LiRe_T, i));
                            if (newsleepers - newsleepers1 <= 0.1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                            //OLD RIGHT
                            //sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                        }


                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", sleeper_file, texture_format);


                        double c = 0.5;
                        int b = 0;

                        //Do we want to invert the textures??

                        if (checkBox3.Checked)
                        {
                            for (double i = a; i >= 0; i--)
                            {
                                //c = (Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3+ gaugeoffset)) / (2.6 + (gaugeoffset* 2));
                                sw.WriteLine("SetTextureCoordinates,{0:f4},0,{1:f4},", b, 15 - (i * 15 * laenge / a));
                                sw.WriteLine("SetTextureCoordinates,{0:f4},1,{1:f4},", b + 1, 15 - (i * 15 * laenge / a));
                                b = b + 2;
                            }
                        }
                        else
                        {
                            for (double i = a; i >= 0; i--)
                            {
                                c = (Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3+ gaugeoffset)) / (2.6 + (gaugeoffset * 2));
                                double newcoords;
                                if (c < 1)
                                {
                                    newcoords = c;
                                }
                                else
                                {
                                    newcoords = 1;
                                }
                                sw.WriteLine("SetTextureCoordinates,{0:f4},0,{1:f4},", b, i * 15 * laenge / a);
                                sw.WriteLine("SetTextureCoordinates,{0:f4},{1:f4},{2:f4},", b + 1, newcoords, i * 15 * laenge / a);
                                b = b + 2;

                            }
                        }

                        //Sleepers for right branch

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Sleepers R Branch");
                         for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            double newsleepers1 = (trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            double newsleepers2 = (trans.X(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            if (newsleepers2 <= newsleepers1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            }
                             //sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (1.3+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (1.3+ gaugeoffset), LiRe_T)));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", sleeper_file, texture_format);
                        
                        c = 0;
                        b = 0;

                        //Do we want to invert the textures??
                        if (checkBox3.Checked)
                        {

                            for (double i = a; i >= 0; i--)
                            {
                                c = 1 - (Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3 + gaugeoffset)) / (2.6 + (gaugeoffset * 2));
                                sw.WriteLine("SetTextureCoordinates,{0:f4},0,{1:f4},", b, 15 - (i * 15 * laenge / a));
                                sw.WriteLine("SetTextureCoordinates,{0:f4},1,{1:f4},", b + 1, 15 - (i * 15 * laenge / a));
                                b = b + 2;
                            }
                        }
                        else
                        {
                            for (double i = a; i >= 0; i--)
                            {
                                double newcoords;
                                c = 1 - (Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3 + gaugeoffset)) / (2.6 + (gaugeoffset * 2));
                                if (c > 0)
                                {
                                    newcoords = c;
                                }
                                else
                                {
                                    newcoords = 0;
                                }
                                sw.WriteLine("SetTextureCoordinates,{0:f4},{1:f4},{2:f4},", b, newcoords, i * 15 * laenge / a);
                                sw.WriteLine("SetTextureCoordinates,{0:f4},1,{1:f4},", b + 1, i * 15 * laenge / a);
                                b = b + 2;
                                //c = c + 0.05 * 10 / a;
                            }
                        }



                        //Left Branch Ballast Shoulder R

                        a = -1;
                        int totalsegs = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder R Branch L");

                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++, totalsegs++)
                        {
                            //New
                            double newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            double newsleepers1 = trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);


                            if (newsleepers - newsleepers1 <= 0.1)
                            {
                            sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                            sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((2.8 + gaugeoffset) * LiRe_T, i), trans.Z((2.8 + gaugeoffset) * LiRe_T, i));
                            }
                            else
                            {
                                //Don't do anything, subtract one from a
                                a--;
                            }
                            
                        }


                        AddFace(sw, a, LiRe_T);
                        double texturefactor1 = a;
                        double texturefactor2 = totalsegs;
                        double texturefactor = ((texturefactor1 / texturefactor2) * 10);
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", ballast_file, texture_format);
                        SetTexture(sw, a, texturefactor * laenge, 5);

                        //Right Branch Ballast Shoulder L

                        a = -1;
                        totalsegs = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder L Branch R");

                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++, totalsegs++)
                        {
                            double newsleepers1 = (trans.X((Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            double newsleepers2 = (trans.X(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));


                            if (newsleepers2 <= newsleepers1)
                            {
                                a--;
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)));
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            }

                        }


                        AddFace(sw, a, LiRe_T);
                        texturefactor1 = a;
                        texturefactor2 = totalsegs;
                        texturefactor = ((texturefactor1 / texturefactor2) * 10);
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", ballast_file, texture_format);
                        SetTexture(sw, a, texturefactor * laenge, 2);



                        //Right Branch Additional Sleeper Bit

                        double d = Abw_tot / (2 * Math.Cos(winkel_tot(laenge, Abw_tot)));

                        /*
                         ** NOT NEEDED ANYMORE **
                        sw.WriteLine("\r\r\nCreateMeshBuilder,");
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(25 * laenge, radiusT, 0, LiRe_T) / (2+ (gaugeoffset* 2)), 25 * laenge), trans.Z(Abbiege_x(25 * laenge, radiusT, 0, LiRe_T) / (2+ (gaugeoffset* 2)), 25 * laenge));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(25 * laenge, radiusT, -d, LiRe_T), Abbiege_z(25 * laenge, radiusT, -d, LiRe_T)), trans.Z(Abbiege_x(25 * laenge, radiusT, -d, LiRe_T), Abbiege_z(25 * laenge, radiusT, -d, LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(Abbiege_x(25 * laenge, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(25 * laenge, radiusT, (1.3+ gaugeoffset), LiRe_T)), trans.Z(Abbiege_x(25 * laenge, radiusT, (1.3+ gaugeoffset), LiRe_T), Abbiege_z(25 * laenge, radiusT, (1.3+ gaugeoffset), LiRe_T)));
                        if (LiRe_T > 0)
                        {
                            sw.WriteLine("AddFace,0,1,2,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,2,1,0,");
                        }

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", sleeper_file, texture_format);
                        
                        c = 1 - (Abbiege_x(25 * laenge, radiusT, 0, LiRe_T * LiRe_T) / 2 + 1.3) / 2.6;
                        sw.WriteLine("SetTextureCoordinates,0,{0:f4},1,", trans.X(c, 25 - z));
                        sw.WriteLine("SetTextureCoordinates,1,{0:f4},{1:f4},", trans.X(c, 25 - z), trans.Z(c, (1 - (Abbiege_z(25 * laenge, radiusT, -d, LiRe_T) - (25 * laenge)) * 15 / 25)) - z);
                        sw.WriteLine("SetTextureCoordinates,2,1,1,");
                        */

                        // SchienenbefestigungAnfang Spielerspur

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");

                        double[] K20 = new double[2];
                        K20[1] = Math.Sqrt((radiusT + (0.72+ gaugeoffset)) * (radiusT + (0.72+ gaugeoffset)) - (radiusT + (0.55+ gaugeoffset)) * (radiusT + (0.55+ gaugeoffset)));

                        j = 0;
                        while (j < K20[1])
                        {
                            j = j + segmente * laenge / 15;
                        }

                        for (double i = 0; i <= j; i = i + 1.25, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((-1.3- gaugeoffset) * LiRe_T, i), trans.Z((-1.3- gaugeoffset) * LiRe_T, i));
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((-0.325- gaugeoffset) * LiRe_T, i), trans.Z((-0.325- gaugeoffset) * LiRe_T, i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,SchieneSpezAnf.{0:f4},", texture_format);

                        b = 0;
                        for (double i = 0; i <= a; i++)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b, 15 * laenge - 0.75 * i);
                            sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, 15 * laenge - 0.75 * i);
                            b = b + 2;
                        }

                        sw.WriteLine("SetDecalTransparentColor,255,255,255,");


                        //Schienenbefestigung Weiche Spielerspur
                        j = a * 1.25;                  //Übergabe des z-Abstandes von voriger Funktion
                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");

                        double k = 0;
                        while (k < K1[1])
                        {
                            k = k + segmente * laenge / 15;
                        }

                        for (double i = j; i <= k; i = i + 1.25, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-1.3- gaugeoffset), LiRe_T), i), trans.Z(Abbiege_x(i, radiusT, (-1.3- gaugeoffset), LiRe_T), i));
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(Abbiege_x(i, radiusT, (-0.325- gaugeoffset), LiRe_T), i), trans.Z(Abbiege_x(i, radiusT, (-0.325- gaugeoffset), LiRe_T), i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,SchieneSpez.{0:f4},", texture_format);

                        j = b / 2 - 1;
                        b = 0;
                        for (double i = 0; i <= a; i++)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b, 15 * laenge - 0.75 * (i + j));
                            sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, 15 * laenge - 0.75 * (i + j));
                            b = b + 2;
                        }

                        sw.WriteLine("SetDecalTransparentColor,255,255,255,");



                        //Schienenbefestigung Weichenanfang Totspur

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");

                        j = 0;
                        while (j < K20[1])
                        {
                            j = j + segmente * laenge / 15;
                        }

                        for (double i = 0; i <= j; i = i + 1.25, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), i), trans.Z(Abbiege_x(i, radiusT, (1.3+ gaugeoffset), LiRe_T), i));
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(Abbiege_x(i, radiusT, (0.325+ gaugeoffset), LiRe_T), i), trans.Z(Abbiege_x(i, radiusT, (0.325+ gaugeoffset), LiRe_T), i));
                        }

                        AddFace(sw, a, -1 * LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,SchieneSpezAnf.{0:f4},", texture_format);
                        
                        b = 0;
                        for (double i = 0; i <= a; i++)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b, 15 * laenge - 0.75 * i);
                            sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, 15 * laenge - 0.75 * i);
                            b = b + 2;
                        }

                        sw.WriteLine("SetDecalTransparentColor,255,255,255,");


                        //Schienenbefestigung Weiche Totspur
                        j = a * 1.25;                  //Übergabe des z-Abstandes von voriger Funktion
                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");

                        for (double i = j; i <= k; i = i + 1.25, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((1.3+ gaugeoffset) * LiRe_T, i), trans.Z((1.3+ gaugeoffset) * LiRe_T, i));
                            sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((0.325+ gaugeoffset) * LiRe_T, i), trans.Z((0.325+ gaugeoffset) * LiRe_T, i));
                        }

                        AddFace(sw, a, -1 * LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,SchieneSpez.{0:f4},", texture_format);
                        
                        j = b / 2 - 1;
                        b = 0;
                        for (double i = 0; i <= a; i++)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},1,{1:f4},", b, 15 * laenge - 0.75 * (i + j));
                            sw.WriteLine("SetTextureCoordinates,{0},0,{1:f4},", b + 1, 15 * laenge - 0.75 * (i + j));
                            b = b + 2;
                        }

                        sw.WriteLine("SetDecalTransparentColor,255,255,255,");

                        //Weichenantrieb
                        if (checkBox1.Checked)
                        {
                            sw.WriteLine("CreateMeshBuilder\r\r\nAddVertex,-1.8,-0.05,0.3,\r\nAddVertex,-1.2,-0.05,0.3,\r\r\nAddVertex,-1.2,-0.25,0.3,\r\r\nAddVertex,-1.8,-0.25,0.3,\r\r\nAddVertex,-1.8,0,0.35,\r\r\nAddVertex,-1.2,0,0.35,\r\r\nAddVertex,-1.8,0,0.55,\r\r\nAddVertex,-1.2,0,0.55,\r\r\nAddVertex,-1.8,-0.05,0.6,\r\r\nAddVertex,-1.2,-0.05,0.6,\r\r\nAddVertex,-1.2,-0.25,0.6,\r\r\nAddVertex,-1.8,-0.25,0.6,\r\r\nAddFace,0,1,2,3,\r\r\nAddFace,4,5,1,0,\r\r\nAddFace,6,7,5,4,\r\r\nAddFace,8,9,7,6,\r\r\nAddFace,11,10,9,8,\r\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0:f4},\r\nSetTextureCoordinates,0,0,0.5,\r\nSetTextureCoordinates,1,1,0.5,\r\nSetTextureCoordinates,2,1,1,\r\nSetTextureCoordinates,3,0,1,\r\nSetTextureCoordinates,4,0,0.3,\r\nSetTextureCoordinates,5,1,0.3,\r\nSetTextureCoordinates,6,0,0,\r\nSetTextureCoordinates,7,1,0,\r\nSetTextureCoordinates,8,0,0.3,\r\nSetTextureCoordinates,9,1,0.3,\r\nSetTextureCoordinates,10,1,0.6,\r\nSetTextureCoordinates,11,0,0.6,", texture_format);

                            sw.WriteLine("CreateMeshBuilder\r\nAddVertex,-1.8,-0.25,0.3,\r\nAddVertex,-1.8,-0.05,0.3,\r\nAddVertex,-1.8,0,0.35,\r\nAddVertex,-1.8,-0.25,0.35\r\nAddVertex,-1.8,0,0.55,\r\nAddVertex,-1.8,-0.25,0.55,\r\nAddVertex,-1.8,-0.05,0.6,\r\nAddVertex,-1.8,-0.25,0.6,\r\nAddFace,2,1,0,3,\r\nAddFace,4,2,3,5,\r\nAddFace,6,4,5,7,\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0:f4},\r\nSetTextureCoordinates,0,1,1\r\nSetTextureCoordinates,1,1,0.5\r\nSetTextureCoordinates,2,0.9,0.5\r\nSetTextureCoordinates,3,0.9,1\r\nSetTextureCoordinates,4,0.7,0.5\r\nSetTextureCoordinates,5,0.7,1\r\nSetTextureCoordinates,6,0.6,0.5\r\nSetTextureCoordinates,7,0.6,1", texture_format);

                            sw.WriteLine("CreateMeshBuilder\r\nAddVertex,-1.2,-0.25,0.3,\r\nAddVertex,-1.2,-0.05,0.3,\r\nAddVertex,-1.2,0,0.35,\r\nAddVertex,-1.2,-0.25,0.35,\r\nAddVertex,-1.2,0,0.55,\r\nAddVertex,-1.2,-0.25,0.55,\r\nAddVertex,-1.2,-0.05,0.6,\r\nAddVertex,-1.2,-0.25,0.6,\r\nAddFace,1,2,3,0,\r\nAddFace,2,4,5,3,\r\nAddFace,4,6,7,5,\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0:f4},\r\nSetTextureCoordinates,0,1,1\r\nSetTextureCoordinates,1,1,0.5\r\nSetTextureCoordinates,2,0.9,0.5\r\nSetTextureCoordinates,3,0.9,1\r\nSetTextureCoordinates,4,0.7,0.5\r\nSetTextureCoordinates,5,0.7,1\r\nSetTextureCoordinates,6,0.6,0.5\r\nSetTextureCoordinates,7,0.6,1", texture_format);
                            
                            sw.WriteLine("CreateMeshBuilder\r\nCylinder,6,0.015,0.015,2\r\nRotate,0,0,1,90\r\nTranslate,-0.3,-0.14,0.4\r\nSetColor,151,151,151");
                            sw.WriteLine("CreateMeshBuilder\r\nCylinder,6,0.01,0.01,2\r\nRotate,0,0,1,90\r\nTranslate,-0.3,-0.12,0.5\r\nSetColor,151,151,151");

                        }

                    }
                }

                //Create Curve
                if (radioButton2.Checked == true)
                {
                    /*
                     Curves
                     */
                    //Initialise

                    //Check that radius is a valid number
                    EingabeOK = double.TryParse(textBox_radius.Text, out radius);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler Radius!");
                        return;
                    }


                    if ((radius <= 49) && (radius >= -49))
                    {
                        MessageBox.Show("Radius is less than 50m");
                        return;
                    }

                    EingabeOK = double.TryParse(textBox_segmente.Text, out segmente);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler Segmente!");
                        return;
                    }

                    //Check that track gauge is a valid number
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

                    //Left or right definition
                    if (radius < 0)
                    {
                        LiRe = -1;
                        radius = radius * -1;
                    }


                    if (LiRe == -1)
                    {
                        name = (richTextBox1.Text) + "\\Output\\Tracks\\L" + radius + ".csv";
                    }
                    else
                    {
                        name = (richTextBox1.Text) + "\\Output\\Tracks\\R" + radius + ".csv";
                    }

                    //Create Output directory
                    if (!System.IO.Directory.Exists(richTextBox1.Text + "\\Output\\Tracks"))
                    {
                        System.IO.Directory.CreateDirectory(richTextBox1.Text + "\\Output\\Tracks");
                    }


                    //Main Textures
                    string outputtype = "Tracks";
                    ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                    ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);

                    //Embankment Texture
                    if (checkBox2.Checked == false)
                    {
                        ConvertAndMove(launchpath, embankment_texture, texture_format, embankment_file, outputtype);
                    }

                    //Rail Textures
                    if (checkBox5.Checked == false)
                    {
                        ConvertAndMove(launchpath, railside_texture, texture_format, railside_file, outputtype);
                        ConvertAndMove(launchpath, railtop_texture, texture_format, railtop_file, outputtype);
                    }


                    trans = new Transform(1, radius, LiRe, 0);


                    //Write out to CSV
                    using (StreamWriter sw = new StreamWriter(name))
                    {

                        //Railtop Left

                        int a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Railtop Links");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.72 -gaugeoffset), (25 / segmente) * i), trans.Z(-0.72, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.78- gaugeoffset), (25 / segmente) * i), trans.Z(-0.78, (25 / segmente) * i));
                        }

                        AddFace(sw, a, -LiRe_T);
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }

                        //Rail Top Right

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Railtop Rechts");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.78 +gaugeoffset), (25 / segmente) * i), trans.Z(0.78, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.72 +gaugeoffset), (25 / segmente) * i), trans.Z(0.72, (25 / segmente) * i));
                        }

                        AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railTop.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 1);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,180,190,200,");
                        }

                        

                        //Railside Left

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Railside Links");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.74 -gaugeoffset), (25 / segmente) * i), trans.Z(-0.74, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.74 -gaugeoffset), (25 / segmente) * i), trans.Z(-0.74, (25 / segmente) * i));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railside.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Right Rail Side

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Railside Rechts");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74 +gaugeoffset), (25 / segmente) * i), trans.Z(0.74, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74 +gaugeoffset), (25 / segmente) * i), trans.Z(0.74, (25 / segmente) * i));
                        }

                        AddFace2(sw, a);

                        sw.WriteLine("GenerateNormals,");
                        if (!checkBox5.Checked)
                        {
                            sw.WriteLine("LoadTexture,railside.{0:f4},", texture_format);
                            SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            sw.WriteLine("SetColor,85,50,50,");
                        }

                        //Sleepers

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Schwellen");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 -gaugeoffset), (25 / segmente) * i), trans.Z(-1.3, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((1.3 +gaugeoffset), (25 / segmente) * i), trans.Z(1.3, (25 / segmente) * i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", sleeper_file, texture_format);
                        SetTexture(sw, a, 15, 2);

                        //Ballast Left

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Links");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((-2.8 -gaugeoffset), (25 / segmente) * i), trans.Z(-2.8, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 -gaugeoffset), (25 / segmente) * i), trans.Z(-1.3, (25 / segmente) * i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},",ballast_file, texture_format);
                        SetTexture(sw, a, 10, 2);

                        //Ballast Right

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Rechts");
                        for (int i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((1.3 +gaugeoffset), (25 / segmente) * i), trans.Z(1.3, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((2.8 +gaugeoffset), (25 / segmente) * i), trans.Z(2.8, (25 / segmente) * i));
                        }

                        AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", ballast_file, texture_format);
                        SetTexture(sw, a, 10, 1);

                        if (!checkBox2.Checked)
                        {

                            //Grass Left

                            a = -1;
                            sw.WriteLine("\r\nCreateMeshBuilder ;Grass Links");
                            for (int i = 0; i <= segmente; i++, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((-3.6 -gaugeoffset), (25 / segmente) * i), trans.Z(-3.6, (25 / segmente) * i));
                                sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((-2.5 -gaugeoffset), (25 / segmente) * i), trans.Z(-2.5, (25 / segmente) * i));
                            }

                            AddFace(sw, a, LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", embankment_file, texture_format);
                            SetTexture(sw, a, 3, 1);

                            //Grass Right

                            a = -1;
                            sw.WriteLine("\r\nCreateMeshBuilder ;Grass Rechts");
                            for (int i = 0; i <= segmente; i++, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((2.5 +gaugeoffset), (25 / segmente) * i), trans.Z(2.5, (25 / segmente) * i));
                                sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((3.6 +gaugeoffset), (25 / segmente) * i), trans.Z(3.6, (25 / segmente) * i));
                            }

                            AddFace(sw, a, LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", embankment_file, texture_format);
                            SetTexture(sw, a, 3, 2);
                        }


                    }
                }

                //Create Straight Track
                if (radioButton3.Checked == true)
                {

                    //Straights

                    if (richTextBox1.TextLength == 0)
                    {
                        MessageBox.Show("Geben sie einen Pfad an!");
                        return;
                    }

                    
                    name = (richTextBox1.Text) + "\\Output\\Tracks\\straight.csv";

                    //Create Output directory
                    if (!System.IO.Directory.Exists(richTextBox1.Text + "\\Output\\Tracks"))
                    {
                        System.IO.Directory.CreateDirectory(richTextBox1.Text + "\\Output\\Tracks");
                    }

                    //Main Textures
                    string outputtype = "Tracks";
                    ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                    ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);

                    //Embankment Texture
                    if (checkBox2.Checked == false)
                    {
                        ConvertAndMove(launchpath, embankment_texture, texture_format, embankment_file, outputtype);
                    }

                    //Rail Textures
                    if (checkBox5.Checked == false)
                    {
                        ConvertAndMove(launchpath, railside_texture, texture_format, railside_file, outputtype);
                        ConvertAndMove(launchpath, railtop_texture, texture_format, railtop_file, outputtype);
                    }

                    //Check that track gauge is a valid number
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

                    //Create Output directory
                    if (!System.IO.Directory.Exists(richTextBox1.Text + "\\Output\\Tracks"))
                    {
                        System.IO.Directory.CreateDirectory(richTextBox1.Text + "\\Output\\Tracks");
                    }


                    //Write Out to CSV
                    using (StreamWriter sw = new StreamWriter(name))
                    {
                        
                            if (!checkBox5.Checked)
                            {
                                sw.WriteLine("CreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,railTop.{2:f4}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", -0.72 - gaugeoffset, -0.78 - gaugeoffset, texture_format);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railTop.{2:f4}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", 0.72 + gaugeoffset, 0.78 + gaugeoffset, texture_format);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railside.{1:f4}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", -0.74 - gaugeoffset, texture_format);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railside.{1:f4}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", 0.74 + gaugeoffset, texture_format);
                            }
                            else
                            {
                                sw.WriteLine("CreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nSetColor,180,190,200", -0.72 - gaugeoffset, -0.78 - gaugeoffset);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nSetColor,180,190,200", 0.72+ gaugeoffset, 0.78 + gaugeoffset);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nSetColor,85,50,50,", -0.74 - gaugeoffset);

                                sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nSetColor,85,50,50", 0.74 + gaugeoffset);
                            }

                            sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4}, -0.15,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2:f4}.{3:f4}\r\nSetTextureCoordinates,0,0.01,0\r\nSetTextureCoordinates,1,0.99,0\r\nSetTextureCoordinates,2,0.99,15\r\nSetTextureCoordinates,3,0.01,15", -1.3 - gaugeoffset, 1.3 + gaugeoffset, sleeper_file, texture_format);

                            sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.4,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.4,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2:f4}.{3:f4}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,10\r\nSetTextureCoordinates,1,0,10", -2.8 - gaugeoffset, -1.3 - gaugeoffset, ballast_file, texture_format);

                            sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.4,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.4,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,{2:f4}.{3:f4}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,10\r\nSetTextureCoordinates,1,0,10", 2.8 + gaugeoffset, 1.3 + gaugeoffset, ballast_file, texture_format);

                        
                        if (!checkBox2.Checked)
                        {
                            sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,{2:f4}.{3:f4}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", -2.5 - gaugeoffset, -3.6 - gaugeoffset, embankment_file, texture_format);

                            sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2:f4}.{3:f4}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", 2.5 + gaugeoffset, 3.6 + gaugeoffset, embankment_file, texture_format);
                            

                        }
                    }

                }

                //Create Platform
                if (radioButton4.Checked == true)
                {
                    
                    //Initialise

                    //Check that radius is a valid number
                    EingabeOK = double.TryParse(textBox_radius.Text, out radius);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Eingabefehler Radius!");
                        return;
                    }
                    //Have we selected a platform side?
                    if (radioButton5.Checked != true && radioButton6.Checked != true)
                        {
                            MessageBox.Show("Please select a Left or Right Platform!");
                            return;
                    }
                    if ((radius != 0) && (radius <= 49) && (radius >= -49))
                    {
                        MessageBox.Show("Radius for platforms should either be 0 for Straight or greater than 50m!");
                        return;
                    }

                    EingabeOK = double.TryParse(textBox_segmente.Text, out segmente);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid number of segments!");
                        return;
                    }

                    //Check platform height is a valid number
                    EingabeOK = double.TryParse(textBox_platheight.Text, out platheight);
                    if (EingabeOK == false)
                    {
                        MessageBox.Show("Invalid Platform Height!");
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
                    }
                    
                    //Check that track gauge is a valid number
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


                    //Check platform widths are over 1m
                    if (platwidth_near < 1 | platwidth_far < 1)
                    {
                        MessageBox.Show("Minimum platform width is 1m!");
                        return;
                    }

                    if (richTextBox1.TextLength == 0)
                    {
                        MessageBox.Show("Please select a valid path!");
                        return;
                    }

                    //Left or right definition
                    if (radius < 0)
                    {
                        LiRe = -1;
                        radius = radius * -1;
                    }

                    //Create Output directory
                    if (!System.IO.Directory.Exists(richTextBox1.Text + "\\Output\\Platforms"))
                    {
                        System.IO.Directory.CreateDirectory(richTextBox1.Text + "\\Output\\Platforms");
                    }

                    //Main Textures
                    string outputtype = "Platforms";
                    ConvertAndMove(launchpath, platform_texture, texture_format, platform_file, outputtype);
                    if (fence_yes.Checked == true)
                    {
                        ConvertAndMove(launchpath, fence_texture, texture_format, fence_file, outputtype);
                    }


                    
                    

                    if (LiRe == -1)
                    {
                        //Left Handed Platforms
                        if (radioButton5.Checked == true)
                        {
                            if (radius == 0)
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_Straight.csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RU_Straight.csv";
                                }
                                else
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RD_Straight.csv";
                                }
                            }
                            else
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_L" + radius + ".csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RU_L" + radius + ".csv";
                                }
                                else
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RD_L" + radius + ".csv";
                            }
                            
                            
                        }
                        else
                        {
                            if (radius == 0)
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_Straight.csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RU_Straight.csv";
                                }
                                else
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RD_Straight.csv";
                                }
                            }
                            else
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_L" + radius + ".csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RU_L" + radius + ".csv";
                                }
                                else
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RD_L" + radius + ".csv";
                            }
                        }
                    }
                    else
                    {
                        //Right Handed Platforms
                        if (radioButton6.Checked == true)
                        {
                            if (radius == 0)
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_Straight.csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RU_Straight.csv";
                                }
                                else
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RD_Straight.csv";
                                }
                            }
                            else
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_R" + radius + ".csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RU_R" + radius + ".csv";
                                }
                                else
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformRight_RD_R" + radius + ".csv";
                            }
                        }
                        else
                        {
                            if (radius == 0)
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_Straight.csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RU_Straight.csv";
                                }
                                else
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RD_Straight.csv";
                                }
                            }
                            else
                            {
                                if (radioButton7.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_R" + radius + ".csv";
                                }
                                else if (radioButton8.Checked == true)
                                {
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RU_R" + radius + ".csv";
                                }
                                else
                                    name = (richTextBox1.Text) + "\\Output\\Platforms\\PlatformLeft_RD_R" + radius + ".csv";
                            }
                        }
                    }

                    //Calculate the track width to move the platforms as appropriate
                    trans = new Transform(1, radius, LiRe, 0);

                    //Write Out to CSV
                    using (StreamWriter sw = new StreamWriter(name))
                    {
                        //Left Sided Platform
                        if (radioButton5.Checked == true)
                        {
                            {
                                int a = -1;
                                int c = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (radioButton7.Checked == true)
                                    {
                                        //Straight Platform No Ramp
                                        segmente = 1;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (a < 0)
                                                {
                                                    platwidth = platwidth_near;
                                                }
                                                else
                                                {
                                                    platwidth = platwidth_far;
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight, trans.Z(-3.5, (25 / segmente) * i));
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55- gaugeoffset), (25 / segmente) * i), platheight, trans.Z(-1.55, (25 / segmente) * i));
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else if (radioButton8.Checked == true)
                                    {
                                        //Straight Platform Ramp Up
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near -(((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {
                                                    
                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            platheight_new = 0;
                                            if (a >= 0)
                                            {

                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Straight Platform Ramp Down
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            platheight_new = 0;
                                            if (a < 1)
                                            {

                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }
                                }
                                else
                                //Otherwise, use number of segments specified by user for curved platforms
                                {
                                    if (radioButton7.Checked == true)
                                    {
                                        //Curved Platform No Ramp
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight, trans.Z(-3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else if (radioButton8.Checked == true)
                                    {
                                        //Curved Platform Ramp Up
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            if (a < (segmente / 2))
                                            {
                                                platheight_new = ((platheight / (segmente / 2)) * a);
                                            }
                                            else
                                            {
                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }

                                    else
                                    {
                                        //Curved Platform Ramp Down
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        int b = 0;
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;

                                            if (a > (segmente / 2))
                                            {
                                                b++;
                                                platheight_new = platheight - ((platheight / (segmente / 2)) * b);
                                            }
                                            else
                                            {
                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 -gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(-1.8, (25 / segmente) * i));
                                        }
                                    }
                                }

                                PlatFace(sw, a, -LiRe_T);
                                sw.WriteLine("GenerateNormals,");
                                                                
                                sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", platform_file, texture_format);
                                SetPlatformTexture(sw, a, 5, 1, platwidth_near, platwidth_far, segmente);

                                //We've created our platform, now create the fence
                                if (fence_yes.Checked == true)
                                {
                                            int d = -1;
                                            //If radius is zero, use one segment
                                            if (radius == 0)
                                            {
                                                if (radioButton7.Checked == true)
                                                {
                                                    //Straight Platform No Ramp
                                                    segmente = 1;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");

                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (d < 0)
                                                            {
                                                                platwidth = platwidth_near;
                                                            }
                                                            else
                                                            {
                                                                platwidth = platwidth_far;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight + fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else if (radioButton8.Checked == true)
                                                {
                                                    //Straight Platform Ramp Up
                                                    segmente = 2;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        platheight_new = 0;
                                                        if (d >= 0)
                                                        {

                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else
                                                {
                                                    //Straight Platform Ramp Down
                                                    segmente = 2;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        platheight_new = 0;
                                                        if (d < 1)
                                                        {

                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                            }
                                            else
                                            //Otherwise, use number of segments specified by user for curved platforms
                                            {
                                                if (radioButton7.Checked == true)
                                                {
                                                    //Curved Platform No Ramp
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight+ fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else if (radioButton8.Checked == true)
                                                {
                                                    //Curved Platform Ramp Up
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        if (d < (segmente / 2))
                                                        {
                                                            platheight_new = ((platheight / (segmente / 2)) * d);
                                                        }
                                                        else
                                                        {
                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else
                                                {
                                                    //Curved Platform Ramp Down
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    int b = 0;
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;

                                                        if (d > (segmente / 2))
                                                        {
                                                            b++;
                                                            platheight_new = platheight - ((platheight / (segmente / 2)) * b);
                                                        }
                                                        else
                                                        {
                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 -gaugeoffset) - platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                            }

                                            //Use new Face2 method
                                            AddFace2_New(sw, d, -LiRe_T);
                                            sw.WriteLine("GenerateNormals,");
                                            //Load texture & set-cordinates
                                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", fence_file, texture_format);
                                            SetTexture(sw, d, 10, 4);
                                            sw.WriteLine("SetDecalTransparentColor,0,0,255,");

                                }
                            }
                        }
                    
                            
                        
                            
                            
                        //Right Sided Platforms
                        else if (radioButton6.Checked == true)
                        {
                            {
                                int a = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (radioButton7.Checked == true)
                                    {
                                        //Straight Platform No Ramp
                                        segmente = 1;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (a < 0)
                                                {
                                                    platwidth = platwidth_near;
                                                }
                                                else
                                                {
                                                    platwidth = platwidth_far;
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 +gaugeoffset)+ platwidth), (25 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 +gaugeoffset), (25 / segmente) * i), platheight, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 +gaugeoffset), (25 / segmente) * i), platheight, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 +gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else if (radioButton8.Checked == true)
                                    {
                                        //Straight Platform Ramp Up
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            platheight_new = 0;
                                            if (a >= 0)
                                            {

                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Straight Platform Ramp Down
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            platheight_new = 0;
                                            if (a < 1)
                                            {

                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }
                                }
                                else
                                //Otherwise, use number of segments specified by user for curved platforms
                                {
                                    if (radioButton7.Checked == true)
                                    {
                                        //Curved Platform No Ramp
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25 / segmente) * i), platheight, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }
                                    else if (radioButton8.Checked == true)
                                    {
                                        //Curved Platform Ramp Up
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            if (a < (segmente / 2))
                                                {
                                                platheight_new = ((platheight / (segmente / 2)) * a);
                                                }
                                            else
                                            {
                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }

                                    else
                                    {
                                        //Curved Platform Ramp Down
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        int b = 0;
                                        for (int i = 0; i <= segmente; i++, a++)
                                        {
                                            if (platwidth_near != platwidth_far)
                                            {
                                                if (platwidth_near > platwidth_far)
                                                {
                                                    platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                }
                                                else
                                                {

                                                    platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                }
                                            }
                                            else
                                            {
                                                platwidth = platwidth_near;
                                            }
                                            double platheight_new;
                                            
                                            if (a > (segmente / 2))
                                            {
                                                b++;
                                                platheight_new = platheight - ((platheight / (segmente / 2)) * b);
                                            }
                                            else
                                            {
                                                platheight_new = platheight;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(3.5, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25 / segmente) * i), -0.2, trans.Z(1.8, (25 / segmente) * i));
                                        }
                                    }
                                }

                                PlatFace(sw, a, LiRe_T);
                                sw.WriteLine("GenerateNormals,");

                                sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", platform_file, texture_format);
                                

                                SetPlatformTexture(sw, a, 5, 1, platwidth_near, platwidth_far, segmente);
                                
                                //We've created our platform, now create the fence
                                if (fence_yes.Checked == true)
                                {
                                            int d = -1;
                                            //If radius is zero, use one segment
                                            if (radius == 0)
                                            {
                                                if (radioButton7.Checked == true)
                                                {
                                                    //Straight Platform No Ramp
                                                    segmente = 1;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");

                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (d < 0)
                                                            {
                                                                platwidth = platwidth_near;
                                                            }
                                                            else
                                                            {
                                                                platwidth = platwidth_far;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else if (radioButton8.Checked == true)
                                                {
                                                    //Straight Platform Ramp Up
                                                    segmente = 2;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        platheight_new = 0;
                                                        if (d >= 0)
                                                        {

                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else
                                                {
                                                    //Straight Platform Ramp Down
                                                    segmente = 2;
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        platheight_new = 0;
                                                        if (d < 1)
                                                        {

                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                            }
                                            else
                                            //Otherwise, use number of segments specified by user for curved platforms
                                            {
                                                if (radioButton7.Checked == true)
                                                {
                                                    //Curved Platform No Ramp
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight, trans.Z((-1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else if (radioButton8.Checked == true)
                                                {
                                                    //Curved Platform Ramp Up
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;
                                                        if (d < (segmente / 2))
                                                        {
                                                            platheight_new = ((platheight / (segmente / 2)) * d);
                                                        }
                                                        else
                                                        {
                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                                else
                                                {
                                                    //Curved Platform Ramp Down
                                                    //Platform Mesh
                                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                                    int b = 0;
                                                    for (int i = 0; i <= segmente; i++, d++)
                                                    {
                                                        if (platwidth_near != platwidth_far)
                                                        {
                                                            if (platwidth_near > platwidth_far)
                                                            {
                                                                platwidth = platwidth_near - (((platwidth_near - platwidth_far) / segmente) * i);
                                                            }
                                                            else
                                                            {

                                                                platwidth = platwidth_near + (((platwidth_far - platwidth_near) / segmente) * i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            platwidth = platwidth_near;
                                                        }
                                                        double platheight_new;

                                                        if (d > (segmente / 2))
                                                        {
                                                            b++;
                                                            platheight_new = platheight - ((platheight / (segmente / 2)) * b);
                                                        }
                                                        else
                                                        {
                                                            platheight_new = platheight;
                                                        }
                                                        platwidth = (platwidth - 0.3);
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25 / segmente) * i));
                                                    }
                                                }
                                            }

                                            //Use new Face2 method
                                            AddFace2_New(sw, d, -LiRe_T);
                                            sw.WriteLine("GenerateNormals,");
                                            //Load texture & set-cordinates
                                            sw.WriteLine("LoadTexture,{0:f4}.{1:f4},", fence_file, texture_format);
                                            SetTexture(sw, d, 10, 4);
                                            sw.WriteLine("SetDecalTransparentColor,0,0,255,");
                                }
                            }

                        }
                    }
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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

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

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureEN_Click_1(object sender, EventArgs e)
        {
            label_radius.Text = "Curve radius for player's track OR deviation (In meters)";
            label_tot.Text = "Deviation for diverging track (In meters)";
            radioButton1.Text = "Switch";
            radioButton2.Text = "Curve";
            radioButton3.Text = "Straight";
            label_laenge.Text = "Switch Length Multiplication (25,50,75,..) ";
            label_segmente.Text = "Number of segments";
            groupBox1.Text = "Advanced options";
            checkBox5.Text = "Do not texture rails";
            checkBox4.Text = "Use PNG textures";
            checkBox3.Text = "Invert textures";
            checkBox2.Text = "Without embankment";
            checkBox1.Text = "Add point motor";
            label_z.Text = "Z - adjustment";
            folderPath.Text = "Path";
            button.Text = "Create";
            label3.Text = "Platform Side";
            label4.Text = "Platform height (in m)";
            label5.Text = "Platform Width (Near)";
            label7.Text = "Plarform Width (Far)";
            label6.Text = "Platform Ramp";
            label8.Text = "Platform Fence";
            radioButton5.Text = "Left";
            radioButton6.Text = "Right";
            texturebutton.Text = "Choose Textures...";
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen",true))
            {

                    key.SetValue("Language", "en");

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            label_radius.Text = "Radius/Abweichung der eigenen Spur (in m) ";
            label_tot.Text = "Abweichung der Totspur (in m) ";
            radioButton1.Text = "Weiche";
            radioButton2.Text = "Kurve";
            radioButton3.Text = "Gerade";
            label_laenge.Text = "Weichenlänge (25,50,75,..) ";
            label_segmente.Text = "Anzahl Segmente";
            groupBox1.Text = "Erweiterte Optionen";
            checkBox5.Text = "Nicht Textur Schiene";
            checkBox4.Text = "PNG Textur";
            checkBox3.Text = "Textur Invertieren";
            checkBox2.Text = "Ohne Böschung";
            checkBox1.Text = "Weichenantrieb";
            label_z.Text = "z - Verschiebung";
            folderPath.Text = "Pfad";
            button.Text = "Erstellen";
            label3.Text = "Bahnsteigseite";
            label4.Text = "Plattformhöhe (in m)";
            label5.Text = "Plattformbreite (Nahe)";
            label7.Text = "Plattformbreite (Weit)";
            label6.Text = "Plattform Rampe";
            label8.Text = "Plattform Zaun";
            texturebutton.Text = "Texturen Wählen...";
            radioButton5.Text = "Links";
            radioButton6.Text = "Recht";
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Trackgen",true))
            {

                    key.SetValue("Language", "de");

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



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void texturebutton_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                using (platformtexture childform = new platformtexture(launchpath))
                {
                    childform.ShowDialog(this);
                }
            }
            else
            {
                using (texturepicker childform = new texturepicker(launchpath))
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


    }
}