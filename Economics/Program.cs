using Economics.Tests;
using System;

namespace Economics
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCustomer customer = new TestCustomer();
            FruitShop shop = new FruitShop();

            DrawShop(shop);

            var orderSheet = new OrderSheet<IFruit>(customer, shop);
            orderSheet.Add(shop[0], 3);
            orderSheet.Add(shop[1]);
            orderSheet.Add(shop[1]);
            orderSheet.Add(shop[2]);
            DrawOrderSheet(orderSheet);


            DrawBalance(customer);
            var approval = shop.TryOrder(orderSheet);
            Console.WriteLine("--판매 승인--");
            customer.Approve(approval);

            DrawBalance(customer);
        }

        private static void DrawBalance(TestCustomer customer)
        {
            Console.WriteLine($"--잔액: {customer.Balance}--\n");
        }

        private static void DrawOrderSheet(OrderSheet<IFruit> orderSheet)
        {
            Console.WriteLine("--주문서 내용--");
            foreach (var line in orderSheet)
            {
                Console.WriteLine($"    {line.Product} {line.Quantity}개 있습니다.");
            }
            Console.WriteLine($"    총 가격: {orderSheet.TotalPrice}");
            Console.WriteLine();
        }

        private static void DrawShop(IShop<IFruit> shop)
        {
            Console.WriteLine("--판매 상품--");
            foreach (var fruit in shop)
            {
                Console.WriteLine($"    {fruit}");
            }
            Console.WriteLine();
        }
    }
}
