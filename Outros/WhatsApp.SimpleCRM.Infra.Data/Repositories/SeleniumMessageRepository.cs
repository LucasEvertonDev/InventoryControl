using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WhatsApp.SimpleCRM.Domain.Contracts.Infra.Data;

namespace WhatsApp.SimpleCRM.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de mensagem que utiliza o Selenium para fazer o envio de mensagem
    /// </summary>
    public class SeleniumMessageRepository : IMessageRepository
    {
        /// <summary>
        /// Driver do navegador a ser instrumentado
        /// </summary>
        private static IWebDriver _webDriver;

        /// <summary>
        /// Serviço de log
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Serviço de log</param>
        public SeleniumMessageRepository(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Send(string recipient, string message)
        {
            if (!IsValid(recipient, message))
            {
                return;
            }

            try
            {
                OpenBrowser();

                await SendMessage(recipient, message);

                await CloseBrowser();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Erro ao tentar enviar mensagem");
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communications"></param>
        /// <returns></returns>
        public async Task SendBatch(IEnumerable<Tuple<string, string>> communications)
        {
            if (!IsValid(communications))
            {
                return;
            }

            try
            {
                OpenBrowser();

                foreach (Tuple<string, string> communication in communications)
                {
                    if (!IsValid(communication.Item1, communication.Item2))
                    {
                        continue;
                    }

                    await SendMessage(communication.Item1, communication.Item2);
                }

                await CloseBrowser();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Erro ao tentar enviar mensagens");
                throw e;
            }
        }

        /// <summary>
        /// Define se um destinatário e mensagem são válidos
        /// </summary>
        /// <param name="recipient">Destinatário</param>
        /// <param name="message">Mensagem</param>
        /// <returns>True se os parâmetros foram válidos e False caso contrário</returns>
        private bool IsValid(string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                _logger.Error("O destinatário da mensagem não foi informado");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.Error("Não é possível enviar uma mensagem em branco para o destinatário {0}", recipient);
            }

            return true;
        }

        /// <summary>
        /// Define se a coleção
        /// </summary>
        /// <param name="communications"></param>
        /// <returns>True se a coleção é válida</returns>
        private bool IsValid(IEnumerable<Tuple<string, string>> communications)
        {
            if (!communications.Any())
            {
                _logger.Information("O arquivo informado está vazio!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Abre o browser navegando para a janela do WhatsApp Web
        /// </summary>
        private void OpenBrowser()
        {
            if (_webDriver == null || !System.Diagnostics.Process.GetProcessesByName("chromedriver").Any())
            { 
                _webDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            }
           
            _webDriver.Navigate().GoToUrl("https://web.whatsapp.com/");
        }

        /// <summary>
        /// Realiza o logout do WhatsApp Web
        /// </summary>
        /// <returns>Operação assíncrona</returns>
        private async Task Logout()
        {
            //IWebElement menuButton = _webDriver.FindElement(By.XPath("//span[@data-icon='menu']"));
            //menuButton.Click();

            await Task.Delay(10);

            ////IWebElement menuButtonParent = menuButton.FindElement(By.XPath("./.."));
            ////IWebElement logoutButton = menuButtonParent.FindElement(By.XPath("//span/div/ul/li[last()]"));
            ////logoutButton.Click();
        }

        /// <summary>
        /// Fecha o browser
        /// </summary>
        private async Task CloseBrowser()
        {
            await Logout();
            //_webDriver.Quit();
        }

        /// <summary>
        /// Envia uma mensagem a um destinatário
        /// </summary>
        /// <param name="recipient">Destinatário</param>
        /// <param name="message">Mensagem</param>
        /// <returns>Operação assíncrona</returns>
        private async Task SendMessage(string recipient, string message)
        {
            try
            {
                await NavigateToRecipientChat(recipient);

                await Task.Delay(500);

                TypeMessage(message);

                await Task.Delay(500);

                ClickOnSend();

                await Task.Delay(500);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Erro ao enviar a mensagem {0} para {1}", message, recipient);
                throw e;
            }
        }

        /// <summary>
        /// Navega até o chat do destinatário da mensagem
        /// </summary>
        /// <param name="recipient">Nome do detinatário no WhatsApp</param>
        private async Task NavigateToRecipientChat(string recipient)
        {
            _webDriver.Url = "https://wa.me/" + recipient;
            //Task.Run(() =>
            //{
            //    //IWebElement qrCode = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-testid='qrcode']")));
            //    //if (qrCode.Displayed)
            //    //{
            //    //    IWebElement sidePane = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@title='Compartilhe no WhatsApp']")));
            //    //    if (sidePane.Displayed)
            //    //    {
            //    //        _webDriver.Manage().Window.Minimize();
            //    //    }
            //    //}
            //});

            IWebElement sidePane = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@title='Compartilhe no WhatsApp']")));

            IWebElement targetChat = sidePane.FindElement(By.XPath("//a[@title='Compartilhe no WhatsApp']"));
            targetChat.Click();

            IWebElement sidePane2 = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'use o WhatsApp Web')]")));

            var pai = sidePane2.FindElement(By.XPath("//span[contains(text(),'use o WhatsApp Web')]"));
            var element = pai.FindElement(By.XPath("./.."));
            element.Click();

            IWebElement sidePane4 = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.Id("pane-side")));
        }

        /// <summary>
        /// Digita o conteúdo da mensagem
        /// </summary>
        /// <param name="message">Mensagem a ser enviada</param>
        private void TypeMessage(string message)
        {
            _webDriver.FindElement(By.TagName("footer")).FindElement(By.XPath("//div[@spellcheck='true']")).SendKeys(message);
        }

        /// <summary>
        /// Clica no botáo de enviar mensagem
        /// </summary>
        private void ClickOnSend()
        {
            _webDriver.FindElement(By.TagName("footer")).FindElement(By.XPath("//span[@data-icon='send']")).Click();
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task OpenWhatsApp()
        {
            OpenBrowser();

            IWebElement sidePane = new WebDriverWait(_webDriver, TimeSpan.FromDays(1)).Until(ExpectedConditions.ElementIsVisible(By.Id("pane-side")));

            if (sidePane.Displayed)
            {
                _webDriver.Manage().Window.Minimize();
            }

            return Task.CompletedTask;
        }
    }
}

