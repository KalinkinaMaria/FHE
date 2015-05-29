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
            float correctlyResult = 4;
            Function func = new Function("1 + x1");
            Dictionary<String, float> args = new Dictionary<string,float>();
            args.Add("x1", 3);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 1+x1 not correct");
        }

        //Тест x1+x2
        [TestMethod]
        public void testCalcResultFunc2()
        {
            float correctlyResult = 13.5f;
            Function func = new Function("x1+x2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 4.5f);
            args.Add("x2", 9);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1+x2 not correct");
        }

        //Тест 1-x1
        [TestMethod]
        public void testCalcResultFunc3()
        {
            float correctlyResult = 0.1f;
            Function func = new Function("1-x1");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 0.9f);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 1-x1 not correct");
        }

        //Тест x1-x2
        [TestMethod]
        public void testCalcResultFunc4()
        {
            float correctlyResult = 26;
            Function func = new Function("x1-x2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 56);
            args.Add("x2", 30);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1-x2 not correct");
        }

        //Тест 3*x1
        [TestMethod]
        public void testCalcResultFunc5()
        {
            float correctlyResult = 27;
            Function func = new Function("3*x1");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 9);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 3*x1 not correct");
        }

        //Тест x1*x2
        [TestMethod]
        public void testCalcResultFunc6()
        {
            float correctlyResult = 0.9f;
            Function func = new Function("x1*x2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 0.1f);
            args.Add("x2", 9);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1*x2 not correct");
        }

        //Тест x1/2
        [TestMethod]
        public void testCalcResultFunc7()
        {
            float correctlyResult = 4.5f;
            Function func = new Function("x1/2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 9);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1/2 not correct");
        }

        //Тест x1/x2
        [TestMethod]
        public void testCalcResultFunc8()
        {
            float correctlyResult = 1;
            Function func = new Function("x1/x2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 3);
            args.Add("x2", 3);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1/x2 not correct");
        }

        //Тест x1^2
        [TestMethod]
        public void testCalcResultFunc9()
        {
            float correctlyResult = 1;
            Function func = new Function("x1^2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 1);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1^2 not correct");
        }

        //Тест x1^x2
        [TestMethod]
        public void testCalcResultFunc10()
        {
            float correctlyResult = 3;
            Function func = new Function("x1^x2");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 9);
            args.Add("x2", 0.5f);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func x1^x2 not correct");
        }

        //Тест 3*x1+x2/4
        [TestMethod]
        public void testCalcResultFunc11()
        {
            float correctlyResult = 8;
            Function func = new Function("3*x1+x2/4");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 2);
            args.Add("x2", 8);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func 3*x1+x2/4 not correct");
        }

        //Тест (x1+x2+x3)/3
        [TestMethod]
        public void testCalcResultFunc12()
        {
            float correctlyResult = 15.466f;
            Function func = new Function("(x1+x2+x3)/3");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 3);
            args.Add("x2", 9.4f);
            args.Add("x3", 34);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func (x1+x2+x3)/3 not correct");
        }

        //Тест (3*x1-x2)^5*x3
        [TestMethod]
        public void testCalcResultFunc13()
        {
            float correctlyResult = 16807;
            Function func = new Function("(3*x1-x2)^5*x3");
            Dictionary<String, float> args = new Dictionary<string, float>();
            args.Add("x1", 4);
            args.Add("x2", 5);
            args.Add("x3", 1);

            float result = func.calcResult(args);

            Assert.AreEqual(result, correctlyResult, 0.001, "Func (3*x1-x2)^5*x3 not correct");
        }
    }
}
