﻿/*
 * Viaduct Generation
 */

using System.Windows.Forms;
using System.IO;
namespace Weiche
{
    class Viaducts
    {
        internal static void BuildViaduct(string[] inputStrings, bool[] inputcheckboxes)
        {
            {
                double radius;
                //Fixed 25 segments for viaducts, required to have consistant mesh
                double segmente = 25;
                double trackgauge;
                double gaugeoffset;
                var LiRe = 1;
                var LiRe_T = 1;
                string name;
                string launchpath = inputStrings[4];
                string texture_format = inputStrings[15];
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

                if ((radius != 0) && (radius <= 49) && (radius >= -49))
                {
                    MessageBox.Show("Radius for viaducts should either be 0 for Straight or greater than 50m!");
                    return;
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
                        gaugeoffset = ((trackgauge - 1.44)/2);
                    }
                    else
                    {
                        gaugeoffset = 0;
                    }
                }

                //Left or right definition
                if (radius < 0)
                {
                    LiRe = -1;
                    radius = radius*-1;
                }

                //Create Output directory
                if (!System.IO.Directory.Exists(inputStrings[3] + "\\Output\\Viaducts"))
                {
                    System.IO.Directory.CreateDirectory(inputStrings[3] + "\\Output\\Viaducts");
                }

                //Main Textures
                const string outputtype = "Viaducts";
                /*
                 * Sort out later when textures are required
                 * 
                Weichengenerator.ConvertAndMove(launchpath, platform_texture, texture_format, platform_file, outputtype);
                if (inputcheckboxes[9] == true)
                {
                    Weichengenerator.ConvertAndMove(launchpath, fence_texture, texture_format, fence_file, outputtype);
                }
                 */
                if (radius == 0)
                {
                    name = inputStrings[3] + "\\Output\\Viaducts\\Viaduct_Straight.csv";
                }
                else if (LiRe == -1)
                {
                    name = inputStrings[3] + "\\Output\\Viaducts\\Viaduct_L" + radius + ".csv";
                }
                else
                {
                    name = inputStrings[3] + "\\Output\\Viaducts\\Viaduct_R" + radius + ".csv";
                }

                //Calculate the track width to move the viaduct sides as appropriate
                trans = new MathFunctions.Transform(1, radius, LiRe, 0);

                //Write Out to CSV
                using (var sw = new StreamWriter(name))
                {
                    //Straight Viaduct, requires a single segment
                    if (radius == 0)
                    {
                        segmente = 1;
                    }

                    
                    {
                        //LHS Main Wall
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall LHS");
                        int a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-4.9 - gaugeoffset, (25.3/ segmente) *i),0,trans.Z(-4.9, (25.3/ segmente) *i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), -22, trans.Z(-4.9, (25.3 / segmente) * i));
                        }

