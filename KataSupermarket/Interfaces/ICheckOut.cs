using System;
using System.Collections.Generic;

namespace KataSupermarket.Interfaces
{
    public interface ICheckOut
    {
        public void GetGoods(Dictionary<IGood, float> goods);
        public Decimal GetTotalPrice();
    }
}
