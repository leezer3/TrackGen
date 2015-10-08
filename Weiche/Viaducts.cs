/*
 * Viaduct Generation
 */

using System.IO;
using System.Runtime.InteropServices;

namespace Weiche
{
    class Viaducts
    {
        internal static void BuildViaduct()
        {
            {
                var LiRe = 1;
                var LiRe_T = 1;
                double gaugeoffset = Weichengenerator.gaugeoffset;
                string texture_format = Weichengenerator.texture_format;
                double radius = Weichengenerator.radius;
                string launchpath = Weichengenerator.path;
                string arch_texture = Weichengenerator.arch_texture;
                string arch_file = Weichengenerator.arch_file;
                string topwall_texture = Weichengenerator.topwall_texture;
                string topwall_file = Weichengenerator.topwall_file;
                string footwalk_texture = Weichengenerator.footwalk_texture;
                string footwalk_file = Weichengenerator.footwalk_file;
                string archis_texture = Weichengenerator.archis_texture;
                string archis_file = Weichengenerator.archis_file;
                double additionalpierheight = Weichengenerator.additionalpierheight;
                string additionalpier_texture = (Weichengenerator.launchpath + "\\Textures\\viaduct6.png");
                string additionalpier_file = "viaduct6";
                int numberoftracks = Weichengenerator.numberoftracks;
                double trackoffset;
                string name;
                double segmente;
                MathFunctions.Transform trans;
                
                //Create Output directory
                if (!System.IO.Directory.Exists(launchpath + "\\Output\\Viaducts"))
                {
                    System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Viaducts");
                }

                //Main Textures
                const string outputtype = "Viaducts";

                Weichengenerator.ConvertAndMove(launchpath, arch_texture, texture_format, arch_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, topwall_texture, texture_format, topwall_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, footwalk_texture, texture_format, footwalk_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, archis_texture, texture_format, archis_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, additionalpier_texture, texture_format, additionalpier_file, outputtype);

                //Left or right definition
                if (radius < 0)
                {
                    LiRe = -1;
                    radius = radius * -1;
                }

                if (radius == 0)
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_Straight.csv";
                    segmente = 1;
                }
                else if (LiRe == -1)
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_L" + radius + ".csv";
                    segmente = 25;
                }
                else
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_R" + radius + ".csv";
                    segmente = 25;
                }
                if (numberoftracks == 1)
                {
                    trackoffset = 0 - (4.0 + (gaugeoffset * 2));
                }
                else
                {
                    trackoffset = (4.0 + (gaugeoffset * 2)) * (numberoftracks - 2);
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
                        if (additionalpierheight != 0)
                        {
                            //LHS Main Wall Bottom
                            sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall LHS Bottom");
                            a = -1;
                            for (var i = 0; i <= segmente; i++, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), -22, trans.Z(-4.9, (25.3 / segmente) * i));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), -22 - additionalpierheight, trans.Z(-4.9, (25.3 / segmente) * i));
                            }

                            Constructors.AddFace(sw, a, -LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0}.{1},", "viaduct6", texture_format);
                            var b = 0;
                            if (LiRe == -1 || radius == 0)
                            {
                                for (double i = a; i >= 0; i--)
                                {
                                    sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, (i * 1 / a));
                                    sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2:f4},", b + 1, (i * 1 / a), 0.089 * additionalpierheight);
                                    b = b + 2;
                                }
                            }
                            else
                            {
                                for (double i = a; i >= 0; i--)
                                {
                                    sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, (i * 1 / a) - 0.015);
                                    sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2:f4},", b + 1, (i * 1 / a) - 0.015, 0.089 * additionalpierheight);
                                    b = b + 2;
                                }
                            }
                            sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        }
                        //RHS Main Wall
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall RHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 0, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -22, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
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
                        if (additionalpierheight != 0)
                        {
                            //RHS Main Wall Bottom
                            sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall LHS Bottom");
                            a = -1;
                            for (var i = 0; i <= segmente; i++, a++)
                            {
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -22, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -22 - additionalpierheight, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            }

                            Constructors.AddFace(sw, a, LiRe_T);

                            sw.WriteLine("GenerateNormals,");
                            sw.WriteLine("LoadTexture,{0}.{1},", "viaduct6", texture_format);
                            var b = 0;
                            for (double i = a; i >= 0; i--)
                            {
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, (i * 1 / a));
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2:f4},", b + 1, (i * 1 / a), 0.089 * additionalpierheight);
                                b = b + 2;
                            }
                            sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        }
                        
                        //LHS Wall top
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall LHS Top");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.8 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-4.8 - gaugeoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.8 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(-4.8 - gaugeoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), 1, trans.Z(-4.9 - gaugeoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-4.9 - gaugeoffset, (25.3 / segmente) * i));
                        }
                        Constructors.AddViaductFace(sw, a, LiRe_T, 1);
                        //Wall Ends
                        sw.WriteLine("AddFace,3,2,1,0,");
                        if (segmente == 1)
                        {
                            sw.WriteLine("AddFace,4,5,6,7,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,100,101,102,103,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct2", texture_format);
                        Constructors.SetTexture(sw, a, 1, 6);
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        
                        //RHS Wall top
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Wall RHS Top");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.8 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 0, trans.Z(8.8 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.8 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 1, trans.Z(8.8 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 1, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 0, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        }
                        Constructors.AddViaductFace(sw, a, -LiRe_T, 1);
                        sw.WriteLine("AddFace,0,1,2,3,");
                        if (segmente == 1)
                        {
                            sw.WriteLine("AddFace,7,6,5,4,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,103,102,101,100,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct2", texture_format);
                        Constructors.SetTexture(sw, a, 1, 6);
                        sw.WriteLine("SetDecalTransparentColor,0,0,0");
                        //LHS Footwalk
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Footwalk LHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-4.9 - gaugeoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, (25.3 / segmente) * i), 0, trans.Z(-2.45 - gaugeoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, (25.3 / segmente) * i), -0.5, trans.Z(-2.45 - gaugeoffset, (25.3 / segmente) * i));
                        }

                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 0), 0, trans.Z(-4.9 - gaugeoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, 0), 0, trans.Z(-2.45 - gaugeoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, 0), -0.5, trans.Z(-2.45 - gaugeoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 0), -0.5, trans.Z(-4.9 - gaugeoffset, 0));

                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, 25.3), 0, trans.Z(-2.45 - gaugeoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.45 - gaugeoffset, 25.3), -0.5, trans.Z(-2.45 - gaugeoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 25.3), -0.5, trans.Z(-4.9 - gaugeoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 25.3), 0, trans.Z(-4.9 - gaugeoffset, 25.3));
                        Constructors.AddViaductFace(sw, a, -LiRe_T, 2);
                        if (segmente == 1)
                        {
                            sw.WriteLine("AddFace,6,7,8,9,");
                            sw.WriteLine("AddFace,13,12,11,10,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,78,79,80,81,");
                            sw.WriteLine("AddFace,85,84,83,82,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct3", texture_format);
                        Constructors.SetTexture(sw, a, 5, 7);
                        if (segmente == 1)
                        {
                            sw.WriteLine("SetTextureCoordinates,6,0,0,");
                            sw.WriteLine("SetTextureCoordinates,7,1,0,");
                            sw.WriteLine("SetTextureCoordinates,8,1,1,");
                            sw.WriteLine("SetTextureCoordinates,9,0,1,");
                            sw.WriteLine("SetTextureCoordinates,10,0,0,");
                            sw.WriteLine("SetTextureCoordinates,11,1,0,");
                            sw.WriteLine("SetTextureCoordinates,12,1,1,");
                            sw.WriteLine("SetTextureCoordinates,13,0,1,");
                        }
                        else
                        {
                            sw.WriteLine("SetTextureCoordinates,78,0,0,");
                            sw.WriteLine("SetTextureCoordinates,79,1,0,");
                            sw.WriteLine("SetTextureCoordinates,80,1,1,");
                            sw.WriteLine("SetTextureCoordinates,81,0,1,");
                            sw.WriteLine("SetTextureCoordinates,82,0,0,");
                            sw.WriteLine("SetTextureCoordinates,83,1,0,");
                            sw.WriteLine("SetTextureCoordinates,84,1,1,");
                            sw.WriteLine("SetTextureCoordinates,85,0,1,");
                        }
                        //RHS Footwalk
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Footwalk RHS");
                        a = -1;
                        for (var i = 0; i <= segmente; i++, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 0, trans.Z(8.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 0, trans.Z(6.45 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -0.5, trans.Z(6.45 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        }
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 0), 0, trans.Z(8.9 + gaugeoffset + trackoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, 0), 0, trans.Z(6.45 + gaugeoffset + trackoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, 0), -0.5, trans.Z(6.45 + gaugeoffset + trackoffset, 0));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 0), -0.5, trans.Z(8.9 + gaugeoffset + trackoffset, 0));

                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, 25.3), 0, trans.Z(6.45 + gaugeoffset + trackoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.45 + gaugeoffset + trackoffset, 25.3), -0.5, trans.Z(6.45 + gaugeoffset + trackoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 25.3), -0.5, trans.Z(8.9 + gaugeoffset + trackoffset, 25.3));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 25.3), 0, trans.Z(8.9 + gaugeoffset + trackoffset, 25.3));
                        Constructors.AddViaductFace(sw, a, LiRe_T, 2);
                        if (segmente == 1)
                        {
                            sw.WriteLine("AddFace,9,8,7,6,");
                            sw.WriteLine("AddFace,10,11,12,13,");
                        }
                        else
                        {
                            sw.WriteLine("AddFace,81,80,79,78,");
                            sw.WriteLine("AddFace,82,83,84,85,");
                        }
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", "viaduct3", texture_format);
                        Constructors.SetTexture(sw, a, 5, 7);
                        if (segmente == 1)
                        {
                            sw.WriteLine("SetTextureCoordinates,6,0,0,");
                            sw.WriteLine("SetTextureCoordinates,7,1,0,");
                            sw.WriteLine("SetTextureCoordinates,8,1,1,");
                            sw.WriteLine("SetTextureCoordinates,9,0,1,");
                            sw.WriteLine("SetTextureCoordinates,10,0,0,");
                            sw.WriteLine("SetTextureCoordinates,11,1,0,");
                            sw.WriteLine("SetTextureCoordinates,12,1,1,");
                            sw.WriteLine("SetTextureCoordinates,13,0,1,");
                        }
                        else
                        {
                            sw.WriteLine("SetTextureCoordinates,78,0,0,");
                            sw.WriteLine("SetTextureCoordinates,79,1,0,");
                            sw.WriteLine("SetTextureCoordinates,80,1,1,");
                            sw.WriteLine("SetTextureCoordinates,81,0,1,");
                            sw.WriteLine("SetTextureCoordinates,82,0,0,");
                            sw.WriteLine("SetTextureCoordinates,83,1,0,");
                            sw.WriteLine("SetTextureCoordinates,84,1,1,");
                            sw.WriteLine("SetTextureCoordinates,85,0,1,");
                        }
                        //Inside
                        //No segment 1
                        //Segment 2
                        //Bottom IS
                        sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Underside");
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 2.024), -22, trans.Z(-4.9 - gaugeoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 2.024), -22, trans.Z(8.9 + gaugeoffset + trackoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 2.024), -10.5, trans.Z(8.9 + gaugeoffset + trackoffset, 2.024));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 2.024), -10.5, trans.Z(-4.9 - gaugeoffset, 2.024));
                        //Segment 3
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 3.036), -7.0, trans.Z(-4.9 - gaugeoffset, 3.036));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 3.036), -7.0, trans.Z(8.9 + gaugeoffset + trackoffset, 3.036));
                        //Segment 4
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 4.048), -5.6, trans.Z(8.9 + gaugeoffset + trackoffset, 4.048));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 4.048), -5.6, trans.Z(-4.9 - gaugeoffset, 4.048));
                        //Segment 5
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 5.06), -4.5, trans.Z(-4.9 - gaugeoffset, 5.06));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 5.06), -4.5, trans.Z(8.9 + gaugeoffset + trackoffset, 5.06));
                        //Segment 6
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 6.072), -3.6, trans.Z(8.9 + gaugeoffset + trackoffset, 6.072));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 6.072), -3.6, trans.Z(-4.9 - gaugeoffset, 6.072));
                        //Segment 7
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 7.084), -3, trans.Z(-4.9 - gaugeoffset, 7.084));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 7.084), -3, trans.Z(8.9 + gaugeoffset + trackoffset, 7.084));
                        //Segment 8
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 8.096), -2.5, trans.Z(8.9 + gaugeoffset + trackoffset, 8.096));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 8.096), -2.5, trans.Z(-4.9 - gaugeoffset, 8.096));
                        //Segment 9
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 9.108), -2.1, trans.Z(-4.9 - gaugeoffset, 9.108));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 9.108), -2.1, trans.Z(8.9 + gaugeoffset + trackoffset, 9.108));
                        //Segment 10
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 10.12), -1.8, trans.Z(8.9 + gaugeoffset + trackoffset, 10.12));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 10.12), -1.8, trans.Z(-4.9 - gaugeoffset, 10.12));
                        //Segment 11
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 11.132), -1.6, trans.Z(-4.9 - gaugeoffset, 11.132));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 11.132), -1.6, trans.Z(8.9 + gaugeoffset + trackoffset, 11.132));
                        //Segment 12
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 12.144), -1.5, trans.Z(8.9 + gaugeoffset + trackoffset, 12.144));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 12.144), -1.5, trans.Z(-4.9 - gaugeoffset, 12.144));
                        //Segment 13
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 13.156), -1.5, trans.Z(-4.9 - gaugeoffset, 13.156));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 13.156), -1.5, trans.Z(8.9 + gaugeoffset + trackoffset, 13.156));
                        //Segment 14
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 14.168), -1.6, trans.Z(8.9 + gaugeoffset + trackoffset, 14.168));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 14.168), -1.6, trans.Z(-4.9 - gaugeoffset, 14.168));
                        //Segment 15
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 15.18), -1.8, trans.Z(-4.9 - gaugeoffset, 15.18));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 15.18), -1.8, trans.Z(8.9 + gaugeoffset + trackoffset, 15.18));
                        //Segment 16
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 16.192), -2.1, trans.Z(8.9 + gaugeoffset + trackoffset, 16.192));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 16.192), -2.1, trans.Z(-4.9 - gaugeoffset, 16.192));
                        //Segment 17
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 17.204), -2.5, trans.Z(-4.9 - gaugeoffset, 17.204));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 17.204), -2.5, trans.Z(8.9 + gaugeoffset + trackoffset, 17.204));
                        //Segement 18
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 18.216), -3, trans.Z(8.9 + gaugeoffset + trackoffset, 18.216));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 18.216), -3, trans.Z(-4.9 - gaugeoffset, 18.216));
                        //Segment 19
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 19.228), -3.6, trans.Z(-4.9 - gaugeoffset, 19.228));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 19.228), -3.6, trans.Z(8.9 + gaugeoffset + trackoffset, 19.228));
                        //Segment 20
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 20.24), -4.5, trans.Z(8.9 + gaugeoffset + trackoffset, 20.24));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 20.24), -4.5, trans.Z(-4.9 - gaugeoffset, 20.24));
                        //Segment 21
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 21.252), -5.6, trans.Z(-4.9 - gaugeoffset, 21.252));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 21.252), -5.6, trans.Z(8.9 + gaugeoffset + trackoffset, 21.252));
                        //Segment 22
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 22.264), -7.0, trans.Z(8.9 + gaugeoffset + trackoffset, 22.264));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 22.264), -7.0, trans.Z(-4.9 - gaugeoffset, 22.264));
                        //Segment 23
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -10.5, trans.Z(-4.9 - gaugeoffset, 23.276));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -10.5, trans.Z(8.9 + gaugeoffset + trackoffset, 23.276));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -22, trans.Z(8.9 + gaugeoffset + trackoffset, 23.276));
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
                        if (additionalpierheight != 0)
                        {
                            //Bottom Of Piers
                            sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Pier Bottom");
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -22, trans.Z(-4.9 - gaugeoffset, 2.024));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -22, trans.Z(8.9 + gaugeoffset + trackoffset, 2.024));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -22 - additionalpierheight, trans.Z(8.9 + gaugeoffset + trackoffset, 2.024));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -22 - additionalpierheight, trans.Z(-4.9 - gaugeoffset, 2.024));

                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -22, trans.Z(-4.9 - gaugeoffset, 23.276));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -22, trans.Z(8.9 + gaugeoffset + trackoffset, 23.276));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(8.9 + gaugeoffset + trackoffset, 23.276), -22 - additionalpierheight, trans.Z(8.9 + gaugeoffset + trackoffset, 23.276));
                            sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-4.9 - gaugeoffset, 23.276), -22 - additionalpierheight, trans.Z(-4.9 - gaugeoffset, 23.276));
                            sw.WriteLine("AddFace,3,2,1,0,");
                            sw.WriteLine("AddFace,4,5,6,7,");
                            sw.WriteLine("LoadTexture,{0}.{1},", "viaduct5", texture_format);
                            sw.WriteLine("SetTextureCoordinates,0,0,{0:f4}", 0.073 * additionalpierheight);
                            sw.WriteLine("SetTextureCoordinates,1,1,{0:f4}", 0.073 * additionalpierheight);
                            sw.WriteLine("SetTextureCoordinates,2,1,0");
                            sw.WriteLine("SetTextureCoordinates,3,0,0");
                            sw.WriteLine("SetTextureCoordinates,4,0,{0:f4}", 0.073 * additionalpierheight);
                            sw.WriteLine("SetTextureCoordinates,5,1,{0:f4}", 0.073 * additionalpierheight);
                            sw.WriteLine("SetTextureCoordinates,6,1,0");
                            sw.WriteLine("SetTextureCoordinates,7,0,0");
                        }


                    }
                }
            }
        }

        internal static void BuildSteelViaduct()
        {
            var LiRe = 1;
            var LiRe_T = 1;
            double gaugeoffset = Weichengenerator.gaugeoffset;
            string texture_format = Weichengenerator.texture_format;
            double radius = Weichengenerator.radius;
            string launchpath = Weichengenerator.path;
            string steel_texture = Weichengenerator.steel_texture;
            string steel_file = Weichengenerator.steel_file;
            string viaductfence_texture = Weichengenerator.viaductfence_texture;
            string viaductfence_file = Weichengenerator.viaductfence_file;
            string black_texture = Weichengenerator.black_texture;
            string black_file = Weichengenerator.black_file;
            
            int numberoftracks = Weichengenerator.numberoftracks;
            double trackoffset;
            string name;
            double segmente;
            MathFunctions.Transform trans;

            //Create Output directory
            if (!System.IO.Directory.Exists(launchpath + "\\Output\\Viaducts"))
            {
                System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Viaducts");
            }

            //Main textures
            const string outputtype = "Viaducts";
            Weichengenerator.ConvertAndMove(launchpath, steel_texture, texture_format, steel_file, outputtype);
            Weichengenerator.ConvertAndMove(launchpath, viaductfence_texture, texture_format, viaductfence_file, outputtype);
            Weichengenerator.ConvertAndMove(launchpath, black_texture, texture_format, black_file, outputtype);

            //Left or right definition
                if (radius < 0)
                {
                    LiRe = -1;
                    radius = radius * -1;
                }

                if (radius == 0)
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_Straight.csv";
                    segmente = 1;
                }
                else if (LiRe == -1)
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_L" + radius + ".csv";
                    segmente = 25;
                }
                else
                {
                    name = launchpath + "\\Output\\Viaducts\\Viaduct_R" + radius + ".csv";
                    segmente = 25;
                }
                if (numberoftracks == 1)
                {
                    trackoffset = 0 - (4.0 + (gaugeoffset * 2));
                }
                else
                {
                    trackoffset = (4.0 + (gaugeoffset * 2)) * (numberoftracks - 2);
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
                    //Main Viaduct Body
                    sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Main Body");
                    int a = -1;
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        //Inner
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(0, (25.3 / segmente) * i), -0.33, trans.Z(0, (25.3 / segmente) * i));
                        //Outer
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, (25.3 / segmente) * i), -0.33, trans.Z(-3 - gaugeoffset, (25.3 / segmente) * i));
                        //Drop
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, (25.3 / segmente) * i), -1, trans.Z(-3 - gaugeoffset, (25.3 / segmente) * i));
                        //Swing In
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-2.0 - gaugeoffset, (25.3 / segmente) * i), -1, trans.Z(-2.0 - gaugeoffset, (25.3 / segmente) * i));
                        //Bottom of Second Drop L
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(-2.0 - gaugeoffset, (25.3 / segmente) * i), -2.0, trans.Z(-2.0 - gaugeoffset, (25.3 / segmente) * i));
                        //Bottom of Second Drop R
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.0 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -2.0, trans.Z(6.0 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        //Swing in R
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.0 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -1, trans.Z(6.0 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        //Drop R
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},",trans.X(7 + gaugeoffset + trackoffset, (25.3/segmente)*i), -1,trans.Z(7 + gaugeoffset + trackoffset, (25.3/segmente)*i));
                        //Outer R
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -0.33, trans.Z(7 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        //Innter R
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(0, (25.3/segmente)*i), -0.33,trans.Z(0, (25.3/segmente)*i));
                    }
                    //End 1 Face
                    sw.WriteLine("AddFace,3,2,1,0,");
                    sw.WriteLine("AddFace,6,5,4,3,0,");
                    sw.WriteLine("AddFace,8,7,6,0,");
                    //End 2 Face
                    sw.WriteLine("AddFace,256,257,258,259,");
                    sw.WriteLine("AddFace,253,254,255,256,259,");
                    sw.WriteLine("AddFace,251,252,253,259,");

                    Constructors.AddSteelViaductFace(sw, a, -LiRe_T);
                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", "viaduct7", texture_format);
                    if (LiRe == -1 || radius == 0)
                    {
                        Constructors.SetTexture(sw, a, 5, 10);
                    }
                    else
                    {
                        Constructors.SetTexture(sw, a, 5, 10);
                    }
                    //Fences
                    sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Fencing L");
                    a = -1;
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.9 - gaugeoffset, (25.3 / segmente) * i), -0.33, trans.Z(-2.9 - gaugeoffset, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.9 - gaugeoffset, (25.3 / segmente) * i), 1.5, trans.Z(-2.9 - gaugeoffset, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace2_New(sw, a, -LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", "viaduct8", texture_format);
                    Constructors.SetTexture(sw, a, 5, 4);
                    sw.WriteLine("SetDecalTransparentColor,0,0,0");
                    sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Fencing R");
                    a = -1;
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), -0.33, trans.Z(6.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i), 1.5, trans.Z(6.9 + gaugeoffset + trackoffset, (25.3 / segmente) * i));
                    }

                    Constructors.AddFace2_New(sw, a, -LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", "viaduct8", texture_format);
                    Constructors.SetTexture(sw, a, 5, 4);

                    sw.WriteLine("SetDecalTransparentColor,0,0,0");
                    //Side Supports
                    //5,10,15,20
                    sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Side Supports L");
                    for (var i = 0; i < 5; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, 5.26), -1, trans.Z(-3 - gaugeoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 5.26), -1, trans.Z(-2.0 - gaugeoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 5.26), -2.0, trans.Z(-2.0 - gaugeoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.5 - gaugeoffset, 5.26), -2.0, trans.Z(-2.5 - gaugeoffset, 5.26));
                            break;
                            case 1:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, 10.3), -1, trans.Z(-3 - gaugeoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 10.3), -1, trans.Z(-2.0 - gaugeoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 10.3), -2.0, trans.Z(-2.0 - gaugeoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.5 - gaugeoffset, 10.3), -2.0, trans.Z(-2.5 - gaugeoffset, 10.3));
                            break;
                            case 2:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, 15.4), -1, trans.Z(-3 - gaugeoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 15.4), -1, trans.Z(-2.0 - gaugeoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 15.4), -2.0, trans.Z(-2.0 - gaugeoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.5 - gaugeoffset, 15.4), -2.0, trans.Z(-2.5 - gaugeoffset, 15.4));
                            break;
                            case 3:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, 20.5), -1, trans.Z(-3 - gaugeoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 20.5), -1, trans.Z(-2.0 - gaugeoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 20.5), -2.0, trans.Z(-2.0 - gaugeoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.5 - gaugeoffset, 20.5), -2.0, trans.Z(-2.5 - gaugeoffset, 20.5));
                            break;
                            case 4:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-3 - gaugeoffset, 25.3), -1, trans.Z(-3 - gaugeoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 25.3), -1, trans.Z(-2.0 - gaugeoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.0 - gaugeoffset, 25.3), -2.0, trans.Z(-2.0 - gaugeoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(-2.5 - gaugeoffset, 25.3), -2.0, trans.Z(-2.5 - gaugeoffset, 25.3));
                            break;
                        }
                        
                    }
                    sw.WriteLine("AddFace2,0,1,2,3");
                    sw.WriteLine("AddFace2,4,5,6,7");
                    sw.WriteLine("AddFace2,8,9,10,11");
                    sw.WriteLine("AddFace2,12,13,14,15");
                    sw.WriteLine("AddFace2,16,17,18,19");
                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", "black", texture_format);
                    Constructors.SetTexture(sw, 9, 5, 4);

                    sw.WriteLine("\r\nCreateMeshBuilder ;Viaduct Side Supports R");
                    for (var i = 0; i < 5; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, 5.26), -1, trans.Z(7 + gaugeoffset + trackoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 5.26), -1, trans.Z(6 + gaugeoffset + trackoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 5.26), -2.0, trans.Z(6 + gaugeoffset + trackoffset, 5.26));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.5 + gaugeoffset + trackoffset, 5.26), -2.0, trans.Z(6.5 + gaugeoffset + trackoffset, 5.26));
                                break;
                            case 1:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, 10.3), -1, trans.Z(7 + gaugeoffset + trackoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 10.3), -1, trans.Z(6 + gaugeoffset + trackoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 10.3), -2.0, trans.Z(6 + gaugeoffset + trackoffset, 10.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.5 + gaugeoffset + trackoffset, 10.3), -2.0, trans.Z(6.5 + gaugeoffset + trackoffset, 10.3));
                                break;
                            case 2:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, 15.4), -1, trans.Z(7 + gaugeoffset + trackoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 15.4), -1, trans.Z(6 + gaugeoffset + trackoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 15.4), -2.0, trans.Z(6 + gaugeoffset + trackoffset, 15.4));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.5 + gaugeoffset + trackoffset, 15.4), -2.0, trans.Z(6.5 + gaugeoffset + trackoffset, 15.4));
                                break;
                            case 3:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, 20.5), -1, trans.Z(7 + gaugeoffset + trackoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 20.5), -1, trans.Z(6 + gaugeoffset + trackoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 20.5), -2.0, trans.Z(6 + gaugeoffset + trackoffset, 20.5));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.5 + gaugeoffset + trackoffset, 20.5), -2.0, trans.Z(6.5 + gaugeoffset + trackoffset, 20.5));
                                break;
                            case 4:
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(7 + gaugeoffset + trackoffset, 25.3), -1, trans.Z(7 + gaugeoffset + trackoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 25.3), -1, trans.Z(6 + gaugeoffset + trackoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6 + gaugeoffset + trackoffset, 25.3), -2.0, trans.Z(6 + gaugeoffset + trackoffset, 25.3));
                                sw.WriteLine("AddVertex,{0:f4},{1:f4},{2:f4},", trans.X(6.5 + gaugeoffset + trackoffset, 25.3), -2.0, trans.Z(6.5 + gaugeoffset + trackoffset, 25.3));
                                break;
                        }

                    }
                    sw.WriteLine("AddFace2,0,1,2,3");
                    sw.WriteLine("AddFace2,4,5,6,7");
                    sw.WriteLine("AddFace2,8,9,10,11");
                    sw.WriteLine("AddFace2,12,13,14,15");
                    sw.WriteLine("AddFace2,16,17,18,19");
                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", "black", texture_format);
                    Constructors.SetTexture(sw, 9, 5, 4);


                }
            }
        }
    }
}
