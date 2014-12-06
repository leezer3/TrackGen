/*
 * Constructors- Face and Textures
 */

using System.ComponentModel;
using System.IO;

namespace Weiche
{
    class Constructors
    {
        public static void SetTexture(StreamWriter sw, int Faktor, double TexturMulti, int Ausrichtung)
        {
            var b = 0;
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
                case 6:
                    //Viaduct Top Wall
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b + 1, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b + 2, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 3, i * TexturMulti / Faktor);
                        b = b + 4;
                    }
                    break;
                case 7:
                    //Viaduct Footwalk
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 1, i * TexturMulti / Faktor);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.1,", b + 2, i * TexturMulti / Faktor);
                        b = b + 3;
                    }
                    break;
                case 8:
                    //Viaduct Underside
                    for (double i = Faktor; i >= 0; i--)
                    {
                        if (i / 2 > 11)
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, i*TexturMulti/Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 1, i*TexturMulti/Faktor);
                        }
                        else
                        {
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, i*TexturMulti/Faktor);
                            sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 1, i*TexturMulti/Faktor);
                        }
                        b = b + 2;
                    }
                    break;
                case 9:
                    //Viaduct Inside Arch
                    for (double i = Faktor; i >= 0; i--)
                    {
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0,", b, (i * TexturMulti / Faktor) -0.015);
                        sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 1, (i * TexturMulti / Faktor) -0.015);
                        b = b + 2;
                    }
                    break;
            }
        }

        public static void SetPlatformTexture(StreamWriter sw, int Faktor, double TexturMulti, int Ausrichtung, double platwidth_near, double platwidth_far, double segmente)
        {
            var b = 0;
            switch (Ausrichtung)
            {

                case 1:
                    //Platform Single Mesh

                    var c = 0;
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
                        else
                        {
                            double calculated_texture;
                            if (platwidth_near == platwidth_far)
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
                                double far_texture;
                                double near_texture;
                                //Calculate co-ordinates for far end texture
                                if (platwidth_far <= 5)
                                {
                                    //Calculate texture for below 5m width
                                    far_texture = (0.67 - (0.1675 * (platwidth_far - 1)));
                                }
                                else
                                {
                                    far_texture = 0;
                                }

                                if (platwidth_near <= 5)
                                {
                                    //Calculate texture for below 5m width
                                    near_texture = (0.67 - (0.1675 * (platwidth_near - 1)));
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
                                double far_texture;
                                double near_texture;
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
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},{2},", b, i * TexturMulti / Faktor, calculated_texture);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.67,", b + 1, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.79,", b + 2, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.8,", b + 3, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},0.81,", b + 4, i * TexturMulti / Faktor);
                                sw.WriteLine("SetTextureCoordinates,{0},{1:f4},1,", b + 5, i * TexturMulti / Faktor);
                                c++;
                            }
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


        public static void AddFace(StreamWriter sw, int Faktor, int LiRe_T)
        {
            var face = new double[4];
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


            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }

        public static void AddFace2_New(StreamWriter sw, int Faktor, int LiRe_T)
        {
            var face = new double[4];
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


            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }

        //New face constructor for platforms
        public static void PlatFace(StreamWriter sw, int Faktor, int LiRe_T)
        {
            var face = new double[20];
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


            for (var i = 0; i < Faktor; i++)
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

        public static void AddViaductFace(StreamWriter sw, int Faktor, int LiRe_T, int Ausrichtung)
        {
            switch (Ausrichtung)
            {
                case 1:
                    //Wall
                    var face = new double[12];
                    if (LiRe_T > 0)
                    {
                        face[0] = 0;
                        face[1] = 1;
                        face[2] = 5;
                        face[3] = 4;

                        face[4] = 6;
                        face[5] = 5;
                        face[6] = 1;
                        face[7] = 2;

                        face[8] = 2;
                        face[9] = 3;
                        face[10] = 7;
                        face[11] = 6;
                    }
                    else
                    {
                        face[0] = 4;
                        face[1] = 5;
                        face[2] = 1;
                        face[3] = 0;

                        face[4] = 2;
                        face[5] = 1;
                        face[6] = 5;
                        face[7] = 6;

                        face[8] = 6;
                        face[9] = 7;
                        face[10] = 3;
                        face[11] = 2;
                    }


                    for (var i = 0; i < Faktor; i++)
                    {
                        sw.WriteLine("AddFace,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                        sw.WriteLine("AddFace,{0},{1},{2},{3},", face[4], face[5], face[6], face[7]);
                        sw.WriteLine("AddFace,{0},{1},{2},{3},", face[8], face[9], face[10], face[11]);
                        face[0] += 4;
                        face[1] += 4;
                        face[2] += 4;
                        face[3] += 4;
                        face[4] += 4;
                        face[5] += 4;
                        face[6] += 4;
                        face[7] += 4;
                        face[8] += 4;
                        face[9] += 4;
                        face[10] += 4;
                        face[11] += 4;

                    }
                    break;
                case 2:
                    //Footwalk
                    var face1 = new double[8];
                    if (LiRe_T > 0)
                    {
                        face1[0] = 0;
                        face1[1] = 1;
                        face1[2] = 4;
                        face1[3] = 3;

                        face1[4] = 1;
                        face1[5] = 2;
                        face1[6] = 5;
                        face1[7] = 4;


                    }
                    else
                    {
                        face1[0] = 3;
                        face1[1] = 4;
                        face1[2] = 1;
                        face1[3] = 0;

                        face1[4] = 4;
                        face1[5] = 5;
                        face1[6] = 2;
                        face1[7] = 1;
                    }


                    for (var i = 0; i < Faktor; i++)
                    {
                        sw.WriteLine("AddFace,{0},{1},{2},{3},", face1[0], face1[1], face1[2], face1[3]);
                        sw.WriteLine("AddFace,{0},{1},{2},{3},", face1[4], face1[5], face1[6], face1[7]);
                        face1[0] += 3;
                        face1[1] += 3;
                        face1[2] += 3;
                        face1[3] += 3;
                        face1[4] += 3;
                        face1[5] += 3;
                        face1[6] += 3;
                        face1[7] += 3;
                    }
                    break;
                case 3:
                    //Underside
                    var face3 = new double[4];
                    face3[0] = 0;
                    face3[1] = 1;
                    face3[2] = 2;
                    face3[3] = 3;
                    for (var i = 0; i < Faktor; i++)
                    {
                        if (i%2 == 0)
                        {
                            sw.WriteLine("AddFace,{0},{1},{2},{3},", face3[0], face3[1], face3[2], face3[3]);
                        }
                        else
                        {
                            sw.WriteLine("AddFace,{0},{1},{2},{3},", face3[3], face3[2], face3[1], face3[0]);
                        }
                        face3[0] += 2;
                        face3[1] += 2;
                        face3[2] += 2;
                        face3[3] += 2;
                    }
                    break;
            }
            
        }

        public static void AddFace2(StreamWriter sw, int Faktor)
        {
            var face = new double[4];

            face[0] = 2;
            face[1] = 3;
            face[2] = 1;
            face[3] = 0;

            for (var i = 0; i < Faktor; i++)
            {
                sw.WriteLine("AddFace2,{0},{1},{2},{3},", face[0], face[1], face[2], face[3]);
                face[0] += 2;
                face[1] += 2;
                face[2] += 2;
                face[3] += 2;
            }
        }
    }
}
