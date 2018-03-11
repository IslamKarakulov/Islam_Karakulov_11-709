using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sem1
{
    [TestFixture]
    public class PolinomTests
    {
        [Test]
        public void InsertIn()
        {
            var polinom = new Polinom();
            polinom.Insert(-1, 2);
            polinom.Insert(4, 3);
            polinom.Insert(10, 1);
            Check(polinom.ToString(), "(10*x^(1))+(-1*x^(2))+(4*x^(3))");
        }

        [Test]
        public void Delete()
        {
            var polinom = new Polinom();
            polinom.Insert(9, 3);
            polinom.Insert(7, 4);
            polinom.Delete(4);
            polinom.Insert(1, 5);
            polinom.Insert(8, 2);
            polinom.Delete(5);
            polinom.Delete(2);
            Check(polinom.ToString(), "(9*x^(3))");
        }

        [Test]
        public void Derivate()
        {
            var polinom = new Polinom();
            polinom.Insert(3, 3);
            polinom.Insert(4, 4);
            polinom.Derivate();
            Check(polinom.ToString(), "(9*x^(2))+(16*x^(3))");
        }

        [Test]
        public void DerivateConstant()
        {
            var polinom = new Polinom();
            polinom.Insert(3, 3);
            polinom.Insert(100, 0);
            polinom.Insert(-100, 0);
            polinom.Derivate();
            Check(polinom.ToString(), "(9*x^(2))");
        }

        [Test]
        public void GetValue()
        {
            var polinom = new Polinom();
            polinom.Insert(1, 1);
            polinom.Insert(4, 3);
            Assert.AreEqual(polinom.Value(1), 5);
            Assert.AreEqual(polinom.Value(2), 34);
            Assert.AreEqual(polinom.Value(0), 0);
            Assert.AreEqual(polinom.Value(-1), -5);
        }

        [Test]
        public void Combine()
        {
            var polinom = new Polinom();
            polinom.Insert(4, 2);
            polinom.Insert(2, 1);
            polinom.Insert(4, 2);
            polinom.Insert(4, 3);
            polinom.Insert(-5, 3);
            polinom.Insert(-4, 2);
            polinom.Insert(3, 2);
            polinom.Insert(-2, 5);
            polinom.Combine();
            Check(polinom.ToString(), "(2*x^(1))+(7*x^(2))+(-1*x^(3))+(-2*x^(5))");
        }

        [Test]
        public void Sum()
        {
            var polinom1 = new Polinom();
            var polinom2 = new Polinom();
            polinom1.Insert(1, 2);
            polinom1.Insert(2, 4);
            polinom1.Insert(3, 1);
            polinom2.Insert(4, 3);
            polinom2.Insert(5, 1);
            polinom2.Insert(6, 2);
            polinom1.Sum(polinom2);
            Check(polinom1.ToString(), "(8*x^(1))(7*x^(2))+(4*x^(3))+(2*x^(4))");
        }

        [Test]
        public void DeleteOdd()
        {
            var polinom = new Polinom();
            polinom.Insert(1, 1);
            polinom.Insert(2, 2);
            polinom.Insert(3, 3);
            polinom.Insert(4, 4);
            polinom.Insert(5, 5);
            polinom.Insert(6, 6);
            polinom.Insert(7, 7);
            polinom.Insert(8, 8);
            polinom.DeleteOdd();
            Check(polinom.ToString(), "(2*x^(2))+(4*x^(4))+(6*x^(6))+(8*x^(8))");
        }

        public void Check(string polinom, string result)
        {
            Assert.AreEqual(polinom.Length, result.Length);
            for (int i = 0; i < polinom.Length; i++)
                Assert.AreEqual(polinom[i], result[i]);
        }
    }
}