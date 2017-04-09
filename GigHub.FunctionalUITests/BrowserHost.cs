using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestStack.Seleno.Configuration;

namespace GigHub.FunctionalUITests
{
    public class BrowserHost
    {
        public static readonly SelenoHost Instance = new SelenoHost();
        public static readonly string RootUrl;

        static BrowserHost()
        {

            //var t =
            //    @"C:\_dev\mvcTesting\5-automated-aspdotnet-mvc-m5-exercise-files\after\StronglyTypedPageObjectModels\BankingSite\BankingSite.FunctionalUITests\bin\Debug\";
            RemoteWebDriver y = new ChromeDriver();
            
            // y.Navigate().GoToUrl("https://www.google.co.uk/");
            Instance.Run("GigHub", 50229, config => config.WithRouteConfig(RouteConfig.RegisterRoutes)
            .WithRemoteWebDriver(() => y));

            RootUrl = Instance.Application.Browser.Url;
        }
    }
}
