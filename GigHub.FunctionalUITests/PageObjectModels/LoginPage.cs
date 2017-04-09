using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Core.ViewModels;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;


namespace GigHub.FunctionalUITests.PageObjectModels
{
    public class LoginPage : Page<LoginViewModel>
    {
        public T Login<T>(LoginViewModel login) where T : UiComponent, new()
        {
            Input.Model(login);
            
            return Navigate.To<T>(By.Id("Login"));
        }
    }
}
