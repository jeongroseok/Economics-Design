namespace Economics
{
    public interface IBankAccount
    {
        decimal Balance { get; }

        bool Deposit(decimal amount);
        bool Withdraw(decimal amount);
    }
}