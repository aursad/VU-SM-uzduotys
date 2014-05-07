using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AntraUzduotis;
using AntraUzduotis.Model;

namespace AntraTests
{
    [TestClass]
    public class AntraUzduotis
    {
        [TestMethod]
        public void TestMatrix()
        {
            Matrix matrix = new Matrix();

            Line lineOne = new Line();
            lineOne.id = 0;
            lineOne.values = new List<double>() { 2, 0.5, 0, 0, 3};
            Line lineTwo = new Line();
            lineTwo.id = 1;
            lineTwo.values = new List<double>() { 0.5, 2, 0.5, 0, 6};
            List<Line> lines = new List<Line>() {lineOne, lineTwo};

            matrix.line = lines;

            Assert.AreEqual(2, matrix.line.Count);
            Assert.AreEqual(0, matrix.line[0].id);
            Assert.AreEqual(1, matrix.line[1].id);

            Line line = matrix.line[1];
            Assert.AreEqual(0.5, line.values[0]);

            line.c = -0.25;
            line.d = 1.5;
            Assert.AreEqual(-0.25, matrix.line[1].c);
            Assert.AreEqual(1.5, matrix.line[1].d);
        }
        [TestMethod]
        public void TestMoveMethod()
        {
            MoveMethod mm = new MoveMethod();
            mm.ReadFromFile("test1.txt");
            mm.Start();
            mm.Reverse();
            mm.Print();
        }
        [TestMethod]
        public void TestMoveMethod2()
        {
            MoveMethod mm = new MoveMethod();
            mm.ReadFromFile("test2.txt");
            mm.Start();
            mm.Reverse();
            mm.Print();
        }
        //[TestMethod]
        public void TestMoveMethod3()
        {
            MoveMethod mm = new MoveMethod();
            mm.ReadFromFile("test3.txt");
            mm.Start();
            mm.Reverse();
            mm.Print();
        }
    }
}
