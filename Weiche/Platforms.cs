/*
 * Platform Generation
 */

using System.Windows.Forms;
using System.IO;

namespace Weiche
{
    class Platforms
    {
        internal static void BuildPlatform(string[] inputStrings, bool[] inputcheckboxes)
        {
            {

                double radius;
                double segmente;
                double trackgauge;
                double gaugeoffset;
                double fenceheight = 0;
                double platwidth_near;
                double platwidth_far;
                double platheight;
                double platwidth;
                var LiRe = 1;
                var LiRe_T = 1;
                string name;
                string launchpath = inputStrings[4];
                string texture_format = inputStrings[15];
                string platform_texture = inputStrings[25];
                string platform_file = inputStrings[26];
                string fence_texture = inputStrings[27];
                string fence_file = inputStrings[28];

                bool EingabeOK;
                MathFunctions.Transform trans;
                //Initialise

                //Check that radius is a valid number
                EingabeOK = double.TryParse(inputStrings[0], out radius);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Eingabefehler Radius!");
                    return;
                }
                //Have we selected a platform side?
                if (inputcheckboxes[5] != true && inputcheckboxes[6] != true)
                {
                    MessageBox.Show("Please select a Left or Right Platform!");
                    return;
                }
                if ((radius != 0) && (radius <= 49) && (radius >= -49))
                {
                    MessageBox.Show("Radius for platforms should either be 0 for Straight or greater than 50m!");
                    return;
                }

                EingabeOK = double.TryParse(inputStrings[1], out segmente);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Invalid number of segments!");
                    return;
                }

                //Check platform height is a valid number
                EingabeOK = double.TryParse(inputStrings[29], out platheight);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Invalid Platform Height!");
                    return;
                }

