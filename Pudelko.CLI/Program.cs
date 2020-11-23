using Pudelko.Lib;
using P = Pudelko.Lib.Pudelko;
using System;

namespace Pudelko.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new P(a: 11.0, b: 2.5, unit: UnitOfMeasure.centimeter);
            Console.WriteLine("{0} x {1} x {2}", p.A, p.B, p.C);
        }
    }
}
