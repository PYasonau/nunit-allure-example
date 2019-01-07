using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AllureStepProject
{
    public class AllureBase
    {
        protected AllureLifecycle _allure = AllureLifecycle.Instance;

        [SetUp]
        public void SetUp()
        {
            _allure.StartTestCase(new TestResult
            {
                uuid = TestContext.CurrentContext.Test.ID
            });
        }

        [TearDown]
        public void TearDown()
        {
            _allure.StopTestCase(x =>
            {
                x.status = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed ? Status.passed : Status.failed;
                x.descriptionHtml = TestContext.CurrentContext.Result.Message;
                x.name = TestContext.CurrentContext.Test.Name;
                x.statusDetails = new StatusDetails
                {
                    message = TestContext.CurrentContext.Result.Message,
                    trace = TestContext.CurrentContext.Result.StackTrace
                };
            });
            _allure.WriteTestCase(TestContext.CurrentContext.Test.ID);
        }
    }

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class BaseTest : AllureBase
    {
        protected IWebDriver driver;

        [SetUp]
        public void WebSetUp()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void WebTearDown()
        {
            driver?.Quit();
        }

        public void Step(string name, Action action)
        {
            Exception stepException = null;

            var stepUid = Guid.NewGuid().ToString("N");
            _allure.StartStep(stepUid, new StepResult
            {
                name = name,
                descriptionHtml = TestContext.CurrentContext.Test.MethodName
            });

            try
            {
                action();
            }
            catch (Exception e)
            {
                stepException = e;
            }

            if (stepException == null)
                _allure.StopStep(x => { x.status = Status.passed; });
            else
            {
                _allure.StopStep(x => { x.status = Status.failed; });
                throw stepException;
            }
        }
    }
}
