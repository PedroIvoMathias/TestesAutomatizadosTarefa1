using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Xunit;

namespace EnviarTrabalhos.Tests.View
{
    public class EnviarTrabalhoTest
    {
        [Fact]
        public void EnviarTrabalho_FormularioSubmetido_ComSucesso()
        {
            // Configura o Selenium WebDriver
            using (var driver = new EdgeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:7064/Trabalho/EnviarTrabalho");

                // Preenche o formulário
                driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
                driver.FindElement(By.Id("titulo")).SendKeys("Trabalho de Matemática");
                driver.FindElement(By.Id("conteudo")).SendKeys("Este é um conteúdo de exemplo com mais de 50 caracteres.");
                driver.FindElement(By.Id("disciplina")).SendKeys("Matematica");

                // Envia o formulário
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                // Valida se a navegação foi feita para a página correta
                Assert.Contains("Listar Trabalhos", driver.PageSource);
            }
        }
    }
}
