using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Economics
{
    public class OrderSheet<TProduct>
        : IEnumerable<OrderSheet<TProduct>.ILineItem>
        where TProduct : IProduct
    {
        private Dictionary<TProduct, OrderLine> orderLines;

        public OrderSheet(ICustomer customer, IShop<TProduct> shop)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            if (shop == null)
            {
                throw new ArgumentNullException("shop");
            }

            Customer = customer;
            Shop = shop;

            orderLines = new Dictionary<TProduct, OrderLine>(
                //comparer: new DescriptorComparer<TDescriptor>());
            );
        }

        public ICustomer Customer { get; private set; }
        public IShop<TProduct> Shop { get; private set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                var e = GetEnumerator();
                while (e.MoveNext())
                {
                    totalPrice += e.Current.TotalPrice;
                }

                return totalPrice;
            }
        }
        public ILineItem this[TProduct index]
        {
            get
            {
                if (orderLines.ContainsKey(index))
                    return orderLines[index];
                return null;
            }
        }

        public void Add(TProduct product, int quantity = 1)
        {
            if (!orderLines.ContainsKey(product))
            {
                orderLines.Add(product, new OrderLine(product));
            }

            var lineItem = orderLines[product];
            lineItem.Quantity += quantity;
            orderLines[product] = lineItem;
        }

        public void Remove(TProduct product, int quantity = 1)
        {
            if (!this.orderLines.ContainsKey(product))
            {
                return;
            }

            var orderLine = this.orderLines[product];
            orderLine.Quantity =
                Math.Max(orderLine.Quantity - quantity, 0);
            if (orderLine.Quantity <= 0)
            {
                this.orderLines.Remove(orderLine.Product);
            }
        }

        public IEnumerator<ILineItem> GetEnumerator()
        {
            return orderLines.Values.Cast<ILineItem>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return orderLines.Values.GetEnumerator();
        }

        /// <summary>
        /// 상품과 주문 수량을 나타낸다.
        /// </summary>
        public interface ILineItem
        {
            TProduct Product { get; }
            int Quantity { get; }
            decimal TotalPrice { get; }
        }

        private struct OrderLine : ILineItem
        {
            public OrderLine(TProduct product)
            {
                Product = product;
                Quantity = 0;
            }

            public OrderLine(TProduct product, int quantity)
                : this(product)
            {
                Quantity = quantity;
            }

            public TProduct Product { get; private set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get { return Product.UnitPrice * Quantity; } }
        }
    }
}
