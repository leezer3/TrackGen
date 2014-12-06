/*
 * Mathematical Functions
 */

using System;

namespace Weiche
{
    class MathFunctions
    {
        public class Transform
        {
            public Transform(int _laenge, double _eigenRadius, int _LiRe, int z)
            {
                laenge = _laenge;
                eigenRadius = _eigenRadius;
                LiRe = _LiRe;
                _z = z;
                radiusOffset = Math.Sqrt(eigenRadius * eigenRadius - 12.5 * 12.5);
            }
            public double X(double x, double z)
            {
                if (eigenRadius != 0)
                {
                    winkel = Math.Atan((z + _z - 12.5) / radiusOffset);
                    return (radiusOffset - (Math.Cos(winkel) * (eigenRadius - x * LiRe))) * LiRe;
                }
                else
                {
                    return x;
                }
            }
            public double Z(double x, double z)
            {
                if (eigenRadius != 0)
                {
                    winkel = Math.Atan((z + _z - 12.5) / radiusOffset);
                    return (12.5 + Math.Sin(winkel) * (eigenRadius - x * LiRe));
                }
                else
                {
                    return z + _z;
                }
            }
            int laenge;          // < in Meter
            double eigenRadius;
            double radiusOffset;
            double winkel;
            int LiRe;
            int _z;
        }

        //Für gebogene tote Kurve mit Abweichung

        //Generate curves using deviation???

        //Paramaaters are as follows:
        //Segment??
        //Radius
        //???
        //Left or right

        public static double Abbiege_x(double z, double radius_tot, double x_Abweich, int LiReT)
        {
            var winkel = Math.Asin(z / radius_tot);
            return ((radius_tot - Math.Sqrt(radius_tot * radius_tot - z * z) + Math.Cos(winkel) * x_Abweich) * LiReT);
        }

        public static double Abbiege_z(double z, double radius_tot, double x_Abweich, int LiReT)
        {
            var winkel = Math.Asin(z / radius_tot);
            return (z + Math.Sin(winkel) * x_Abweich * -1);
        }


        public static double radius_tot(int laenge, double Abweichung)
        {
            var winkel = Math.Atan(Abweichung / (laenge * 25));
            var radius_t = laenge * 25 / (Math.Sin(2 * winkel));
            return (radius_t);
        }

        public static double winkel_tot(int laenge, double Abweichung)
        {
            return (Math.Atan(Abweichung / (laenge * 25)));
        }

        public static double x_Weichenoefnung(double radius, double z, double Endkoordinate)
        {
            return (radius - Math.Sqrt(radius * radius - (25 - 25 * z / Endkoordinate) * (25 - 25 * z / Endkoordinate)));
        }
    }
}
