/*
 * Platform Generation
 */

using System.Windows.Forms;
using System.IO;

namespace Weiche
{
    class Platforms
    {
        internal static void BuildPlatform()
        {
            {
                string name;
                double radius = Weichengenerator.radius;
                double segmente = Weichengenerator.segmente;
                double gaugeoffset = Weichengenerator.gaugeoffset;
                string launchpath = Weichengenerator.path;
                string texture_format = Weichengenerator.texture_format;
                double fenceheight = Weichengenerator.fenceheight;
                double platwidth_near = Weichengenerator.platwidth_near;
                double platwidth_far = Weichengenerator.platwidth_far;
                double platheight = Weichengenerator.platheight;
                string platform_texture = Weichengenerator.platform_texture;
                string platform_file = Weichengenerator.platform_file;
                string fence_texture = Weichengenerator.fence_texture;
                string fence_file = Weichengenerator.fence_file;
                bool hasfence = Weichengenerator.hasfence;
                double platwidth;
                PlatformType CurrentPlatformType = Weichengenerator.CurrentPlatformType;
                
                var LiRe = 1;
                var LiRe_T = 1;

                

                MathFunctions.Transform trans;


                //Left or right definition
                if (radius < 0)
                {
                    LiRe = -1;
                    radius = radius * -1;
                }

                //Create Output directory
                if (!System.IO.Directory.Exists(launchpath + "\\Output\\Platforms"))
                {
                    System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Platforms");
                }

                //Main Textures
                const string outputtype = "Platforms";
                Weichengenerator.ConvertAndMove(launchpath, platform_texture, texture_format, platform_file, outputtype);
                if (hasfence == true)
                {
                    Weichengenerator.ConvertAndMove(launchpath, fence_texture, texture_format, fence_file, outputtype);
                }





                switch(CurrentPlatformType)
                {
                    case PlatformType.RightLevel:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_R" + radius + ".csv";
                        }
                        break;
                    case PlatformType.RightRU:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RU_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RU_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RU_R" + radius + ".csv";
                        }
                        break;
                    case PlatformType.RightRD:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RD_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RD_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformRight_RD_R" + radius + ".csv";
                        }
                        break;
                    case PlatformType.LeftLevel:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_R" + radius + ".csv";
                        }
                        break;
                    case PlatformType.LeftRU:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RU_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RU_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RU_R" + radius + ".csv";
                        }
                        break;
                    case PlatformType.LeftRD:
                        if (radius == 0)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RD_Straight.csv";
                        }
                        else if (LiRe == -1)
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RD_L" + radius + ".csv";
                        }
                        else
                        {
                            name = launchpath + "\\Output\\Platforms\\PlatformLeft_RD_R" + radius + ".csv";
                        }
                        break;
                    default:
                        return;
                }

                //Calculate the track width to move the platforms as appropriate
                trans = new MathFunctions.Transform(1, radius, LiRe, 0);

                //Write Out to CSV
                using (var sw = new StreamWriter(name))
                {
                    
                        //Left Sided Platform
                        {
                            var a = -1;
                            //If radius is zero, use one segment
                            if (radius == 0)
                            {
                                if (CurrentPlatformType== PlatformType.LeftLevel)
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
                                else if (CurrentPlatformType == PlatformType.LeftRU)
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
                                else if(CurrentPlatformType == PlatformType.LeftRD)
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
                                if (CurrentPlatformType == PlatformType.LeftLevel)
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
                                else if (CurrentPlatformType == PlatformType.LeftRU)
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

                                else if (CurrentPlatformType == PlatformType.LeftRD)
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
                            if (hasfence == true)
                            {
                                var d = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (CurrentPlatformType == PlatformType.LeftLevel)
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
                                    else if (CurrentPlatformType == PlatformType.LeftRU)
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
                                    else if (CurrentPlatformType == PlatformType.LeftRD)
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
                                    if (CurrentPlatformType == PlatformType.LeftLevel)
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
                                    else if (CurrentPlatformType == PlatformType.LeftRU)
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
                                    else if (CurrentPlatformType == PlatformType.LeftRD)
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
                    





                    //Right Sided Platforms
                    
                    
                        {
                            var a = -1;
                            //If radius is zero, use one segment
                            if (radius == 0)
                            {
                                if (CurrentPlatformType == PlatformType.RightLevel)
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
                                else if (CurrentPlatformType == PlatformType.RightRU)
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
                                else if (CurrentPlatformType == PlatformType.RightRD)
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
                                if (CurrentPlatformType == PlatformType.RightLevel)
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
                                else if (CurrentPlatformType == PlatformType.RightRU)
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

                                else if (CurrentPlatformType == PlatformType.RightRD)
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
                            if (hasfence == true)
                            {
                                var d = -1;
                                //If radius is zero, use one segment
                                if (radius == 0)
                                {
                                    if (CurrentPlatformType == PlatformType.RightLevel)
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
                                    else if (CurrentPlatformType == PlatformType.RightRU)
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
                                    else if (CurrentPlatformType == PlatformType.RightRD)
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
                                    if (CurrentPlatformType == PlatformType.RightLevel)
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
                                    else if (CurrentPlatformType == PlatformType.RightRU)
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
                                    else if (CurrentPlatformType == PlatformType.RightRD)
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
