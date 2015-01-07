/*
 * Curve Generation
 */

using System.IO;

namespace Weiche
{
    class CurvedTrack
    {
        internal static void BuildCurve()
        {
            {
                var LiRe = 1;
                var LiRe_T = 1;
                string name;
                MathFunctions.Transform trans;
                double radius = Weichengenerator.radius;
                double segmente = Weichengenerator.segmente;
                double gaugeoffset = Weichengenerator.gaugeoffset;
                string launchpath = Weichengenerator.path;
                string texture_format = Weichengenerator.texture_format;
                string ballast_texture = Weichengenerator.ballast_texture;
                string ballast_file = Weichengenerator.ballast_file;
                string sleeper_texture = Weichengenerator.sleeper_texture;
                string sleeper_file = Weichengenerator.sleeper_file;
                string railtop_texture = Weichengenerator.railtop_texture;
                string railtop_file = Weichengenerator.railtop_file;
                string railside_texture = Weichengenerator.railside_texture;
                string railside_file = Weichengenerator.railside_file;
                string embankment_texture = Weichengenerator.embankment_texture;
                string embankment_file = Weichengenerator.embankment_file;
               /*
                 Curves
                 */
                //Initialise
                

                //Left or right definition
                if (radius < 0)
                {
                    LiRe = -1;
                    radius = radius * -1;
                }


                if (LiRe == -1)
                {
                    name = launchpath + "\\Output\\Tracks\\L" + radius + ".csv";
                }
                else
                {
                    name = launchpath + "\\Output\\Tracks\\R" + radius + ".csv";
                }

                //Create Output directory
                if (!System.IO.Directory.Exists(launchpath + "\\Output\\Tracks"))
                {
                    System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Tracks");
                }


                //Main Textures
                const string outputtype = "Tracks";
                Weichengenerator.ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);

                //Embankment Texture
                if (Weichengenerator.noembankment == false)
                {
                    Weichengenerator.ConvertAndMove(launchpath, embankment_texture, texture_format, embankment_file, outputtype);
                }

                //Rail Textures
                if (Weichengenerator.norailtexture == false)
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
                    var height = 0.0;
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-0.72 - gaugeoffset), (25.3 / segmente) * i),height, trans.Z(-0.72, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-0.78 - gaugeoffset), (25.3 / segmente) * i),height, trans.Z(-0.78, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, -LiRe_T);
                    if (Weichengenerator.norailtexture == false)
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
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railtop Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((0.78 + gaugeoffset), (25.3 / segmente) * i),height, trans.Z(0.78, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((0.72 + gaugeoffset), (25.3 / segmente) * i),height, trans.Z(0.72, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, -LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }



                    //Railside Left
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railside Links");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-0.74 - gaugeoffset), (25.3 / segmente) * i),height, trans.Z(-0.74, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-0.74 - gaugeoffset), (25.3 / segmente) * i),height -0.15, trans.Z(-0.74, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railside.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Right Rail Side
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Railside Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((0.74 + gaugeoffset), (25.3 / segmente) * i),height, trans.Z(0.74, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((0.74 + gaugeoffset), (25.3 / segmente) * i),height - 0.15, trans.Z(0.74, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railside.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Sleepers
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Schwellen");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-1.3 - gaugeoffset), (25.3 / segmente) * i), height -0.15, trans.Z(-1.3, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((1.3 + gaugeoffset), (25.3 / segmente) * i), height -0.15, trans.Z(1.3, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", sleeper_file, texture_format);
                    Constructors.SetTexture(sw, a, 15, 2);

                    //Ballast Left
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Links");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-2.8 - gaugeoffset), (25.3 / segmente) * i), height -0.4, trans.Z(-2.8, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-1.3 - gaugeoffset), (25.3 / segmente) * i), height -0.15, trans.Z(-1.3, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                    Constructors.SetTexture(sw, a, 10, 2);

                    //Ballast Right
                    height = 0.0;
                    a = -1;
                    sw.WriteLine("\r\nCreateMeshBuilder ;Boeschung Rechts");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        if (i == segmente)
                        {
                            height += 0.01;
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((1.3 + gaugeoffset), (25.3 / segmente) * i), height -0.15, trans.Z(1.3, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((2.8 + gaugeoffset), (25.3 / segmente) * i), height -0.4, trans.Z(2.8, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                    Constructors.SetTexture(sw, a, 10, 1);

                    if (Weichengenerator.noembankment == false)
                    {

                        //Grass Left
                        height = 0.0;
                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Grass Links");
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            if (i == segmente)
                            {
                                height += 0.01;
                            }
                            sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-3.6 - gaugeoffset), (25.3 / segmente) * i), height -0.3,  trans.Z(-3.6, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((-2.5 - gaugeoffset), (25.3 / segmente) * i), height -0.35, trans.Z(-2.5, (25.3 / segmente) * i));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", embankment_file, texture_format);
                        Constructors.SetTexture(sw, a, 3, 1);

                        //Grass Right
                        height = 0.0;
                        a = -1;
                        sw.WriteLine("\r\nCreateMeshBuilder ;Grass Rechts");
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            if (i == segmente)
                            {
                                height += 0.01;
                            }
                            sw.WriteLine("AddVertex,{0:f4},{1:f2},{2:f4},", trans.X((2.5 + gaugeoffset), (25.3 / segmente) * i), height -0.35, trans.Z(2.5, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X((3.6 + gaugeoffset), (25.3 / segmente) * i), height -0.3, trans.Z(3.6, (25.3 / segmente) * i));
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
