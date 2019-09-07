using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void �����z������Ȃ��ꍇ�͍w���ł��Ȃ�()
        {
            var vendingMachine = CreateVendingMachine();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void ���ނ肪����Ȃ��ꍇ�͍w���ł��Ȃ�()
        {
            var vendingMachine = CreateVendingMachine();

            vendingMachine.Post(Money._10);
            vendingMachine.Post(Money._500);
            vendingMachine.Post(Money._1000);

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void ����؂�̏ꍇ�w���ł��Ȃ�()
        {
            var vendingMachine = CreateVendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�R�[��", new Price(100))));

            vendingMachine.Post(Money._100);

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

        [TestMethod]
        public void �g�p�\�Ȃ����̂ݓ����ł���()
        {
            var vendingMachine = CreateVendingMachine();

            Assert.IsFalse(vendingMachine.Post(Money._1));
            Assert.IsFalse(vendingMachine.Post(Money._5));
            Assert.IsTrue(vendingMachine.Post(Money._10));
            Assert.IsTrue(vendingMachine.Post(Money._100));
            Assert.IsTrue(vendingMachine.Post(Money._500));
            Assert.IsFalse(vendingMachine.Post(Money._2000));
            Assert.IsFalse(vendingMachine.Post(Money._5000));
            Assert.IsFalse(vendingMachine.Post(Money._10000));
        }

        [TestMethod]
        public void ��������ȏ�̂����͓����ł��Ȃ�()
        {
            var vendingMachine = CreateVendingMachine();

            void TestPost(Money money, int maxPostableCount)
            {
                for (int postCount = 0; postCount < maxPostableCount; postCount++)
                    Assert.IsTrue(vendingMachine.Post(money));

                Assert.IsFalse(vendingMachine.Post(money));
            }

            TestPost(Money._10, 30);
            TestPost(Money._100, 30);
            TestPost(Money._500, 30);
            TestPost(Money._1000, 30);
        }

        [TestMethod]
        public void ���i����50�܂�()
        {
            var vendingMachine = new VendingMachine();

            for (int productCount = 0; productCount < 50; productCount++)
            {
                var number = productCount + 1;
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(number), new Product("�e�X�g" + number, new Price(100))));
            }

            Assert.ThrowsException<InvalidOperationException>(() => 
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(51), new Product("�e�X�g" + 51, new Price(100))));
            });
        }

        [TestMethod]
        public void ���i�̒l�i��10�~�ȏ�300�~�ȉ��ł��ނ肪�Ԃ���l�i�ł���()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�e�X�g", new Price(10))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�e�X�g", new Price(300))));

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�e�X�g", new Price(0))));
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�e�X�g", new Price(105))));
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�e�X�g", new Price(310))));
            });
        }

        private VendingMachine CreateVendingMachine()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("�R�[��", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(2), new Product("�I�����W�W���[�X", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(3), new Product("�Β�", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(4), new Product("���X�`", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(5), new Product("���b�h�u��", new Price(100))));

            vendingMachine.RestockProduct(new DisplayProductNumber(1), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(2), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(3), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(4), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(5), new ProductStockQuantity(5));
            return vendingMachine;
        }
    }
}
