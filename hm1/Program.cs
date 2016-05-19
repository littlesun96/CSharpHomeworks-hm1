using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hm1
{

    public struct Rational
    {
        int numerator;
        int denominator;

        public Rational(int a, int b)
        {
            try
            {
                numerator = a;
                denominator = b;
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                Reduction();
            }
            catch
            {
                System.Console.WriteLine("Exception, denominator is 0");
                numerator = 0;
                denominator = 1;
            }
           
        }

        public Rational(int c, int a, int b)
        {
            try
            {
                numerator = a + c * b;
                denominator = b;
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                Reduction();
            }
            catch
            {
                System.Console.WriteLine("Exception, denominator is 0");
                numerator = 0;
                denominator = 1;
            }

        }

        public override bool Equals(Object obj)
        {
    
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Rational rat = (Rational) obj;

            return (numerator == rat.numerator && denominator == rat.denominator);
        }

        public override int GetHashCode()
        {
            int hash = numerator.GetHashCode();
            hash = (hash * 397) ^ denominator.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            if (denominator == 1)
            {
                return numerator.ToString();
            }
            if (numerator == 0)
            {
                return "0";
            }
            return numerator.ToString() + " / " + denominator.ToString();
        }

        public static Rational operator +(Rational f1, Rational f2) => Addition(f1, f2, 1);

        public static Rational operator -(Rational f1, Rational f2) => Addition(f1, f2, -1);

        public static Rational operator *(Rational f1, Rational f2) => Multiplication(f1, f2, 1);

        public static Rational operator /(Rational f1, Rational f2) => Multiplication(f1, f2, -1);

        public static Rational Addition(Rational f1, Rational f2, int sign)
        {
            int an = f1.numerator;
            int ad = f1.denominator;
            int bn = f2.numerator;
            int bd = f2.denominator;
            int gcd = Gcd(ad, bd);
            int denom = ad / gcd * bd;
            int numer = an * (bd / gcd) + sign * bn * (ad / gcd);
            return Reduction(new Rational(numer, denom));
        }

        public static Rational Multiplication(Rational f1, Rational f2, int sign)
        {
            int an = f1.numerator;
            int ad = f1.denominator;
            int bn;
            int bd;
            if (sign < 0)
            {
                bn = f2.denominator;
                bd = f2.numerator;
            }
            else
            {
                bn = f2.numerator;
                bd = f2.denominator;
            }
            int gcd1 = Gcd(Math.Abs(an), Math.Abs(bd));
            int gcd2 = Gcd(Math.Abs(ad), Math.Abs(bn));
            int numer = an / gcd1 * (bn / gcd2);
            int denom = ad / gcd2 * (bd / gcd1);
            
            return Reduction(new Rational(numer, denom));
        }

        public static Rational Reduction(Rational rational)
        {
            int gcd = Gcd(Math.Abs(rational.numerator), Math.Abs(rational.denominator));
            int numer = rational.numerator / gcd;
            int denom = rational.denominator / gcd;
            if (denom < 0)
            {
                return new Rational(-numer, -denom);
            }
            else
            {
                return new Rational(numer, denom);
            }
        }

        public void Reduction()
        {
            int gcd = Gcd(Math.Abs(numerator), Math.Abs(denominator));
            numerator /= gcd;
            denominator /= gcd;
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
        }

        public static int Gcd(int a, int b)
        {
            if (a == 0 || b == 0) return 1;
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }

        public void ToMixedFraction()
        {
            int an = Math.Abs(numerator);
            int ad = denominator;
            int z = an / ad;
            an %= ad;
            if (numerator < 0)
            {
                z *= -1;
            }
            System.Console.WriteLine(z.ToString() + " + " + new Rational(an, ad).ToString());
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Rational rat1 = new Rational(-3, -4);
            Rational rat2 = new Rational(4, -3);
            System.Console.WriteLine(rat1.Equals(rat2));
            System.Console.WriteLine(rat1.ToString());
            System.Console.WriteLine(rat2.ToString());
            System.Console.WriteLine((rat1 + rat2).ToString());
            System.Console.WriteLine((rat1 - rat2).ToString());
            System.Console.WriteLine((rat1 * rat2).ToString());
            System.Console.WriteLine((rat1 / rat2).ToString());

            System.Console.WriteLine();

            rat1 = new Rational(1, 2, 3);
            System.Console.WriteLine(rat1.ToString());
            rat1.ToMixedFraction();

        }
    }
}
