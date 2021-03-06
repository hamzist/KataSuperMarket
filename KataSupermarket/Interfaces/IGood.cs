using KataSupermarket.Config;
using System;

namespace KataSupermarket.Interfaces
{
    public interface IGood
    {
        public String Code { get; }
        public String Name { get; }
        public Decimal Price { get; }
        public GoodUnit Unit { get; }
    }
}
