using System;
using System.Linq;

namespace PointInPolygon
{
    class Program
    {
        public readonly struct Coord
        {
            public Coord(decimal lat, decimal lon)
            {
                Lat = lat;
                Lon = lon;
            }

            public readonly decimal Lat;
            public readonly decimal Lon;
        }

        private static readonly Coord[] SouthAmerica = new[]
        {
            new Coord(13.5654992m,-75.7124747m),
            new Coord(-3.8184486m,-82.4393237m),
            new Coord(-19.9545679m,-72.7876217m),
            new Coord(-52.224575m, -77.474365m),
            new Coord(-56.248649m, -68.896744m),
            new Coord(-54.552111m, -63.974869m),
            new Coord(-50.992102m, -67.578384m),
            new Coord(-4.130257m, -31.733221m),
            new Coord(13.749831m, -66.977361m),
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Point inside polygon");

            // Dentro
            Test(-26.362579m, -50.427868m);
            Test(-49.373311m, -70.561694m);
            Test(-54.257094m, -68.100757m);

            // Fora
            Test(8.420936m, -80.757006m);
            Test(-22.418474m, -79.702319m);
            Test(-31.041009m, -41.382008m);
        }

        static void Test(decimal lat, decimal lon)
        {
            Console.WriteLine($"{lat}, {lon}: " + PointInside(SouthAmerica, new Coord(lat, lon)));
        }

        static bool PointInside(Coord[] poly, Coord pnt)
        {
            int i, j;
            int nvert = poly.Length;
            bool c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((poly[i].Lon > pnt.Lon) != (poly[j].Lon > pnt.Lon)) &&
                 (pnt.Lat < (poly[j].Lat - poly[i].Lat) * (pnt.Lon - poly[i].Lon) / (poly[j].Lon - poly[i].Lon) + poly[i].Lat))
                    c = !c;
            }
            return c;
        }
    }
}
