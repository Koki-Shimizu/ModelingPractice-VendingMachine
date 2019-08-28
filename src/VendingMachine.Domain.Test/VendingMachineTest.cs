using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VendingMachine.Domain.Test
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void �w�肳�ꂽ�ԍ��̏��i���w���ł���()
        {
            var vendingMachine = CreateVendingMachine();
            vendingMachine.Restock(new DisplayProductNumber(1), 5);

            vendingMachine.Post(Money._100);
            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual("�R�[��", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(2));
            Assert.AreEqual("�I�����W�W���[�X", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(3));
            Assert.AreEqual("�Β�", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(4));
            Assert.AreEqual("���X�`", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(5));
            Assert.AreEqual("���b�h�u��", product.Name);
        }

        [TestMethod]
        public void �����z������Ă��Ȃ��ꍇ�͍w���ł��Ȃ�()
        {
            var vendingMachine = CreateVendingMachine();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void �w���エ�ނ��ԋp����()
        {
            var vendingMachine = CreateVendingMachine();

            vendingMachine.Post(Money._100);
            vendingMachine.Purchase(new DisplayProductNumber(1));
            var change = vendingMachine.Refund();
            Assert.AreEqual(0, change.Count());

            vendingMachine.Post(Money._100);
            vendingMachine.Post(Money._100);
            vendingMachine.Purchase(new DisplayProductNumber(1));
            change = vendingMachine.Refund();
            Assert.AreEqual(1, change.Count());
            Assert.AreEqual(Money._100, change.ElementAt(0));
        }

        private VendingMachine CreateVendingMachine()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�R�[��", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(2), new Product("�I�����W�W���[�X", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(3), new Product("�Β�", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(4), new Product("���X�`", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(5), new Product("���b�h�u��", new Price(100))));

            vendingMachine.Restock(new DisplayProductNumber(1), 5);
            vendingMachine.Restock(new DisplayProductNumber(2), 5);
            vendingMachine.Restock(new DisplayProductNumber(3), 5);
            vendingMachine.Restock(new DisplayProductNumber(4), 5);
            vendingMachine.Restock(new DisplayProductNumber(5), 5);
            return vendingMachine;
        }
    }
}
