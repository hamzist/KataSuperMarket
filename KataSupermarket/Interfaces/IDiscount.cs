using System;

namespace KataSupermarket.Interfaces
{
    public interface IDiscount
    {
        public String Code { get; }
        public float Quantity { get; }
        public Decimal TotalPrice { get; }
    }
}
