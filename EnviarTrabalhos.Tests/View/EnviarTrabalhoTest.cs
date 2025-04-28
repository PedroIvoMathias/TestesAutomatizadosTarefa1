using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
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
                //ULR de acesso para testar o front(Usando a url do site upado pelo railway)
                //Se for rodar o test usando a url local, lembre de configurar o certificado https para conseguir acessar.
                driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

                // Preenche o formulário
                driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
                driver.FindElement(By.Id("titulo")).SendKeys("Trabalho de Matemática");
                driver.FindElement(By.Id("conteudo")).SendKeys("Este é um conteúdo de exemplo com mais de 50 caracteres.");
                var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
                selectElement.SelectByValue("1");  // Seleciona o valor "1", que corresponde a "Matemática"

                // Envia o formulário
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                // Valida se a navegação foi feita para a página correta
                Assert.Contains("Listar Trabalhos", driver.PageSource);
            }
        }
    }
}
