using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using TechTalk.SpecFlow;
using System;

namespace AdminAssistant
{
    [Binding]
    public class TestHooks
    {
        [Before]
        public void CreateWebDriver(ScenarioContext context)
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Debug.WriteLine($"AdminAssistant.Blazor.Test - The Directory where your webdriver exe should be placed is: {currentDirectory}");

            // Launch Microsoft Edge (Chromium) - See https://github.com/microsoft/edge-selenium-tools
            // Requires the WebDriver for your version of Edge (chromium) - see https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/#downloads
            var options = new EdgeOptions();
            options.UseChromium = true;
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            IWebDriver webDriver = new EdgeDriver(options);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            context["WEB_DRIVER"] = webDriver;
        }

        [After]
        public void CloseWebDriver(ScenarioContext context)
        {
            var driver = (IWebDriver)context["WEB_DRIVER"];
            driver.Quit();
        }
    }
}
