using System;
using System.Globalization;

namespace Pudelko.Lib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private readonly double _length;
        private readonly double _heigth;
        private readonly double _width;
        private readonly UnitOfMeasure _unit;

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _length = (a > 0) && (a / (double)unit <= 10.0) ? a : throw new ArgumentException(a.ToString());
            _width = (b > 0) && (b / (double)unit <= 10.0) ? b : throw new ArgumentException(b.ToString());
            _heigth = (c > 0) && (c / (double)unit <= 10.0) ? c : throw new ArgumentException(c.ToString());
        }

        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter) { }

        //Length of the box rounded to 3 decimals
        public double A
        {
            get => Math.Round(this._length, 3);
        }

        //Width of the box rounded to 3 decimals
        public double B
        {
            get => Math.Round(this._width, 3);
        }

        //Heigth of the box rounded to 3 decimals
        public double C
        {
            get => Math.Round(this._heigth, 3);
        }

        public double Objetosc => Math.Round((A / (double)_unit) * (B / (double)_unit) * (C / (double)_unit), 9);

        public double Pole => Math.Round((2 * (A / (double)_unit) * (B / (double)_unit)) + (4 * (B / (double)_unit) * (C / (double)_unit)), 6);

        public override string ToString()
        {
            char x = '\u00D7';
            double a = A / (double)_unit;
            double b = B / (double)_unit;
            double c = C / (double)_unit;

            string unit = "m";

            return string.Format("{2} {0} {1} {3} {0} {1} {4} {0}", unit, x, a.ToString("F3"), b.ToString("F3"), c.ToString("F3"));
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if(String.IsNullOrEmpty(format)) format = "m";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            char x = '\u00D7';
            double a = 0.1, b = 0.1, c = 0.1;

            switch(format)
            {
                case "m":
                    ToString();
                    break;
                case "cm":
                    a = Math.Round(A * (double)_unit / 100, 1);
                    b = Math.Round(B * (double)_unit / 100, 1);
                    c = Math.Round(C * (double)_unit / 100, 1);
                    break;
                case "mm":
                    a = Math.Round(A * (double)_unit / 1000);
                    b = Math.Round(B * (double)_unit / 1000);
                    c = Math.Round(C * (double)_unit / 1000);
                    break;
                default:
                    throw new ArgumentException(format);
            }

            return string.Format("{2} {0} {1} {3} {0} {1} {4} {0}", format, x, a, b, c);
        }

        public override int GetHashCode() => (A / (double)_unit, B / (double)_unit, C / (double)_unit).GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
                return (this as IEquatable<Pudelko>).Equals(obj as Pudelko);

            return false;
        }

        public bool Equals(Pudelko p)
        {
            if (p is null)
                return false;
            if (Object.ReferenceEquals(this, p)) return true;

            return this.Objetosc == p.Objetosc;
        }

        public static bool Equals(Pudelko p1, Pudelko p2)
        {
            if ((p1 is null) && (p2 is null)) return true;
            if (p1 is null) return false;

            return p1.Equals(p2);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2) => Pudelko.Equals(p1, p2);

        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);
    }

    public enum UnitOfMeasure
    {
        milimeter = 1000,
        centimeter = 100,
        meter = 1
    }
}
