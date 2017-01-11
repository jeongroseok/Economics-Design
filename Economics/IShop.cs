using System.Collections.Generic;

namespace Economics
{
    public interface IShop<TProduct> : IEnumerable<TProduct> where TProduct : IProduct
    {
        TProduct this[int index] { get; }

        IPaymentApproval TryOrder(OrderSheet<TProduct> orderSheet);
    }
}
