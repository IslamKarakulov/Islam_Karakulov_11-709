using System;
using System.Linq;
using System.IO;
using System.Text;

namespace Sem1
{
    class Polinom
    {
        public LinkedList<Monom> Polynomials;

        public Polinom()
        {
            Polynomials = new LinkedList<Monom>();
        }

        public Polinom(string filename)
        {
            Polynomials = new LinkedList<Monom>();
            var polynomials = File.ReadAllLines(filename);
            string[] array;
            foreach (var polynomial in polynomials)
            {
                array = polynomial.Split(' ');
                Insert(int.Parse(array[0]), int.Parse(array[1]));
            }
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("({0}*x^({1}))", Polynomials.First.Item.Coef, Polynomials.First.Item.Deg);
            foreach (var pair in Polynomials.Skip(1))
                strBuilder.AppendFormat("+({0}*x^({1}))", pair.Coef, pair.Deg);
            return strBuilder.ToString();
        }

        public void Insert(int coef, int deg)
        {
            var newMonom = new Monom(coef, deg);
            var current = Polynomials.Last;
            while (current != null)
            {
                if (deg < current.Item.Deg)
                {
                    if (current.Previous != null)
                    {
                        if (deg >= current.Previous.Item.Deg)
                        {
                            Polynomials.AddBefore(current, newMonom);
                            return;
                        }
                    }
                    else
                    {
                        Polynomials.AddFirst(newMonom);
                        return;
                    }
                }
                if (deg >= current.Item.Deg) 
                {
                    if (current.Previous != null && deg < current.Next.Item.Deg)
                    {
                        Polynomials.AddAfter(current, newMonom);
                        return;
                    }
                    else
                    {
                        if (current.Next == null)
                        {
                            Polynomials.AddLast(newMonom);
                        }
                        return;
                    }
                }
                current = current.Previous;
            }
            if (current == null)
            {
                Polynomials.AddLast(newMonom);
            }
        }

        public void Combine()
        {
            var current = Polynomials.First;
            while (current != null && current.Next != null)
            {
                if (current.Item.Deg == current.Next.Item.Deg)
                {
                    int newCoef = current.Item.Coef + current.Next.Item.Coef;
                    var newMonom = new Monom(newCoef, current.Item.Deg);
                    Insert(newMonom.Coef, newMonom.Deg);
                    current = current.Next;
                    Polynomials.Remove(current.Previous);
                    current = current.Next;
                    Polynomials.Remove(current.Previous);
                }
                else current = current.Next;
            }
            foreach (var polynomial in Polynomials)
            {
                if (polynomial.Coef == 0)
                    Delete(polynomial.Deg);
            }
        }

        public void Delete(int deg)
        {
            var current = Polynomials.First;
            while (current != null)
            {
                if (current.Item.Deg == deg)
                {
                    Polynomials.Remove(current);
                }
                current = current.Next;
            }
        }

        public void Derivate()
        {
            Delete(0);
            var current = Polynomials.First;
            while (current != null)
            {
                current.Item = new Monom(current.Item.Coef * current.Item.Deg, current.Item.Deg - 1);
                current = current.Next;
            }
        }

        public void Sum(Polinom p)
        {
            var current = p.Polynomials.First;
            while (current != null)
            {
                Insert(current.Item.Coef, current.Item.Deg);
                current = current.Next;
            }
            Combine();
        }

        public int Value(int x)
        {
            Combine();
            var i = 0;
            var current = Polynomials.First;
            while (i != Polynomials.Last.Item.Deg)
            {
                if (current.Item.Deg != i)
                {
                    Insert(0, i);
                }
                else
                {
                    current = current.Next;
                }
                i++;
            }
            int value = Polynomials.Last.Item.Coef;
            current = Polynomials.Last.Previous;
            while (current != null)
            {
                value = value * x + current.Item.Coef;
                current = current.Previous;
            }
            return value;
        }

        public void DeleteOdd()
        {
            var current = Polynomials.First;
            while (current != null)
            {
                if (current.Item.Coef % 2 != 0)
                    Polynomials.Remove(current);
                current = current.Next;
            }
        }
    }
}