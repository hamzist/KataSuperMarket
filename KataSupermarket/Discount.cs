using KataSupermarket.Interfaces;
using System;

namespace KataSupermarket
{
    public class Discount : IDiscount
    {
        public String Code { get; }
        public float Quantity { get; }
        public Decimal TotalPrice { get; }

        public Discount(String code, float quantity, Decimal totalPrice)
        {
            this.Code = code;
            this.Quantity = quantity;
            this.TotalPrice = totalPrice;
        }

    }
}
