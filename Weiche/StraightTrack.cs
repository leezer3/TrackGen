/*
 * Straight Track Generation
 */

using System.IO;
using System.Windows.Forms;

namespace Weiche
{
    class StraightTrack
    {
        internal static void BuildStraight()
        {
            {
                string name;
                double gaugeoffset = Weichengenerator.gaugeoffset;
                string launchpath = Weichengenerator.launchpath;
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
                bool EingabeOK;

                

                name = launchpath + "\\Output\\Tracks\\straight.csv";

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
                
                //Create Output directory
                if (!System.IO.Directory.Exists(launchpath + "\\Output\\Tracks"))
                {
                    System.IO.Directory.CreateDirectory(launchpath + "\\Output\\Tracks");
                }


                //Write Out to CSV
                using (var sw = new StreamWriter(name))
                {

                    if (Weichengenerator.norailtexture == false)
                    {
                        sw.WriteLine("CreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,railTop.{2}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", -0.72 - gaugeoffset, -0.78 - gaugeoffset, texture_format);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railTop.{2}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", 0.72 + gaugeoffset, 0.78 + gaugeoffset, texture_format);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railside.{1}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", -0.74 - gaugeoffset, texture_format);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,railside.{1}\r\nSetTextureCoordinates,0,0,0\r\nSetTextureCoordinates,1,1,0\r\nSetTextureCoordinates,2,1,1\r\nSetTextureCoordinates,3,0,1", 0.74 + gaugeoffset, texture_format);
                    }
                    else
                    {
                        sw.WriteLine("CreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nSetColor,180,190,200", -0.72 - gaugeoffset, -0.78 - gaugeoffset);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{1:f4},0,25\r\nAddVertex,{1:f4},0,0\r\nAddVertex,{0:f4},0,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nSetColor,180,190,200", 0.72 + gaugeoffset, 0.78 + gaugeoffset);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nSetColor,85,50,50,", -0.74 - gaugeoffset);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},0,0\r\nAddVertex,{0:f4},0,25\r\nAddVertex,{0:f4},-0.15,25\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace2,0,1,2,3\r\nGenerateNormals\r\nSetColor,85,50,50", 0.74 + gaugeoffset);
                    }

                    sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4}, -0.15,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.15,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,0,0.01,0\r\nSetTextureCoordinates,1,0.99,0\r\nSetTextureCoordinates,2,0.99,15\r\nSetTextureCoordinates,3,0.01,15", -1.3 - gaugeoffset, 1.3 + gaugeoffset, sleeper_file, texture_format);

                    sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.4,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.4,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,10\r\nSetTextureCoordinates,1,0,10", -2.8 - gaugeoffset, -1.3 - gaugeoffset, ballast_file, texture_format);

                    sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.4,25\r\nAddVertex,{1:f4},-0.15,25\r\nAddVertex,{1:f4},-0.15,0\r\nAddVertex,{0:f4},-0.4,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,10\r\nSetTextureCoordinates,1,0,10", 2.8 + gaugeoffset, 1.3 + gaugeoffset, ballast_file, texture_format);


                    if (Weichengenerator.noembankment == false)
                    {
                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", -2.5 - gaugeoffset, -3.6 - gaugeoffset, embankment_file, texture_format);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", 2.5 + gaugeoffset, 3.6 + gaugeoffset, embankment_file, texture_format);


                    }
                }

            }
        }
    }
}
