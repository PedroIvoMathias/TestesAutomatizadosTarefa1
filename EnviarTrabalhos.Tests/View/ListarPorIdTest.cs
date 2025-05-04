using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace EnviarTrabalhos.Tests.View
{
    public class ListarPorIdTest
    {
        [Fact]
        public void BuscarPorId_DeveMostrarTrabalhoQuandoIdExiste()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/ListarTrabalhos");

            driver.FindElement(By.Id("id")).SendKeys("1"); // Um ID conhecido que exista no banco
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Resultado da Busca", driver.PageSource);
        }

        [Fact]
        public void BuscarPorId_DeveMostrarNadaQuandoIdNaoExiste()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://testesautomatizadostarefa1-production.up.railway.app/Trabalho/ListarTrabalhos");

            driver.FindElement(By.Id("id")).SendKeys("9999"); // Um ID que não existe
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.Contains("Não foram localizados trabalhos para este Id!", driver.PageSource);
        }
    }
}
