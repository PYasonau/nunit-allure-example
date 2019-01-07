using System;
using System.Collections.Generic;
using System.Text;
using Allure.Commons;
using NUnit.Framework;

namespace AllureStepProject.Allure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureStepAttribute : NUnitAttribute
    {
        private readonly string _stepName;
        private string _ignoredContainerId;

        public AllureStepAttribute(string stepName = "Set Up step name")
        {
            _stepName = stepName;
        }
        
        public void Step()
        {
            Exception stepException = null;

            var stepUid = Guid.NewGuid().ToString("N");
            AllureLifecycle.Instance.StartStep(stepUid, new StepResult
            {
                name = _stepName,
                descriptionHtml = TestContext.CurrentContext.Test.MethodName
            });

            try
            {
               // action();
            }
            catch (Exception e)
            {
                stepException = e;
            }

            if (stepException == null)
                AllureLifecycle.Instance.StopStep(x => { x.status = Status.passed; });
            else
            {
                AllureLifecycle.Instance.StopStep(x => { x.status = Status.failed; });
                throw stepException;
            }
        }

        //public void BeforeTest(ITest suite)
        //{
        //    _ignoredContainerId = suite.Id + "-ignored";
        //    var fixture = new TestResultContainer
        //    {
        //        uuid = _ignoredContainerId,
        //        name = suite.ClassName
        //    };
        //    AllureLifecycle.Instance.StartTestContainer(fixture);
        //}

        //public void AfterTest(ITest suite)
        //{
        //    suite = (TestSuite)suite;
        //    if (suite.HasChildren)
        //    {
        //        var ignoredTests =
        //            suite.Tests.Where(t => t.RunState == RunState.Ignored || t.RunState == RunState.Skipped);
        //        foreach (var test in ignoredTests)
        //        {
        //            AllureLifecycle.Instance.UpdateTestContainer(_ignoredContainerId, t => t.children.Add(test.Id));

        //            var reason = test.Properties.Get(PropertyNames.SkipReason).ToString();

        //            var ignoredTestResult = new TestResult
        //            {
        //                uuid = test.Id,
        //                name = test.Name,
        //                fullName = test.FullName,
        //                status = Status.skipped,
        //                labels = new List<Label>
        //                {
        //                    Label.Suite(_suiteName),
        //                    Label.SubSuite(reason),
        //                    Label.Thread(),
        //                    Label.Host(),
        //                    Label.TestClass(test.ClassName),
        //                    Label.TestMethod(test.MethodName),
        //                    Label.Package(test.ClassName)
        //                }
        //            };
        //            AllureLifecycle.Instance.StartTestCase(ignoredTestResult);
        //            AllureLifecycle.Instance.StopTestCase(ignoredTestResult.uuid);
        //            AllureLifecycle.Instance.WriteTestCase(ignoredTestResult.uuid);
        //        }

        //        AllureLifecycle.Instance.StopTestContainer(_ignoredContainerId);
        //        AllureLifecycle.Instance.WriteTestContainer(_ignoredContainerId);
        //    }
        //}

        //public ActionTargets Targets => ActionTargets.Suite;
    }
}
