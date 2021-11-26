using KataSupermarket.Config;
using KataSupermarket.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KataSupermarket
{
    public class Cart : ICart
    {
        public Cart()
        {
            this._goods = new Dictionary<IGood, float>();
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

        public Dictionary<IGood, float> GetGoodsList()
        {
            // We return a copy of goods list to have original list only updatable by cart class
            return new Dictionary<IGood, float>(this._goods);
        }
        
        public int GetTotalItems()
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
    }
}
