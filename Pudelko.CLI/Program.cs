using Pudelko.Lib;
using P = Pudelko.Lib.Pudelko;
using System;
using System.Collections.Generic;

namespace Pudelko.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var boxes = new List<P>();
            var r = new Random();

            for(int i = 0; i < 5; i++)
            {
                var p = new P(r.NextDouble() * 10, r.NextDouble() * 10, r.NextDouble() * 10);

                Console.WriteLine($"Wymiary: {p.ToString()} Objetosc: {p.Objetosc} Pole powierzchni: {p.Pole}");

                boxes.Add(p);
            }
            Console.WriteLine();

            boxes.Sort((p1, p2) =>
            {
                if (p1.Objetosc < p2.Objetosc) return -1;
                if (p1.Objetosc > p2.Objetosc) return 1;
                if (p1.Pole < p2.Pole) return -1;
                if (p1.Pole > p2.Pole) return 1;
                if (p1.A + p1.B + p1.C < p2.A + p2.B + p2.C) return -1;
                if (p1.A + p1.B + p1.C > p2.A + p2.B + p2.C) return 1;
                return 0;
            });

            foreach(var p in boxes)
                Console.WriteLine($"Wymiary: {p.ToString()} Objetosc: {p.Objetosc} Pole powierzchni: {p.Pole}");
        }
    }
}
