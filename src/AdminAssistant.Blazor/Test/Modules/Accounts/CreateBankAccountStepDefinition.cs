using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AdminAssistant.Modules.Accounts
{
    [Binding]
    public sealed class CreateBankAccountStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ScenarioContext context;
        private readonly IWebDriver webDriver;

        public CreateBankAccountStepDefinition(ScenarioContext injectedContext)
        {
            context = injectedContext;
            webDriver = (IWebDriver)context["WEB_DRIVER"];
        }

        [Given(@"I click 'Add Account'")]
        public void GivenIClick()
        {
            webDriver.FindElement(By.Id("Accounts_AddAccount")).Click();
        }

        [Given(@"The Bank Account dialog Title reads '(.*)'")]
        public void GivenTheBankAccountDialogTitleReads(string title)
        {
            webDriver.FindElement(By.Id("Accounts_BankAccountEditDialog_Title")).Text.Should().Be(title);
        }
    }
}