                        Constructors.AddFace(sw, a, -LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct1", texture_format);
                        if(LiRe == -1 || radius == 0)
                        { 
                            Constructors.SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            Constructors.SetTexture(sw, a, 1.03, 9);
                        }
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        //RHS Main Wall
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall RHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(8.9, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, (25.3 / segmente) * i), -22, trans.Z(8.9, (25.3 / segmente) * i));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct1", texture_format);
                        if (LiRe == -1 || radius == 0)
                        {
                            Constructors.SetTexture(sw, a, 1, 3);
                        }
                        else
                        {
                            Constructors.SetTexture(sw, a, 1, 3);
                        }
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        //LHS Wall top
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall LHS Top");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.8 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-4.8, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.8 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(-4.8, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(-4.9, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-4.9, (25.3 / segmente) * i));
                        }
                        Constructors.AddViaductFace(sw, a, LiRe_T, 1);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct2", texture_format);
                        Constructors.SetTexture(sw, a, 1, 6);
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        //RHS Wall top
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall RHS Top");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.8 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(8.8, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.8 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(8.8, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(8.9, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(8.9, (25.3 / segmente) * i));
                        }
                        Constructors.AddViaductFace(sw, a, -LiRe_T, 1);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct2", texture_format);
                        Constructors.SetTexture(sw, a, 1, 6);
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        //LHS Footwalk
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Footwalk LHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-4.8 - gaugeoffset, (25.3/segmente)*i), 0, trans.Z(-4.8, (25.3/segmente)*i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-2.45 - gaugeoffset, (25.3/segmente)*i), 0, trans.Z(-2.45, (25.3/segmente)*i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-2.45 - gaugeoffset, (25.3/segmente)*i), -0.5, trans.Z(-2.45, (25.3/segmente)*i));
                        }
                        Constructors.AddViaductFace(sw, a, -LiRe_T, 2);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct3", texture_format);
                        Constructors.SetTexture(sw, a, 5, 7);

                        //RHS Footwalk
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Footwalk RHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.8 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(8.8, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(6.45, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 - gaugeoffset, (25.3 / segmente) * i), -0.5, trans.Z(6.45, (25.3 / segmente) * i));
                        }
                        Constructors.AddViaductFace(sw, a, LiRe_T, 2);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct3", texture_format);
                        Constructors.SetTexture(sw, a, 5, 7);
                        //Inside
                        //No segment 1
                        //Segment 2
                        //Bottom IS
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Underside");
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 2.024), -22, trans.Z(-4.9 - gaugeoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 2.024), -22, trans.Z(-4.9 - gaugeoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 2.024), -10.5, trans.Z(-4.9 - gaugeoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 2.024), -10.5, trans.Z(-4.9 - gaugeoffset, 2.024));
                        //Segment 3
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 3.036), -7.0, trans.Z(-4.9 - gaugeoffset, 3.036));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 3.036), -7.0, trans.Z(8.9 - gaugeoffset, 3.036));
                        //Segment 4
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 4.048), -5.6, trans.Z(8.9 - gaugeoffset, 4.048));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 4.048), -5.6, trans.Z(-4.9 - gaugeoffset, 4.048));
                        //Segment 5
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 5.06), -4.5, trans.Z(-4.9 - gaugeoffset, 5.06));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 5.06), -4.5, trans.Z(8.9 - gaugeoffset, 5.06));
                        //Segment 6
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 6.072), -3.6, trans.Z(8.9 - gaugeoffset, 6.072));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 6.072), -3.6, trans.Z(-4.9 - gaugeoffset, 6.072));
                        //Segment 7
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 7.084), -3, trans.Z(-4.9 - gaugeoffset, 7.084));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 7.084), -3, trans.Z(8.9 - gaugeoffset, 7.084));
                        //Segment 8
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 8.096), -2.5, trans.Z(8.9 - gaugeoffset, 8.096));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 8.096), -2.5, trans.Z(-4.9 - gaugeoffset, 8.096));
                        //Segment 9
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 9.108), -2.1, trans.Z(-4.9 - gaugeoffset, 9.108));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 9.108), -2.1, trans.Z(8.9 - gaugeoffset, 9.108));
                        //Segment 10
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 10.12), -1.8, trans.Z(8.9 - gaugeoffset, 10.12));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 10.12), -1.8, trans.Z(-4.9 - gaugeoffset, 10.12));
                        //Segment 11
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 11.132), -1.6, trans.Z(-4.9 - gaugeoffset, 11.132));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 11.132), -1.6, trans.Z(8.9 - gaugeoffset, 11.132));
                        //Segment 12
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 12.144), -1.5, trans.Z(8.9 - gaugeoffset, 12.144));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 12.144), -1.5, trans.Z(-4.9 - gaugeoffset, 12.144));
                        //Segment 13
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 13.156), -1.5, trans.Z(-4.9 - gaugeoffset, 13.156));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 13.156), -1.5, trans.Z(8.9 - gaugeoffset, 13.156));
                        //Segment 14
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 14.168), -1.6, trans.Z(8.9 - gaugeoffset, 14.168));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 14.168), -1.6, trans.Z(-4.9 - gaugeoffset, 14.168));
                        //Segment 15
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 15.18), -1.8, trans.Z(-4.9 - gaugeoffset, 15.18));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 15.18), -1.8, trans.Z(8.9 - gaugeoffset, 15.18));
                        //Segment 16
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 16.192), -2.1, trans.Z(8.9 - gaugeoffset, 16.192));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 16.192), -2.1, trans.Z(-4.9 - gaugeoffset, 16.192));
                        //Segment 17
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 17.204), -2.5, trans.Z(-4.9 - gaugeoffset, 17.204));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 17.204), -2.5, trans.Z(8.9 - gaugeoffset, 17.204));
                        //Segement 18
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 18.216), -3, trans.Z(8.9 - gaugeoffset, 18.216));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 18.216), -3, trans.Z(-4.9 - gaugeoffset, 18.216));
                        //Segment 19
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 19.228), -3.6, trans.Z(-4.9 - gaugeoffset, 19.228));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 19.228), -3.6, trans.Z(8.9 - gaugeoffset, 19.228));
                        //Segment 20
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 20.24), -4.5, trans.Z(8.9 - gaugeoffset, 20.24));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 20.24), -4.5, trans.Z(-4.9 - gaugeoffset, 20.24));
                        //Segment 21
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 21.252), -5.6, trans.Z(-4.9 - gaugeoffset, 21.252));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 21.252), -5.6, trans.Z(8.9 - gaugeoffset, 21.252));
                        //Segment 22
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 22.264), -7.0, trans.Z(8.9 - gaugeoffset, 22.264));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 22.264), -7.0, trans.Z(-4.9 - gaugeoffset, 22.264));
                        //Segment 23
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -10.5, trans.Z(-4.9 - gaugeoffset, 23.276));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 23.276), -10.5, trans.Z(8.9 - gaugeoffset, 23.276));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 - gaugeoffset, 23.276), -22, trans.Z(8.9 - gaugeoffset, 23.276));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -22, trans.Z(-4.9 - gaugeoffset, 23.276));
                        
                        Constructors.AddViaductFace(sw, 23, LiRe_T,3);
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct4", texture_format);
                        sw.WriteLine("SetTextureCoordinates,0,0,1");
                        sw.WriteLine("SetTextureCoordinates,1,1,1");
                        sw.WriteLine("SetTextureCoordinates,2,1,0.27");
                        sw.WriteLine("SetTextureCoordinates,3,0,0.27");
                        int j = 4;
                        for (var i = 0; i <= 18; i++)
                        {
                            if (i%2 == 0)
                            {
                                sw.WriteLine("SetTextureCoordinates,{0},0,0", j);
                                sw.WriteLine("SetTextureCoordinates,{0},1,0", j+1);
                            }
                            else
                            {
                                sw.WriteLine("SetTextureCoordinates,{0},1,0.25", j);
                                sw.WriteLine("SetTextureCoordinates,{0},0,0.25", j + 1);
                            }
                            j = j+ 2;
                        }
                        sw.WriteLine("SetTextureCoordinates,42,1,0.1");
                        sw.WriteLine("SetTextureCoordinates,43,0,0.1");
                        sw.WriteLine("SetTextureCoordinates,44,0,0.27");
                        sw.WriteLine("SetTextureCoordinates,45,1,0.27");
                        sw.WriteLine("SetTextureCoordinates,46,1,1");
                        sw.WriteLine("SetTextureCoordinates,47,0,1");
                        


                    }
                }
            }
        }
    }
}