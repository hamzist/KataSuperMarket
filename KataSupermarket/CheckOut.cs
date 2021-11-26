using KataSupermarket.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KataSupermarket
{
    public class CheckOut : ICheckOut
    {
        public CheckOut(IList<IDiscount> discounts)
        {
            this._goods = null;
            this._discounts = discounts;
        }

        public CheckOut(Dictionary<IGood, float> goods, IList<IDiscount> discounts)
        {
            this._goods = goods;
            this._discounts = discounts;
        }

        public void GetGoods(Dictionary<IGood, float> goods)
        {
            this._goods = goods;
        }

        public Decimal GetTotalPrice()
        {
            Decimal totalPrice = 0;

            // Check goods
            if (this._goods == null || this._goods.Count == 0)
            {
                return 0;
            }

            foreach (var good in this._goods)
            {
                // Check if a discount can be applied
                IDiscount discount = this._discounts.Where(x => x.Code.Equals(good.Key.Code) && (x.Quantity == good.Value || x.Quantity == 1)).FirstOrDefault();

                if (discount == null)
                {
                    // No discount found
                    totalPrice += good.Key.Price * (Decimal)good.Value;
                }
                else if (discount.Quantity == 1)
                {
                    // Discount found on unit price. We use reduced price unstead of normal price
                    totalPrice += discount.TotalPrice * (Decimal)good.Value;
                }
                else
                {
                    // A discount can be applied
                    totalPrice += discount.TotalPrice;
                }
            }

            return totalPrice;
        }

        private Dictionary<IGood, float> _goods;
        private IList<IDiscount> _discounts;
    }
}