                //Check platform widths are valid numbers
                EingabeOK = double.TryParse(inputStrings[30], out platwidth_near);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Invalid Platform Width (Near)!");
                    return;
                }

                EingabeOK = double.TryParse(inputStrings[31], out platwidth_far);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Invalid Platform Width (Far)!");
                    return;
                }

                //Parse fence height into number
                if (inputcheckboxes[9] == true)
                {
                    EingabeOK = double.TryParse(inputStrings[32], out fenceheight);
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
                EingabeOK = double.TryParse(inputStrings[2], out trackgauge);
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

                if (inputStrings[3].Length == 0)
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
                if (!System.IO.Directory.Exists(inputStrings[3] + "\\Output\\Platforms"))
                {
                    System.IO.Directory.CreateDirectory(inputStrings[3] + "\\Output\\Platforms");
                }

                //Main Textures
                const string outputtype = "Platforms";
                Weichengenerator.ConvertAndMove(launchpath, platform_texture, texture_format, platform_file, outputtype);
                if (inputcheckboxes[9] == true)
                {
                    Weichengenerator.ConvertAndMove(launchpath, fence_texture, texture_format, fence_file, outputtype);
                }





                if (LiRe == -1)
                {
                    //Left Handed Platforms
                    if (inputcheckboxes[5] == true)
                    {
                        if (radius == 0)
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_Straight.csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RU_Straight.csv";
                            }
                            else
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RD_Straight.csv";
                            }
                        }
                        else
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_L" + radius + ".csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RU_L" + radius + ".csv";
                            }
                            else
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RD_L" + radius + ".csv";
                        }


                    }
                    else
                    {
                        if (radius == 0)
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_Straight.csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RU_Straight.csv";
                            }
                            else
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RD_Straight.csv";
                            }
                        }
                        else
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_L" + radius + ".csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RU_L" + radius + ".csv";
                            }
                            else
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RD_L" + radius + ".csv";
                        }
                    }
                }
                else
                {
                    //Right Handed Platforms
                    if (inputcheckboxes[6] == true)
                    {
                        if (radius == 0)
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_Straight.csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RU_Straight.csv";
                            }
                            else
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RD_Straight.csv";
                            }
                        }
                        else
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_R" + radius + ".csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RU_R" + radius + ".csv";
                            }
                            else
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformRight_RD_R" + radius + ".csv";
                        }
                    }
                    else
                    {
                        if (radius == 0)
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_Straight.csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RU_Straight.csv";
                            }
                            else
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RD_Straight.csv";
                            }
                        }
                        else
                        {
                            if (inputcheckboxes[7] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_R" + radius + ".csv";
                            }
                            else if (inputcheckboxes[8] == true)
                            {
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RU_R" + radius + ".csv";
                            }
                            else
                                name = inputStrings[3] + "\\Output\\Platforms\\PlatformLeft_RD_R" + radius + ".csv";
                        }
                    }
                }

                //Calculate the track width to move the platforms as appropriate
                trans = new MathFunctions.Transform(1, radius, LiRe, 0);

                //Write Out to CSV
                using (var sw = new StreamWriter(name))
                {
                    //Left Sided Platform
                    if (inputcheckboxes[5] == true)
                    {
                        {
                            var a = -1;
                            //If radius is zero, use one segment
                            if (radius == 0)
                            {
                                if (inputcheckboxes[7] == true)
                                {
                                    //Straight Platform No Ramp
                                    segmente = 1;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));

                                    }
                                }
                                else if (inputcheckboxes[8] == true)
                                {
                                    //Straight Platform Ramp Up
                                    segmente = 2;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        double platheight_new = 0;
                                        if (a >= 0)
                                        {

                                            platheight_new = platheight;
                                        }
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));
                                    }
                                }
                                else
                                {
                                    //Straight Platform Ramp Down
                                    segmente = 2;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        double platheight_new = 0;
                                        if (a < 1)
                                        {

                                            platheight_new = platheight;
                                        }
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));
                                    }
                                }
                            }
                            else
                            //Otherwise, use number of segments specified by user for curved platforms
                            {
                                if (inputcheckboxes[7] == true)
                                {
                                    //Curved Platform No Ramp
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));
                                    }
                                }
                                else if (inputcheckboxes[8] == true)
                                {
                                    //Curved Platform Ramp Up
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));
                                    }
                                }

                                else
                                {
                                    //Curved Platform Ramp Down
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    var b = 0;
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + 0.01, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-2.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.55 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(-1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((-1.8 - gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(-1.8, (25.3 / segmente) * i));
                                    }
                                }
                            }

                            Constructors.PlatFace(sw, a, -LiRe_T);
                            sw.WriteLine("GenerateNormals,");

                            sw.WriteLine("LoadTexture,{0}.{1},", platform_file, texture_format);
                            Constructors.SetPlatformTexture(sw, a, 5, 1, platwidth_near, platwidth_far, segmente);

                            //We've created our platform, now create the fence
                            if (inputcheckboxes[9] == true)
                            {
                                var d = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (inputcheckboxes[7] == true)
                                    {
                                        //Straight Platform No Ramp
                                        segmente = 1;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");

                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else if (inputcheckboxes[8] == true)
                                    {
                                        //Straight Platform Ramp Up
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            double platheight_new = 0;
                                            if (d >= 0)
                                            {

                                                platheight_new = platheight;
                                            }
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Straight Platform Ramp Down
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            double platheight_new = 0;
                                            if (d < 1)
                                            {

                                                platheight_new = platheight;
                                            }
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                }
                                else
                                //Otherwise, use number of segments specified by user for curved platforms
                                {
                                    if (inputcheckboxes[7] == true)
                                    {
                                        //Curved Platform No Ramp
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else if (inputcheckboxes[8] == true)
                                    {
                                        //Curved Platform Ramp Up
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Curved Platform Ramp Down
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        var b = 0;
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((-1.55 - gaugeoffset) - platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((-1.55 - platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                }

                                //Use new Face2 method
                                Constructors.AddFace2_New(sw, d, -LiRe_T);
                                sw.WriteLine("GenerateNormals,");
                                //Load texture & set-cordinates
                                sw.WriteLine("LoadTexture,{0}.{1},", fence_file, texture_format);
                                Constructors.SetTexture(sw, d, 10, 4);
                                sw.WriteLine("SetDecalTransparentColor,0,0,255,");

                            }
                        }
                    }





                    //Right Sided Platforms
                    else if (inputcheckboxes[6] == true)
                    {
                        {
                            var a = -1;
                            //If radius is zero, use one segment
                            if (radius == 0)
                            {
                                if (inputcheckboxes[7] == true)
                                {
                                    //Straight Platform No Ramp
                                    segmente = 1;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }
                                else if (inputcheckboxes[8] == true)
                                {
                                    //Straight Platform Ramp Up
                                    segmente = 2;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        double platheight_new = 0;
                                        if (a >= 0)
                                        {

                                            platheight_new = platheight;
                                        }
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }
                                else
                                {
                                    //Straight Platform Ramp Down
                                    segmente = 2;
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        double platheight_new = 0;
                                        if (a < 1)
                                        {

                                            platheight_new = platheight;
                                        }
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }
                            }
                            else
                            //Otherwise, use number of segments specified by user for curved platforms
                            {
                                if (inputcheckboxes[7] == true)
                                {
                                    //Curved Platform No Ramp
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }
                                else if (inputcheckboxes[8] == true)
                                {
                                    //Curved Platform Ramp Up
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }

                                else
                                {
                                    //Curved Platform Ramp Down
                                    //Platform Mesh
                                    sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                    var b = 0;
                                    for (var i = 0; i <= segmente; i++, a++)
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
                                        if (i == segmente)
                                        {
                                            platheight += 0.01;
                                        }
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((2.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(3.5, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.55 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.55, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), platheight_new - 0.104, trans.Z(1.8, (25.3 / segmente) * i));
                                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((1.8 + gaugeoffset), (25.3 / segmente) * i), -0.2, trans.Z(1.8, (25.3 / segmente) * i));
                                    }
                                }
                            }

                            Constructors.PlatFace(sw, a, LiRe_T);
                            sw.WriteLine("GenerateNormals,");

                            sw.WriteLine("LoadTexture,{0}.{1},", platform_file, texture_format);


                            Constructors.SetPlatformTexture(sw, a, 5, 1, platwidth_near, platwidth_far, segmente);

                            //We've created our platform, now create the fence
                            if (inputcheckboxes[9] == true)
                            {
                                var d = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (inputcheckboxes[7] == true)
                                    {
                                        //Straight Platform No Ramp
                                        segmente = 1;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");

                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else if (inputcheckboxes[8] == true)
                                    {
                                        //Straight Platform Ramp Up
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            double platheight_new = 0;
                                            if (d >= 0)
                                            {

                                                platheight_new = platheight;
                                            }
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Straight Platform Ramp Down
                                        segmente = 2;
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            double platheight_new = 0;
                                            if (d < 1)
                                            {

                                                platheight_new = platheight;
                                            }
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            platwidth = (platwidth - 0.3);
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                }
                                else
                                //Otherwise, use number of segments specified by user for curved platforms
                                {
                                    if (inputcheckboxes[7] == true)
                                    {
                                        //Curved Platform No Ramp
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight, trans.Z((-1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else if (inputcheckboxes[8] == true)
                                    {
                                        //Curved Platform Ramp Up
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                    else
                                    {
                                        //Curved Platform Ramp Down
                                        //Platform Mesh
                                        sw.WriteLine("\r\nCreateMeshBuilder ;Platform Mesh");
                                        var b = 0;
                                        for (var i = 0; i <= segmente; i++, d++)
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
                                            if (i == segmente)
                                            {
                                                platheight += 0.01;
                                            }
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(((1.55 + gaugeoffset) + platwidth), (25.3 / segmente) * i), platheight_new + fenceheight, trans.Z((1.55 + platwidth), (25.3 / segmente) * i));
                                        }
                                    }
                                }

                                //Use new Face2 method
                                Constructors.AddFace2_New(sw, d, -LiRe_T);
                                sw.WriteLine("GenerateNormals,");
                                //Load texture & set-cordinates
                                sw.WriteLine("LoadTexture,{0}.{1},", fence_file, texture_format);
                                Constructors.SetTexture(sw, d, 10, 4);
                                sw.WriteLine("SetDecalTransparentColor,0,0,255,");
                            }
                        }

                    }
                }
            }
        }
    }
}
