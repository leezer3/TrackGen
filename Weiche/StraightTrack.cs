using System.IO;
using System.Windows.Forms;

namespace Weiche
{
    class StraightTrack
    {
        internal static void BuildStraight(string[] inputStrings, bool[] inputcheckboxes)
        {
            {
                double trackgauge;
                double gaugeoffset;
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
                bool EingabeOK;

                if (inputStrings[3].Length == 0)
                {
                    MessageBox.Show("Geben sie einen Pfad an!");
                    return;
                }


                name = inputStrings[3] + "\\Output\\Tracks\\straight.csv";

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

                //Create Output directory
                if (!System.IO.Directory.Exists(inputStrings[3] + "\\Output\\Tracks"))
                {
                    System.IO.Directory.CreateDirectory(inputStrings[3] + "\\Output\\Tracks");
                }


                //Write Out to CSV
                using (var sw = new StreamWriter(name))
                {

                    if (inputcheckboxes[4] == false)
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


                    if (inputcheckboxes[1] == false)
                    {
                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,1,0,3,2\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", -2.5 - gaugeoffset, -3.6 - gaugeoffset, embankment_file, texture_format);

                        sw.WriteLine("\r\nCreateMeshBuilder\r\nAddVertex,{0:f4},-0.35,25\r\nAddVertex,{1:f4},-0.3,25\r\nAddVertex,{1:f4},-0.3,0\r\nAddVertex,{0:f4},-0.35,0\r\nAddFace,0,1,2,3\r\nGenerateNormals\r\nLoadTexture,{2}.{3}\r\nSetTextureCoordinates,2,0,0\r\nSetTextureCoordinates,3,1,0\r\nSetTextureCoordinates,0,1,3\r\nSetTextureCoordinates,1,0,3", 2.5 + gaugeoffset, 3.6 + gaugeoffset, embankment_file, texture_format);


                    }
                }

            }
        }
    }
}
