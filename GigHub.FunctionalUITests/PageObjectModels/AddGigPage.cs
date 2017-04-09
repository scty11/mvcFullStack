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
    public class AddGigPage : Page<GigFormViewModel>
    {
        public T AddGig<T>(GigFormViewModel gig) where T : UiComponent, new()
        {
            Input.Model(gig);

            return Navigate.To<T>(By.Id("AddGid"));
        }
    }
}
