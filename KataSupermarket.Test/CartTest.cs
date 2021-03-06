using KataSupermarket.Config;
using KataSupermarket.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace KataSupermarket.Test
{
    public class CartTest
    {
        private Cart _cart;
        private CheckOut _checkOut;

        public static IEnumerable<object[]> GoodsData =>
        new List<object[]>
        {
            // items of object array: IGood good | float quantity | int ExpectedTotalItems | Decimal ExpectedTotalPrice
            new object[] {new Good("stylo", "Stylo Bleu", 3, GoodUnit.Unit), 3, 3, 9 },             // Test good without discount
            new object[] {new Good("stylo", "Stylo Bleu", 3, GoodUnit.Unit), 4, 4, 10 },            // Test good with discount

            new object[] {new Good("riz", "Riz", 5.4M, GoodUnit.Unit), 1, 1, 3.78 },                // Test good with discount
            new object[] {new Good("riz", "Riz", 5.4M, GoodUnit.Unit), 2, 2, 7.56 },                // Test good with discount
            
            new object[] {new Good("pomme", "Pomme Pink Lady", 4M, GoodUnit.Kilo), 2, 1, 8 },       // Test good without discount
            new object[] {new Good("pomme", "Pomme Pink Lady", 4M, GoodUnit.Kilo), 1.5, 1, 6 },     // Test good without discount
            new object[] {new Good("pomme", "Pomme Pink Lady", 4M, GoodUnit.Kilo), 3, 1, 8 },       // Test good with discount
            
            new object[] {new Good("huile", "Huile vegetale", 2.69M, GoodUnit.Unit), 1, 1, 2.69 },  // Test good without discount
            new object[] {new Good("huile", "Huile vegetale", 2.69M, GoodUnit.Unit), 2, 2, 2.69 }   // Test good with discount
        };

        public static IList<IDiscount> DiscountsData =>
        new List<IDiscount>
        {
            new Discount("stylo", 4, 10),       // buy 4 units for 10 unstread of 12
            new Discount("pomme", 3, 8),        // buy 3 KG for the price of 2 KG
            new Discount("huile", 2, 2.69M),    // buy one, get one for free
            new Discount("riz", 1, 3.78M)       // buy one with 20% discount (3.78 unstead of 5.4)
        };

        public CartTest()
        {
            this._cart = new Cart();
            this._checkOut = new CheckOut(DiscountsData);
        }

        [Fact]
        public void TestCartTotalItems()
        {
            Assert.Equal(0, this._cart.GetTotalItems());
        }

        // Mandatory param "ExpectedTotalPrice" to use GoodsData, but the method is not to test TotalPrice
        [Theory, MemberData(nameof(GoodsData))]
        public void TestAddGood(IGood good, float quantity, int ExpectedTotalItems, Decimal ExpectedTotalPrice)
        {
            this._cart.AddGood(good, quantity);

            Assert.Equal(ExpectedTotalItems, this._cart.GetTotalItems());
        }

        [Fact]
        public void TestAddMultipleGoods()
        {
            Good good = new Good("stylo", "Stylo Bleu", 3, GoodUnit.Unit);
            this._cart.AddGood(good, 2);
            this._cart.AddGood(good, 4);

            Assert.Equal(6, this._cart.GetTotalItems());
        }

        [Theory]
        [InlineData("stylo", true)]
        [InlineData("pomme", true)]
        [InlineData("tomate", false)]
        public void TestRemoveGood(String code, bool ExpectedResult)
        {
            // Add goods to cart
            foreach (var good in GoodsData)
            {
                this._cart.AddGood((IGood)good[0], float.Parse(good[1] + ""));
            }

            // Test remove
            Assert.Equal(ExpectedResult, this._cart.RemoveGood(code));
        }

        [Theory, MemberData(nameof(GoodsData))]
        public void TestCheckOut(IGood good, float quantity, int ExpectedTotalItems, Decimal ExpectedTotalPrice)
        {
            this._cart.AddGood(good, quantity);
            this._checkOut.GetGoods(this._cart.GetGoodsList());

            Assert.Equal(ExpectedTotalItems, this._cart.GetTotalItems());
            Assert.Equal(ExpectedTotalPrice, this._checkOut.GetTotalPrice());
        }

    }
}
