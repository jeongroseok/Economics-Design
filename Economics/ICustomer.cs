namespace Economics
{
    public interface ICustomer
    {
        bool Approve(IPaymentApproval approval);
    }
}
