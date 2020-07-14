using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AdminAssistant
{
    [Binding]
    public sealed class CommonStepDefinition
    {
        private readonly ScenarioContext context;
        private readonly IWebDriver webDriver;

        public CommonStepDefinition(ScenarioContext injectedContext)
        {
            context = injectedContext;
            webDriver = (IWebDriver)context["WEB_DRIVER"];
        }

        [Given(@"The application is loaded")]
        public void GivenTheApplicationIsLoaded()
        {
            webDriver.Navigate().GoToUrl(new Uri($"https://localhost:5001"));
        }

        [Given(@"I am on the '(.*)' screen")]
        public void GivenIAmOnTheScreen(string route)
        {
            webDriver.Navigate().GoToUrl(new Uri($"https://localhost:5001/{route}"));
        }
    }
}
