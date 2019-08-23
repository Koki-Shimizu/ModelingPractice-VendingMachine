using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Domain.Test
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void �w��()
        {
            var vendingMachine = VendingMachine.Create();

            vendingMachine.Post(Money._100);

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));

            Assert.AreEqual("�R�[��", product.Name);
        }
    }
}
