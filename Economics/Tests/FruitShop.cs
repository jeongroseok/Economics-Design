using System;
using System.Collections;
using System.Collections.Generic;

namespace Economics.Tests
{
    class FruitShop : IShop<IFruit>
    {
        private List<InternalProduct> products;

        public FruitShop()
        {
            products = new List<InternalProduct>
            {
                new InternalProduct()
                {
                    Name = "사과",
                    Description = "싱싱한 사과",
                    UnitPrice = 100
                },
                new InternalProduct()
                {
                    Name = "바나나",
                    Description = "노란 바나나",
                    UnitPrice = 80
                },
                new InternalProduct()
                {
                    Name = "오렌지",
                    Description = "맛좋은 오렌지",
                    UnitPrice = 120
                },
            };
        }

        public IFruit this[int index] { get { return products[index]; } }

        public IEnumerator<IFruit> GetEnumerator()
        {
            return products.GetEnumerator();
        }

        public IPaymentApproval TryOrder(OrderSheet<IFruit> orderSheet)
        {
            return new PaymentApproval(orderSheet);
        }

        private InternalProduct GetInternalProductBy(IFruit product)
        {
            return product as InternalProduct;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return products.GetEnumerator();
        }

        private class InternalProduct : IFruit
        {
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }

            public void Work()
            {
                // inventory.Insert(xxx);
                // bufferItem.Affect(character);
                // item.Use(target);
                Console.WriteLine($"    {Name}({Description})를 먹었다.");
            }

            public override string ToString()
            {
                return Description;
            }
        }

        private class PaymentApproval : IPaymentApproval
        {
            public PaymentApproval(OrderSheet<IFruit> orderSheet)
            {
                OrderSheet = orderSheet;
            }

            public OrderSheet<IFruit> OrderSheet { get; set; }

            public bool Approve(IBankAccount paymentAccount)
            {
                if (!paymentAccount.Withdraw(OrderSheet.TotalPrice))
                {
                    return false;
                }
                var shop = OrderSheet.Shop as FruitShop;
                foreach (var lineItem in OrderSheet)
                {
                    var internalProduct = shop.GetInternalProductBy(lineItem.Product);
                    for (int i = 0; i < lineItem.Quantity; i++)
                    {
                        internalProduct.Work();
                    }
                }

                return true;
            }
        }
    }
}
