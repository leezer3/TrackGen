/*
 * Switch Generation
 */

using System;
using System.Windows.Forms;
using System.IO;

namespace Weiche
{
    class Switch
    {
        internal static void BuildSwitch()
        {
            {

                
                var LiRe = 1;
                var LiRe_T = 1;
                string name;
                double radiusT;
                
                MathFunctions.Transform trans;
                bool EingabeOK;

                double radius = Weichengenerator.radius;
                double Abw_tot = Weichengenerator.Abw_tot;
                int laenge = Weichengenerator.laenge;
                int segmente = Weichengenerator.segmente;
                double trackgauge = Weichengenerator.trackgauge;
                double gaugeoffset = Weichengenerator.gaugeoffset;
                string launchpath = Weichengenerator.path;
                string texture_format = Weichengenerator.texture_format;
                string spez_texture = Weichengenerator.spez_texture;
                string spez_file = Weichengenerator.spez_file;
                string spezanf_texture = Weichengenerator.spezanf_texture;
                string spezanf_file = Weichengenerator.spezanf_file;
                string motor_texture = Weichengenerator.motor_texture;
                string motor_file = Weichengenerator.motor_file;
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
                int z = Weichengenerator.zmovement;
                
                //Create Output directory
                if (!System.IO.Directory.Exists(launchpath + "\\Output\\Tracks"))
                {
                    System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Tracks");
                }



                

                if (z != 0)
                {
                    name = launchpath + "\\Output\\Tracks\\W" + radius + "m_" + Abw_tot + "m_" + 25 * laenge + "m_" + z + "z.csv";
                }
                else
                {
                    name = launchpath + "\\Output\\Tracks\\W" + radius + "m_" + Abw_tot + "m_" + 25 * laenge + "m.csv";
                }


                //Main Textures
                const string outputtype = "Tracks";
                Weichengenerator.ConvertAndMove(launchpath, ballast_texture, texture_format, ballast_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, sleeper_texture, texture_format, sleeper_file, outputtype);

                //Spez Textures
                Weichengenerator.ConvertAndMove(launchpath, spez_texture, texture_format, spez_file, outputtype);
                Weichengenerator.ConvertAndMove(launchpath, spezanf_texture, texture_format, spezanf_file, outputtype);
                if (Weichengenerator.pointmotor == true)
                {
                    Weichengenerator.ConvertAndMove(launchpath, motor_texture, texture_format, motor_file, outputtype);
                }
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

                trans = new MathFunctions.Transform(laenge, radius, LiRe, z);


                //Calculate Frog

                radiusT = MathFunctions.radius_tot(laenge, Abw_tot);

                // Co-ordinates 1- Top of switch
                /*
                double[] K1 = new double[2];
                K1[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                K1[0] = (0.72 + gaugeoffset) * LiRe_T;

                 * Did we need to move these???
                 * Try below.
                 */
                var K1 = new double[2];
                K1[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                K1[0] = (0.72 + gaugeoffset) * LiRe_T;

                // Co-ordinates 2- Left End
                var K2 = new double[2];
                K2[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset) - 0.15) * (radiusT - (0.72 + gaugeoffset) - 0.15));
                K2[0] = (radiusT - Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - K2[1] * K2[1])) * LiRe_T;

                //Co-ordinate 3- Right End
                var K3 = new double[2];
                K3[1] = K2[1] + Math.Tan(MathFunctions.winkel_tot(laenge, Abw_tot)) * (K2[0] * LiRe_T - (0.72 + gaugeoffset));
                K3[0] = (0.72 + gaugeoffset) * LiRe_T;

                //Co-ordinates 4- Toe of point
                var K4 = new double[2];
                K4[1] = K3[1];
                K4[0] = (K2[0] - K3[0]) / 2 + K3[0];

                //Write out to CSV

                using (var sw = new StreamWriter(name))
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

