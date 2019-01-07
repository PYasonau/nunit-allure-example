using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace AllureStepProject
{
    [SetUpFixture]
    public class FixtureAssembly
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Console.WriteLine("Something to do before all tests in project");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Something to do after all tests in project");
        }
    }
}
