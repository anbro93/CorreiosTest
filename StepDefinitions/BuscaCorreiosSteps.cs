using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.ConstrainedExecution;
using TechTalk.SpecFlow;

namespace CorreiosTests.Steps
{
    [Binding]
    public class BuscaCorreiosSteps
    {
        private IWebDriver _driver;

        [Given(@"que eu estou na pagina de busca de CEP dos Correios")]
        public void GivenQueEuEstouNaPaginaDeBuscaDeCEPDosCorreios()
        {
            var options = new ChromeOptions();
            // Ensure headless mode is not set
            options.AddArgument("--disable-gpu");
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(options);
            _driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php");
        }

        [Given(@"que eu estou na pagina de busca de Rastreio dos Correios")]
        public void GivenQueEuEstouNaPaginaDeBuscaDeRastreioDosCorreios()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://rastreamento.correios.com.br/app/index.php");
        }

        [When(@"eu busco pelo CEP ""(.*)""")]
        public void WhenEuBuscoPeloCEP(string cep)
        {
            var searchBox = _driver.FindElement(By.Name("endereco"));
            searchBox.Clear();
            searchBox.SendKeys(cep);

            //solução do CAPTCHA interrompe o fluxo aqui

            var searchButton = _driver.FindElement(By.Id("btn_pesquisar"));
            searchButton.Click();
        }

        [When(@"eu busco pelo rastreio ""(.*)""")]
        public void WhenEuBuscoPeloRastreio(string rastreio)
        {
            var searchBox = _driver.FindElement(By.Name("objeto"));
            searchBox.Clear();
            searchBox.SendKeys(rastreio);

            //solução do CAPTCHA interrompe o fluxo aqui

            var searchButton = _driver.FindElement(By.Id("b-pesquisar"));
            searchButton.Click();
        }

        [Then(@"o resultado deve indicar que o CEP e invalido")]
        public void ThenOResultadoDeveIndicarQueOCEPEInvalido()
        {
            var resultElement = _driver.FindElement(By.Id("mensagem-resultado"));
            string resultText = resultElement.Text;
            Assert.That(resultText.Contains("Não há dados a serem exibidos"), $"Esperado que o resultado indique CEP inválido, mas foi '{resultText}'");
            _driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php");
        }

        [Then(@"o resultado deve conter o endereco ""(.*)""")]
        public void ThenOResultadoDeveConterOEndereco(string enderecoEsperado)
        {
            var resultElement = _driver.FindElement(By.XPath("//table[@id='resultado-DNEC']//td[contains(text(), '" + enderecoEsperado + "')]"));
            string resultText = resultElement.Text;
            Assert.That(resultText.Contains(enderecoEsperado), $"Esperado que o resultado contenha '{enderecoEsperado}', mas foi '{resultText}'");
            _driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php");
        }

        [Then(@"o resultado deve indicar que o rastreio e invalido")]
        public void ThenOResultadoDeveIndicarQueORastreioEInvalido()
        {
            var resultElement = _driver.FindElement(By.CssSelector("#alerta > div.msg"));
            string resultText = resultElement.Text;
            Assert.That(resultText.Contains("Objeto não encontrado na base de dados dos Correios."), $"Esperado que o resultado indique um rastreio inválido, mas foi '{resultText}'");
        }

    }
}