using KataSupermarket.Interfaces;
using System;
using System.Collections.Generic;

namespace KataSupermarket
{
    class Cart : ICart
    {
        public int TotalItems { get; }
        public Decimal TotalPrice { get; }

        public Cart(IList<IDiscount> discounts)
        {
            this._goods = new Dictionary<IGood, float>();
            this._discounts = discounts;
        }

        public void AddGood(IGood good, float quantity)
        {

        }

        public bool RemoveGood(String code)
        {
            return false;
        }

        /// <summary>
        /// Key: good object
        /// Value: good quantity
        /// </summary>
        private Dictionary<IGood, float> _goods;
        private IList<IDiscount> _discounts;
    }
}
