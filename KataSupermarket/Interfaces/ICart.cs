using System;
using System.Collections.Generic;

namespace KataSupermarket.Interfaces
{
    public interface ICart
    {
        public void AddGood(IGood good, float quantity);
        public bool RemoveGood(String Code);
        public Dictionary<IGood, float> GetGoodsList();
        public int GetTotalItems();
    }
}
