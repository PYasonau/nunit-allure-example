using System;
using System.Collections.Generic;
using System.Text;
using AllureStepProject.Allure;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AllureStepProject.Tests
{
    class WebSample : BaseTest
    {
        [Test]
        public void WebSampleTest()
        {
            Step("Navigate to Google.com", () =>
            {
                driver.Navigate().GoToUrl("https://google.com");
            });

            Step("Validate URL", () => Assert.That(driver.Url, Does.Contain("google").IgnoreCase, "Unexpected URL"));
        }
    }

    public class WebSampleFail : BaseTest
    {
        [Test]
        public void WebSampleFailTest()
        {
            Step("Navigate to Google.com", () =>
            {
                driver.Navigate().GoToUrl("https://google.com");
            });

            Step("Validate URL", () => Assert.That(driver.Url, Does.Contain("gooogle").IgnoreCase, "Unexpected URL"));
        }
    }

    public class WebSampleTestStep : BaseTest
    {
        [Test]
        public void T()
        {

            SomeMethod();
        }
        [AllureStep("asdfasdf")]
        public void SomeMethod()
        {
            int d = 1;
            var c = "string";
            Action a = new Action(() => { });
        }
    }
}
