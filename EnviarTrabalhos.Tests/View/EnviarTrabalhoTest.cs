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

        [Fact]
        public void EnviarTrabalho_DeveFalharComCamposEmBranco()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("required", driver.PageSource);
        }

        [Fact]
        public void EnviarTrabalho_ComNomeVazio_DeveExibirErro()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.Id("nomeAluno")).SendKeys("");//Falha aqui
            driver.FindElement(By.Id("titulo")).SendKeys("Trabalho de Matemática");
            driver.FindElement(By.Id("conteudo")).SendKeys("Conteúdo válido");

            var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
            selectElement.SelectByValue("1");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Enviar Trabalho", driver.PageSource); // Continua na mesma tela
        }

        [Fact]
        public void EnviarTrabalho_ComTituloVazio_DeveExibirErro()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
            driver.FindElement(By.Id("titulo")).SendKeys(""); // Falha aqui
            driver.FindElement(By.Id("conteudo")).SendKeys("Conteúdo válido");

            var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
            selectElement.SelectByValue("1");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Enviar Trabalho", driver.PageSource); // Continua na mesma tela
        }

        [Fact]
        public void EnviarTrabalho_ComConteudoVazio_DeveExibirErro()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
            driver.FindElement(By.Id("titulo")).SendKeys("Trabalho de Matemática");
            driver.FindElement(By.Id("conteudo")).SendKeys(" ");//Falha aqui

            var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
            selectElement.SelectByValue("1");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Enviar Trabalho", driver.PageSource); // Continua na mesma tela
        }

        public void EnviarTrabalho_ComSelectVazio_DeveExibirErro()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
            driver.FindElement(By.Id("titulo")).SendKeys("Trabalho de Matemática");
            driver.FindElement(By.Id("conteudo")).SendKeys(" ");//Falha aqui

            var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
            selectElement.SelectByValue(" ");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Enviar Trabalho", driver.PageSource); // Continua na mesma tela
        }


        [Fact]
        public void EnviarTrabalho_DeveFalharComConteudoMuitoCurto()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/EnviarTrabalho");

            driver.FindElement(By.Id("nomeAluno")).SendKeys("João Silva");
            driver.FindElement(By.Id("titulo")).SendKeys("Trabalho X");
            driver.FindElement(By.Id("conteudo")).SendKeys("Curto"); // Conteúdo < 50 caracteres

            var selectElement = new SelectElement(driver.FindElement(By.Id("disciplina")));
            selectElement.SelectByText("Matemática");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.DoesNotContain("Trabalhos Enviados", driver.PageSource);
        }


    }
}
