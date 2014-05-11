using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreciaUzduotis;
using System.Collections.Generic;
using TreciaUzduotis.aim;

namespace AntraTests
{
    [TestClass]
    public class TreciaUzduotis
    {
        [TestMethod]
        public void TestMethod1()
        {
            double[,] A = new double[3, 3] 
            {
                {2, 1, 0.95},
                {1, 2, 1},
                {0.95, 1, 2}
            };
            double[,] f = new double[3, 1] {
                {3.95}, {4}, {3.95}
            };
            List<double[,]> x = new List<double[,]>()
            {
                new double[3, 1] {{0}, {0}, {0}}
            };
            dnm didziausioNuolydzio = new dnm(A, f, x, 0.0001);

            var Af = didziausioNuolydzio.Mult(A, f);
            var z0 = didziausioNuolydzio.Subt(x[0], f);

            double[,] test = new double[1, 3] {
                {-15.6525,15.9,-15.6525}
            };
            var ah = didziausioNuolydzio.Mult(z0, test);
            var ahh = didziausioNuolydzio.Mult(f, 0.2);
            double az0 = didziausioNuolydzio.Sum(ahh) / didziausioNuolydzio.Sum(ah);
        }
        [TestMethod]
        public void TestMethod2()
        {
            double[,] A = new double[3, 3] 
            {
                {2, 1, 0.95},
                {1, 2, 1},
                {0.95, 1, 2}
            };
            double[,] f = new double[3, 1] {
                {3.95}, {4}, {3.95}
            };
            List<double[,]> x = new List<double[,]>()
            {
                new double[3, 1] {{0}, {0}, {0}}
            };
            dnm didziausioNuolydzio = new dnm(A, f, x, 0.0001);
            didziausioNuolydzio.Program();
        }
        [TestMethod]
        public void TestMethod3()
        {
            double[,] A = new double[4, 4] 
            { 
                { 1.812, 0.202, -0.599, 0.432 }, 
                { 0.202, 1.812, 0.202, -0.599 }, 
                { -0.599, 0.202, 1.812, 0.202 }, 
                { 0.432, -0.599, 0.202, 1.812 } 
            };
            double[,] f = new double[4, 1] {
                {6.64727}, {6.19459}, {5.43449}, {1.66165}
            };
            List<double[,]> x = new List<double[,]>()
            {
                new double[1,4] {{0, 0, 0, 0}}
            };
            double[,] answer = new double[,] {
                {10.759}, {12.670}, {15.416}, {3.270}
            };

            dnm didziausioNuolydzio = new dnm(A, f, x, 0.0001);
            double[,] test = didziausioNuolydzio.Mult(A, f);

        }
        [TestMethod]
        public void AIM()
        {
            double[,] A = new double[,] { 
            { 47.702, 47.6, 47, 47 }, 
            { 47.6, 47.702, 47.6, 47 }, 
            { 47, 47.6, 47.702, 47.6 }, 
            { 47, 47, 47.6, 47.702 } 
            };
            double[,] xo = new double[4, 1] {
                {1}, {0}, {0}, {0}
            };
            double[,] e = new double[,] {
                {1,0,0,0},
                {0,1,0,0},
                {0,0,1,0},
                {0,0,0,1}
            };
            aim AtvIter = new aim(A, xo, 20, 0.0001, e);
            AtvIter.Program();
        }
    }
}
