using NUnit.Framework;
using System.Transactions;

namespace Koleski.IntegrationTesting
{
    [TestFixture]   
    public abstract class KoleskiTestWrapper
    {
        private TransactionScope _transaction;

        [SetUp]
        public void Setup()
        {
            _transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TearDown]
        public void Teardown()
        {
            _transaction.Dispose();
        }
    }
}
