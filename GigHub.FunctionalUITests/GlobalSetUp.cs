using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GigHub.FunctionalUITests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeTearDown]
        public void Dispose()
        {
            Task.Delay(5000).Wait();

            // Cleanup and close browser
            BrowserHost.Instance.Dispose();
        }
    }
}
