namespace Economics
{
    public class TestBankAccount : IBankAccount
    {
        public decimal Balance { get; private set; } = 2000;

        public bool Deposit(decimal amount)
        {
            Balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            Balance -= amount;
            return true;
        }
    }
}