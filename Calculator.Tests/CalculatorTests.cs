using NUnit.Framework;
using Calculator.Application;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Calculator.Tests
{
    public class CalculatorTests
    {
        private ICalculate _test;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = new ServiceCollection().
                AddSingleton<ICalculate, CalculatorApplication>().
                BuildServiceProvider();

            _test = _serviceProvider.GetService<ICalculate>();
        }
          
        [Test]
        public void EmptyStringTest()
        {
            string test = "";
            bool callFailed = false;

            try
            {
                _test.Calculate(test);
            }
            catch (InvalidOperationException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Expected call of Calculate method failed with InvalidOperationException");
        }

        [Test]
        public void NullStringTest()
        {
            string test = null;
            bool callFailed = false;

            try
            {
                _test.Calculate(test);
            }
            catch (ArgumentNullException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Ecpected call of Calculate method failed with ArgumentNullException");
        }

        [Test]
        public void CorrectResultTest()
        {
            string test = "50+5*5-2*(25+5)";
            double result = 15;

            Assert.AreEqual(result, _test.Calculate(test));
        }

        [Test]
        public void StartWithMinusTest()
        {
            string test = "-234+321/30+75*79";
            double result = 5701.6999999999998d;

            Assert.AreEqual(_test.Calculate(test), result);
        }

        [Test]
        public void BracketMinusTest()
        {
            string test = "-55*(-25)+761-5*5";
            double result = 2111.0d;

            Assert.AreEqual(_test.Calculate(test), result);
        }

        [Test]
        public void InvalidDataTest()
        {
            string test = "24t+55";
            bool callFailed = false;

            try
            {
                _test.Calculate(test);
            }
            catch (InvalidOperationException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Expected call of Calculate method failed with InvalidOperationException");
        }
    }
}