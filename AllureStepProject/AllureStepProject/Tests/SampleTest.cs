using System;
using NUnit.Framework;

namespace AllureStepProject
{
    public class Example : BaseTest
    {
        [Test]
        public void PassTest()
        {
            Console.WriteLine("Hello test");
            Step("First step", () => { });
            Step("Second step", () => { });
            Step("Third step", () => { });
        }

        [Test]
        public void FailTest()
        {
            Console.WriteLine("Hello test");
            Step("First step", () => { });
            Step("Second step", () => { });
            Step("Third step", () => { });
            Step("Fourth step", () => { throw new Exception("Should fail on this step"); });
        }
    }
}
