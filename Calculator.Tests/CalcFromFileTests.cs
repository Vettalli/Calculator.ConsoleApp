using NUnit.Framework;
using Calculator.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Calculator.Tests
{
    public class CalcFromFileTests
    {
        private IFile _test;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = new ServiceCollection().
                AddSingleton<IFile, CalcFromFile>().
                BuildServiceProvider();

            _test = _serviceProvider.GetService<IFile>();
        }

        [Test]
        public void EmptyPathTest()
        {
            string path = "";
            bool callFailed = false;

            try
            {
                _test.CalculateExpressions(path);
            }
            catch (ArgumentException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Expected call of CalculateExpressions method failed with ArgumentException");
        }

        [Test]
        public void WrongPathTest()
        {
            string path = "asg";
            bool callFailed = false;

            try
            {
                _test.CalculateExpressions(path);
            }
            catch (FileNotFoundException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Expected call of CalculateExpressions method failed with FileNotFoundException");
        }

        [Test]
        public void NullPathTest()
        {
            string path = null;
            bool callFailed = false;

            try
            {
                _test.CalculateExpressions(path);
            }
            catch (ArgumentNullException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, "Expected call of CalculateExpressions method failed with ArgumentNullException");
        }
    }
}