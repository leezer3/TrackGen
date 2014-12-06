/*
 * Curve Generation
 */

using System.IO;
using System.Windows.Forms;

namespace Weiche
{
    class CurvedTrack
    {
        internal static void BuildCurve(string[] inputStrings, bool[] inputcheckboxes)
        {
            {
                double radius;
                double segmente;
                double trackgauge;
                double gaugeoffset;
                var LiRe = 1;
                var LiRe_T = 1;
                string name;
                string launchpath = inputStrings[4];
                string ballast_texture = inputStrings[5];
                string ballast_file = inputStrings[6];
                string sleeper_texture = inputStrings[7];
                string sleeper_file = inputStrings[8];
                string railside_texture = inputStrings[9];
                string railside_file = inputStrings[10];
                string railtop_texture = inputStrings[11];
                string railtop_file = inputStrings[12];
                string embankment_texture = inputStrings[13];
                string embankment_file = inputStrings[14];
                string texture_format = inputStrings[15];
                MathFunctions.Transform trans;
                /*
                 Curves
                 */
                //Initialise
                bool EingabeOK;
                //Check that radius is a valid number
                EingabeOK = double.TryParse(inputStrings[0], out radius);
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

                EingabeOK = double.TryParse(inputStrings[1], out segmente);
                if (EingabeOK == false)
                {
                    MessageBox.Show("Eingabefehler Segmente!");
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
                        gaugeoffset = ((trackgauge - 1.44) / 2);
                    }
                    else
                    {
                        gaugeoffset = 0;
                    }
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


                if (LiRe == -1)
                {
                    name = (inputStrings[3]) + "\\Output\\Tracks\\L" + radius + ".csv";
                }
                else
                {
                    name = (inputStrings[3]) + "\\Output\\Tracks\\R" + radius + ".csv";
                }

                //Create Output directory
                if (!System.IO.Directory.Exists(inputStrings[3] + "\\Output\\Tracks"))
                {
                    System.IO.Directory.CreateDirectory(inputStrings[3] + "\\Output\\Tracks");
                }


                //Main Textures
                const string outputtype = "Tracks";
                Weichengenerator.ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);

                //Embankment Texture
                if (inputcheckboxes[1] == false)
                {
                    Weichengenerator.ConvertAndMove(launchpath, embankment_texture, texture_format, embankment_file, outputtype);
                }

                //Rail Textures
                if (inputcheckboxes[4] == false)
                {
                    Weichengenerator.ConvertAndMove(launchpath, railside_texture, texture_format, railside_file, outputtype);
                    Weichengenerator.ConvertAndMove(launchpath, railtop_texture, texture_format, railtop_file, outputtype);
                }


                trans = new MathFunctions.Transform(1, radius, LiRe, 0);


                //Write out to CSV
                using (var sw = new StreamWriter(name))
                {

                    //Railtop Left

                    var a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railtop Links");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.72 - gaugeoffset), (25 / segmente) * i), trans.Z(-0.72, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.78 - gaugeoffset), (25 / segmente) * i), trans.Z(-0.78, (25 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, -LiRe_T);
                    if (inputcheckboxes[4] == false)
                    {
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);

                        Constructors.SetTexture(sw, a, 1, 1);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }

                    //Rail Top Right

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railtop Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.78 + gaugeoffset), (25 / segmente) * i), trans.Z(0.78, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.72 + gaugeoffset), (25 / segmente) * i), trans.Z(0.72, (25 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, -LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (inputcheckboxes[4] == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }



                    //Railside Left

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railside Links");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.74 - gaugeoffset), (25 / segmente) * i), trans.Z(-0.74, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.74 - gaugeoffset), (25 / segmente) * i), trans.Z(-0.74, (25 / segmente) * i));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (inputcheckboxes[4] == false)
                    {
                        sw.WriteLine("LoadTexture,railside.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Right Rail Side

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railside Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74 + gaugeoffset), (25 / segmente) * i), trans.Z(0.74, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74 + gaugeoffset), (25 / segmente) * i), trans.Z(0.74, (25 / segmente) * i));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (inputcheckboxes[4] == false)
                    {
                        sw.WriteLine("LoadTexture,railside.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Sleepers

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Schwellen");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 - gaugeoffset), (25 / segmente) * i), trans.Z(-1.3, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((1.3 + gaugeoffset), (25 / segmente) * i), trans.Z(1.3, (25 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", sleeper_file, texture_format);
                    Constructors.SetTexture(sw, a, 15, 2);

                    //Ballast Left

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Links");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((-2.8 - gaugeoffset), (25 / segmente) * i), trans.Z(-2.8, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 - gaugeoffset), (25 / segmente) * i), trans.Z(-1.3, (25 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                    Constructors.SetTexture(sw, a, 10, 2);

                    //Ballast Right

                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((1.3 + gaugeoffset), (25 / segmente) * i), trans.Z(1.3, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((2.8 + gaugeoffset), (25 / segmente) * i), trans.Z(2.8, (25 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                    Constructors.SetTexture(sw, a, 10, 1);

                    if (inputcheckboxes[1] == false)
                    {

                        //Grass Left

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Grass Links");
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((-3.6 - gaugeoffset), (25 / segmente) * i), trans.Z(-3.6, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((-2.5 - gaugeoffset), (25 / segmente) * i), trans.Z(-2.5, (25 / segmente) * i));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", embankment_file, texture_format);
                        Constructors.SetTexture(sw, a, 3, 1);

                        //Grass Right

                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Grass Rechts");
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((2.5 + gaugeoffset), (25 / segmente) * i), trans.Z(2.5, (25 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((3.6 + gaugeoffset), (25 / segmente) * i), trans.Z(3.6, (25 / segmente) * i));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", embankment_file, texture_format);
                        Constructors.SetTexture(sw, a, 3, 2);
                    }


                }
            }
        }
    }
}
