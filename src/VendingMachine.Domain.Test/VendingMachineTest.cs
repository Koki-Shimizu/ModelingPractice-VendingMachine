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
            var vendingMachine = VendingMachine.Create();

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
            var vendingMachine = VendingMachine.Create();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void �w���エ�ނ��ԋp����()
        {
            var vendingMachine = VendingMachine.Create();

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
    }
}
