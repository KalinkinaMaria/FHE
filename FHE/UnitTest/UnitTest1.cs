using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using FHE;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testCalcMembershipFuncOneDependence()
        {
        }

        [TestMethod]
        public void testCalcMembershipFuncTwoDependence()
        {
        }

        [TestMethod]
        public void testCalcMembershipFuncFiveDependence()
        {
        }

        [TestMethod]
        public void testCalcMFPointSelectFromOne()
        {
        }

        [TestMethod]
        public void testCalcMFPointSelectFromThreeOther()
        {
        }

        [TestMethod]
        public void testCalcMFPointSelectFromThreeSame()
        {
        }

        [TestMethod]
        public void testMergeEmptyTable()
        {
        }

        [TestMethod]
        public void testMergeEmptyFunc()
        {
        }

        [TestMethod]
        public void testMergeEmptyFuncAndTable()
        {
        }

        [TestMethod]
        public void testMergeNotEmptyFuncAndTable()
        {
        }

        [TestMethod]
        public void testCulcResultGoalPointsMatch()
        {
        }

        [TestMethod]
        public void testCulcResultGoalPointsNotMatch()
        {
        }

        [TestMethod]
        public void testAddMFPointThereIsPoint()
        {
        }

        [TestMethod]
        public void testAddMFPointThereIsPointPlus005()
        {
        }

        [TestMethod]
        public void testAddMFPointThereIsPointMinus005()
        {
        }

        [TestMethod]
        public void testAddMFPointNull()
        {
        }

        [TestMethod]
        public void testAddMFPointToEnd()
        {
        }

        [TestMethod]
        public void testAddMFPointToBegin()
        {
        }

        [TestMethod]
        public void testAddMFPointToMiddle()
        {
        }

        //Тест 1+x1
        [TestMethod]
        public void testCalcResultFunc1()
        {
            double correctlyResult = 4;
            Function func = new Function("1 + x1");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 3);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 1+x1 not correct");
        }

        //Тест x1+x2
        [TestMethod]
        public void testCalcResultFunc2()
        {
            double correctlyResult = 13.5f;
            Function func = new Function("x1+x2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 4.5f);
            args.Add("x2", 9);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1+x2 not correct");
        }

        //Тест 1-x1
        [TestMethod]
        public void testCalcResultFunc3()
        {
            double correctlyResult = 0.1f;
            Function func = new Function("1-x1");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 0.9f);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 1-x1 not correct");
        }

        //Тест x1-x2
        [TestMethod]
        public void testCalcResultFunc4()
        {
            double correctlyResult = 26;
            Function func = new Function("x1-x2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 56);
            args.Add("x2", 30);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1-x2 not correct");
        }

        //Тест 3*x1
        [TestMethod]
        public void testCalcResultFunc5()
        {
            double correctlyResult = 27;
            Function func = new Function("3*x1");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 9);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 3*x1 not correct");
        }

        //Тест x1*x2
        [TestMethod]
        public void testCalcResultFunc6()
        {
            double correctlyResult = 0.9f;
            Function func = new Function("x1*x2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 0.1f);
            args.Add("x2", 9);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1*x2 not correct");
        }

        //Тест x1/2
        [TestMethod]
        public void testCalcResultFunc7()
        {
            double correctlyResult = 4.5f;
            Function func = new Function("x1/2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 9);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1/2 not correct");
        }

        //Тест x1/x2
        [TestMethod]
        public void testCalcResultFunc8()
        {
            double correctlyResult = 1;
            Function func = new Function("x1/x2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 3);
            args.Add("x2", 3);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1/x2 not correct");
        }

        //Тест x1^2
        [TestMethod]
        public void testCalcResultFunc9()
        {
            double correctlyResult = 1;
            Function func = new Function("x1^2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 1);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1^2 not correct");
        }

        //Тест x1^x2
        [TestMethod]
        public void testCalcResultFunc10()
        {
            double correctlyResult = 3;
            Function func = new Function("x1^x2");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 9);
            args.Add("x2", 0.5f);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1^x2 not correct");
        }

        //Тест 3*x1+x2/4
        [TestMethod]
        public void testCalcResultFunc11()
        {
            double correctlyResult = 8;
            Function func = new Function("3*x1+x2/4");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 2);
            args.Add("x2", 8);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 3*x1+x2/4 not correct");
        }

        //Тест (x1+x2+x3)/3
        [TestMethod]
        public void testCalcResultFunc12()
        {
            double correctlyResult = 15.466f;
            Function func = new Function("(x1+x2+x3)/3");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 3);
            args.Add("x2", 9.4f);
            args.Add("x3", 34);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func (x1+x2+x3)/3 not correct");
        }

        //Тест (3*x1-x2)^5*x3
        [TestMethod]
        public void testCalcResultFunc13()
        {
            double correctlyResult = 16807;
            Function func = new Function("(3*x1-x2)^5*x3");
            Dictionary<String, double> args = new Dictionary<string, double>();
            args.Add("x1", 4);
            args.Add("x2", 5);
            args.Add("x3", 1);

            double result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func (3*x1-x2)^5*x3 not correct");
        }
    }
}
