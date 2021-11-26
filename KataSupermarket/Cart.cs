using KataSupermarket.Config;
using KataSupermarket.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KataSupermarket
{
    public class Cart : ICart
    {
        public int TotalItems { get { return this.GetTotalItems(); } }
        public Decimal TotalPrice { get { return this.GetTotalPrice(); } }

        public Cart(IList<IDiscount> discounts)
        {
            this._goods = new Dictionary<IGood, float>();
            this._discounts = discounts;
        }

        public void AddGood(IGood good, float quantity)
        {
            // Check if good exists in Cart
            if (this.IsGoodExists(good.Code))
            {
                // Compute new quantity
                float new_q = this._goods.Where(x => x.Key.Code.Equals(good.Code)).FirstOrDefault().Value + quantity;

                // Remove existing good
                this._goods.Remove(this._goods.Where(x => x.Key.Code.Equals(good.Code)).FirstOrDefault().Key);

                // Adding good with new quantity
                this._goods.Add(good, new_q);
            }
            else
            {
                // Adding good to cart
                this._goods.Add(good, quantity);
            }
        }

        public bool RemoveGood(String code)
        {
            // Check if good exsits id Cart
            if (this.IsGoodExists(code))
            {
                this._goods.Remove(this._goods.Where(x => x.Key.Code.Equals(code)).FirstOrDefault().Key);
                return true;
            }

            return false;
        }

        private Decimal GetTotalPrice()
        {
            Decimal totalPrice = 0;

            foreach (var good in this._goods)
            {
                // Check if a discount can be applied
                IDiscount discount = this._discounts.Where(x => x.Code.Equals(good.Key.Code) && x.Quantity == good.Value).FirstOrDefault();

                if (discount == null)
                {
                    // No discount found
                    totalPrice += good.Key.Price * (Decimal)good.Value;
                }
                else
                {
                    // A discount can be applied
                    totalPrice += discount.TotalPrice;
                }
            }

            return totalPrice;
        }

        private int GetTotalItems()
        {
            int totalItems = 0;

            foreach (var good in this._goods)
            {
                if (good.Key.Unit == GoodUnit.Unit)
                {
                    // If good has unit unit, add number of items. The quantity here cannot be a floating number
                    totalItems += (int)good.Value;
                }
                else
                {
                    // If good has KG/Meter/... unit, add 1 item. We cannot count the number of items in 1 KG for exp
                    totalItems += 1;
                }
            }

            return totalItems;
        }

        protected bool IsGoodExists(String code)
        {
            return (this._goods.Where(x => x.Key.Code.Equals(code)).Count() > 0) ? true : false;
        }

        /// <summary>
        /// Key: good object
        /// Value: good quantity
        /// </summary>
        private Dictionary<IGood, float> _goods;

        private IList<IDiscount> _discounts;
    }
}