                    if (Weichengenerator.norailtexture == false)
                    {

                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
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
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
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

                    var a = 0;
                    for (; j <= 25 * laenge; j = j + 25 / segmente, a++)
                    {

                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

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



                    //Gerade Spielerspur Vorne
                    //It's the frog that's the problem....
                    var K5 = new double[2];
                    K5[0] = (0.72 + gaugeoffset) * LiRe_T;
                    K5[1] = Math.Sqrt((radiusT + (0.67 + gaugeoffset)) * (radiusT + (0.67 + gaugeoffset)) - (radiusT - (0.72 + gaugeoffset)) * (radiusT - (0.72 + gaugeoffset)));
                    var K6 = new double[2];
                    K6[0] = (0.78 + gaugeoffset) * LiRe_T;
                    K6[1] = Math.Sqrt((radiusT + (0.61 + gaugeoffset)) * (radiusT + (0.61 + gaugeoffset)) - (radiusT - (0.78 + gaugeoffset)) * (radiusT - (0.78 + gaugeoffset)));

                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Gerade Spielerspur Vorne");
                    a = 0;
                    for (var i = 0; (25 / segmente) * i <= K6[1]; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.72 + gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((0.72 + gaugeoffset) * LiRe_T, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.78 + gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((0.78 + gaugeoffset) * LiRe_T, (25 / segmente) * i));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K5[0], K5[1]), trans.Z(K5[0], K5[1]));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K6[0], K6[1]), trans.Z(K6[0], K6[1]));

                    Constructors.AddFace(sw, a, LiRe_T);

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



                    //Totspur vorne

