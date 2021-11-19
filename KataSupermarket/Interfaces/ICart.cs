using System;

namespace KataSupermarket.Interfaces
{
    public interface ICart
    {
        public int TotalItems { get; }
        public Decimal TotalPrice { get; }
        public void AddGood(IGood good, float quantity);
        public bool RemoveGood(String Code);
    }
}
