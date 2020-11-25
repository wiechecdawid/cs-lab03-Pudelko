using System;
using System.Collections.Generic;
using System.Text;
using P = Pudelko.Lib.Pudelko;

namespace Pudelko.CLI
{
    public static class PudelkoExtensions
    {
        public static P Kompresuj(this P pudelko)
        {
            double a = Math.Pow(pudelko.Objetosc, 1 / 3);

            return new P(a, a, a);
        }
    }
}
