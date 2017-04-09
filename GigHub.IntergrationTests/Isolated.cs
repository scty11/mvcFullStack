using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NUnit.Framework;

namespace GigHub.IntergrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public void AfterTest(NUnit.Framework.Interfaces.ITest test)
        {
            _transactionScope.Dispose();
        }

        public void BeforeTest(NUnit.Framework.Interfaces.ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test;}
        }
    }
}
