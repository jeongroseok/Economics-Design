namespace Economics
{
    public interface IPaymentApproval
    {
        bool Approve(IBankAccount paymentAccount);
    }
}
