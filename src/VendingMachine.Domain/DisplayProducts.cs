﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProducts
    {
        private readonly ConcurrentDictionary<DisplayProductNumber, DisplayProduct> _displayProducts = new ConcurrentDictionary<DisplayProductNumber, DisplayProduct>();

        public void SetDisplayProduct(DisplayProduct displayProduct)
        {
            _displayProducts.AddOrUpdate(displayProduct.ProductNumber, displayProduct, (key, value) => displayProduct);
        }

        public DisplayProduct Find(DisplayProductNumber displayProductNumber)
        {
            _displayProducts.TryGetValue(displayProductNumber, out var displayProduct);
            return displayProduct;
        }

        public DisplayProduct FindWithValidate(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = Find(displayProductNumber);
            if (displayProduct == null) throw new InvalidOperationException("DisplayProduct is not exists.");

            return displayProduct;
        }

        public void Restock(DisplayProductNumber displayProductNumber, int salableStock)
        {
            var displayProduct = FindWithValidate(displayProductNumber);
            displayProduct.Restock(salableStock);
        }
    }
}
