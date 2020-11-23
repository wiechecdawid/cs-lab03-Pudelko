using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Pudelko.Lib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        private readonly double _length;
        private readonly double _heigth;
        private readonly double _width;
        private readonly UnitOfMeasure _unit;
        private readonly double[] _indexArr;

        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _length = (a > 0) && (a / (double)unit <= 10.0) ? a / (double)unit : throw new ArgumentOutOfRangeException(a.ToString());
            _width = (b > 0) && (b / (double)unit <= 10.0) ? b / (double)unit : throw new ArgumentOutOfRangeException(b.ToString());
            _heigth = (c > 0) && (c / (double)unit <= 10.0) ? c / (double)unit : throw new ArgumentOutOfRangeException(c.ToString());

            if (A == 0 || B == 0 || C == 0)
                throw new ArgumentOutOfRangeException();

            _indexArr = new double[] { A, B, C };
            _unit = unit;
        }

        public Pudelko(double a, double b, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _length = (a > 0) && (a / (double)unit <= 10.0) ? a / (double)unit : throw new ArgumentOutOfRangeException(a.ToString());
            _width = (b > 0) && (b / (double)unit <= 10.0) ? b / (double)unit : throw new ArgumentOutOfRangeException(b.ToString());

            if (A == 0 || B == 0)
                throw new ArgumentOutOfRangeException();

            _heigth = 0.1;

            _indexArr = new double[] { A, B, C };
            _unit = unit;
        }

        public Pudelko(double a, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _length = (a > 0) && (a / (double)unit <= 10.0) ? a / (double)unit : throw new ArgumentOutOfRangeException(a.ToString());

            if (A == 0)
                throw new ArgumentOutOfRangeException();

            _width = 0.1;
            _heigth = 0.1;

            _indexArr = new double[] { A, B, C };
            _unit = unit;
        }

        public Pudelko(UnitOfMeasure unit)
        {
            _length = 0.1;
            _width = 0.1;
            _heigth = 0.1;

            _indexArr = new double[] { A, B, C };
            _unit = unit;
        }

        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter) { }

        //Length of the box rounded to 3 decimals
        public double A
        {
            get => Math.Truncate(1000 * this._length) / 1000;
        }

        //Width of the box rounded to 3 decimals
        public double B
        {
            get => Math.Truncate(1000 * this._width) / 1000;
        }

        //Heigth of the box rounded to 3 decimals
        public double C
        {
            get => Math.Truncate(1000 * this._heigth) / 1000;
        }

        public double Objetosc => Math.Round(A * B * C, 9);

        public double Pole => Math.Round((2 * A * B) + (4 * B * C), 6);

        public override string ToString()
        {
            char x = '\u00D7';
            double a = A ;
            double b = B;
            double c = C;

            string unit = "m";

            return string.Format("{2} {0} {1} {3} {0} {1} {4} {0}", unit, x, a.ToString("F3"), b.ToString("F3"), c.ToString("F3"));
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            if(String.IsNullOrEmpty(format)) format = "m";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            char x = '\u00D7';
            string a = "", b = "", c = "";

            switch(format)
            {
                case "m":
                    return ToString();
                case "cm":
                    a = Math.Round(A * 100, 1).ToString("F1");
                    b = Math.Round(B * 100, 1).ToString("F1");
                    c = Math.Round(C * 100, 1).ToString("F1");
                    break;
                case "mm":
                    a = Math.Round(A * 1000).ToString();
                    b = Math.Round(B * 1000).ToString();
                    c = Math.Round(C * 1000).ToString();
                    break;
                default:
                    throw new FormatException(format);
            }

            return string.Format("{2} {0} {1} {3} {0} {1} {4} {0}", format, x, a, b, c);
        }

        public override int GetHashCode() => (A, B, C).GetHashCode();

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

        public IEnumerator<double> GetEnumerator()
        {
            foreach(var i in _indexArr)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public static bool operator ==(Pudelko p1, Pudelko p2) => Pudelko.Equals(p1, p2);

        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);

        public static explicit operator double[] (Pudelko p) => new double[] {p.A, p.B, p.C};

        public static implicit operator Pudelko ((int, int, int) t) => new Pudelko(t.Item1, t.Item2, t.Item3, UnitOfMeasure.milimeter);

        public double this[int i]
        {
            get => _indexArr[i];
        }

        public static Pudelko Parse(string input)
        {
            List<double> sizes = new List<double>();
            string[] inputs = input.Split(' ');

            foreach(var i in inputs)
            {
                if (double.TryParse(i, out double temp))
                    sizes.Add(temp);
            }

            return sizes.Count == 3 ? new Pudelko(sizes[0], sizes[1], sizes[2]) : throw new FormatException($"Incorrect string format: {input}");
        }
    }

    public enum UnitOfMeasure
    {
        milimeter = 1000,
        centimeter = 100,
        meter = 1
    }
}
