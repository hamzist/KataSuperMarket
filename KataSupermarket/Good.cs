using KataSupermarket.Config;
using KataSupermarket.Interfaces;
using System;

namespace KataSupermarket
{
    public class Good : IGood
    {
        public String Code { get; }
        public String Name { get; }
        public Decimal Price { get; }
        public GoodUnit Unit { get; }

        public Good(String code, String name, Decimal price, GoodUnit unit)
        {
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.Unit = unit;
        }

    }
}