                    var K7 = new double[2];
                    K7[0] = (0.61 + gaugeoffset) * LiRe_T;
                    K7[1] = Math.Sqrt((radiusT + (0.78 + gaugeoffset)) * (radiusT + (0.78 + gaugeoffset)) - (radiusT - (0.61 + gaugeoffset)) * (radiusT - (0.61 + gaugeoffset)));
                    var K8 = new double[2];
                    K8[0] = (0.67 + gaugeoffset) * LiRe_T;
                    K8[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT - (0.67 + gaugeoffset)) * (radiusT - (0.67 + gaugeoffset)));
                    var K11 = new double[2];
                    K11[1] = Math.Sqrt((radiusT + (0.78 + gaugeoffset)) * (radiusT + (0.78 + gaugeoffset)) - (radiusT + 0) * (radiusT + 0));
                    K11[0] = 0;
                    var K12 = new double[2];
                    K12[0] = MathFunctions.Abbiege_x(K11[1], radiusT, (-0.72 - gaugeoffset), LiRe_T);
                    K12[1] = MathFunctions.Abbiege_z(Math.Sqrt((radiusT + (0.78 + gaugeoffset)) * (radiusT + (0.78 + gaugeoffset)) - (radiusT + 0) * (radiusT + 0)), radiusT, (-0.72 - gaugeoffset), LiRe_T);


                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Totspur vorne");

                    a = 1;
                    for (double i = 0; i <= K11[1]; i = i + 25 / segmente, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-0.72 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(4200, i, K11[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.72 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-0.72 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(4200, i, K11[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.72 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-0.78 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3000, i, K11[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.78 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-0.78 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3000, i, K11[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.78 - gaugeoffset), LiRe_T)));
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
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.72 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.72 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.78 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.78 - gaugeoffset), LiRe_T)));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K8[0], K8[1]), trans.Z(K8[0], K8[1]));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K7[0], K7[1]), trans.Z(K7[0], K7[1]));

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

                    //Flügelschiene Kurve Spielerspur

                    var K18 = new double[2];
                    K18[0] = (0.78 + gaugeoffset) * LiRe_T;
                    K18[1] = Math.Sqrt((radiusT + (0.67 + gaugeoffset)) * (radiusT + (0.67 + gaugeoffset)) - (radiusT - (0.78 + gaugeoffset)) * (radiusT - (0.78 + gaugeoffset)));

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
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
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
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.62 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.62 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.62 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.62 - gaugeoffset), LiRe_T)));
                    a = 1;
                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
                        sw.WriteLine("SetColor,169,140,114,");
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }


                    //Flügelschiene Gerade Totspur

                    var K17 = new double[2];
                    K17[0] = (0.67 + gaugeoffset) * LiRe_T;
                    K17[1] = Math.Sqrt((radiusT + (0.78 + gaugeoffset)) * (radiusT + (0.78 + gaugeoffset)) - (radiusT - (0.67 + gaugeoffset)) * (radiusT - (0.67 + gaugeoffset)));

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
                    if (Weichengenerator.norailtexture == false)
                    {

                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
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
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.62 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)), trans.Z((0.62 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.67 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)), trans.Z((0.67 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.67 - gaugeoffset), LiRe_T)));
                    a = 1;
                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
                        sw.WriteLine("SetColor,169,140,114,");
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }



                    //Gerade Schiene Außen

                    a = -1;
                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Gerade Schiene Aussen");
                    for (var i = 0; i <= segmente * laenge; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.72 - gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((-0.72 - gaugeoffset) * LiRe_T, (25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.78 - gaugeoffset) * LiRe_T, (25 / segmente) * i), trans.Z((-0.78 - gaugeoffset) * LiRe_T, (25 / segmente) * i));
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

                    //Radlenker Spielerspur


                    //Do we need to move the 1.65 numbers here???


                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Radlenker Spielerspur");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] - 1.65), trans.Z((-0.66 - gaugeoffset) * LiRe_T, K1[1] - 1.65));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.62 - gaugeoffset) * LiRe_T, K1[1] - 1.65), trans.Z((-0.62 - gaugeoffset) * LiRe_T, K1[1] - 1.65));
                    a = 1;
                    for (var i = -1.5; i <= 1.5; i = i + 1, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.68 - gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.68 - gaugeoffset) * LiRe_T, K1[1] + i));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] + i));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] + 1.65), trans.Z((-0.66 - gaugeoffset) * LiRe_T, K1[1] + 1.65));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.62 - gaugeoffset) * LiRe_T, K1[1] + 1.65), trans.Z((-0.62 - gaugeoffset) * LiRe_T, K1[1] + 1.65));

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
                        sw.WriteLine("SetColor,169,140,114,");
                    }
                    else
                    {
                        sw.WriteLine("SetColor,180,190,200,");
                    }


                    //Radlenker Totspur

                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Radlenker Totspur");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                        trans.X(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)),
                        trans.Z(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                        trans.X(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)),
                        trans.Z(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)));
                    a = 1;
                    for (var i = -1.5; i <= 1.5; i = i + 1, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.68 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.68 + gaugeoffset), LiRe_T)),
                            trans.Z(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.68 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.68 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                            trans.X(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.64 + gaugeoffset), LiRe_T)),
                            trans.Z(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                        trans.X(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)),
                        trans.Z(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},",
                        trans.X(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)),
                        trans.Z(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.62 + gaugeoffset), LiRe_T)));

                    Constructors.AddFace(sw, a, -LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railTop.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 1);
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
                        sw.WriteLine("AddVertex,{0:f4},0.001,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (0.72 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.72 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (0.72 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.72 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},0.001,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (0.78 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.78 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (0.78 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.78 + gaugeoffset), LiRe_T)));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

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



                    ////////////////////////////////////////
                    ///////////SchienenMittelteil //////////
                    ////////////////////////////////////////






                    //Railside Gerade Schiene Außen

                    a = -1;
                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    for (var i = 0; i <= segmente; i++, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.74 - gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i), trans.Z((-0.74 - gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.74 - gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i), trans.Z((-0.74 - gaugeoffset) * LiRe_T, (laenge * 25 / segmente) * i));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
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
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (0.74 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.74 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (0.74 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.74 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (0.74 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.74 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (0.74 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (0.74 + gaugeoffset), LiRe_T)));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    // Railside Hinter Herzstück - Spielerspur

                    var K13 = new double[2];
                    K13[0] = (0.74 + gaugeoffset) * LiRe_T;
                    K13[1] = Math.Sqrt((radiusT + (0.74 + gaugeoffset)) * (radiusT + (0.74 + gaugeoffset)) - (radiusT - (0.74 + gaugeoffset)) * (radiusT - (0.74 + gaugeoffset)));

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
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74 + gaugeoffset) * LiRe_T, j), trans.Z((0.74 + gaugeoffset) * LiRe_T, j));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74 + gaugeoffset) * LiRe_T, j), trans.Z((0.74 + gaugeoffset) * LiRe_T, j));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
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
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
                    }

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    // Railside Vor Herzstück - Spielerspur

                    var K15 = new double[2];
                    K15[0] = (0.74 + gaugeoffset) * LiRe_T;
                    K15[1] = Math.Sqrt((radiusT + (0.65 + gaugeoffset)) * (radiusT + (0.65 + gaugeoffset)) - (radiusT - (0.74 + gaugeoffset)) * (radiusT - (0.74 + gaugeoffset)));

                    a = 0;
                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    for (j = 0; j <= K15[1]; j = j + 25 / segmente, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.74 + gaugeoffset) * LiRe_T, j), trans.Z((0.74 + gaugeoffset) * LiRe_T, j));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.74 + gaugeoffset) * LiRe_T, j), trans.Z((0.74 + gaugeoffset) * LiRe_T, j));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Flügelrailside Totspur

                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K15[0], K15[1]), trans.Z(K15[0], K15[1]));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)));
                    a = 1;
                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }


                    //Flügelrailside Gerade Spielerspur

                    var K14 = new double[2];
                    K14[0] = (0.65 + gaugeoffset) * LiRe_T;
                    K14[1] = Math.Sqrt((radiusT + (0.74 + gaugeoffset)) * (radiusT + (0.74 + gaugeoffset)) - (radiusT - (0.65 + gaugeoffset)) * (radiusT - (0.65 + gaugeoffset)));

                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((0.65 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 + gaugeoffset), LiRe_T)), trans.Z((0.65 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((0.65 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)), trans.Z((0.65 + gaugeoffset) * LiRe_T, MathFunctions.Abbiege_z(K3[1], radiusT, (-0.65 - gaugeoffset), LiRe_T)));
                    a = 1;
                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //TotspurRailside vorne

                    var K16 = new double[2];
                    K16[1] = Math.Sqrt((radiusT + (0.74 + gaugeoffset)) * (radiusT + (0.74 + gaugeoffset)) - (radiusT) * (radiusT));
                    K16[0] = 0;


                    sw.WriteLine("\r\r\nCreateMeshBuilder");

                    a = 1;
                    for (double i = 0; i <= K16[1]; i = i + 25 / segmente, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-0.74 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-0.74 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-0.74 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-0.74 - gaugeoffset), LiRe_T) + MathFunctions.x_Weichenoefnung(3600, i, K16[1]) * LiRe_T, MathFunctions.Abbiege_z(i, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
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
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(j, radiusT, (-0.74 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(j, radiusT, (-0.74 - gaugeoffset), LiRe_T)));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(K14[0], K14[1]), trans.Z(K14[0], K14[1]));

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Radlenker Railside Spielerspur

                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] - (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] - 1.65));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] - (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] - 1.65));
                    a = 1;
                    for (var i = -1.5; i <= 1.5; i = i + 1, a++)
                    {
                        sw.WriteLine("AddVertex,{0},0,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.66 - gaugeoffset) * LiRe_T, K1[1] + i));
                        sw.WriteLine("AddVertex,{0},-0.15,{1:f4},", trans.X((-0.66 - gaugeoffset) * LiRe_T, K1[1] + i), trans.Z((-0.66 - gaugeoffset) * LiRe_T, K1[1] + i));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] + (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] + 1.65));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-0.64 - gaugeoffset) * LiRe_T, K1[1] + (1.65 + gaugeoffset)), trans.Z((-0.64 - gaugeoffset) * LiRe_T, K1[1] + 1.65));

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);
                    }
                    else
                    {
                        sw.WriteLine("SetColor,85,50,50,");
                    }

                    //Radlenker Railside Totspur

                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] - 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                    a = 1;
                    for (var i = -1.5; i <= 1.5; i = i + 1, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + i, radiusT, (0.66 + gaugeoffset), LiRe_T)));
                    }
                    sw.WriteLine("AddVertex,{0:f4},0,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));
                    sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(K1[1] + 1.65, radiusT, (0.64 + gaugeoffset), LiRe_T)));

                    Constructors.AddFace2(sw, a);

                    sw.WriteLine("GenerateNormals,");
                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("LoadTexture,railSide.{0},", texture_format);
                        Constructors.SetTexture(sw, a, 1, 3);

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
                        sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((-2.8 - gaugeoffset) * LiRe_T, i), trans.Z((-2.8 - gaugeoffset) * LiRe_T, i));
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 - gaugeoffset) * LiRe_T, i), trans.Z((-1.3 - gaugeoffset) * LiRe_T, i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");

                    sw.WriteLine("LoadTexture,ballast.{0},", texture_format);
                    Constructors.SetTexture(sw, a, 10 * laenge, 2);

                    // Ballast Left

                    a = -1;
                    sw.WriteLine("\r\r\nCreateMeshBuilder");
                    for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)));
                        sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (2.8 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (2.8 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (2.8 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (2.8 + gaugeoffset), LiRe_T)));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,ballast.{0},", texture_format);
                    Constructors.SetTexture(sw, a, 10 * laenge, 1);

                    if (Weichengenerator.noembankment == false)
                    {

                        // Embankment Right

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X((-3.6 - gaugeoffset) * LiRe_T, i), trans.Z((-3.6 - gaugeoffset) * LiRe_T, i));
                            sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X((-2.5 - gaugeoffset) * LiRe_T, i), trans.Z((-2.5 - gaugeoffset) * LiRe_T, i));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", embankment_file, texture_format);
                        Constructors.SetTexture(sw, a, 3 * laenge, 1);


                        // Embankment Left

                        a = -1;
                        sw.WriteLine("\r\r\nCreateMeshBuilder");
                        for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                        {
                            sw.WriteLine("AddVertex,{0:f4},-0.35,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (2.5 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (2.5 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (2.5 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (2.5 + gaugeoffset), LiRe_T)));
                            sw.WriteLine("AddVertex,{0:f4},-0.3,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (3.6 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (3.6 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (3.6 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (3.6 + gaugeoffset), LiRe_T)));
                        }

                        Constructors.AddFace(sw, a, LiRe_T);

                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", embankment_file, texture_format);
                        Constructors.SetTexture(sw, a, 3 * laenge, 2);
                    }

                    //Sleepers for left branch
                    a = -1;
                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Sleepers L Branch");



                    //This only works properly on right branching switches

                    for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++)
                    {

                        if (LiRe_T == 1)
                        {

                            //New
                            var newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            var newsleepers1 = trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 - gaugeoffset) * LiRe_T, i), trans.Z((-1.3 - gaugeoffset) * LiRe_T, i));
                            if (newsleepers - newsleepers1 <= 0.1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                        }
                        else
                        {
                            //New
                            var newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            var newsleepers1 = trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);
                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((-1.3 - gaugeoffset) * LiRe_T, i), trans.Z((-1.3 - gaugeoffset) * LiRe_T, i));
                            if (newsleepers - newsleepers1 >= -0.1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                        }

                    }


                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", sleeper_file, texture_format);


                    double c;
                    var b = 0;

                    //Do we want to invert the textures??

                    if (Weichengenerator.inverttextures == true)
                    {
                        for (double i = a; i >= 0; i--)
                        {
                            //c = (Weichengenerator.Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3+ gaugeoffset)) / (2.6 + (gaugeoffset* 2));
                            sw.WriteLine("SetTextureCoordinates,{0:f4},0,{1:f4},", b, 15 - (i * 15 * laenge / a));
                            sw.WriteLine("SetTextureCoordinates,{0:f4},1,{1:f4},", b + 1, 15 - (i * 15 * laenge / a));
                            b = b + 2;
                        }
                    }
                    else
                    {

                        for (double i = a; i >= 0; i--)
                        {

                            c = (MathFunctions.Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3 + gaugeoffset)) / (2.6 + (gaugeoffset * 2));

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
                        if (LiRe_T == 1)
                        {
                            var newsleepers1 = (trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            var newsleepers2 = (trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            if (newsleepers2 <= newsleepers1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            }


                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)));
                        }
                        else
                        {
                            var newsleepers1 = (trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            var newsleepers2 = (trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            if (newsleepers2 >= newsleepers1)
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i), trans.Z((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            }
                            else
                            {
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                            }


                            sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (1.3 + gaugeoffset), LiRe_T)));
                        }
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,{0}.{1},", sleeper_file, texture_format);

                    c = 0;
                    b = 0;

                    //Do we want to invert the textures??
                    if (Weichengenerator.inverttextures == true)
                    {

                        for (double i = a; i >= 0; i--)
                        {
                            c = 1 - (MathFunctions.Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3 + gaugeoffset)) / (2.6 + (gaugeoffset * 2));
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
                            c = 1 - (MathFunctions.Abbiege_x(25 * laenge * (a - i) / a, radiusT, 0, LiRe_T * LiRe_T) / 2 + (1.3 + gaugeoffset)) / (2.6 + (gaugeoffset * 2));
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

                    //Increment this one if we add any verticies
                    //Needed to eliminate writing out stuff uncessarily
                    var neededtest = 0;
                    a = -1;
                    var totalsegs = -1;


                    for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++, totalsegs++)
                    {
                        if (LiRe_T == 1)
                        {
                            //New
                            var newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            var newsleepers1 = trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);


                            if (newsleepers - newsleepers1 <= 0.1)
                            {
                                if (neededtest == 0)
                                {
                                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder R Branch L");
                                }
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                                sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((2.8 + gaugeoffset) * LiRe_T, i), trans.Z((2.8 + gaugeoffset) * LiRe_T, i));
                                neededtest++;
                            }
                            else
                            {
                                //Don't do anything, subtract one from a
                                a--;
                            }
                        }
                        else
                        {
                            //New
                            var newsleepers = trans.X((1.3 + gaugeoffset) * LiRe_T, i);
                            //Original
                            var newsleepers1 = trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i);


                            if (newsleepers - newsleepers1 >= -0.1)
                            {
                                if (neededtest == 0)
                                {
                                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder R Branch L");
                                }
                                sw.WriteLine("AddVertex,{0:f4},-0.151,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                                sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X((2.8 + gaugeoffset) * LiRe_T, i), trans.Z((2.8 + gaugeoffset) * LiRe_T, i));
                                neededtest++;
                            }
                            else
                            {
                                //Don't do anything, subtract one from a
                                a--;
                            }
                        }

                    }


                    Constructors.AddFace(sw, a, LiRe_T);
                    double texturefactor1 = a;
                    double texturefactor2 = totalsegs;
                    var texturefactor = ((texturefactor1 / texturefactor2) * 10);
                    if (neededtest != 0)
                    {
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                        Constructors.SetTexture(sw, a, texturefactor * laenge, 5);
                    }
                    //Right Branch Ballast Shoulder L
                    neededtest = 0;
                    a = -1;
                    totalsegs = -1;


                    for (double i = 0; i <= 25 * laenge; i = i + 25 / segmente, a++, totalsegs++)
                    {
                        if (LiRe_T == 1)
                        {
                            var newsleepers1 = (trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            var newsleepers2 = (trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));


                            if (newsleepers2 <= newsleepers1)
                            {
                                a--;
                            }
                            else
                            {
                                if (neededtest == 0)
                                {
                                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder L Branch R");
                                }
                                sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)));
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                                neededtest++;
                            }
                        }
                        else
                        {
                            var newsleepers1 = (trans.X((MathFunctions.Abbiege_x(i, radiusT, 0, LiRe_T)) / 2, i));
                            var newsleepers2 = (trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));


                            if (newsleepers2 >= newsleepers1)
                            {
                                a--;
                            }
                            else
                            {
                                if (neededtest == 0)
                                {
                                    sw.WriteLine("\r\r\nCreateMeshBuilder ;Shoulder L Branch R");
                                }
                                sw.WriteLine("AddVertex,{0:f4},-0.4,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-2.8 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-2.8 - gaugeoffset), LiRe_T)));
                                sw.WriteLine("AddVertex,{0:f4},-0.15,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), MathFunctions.Abbiege_z(i, radiusT, (-1.3 - gaugeoffset), LiRe_T)));
                                neededtest++;
                            }
                        }

                    }


                    Constructors.AddFace(sw, a, LiRe_T);
                    texturefactor1 = a;
                    texturefactor2 = totalsegs;
                    texturefactor = ((texturefactor1 / texturefactor2) * 10);
                    if (neededtest != 0)
                    {
                        sw.WriteLine("GenerateNormals,");
                        sw.WriteLine("LoadTexture,{0}.{1},", ballast_file, texture_format);
                        Constructors.SetTexture(sw, a, texturefactor * laenge, 2);
                    }


                    //Right Branch Additional Sleeper Bit
                    a = -1;
                    sw.WriteLine("\r\r\nCreateMeshBuilder");

                    var K20 = new double[2];
                    K20[1] = Math.Sqrt((radiusT + (0.72 + gaugeoffset)) * (radiusT + (0.72 + gaugeoffset)) - (radiusT + (0.55 + gaugeoffset)) * (radiusT + (0.55 + gaugeoffset)));

                    j = 0;
                    while (j < K20[1])
                    {
                        j = j + segmente * laenge / 15;
                    }

                    for (double i = 0; i <= j; i = i + 1.25, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((-1.3 - gaugeoffset) * LiRe_T, i), trans.Z((-1.3 - gaugeoffset) * LiRe_T, i));
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((-0.325 - gaugeoffset) * LiRe_T, i), trans.Z((-0.325 - gaugeoffset) * LiRe_T, i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,SchieneSpezAnf.{0},", texture_format);

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

                    for (var i = j; i <= k; i = i + 1.25, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), i), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-1.3 - gaugeoffset), LiRe_T), i));
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (-0.325 - gaugeoffset), LiRe_T), i), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (-0.325 - gaugeoffset), LiRe_T), i));
                    }

                    Constructors.AddFace(sw, a, LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,SchieneSpez.{0},", texture_format);

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
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), i), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (1.3 + gaugeoffset), LiRe_T), i));
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X(MathFunctions.Abbiege_x(i, radiusT, (0.325 + gaugeoffset), LiRe_T), i), trans.Z(MathFunctions.Abbiege_x(i, radiusT, (0.325 + gaugeoffset), LiRe_T), i));
                    }

                    Constructors.AddFace(sw, a, -1 * LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,SchieneSpezAnf.{0},", texture_format);

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

                    for (var i = j; i <= k; i = i + 1.25, a++)
                    {
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((1.3 + gaugeoffset) * LiRe_T, i), trans.Z((1.3 + gaugeoffset) * LiRe_T, i));
                        sw.WriteLine("AddVertex,{0:f4},-0.14,{1:f4},", trans.X((0.325 + gaugeoffset) * LiRe_T, i), trans.Z((0.325 + gaugeoffset) * LiRe_T, i));
                    }

                    Constructors.AddFace(sw, a, -1 * LiRe_T);

                    sw.WriteLine("GenerateNormals,");
                    sw.WriteLine("LoadTexture,SchieneSpez.{0},", texture_format);

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
                    if (Weichengenerator.pointmotor == true)
                    {
                        sw.WriteLine("CreateMeshBuilder\r\r\nAddVertex,-1.8,-0.05,0.3,\r\nAddVertex,-1.2,-0.05,0.3,\r\r\nAddVertex,-1.2,-0.25,0.3,\r\r\nAddVertex,-1.8,-0.25,0.3,\r\r\nAddVertex,-1.8,0,0.35,\r\r\nAddVertex,-1.2,0,0.35,\r\r\nAddVertex,-1.8,0,0.55,\r\r\nAddVertex,-1.2,0,0.55,\r\r\nAddVertex,-1.8,-0.05,0.6,\r\r\nAddVertex,-1.2,-0.05,0.6,\r\r\nAddVertex,-1.2,-0.25,0.6,\r\r\nAddVertex,-1.8,-0.25,0.6,\r\r\nAddFace,0,1,2,3,\r\r\nAddFace,4,5,1,0,\r\r\nAddFace,6,7,5,4,\r\r\nAddFace,8,9,7,6,\r\r\nAddFace,11,10,9,8,\r\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0},\r\nSetTextureCoordinates,0,0,0.5,\r\nSetTextureCoordinates,1,1,0.5,\r\nSetTextureCoordinates,2,1,1,\r\nSetTextureCoordinates,3,0,1,\r\nSetTextureCoordinates,4,0,0.3,\r\nSetTextureCoordinates,5,1,0.3,\r\nSetTextureCoordinates,6,0,0,\r\nSetTextureCoordinates,7,1,0,\r\nSetTextureCoordinates,8,0,0.3,\r\nSetTextureCoordinates,9,1,0.3,\r\nSetTextureCoordinates,10,1,0.6,\r\nSetTextureCoordinates,11,0,0.6,", texture_format);

                        sw.WriteLine("CreateMeshBuilder\r\nAddVertex,-1.8,-0.25,0.3,\r\nAddVertex,-1.8,-0.05,0.3,\r\nAddVertex,-1.8,0,0.35,\r\nAddVertex,-1.8,-0.25,0.35\r\nAddVertex,-1.8,0,0.55,\r\nAddVertex,-1.8,-0.25,0.55,\r\nAddVertex,-1.8,-0.05,0.6,\r\nAddVertex,-1.8,-0.25,0.6,\r\nAddFace,2,1,0,3,\r\nAddFace,4,2,3,5,\r\nAddFace,6,4,5,7,\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0:f4},\r\nSetTextureCoordinates,0,1,1\r\nSetTextureCoordinates,1,1,0.5\r\nSetTextureCoordinates,2,0.9,0.5\r\nSetTextureCoordinates,3,0.9,1\r\nSetTextureCoordinates,4,0.7,0.5\r\nSetTextureCoordinates,5,0.7,1\r\nSetTextureCoordinates,6,0.6,0.5\r\nSetTextureCoordinates,7,0.6,1", texture_format);

                        sw.WriteLine("CreateMeshBuilder\r\nAddVertex,-1.2,-0.25,0.3,\r\nAddVertex,-1.2,-0.05,0.3,\r\nAddVertex,-1.2,0,0.35,\r\nAddVertex,-1.2,-0.25,0.35,\r\nAddVertex,-1.2,0,0.55,\r\nAddVertex,-1.2,-0.25,0.55,\r\nAddVertex,-1.2,-0.05,0.6,\r\nAddVertex,-1.2,-0.25,0.6,\r\nAddFace,1,2,3,0,\r\nAddFace,2,4,5,3,\r\nAddFace,4,6,7,5,\r\nGenerateNormals,\r\nLoadTexture,WeichAntrieb.{0:f4},\r\nSetTextureCoordinates,0,1,1\r\nSetTextureCoordinates,1,1,0.5\r\nSetTextureCoordinates,2,0.9,0.5\r\nSetTextureCoordinates,3,0.9,1\r\nSetTextureCoordinates,4,0.7,0.5\r\nSetTextureCoordinates,5,0.7,1\r\nSetTextureCoordinates,6,0.6,0.5\r\nSetTextureCoordinates,7,0.6,1", texture_format);

                        sw.WriteLine("CreateMeshBuilder\r\nCylinder,6,0.015,0.015,2\r\nRotate,0,0,1,90\r\nTranslate,-0.3,-0.14,0.4\r\nSetColor,151,151,151");
                        sw.WriteLine("CreateMeshBuilder\r\nCylinder,6,0.01,0.01,2\r\nRotate,0,0,1,90\r\nTranslate,-0.3,-0.12,0.5\r\nSetColor,151,151,151");

                    }

                }
            }
        }
    }
}
