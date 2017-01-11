namespace Economics.Tests
{
    class TestCustomer : ICustomer
    {
        protected IBankAccount BankAccount { get; private set; }

        public decimal Balance { get { return BankAccount.Balance; } }

        public TestCustomer()
        {
            //BankAccount = RubyBank.CreateAccount(xxx);
            //BankAccount = CoinBank.CreateAccount(xxx);
            //BankAccount = CrystalBank.CreateAccount(xxx);
            BankAccount = new TestBankAccount();
        }

        public bool Approve(IPaymentApproval approval)
        {
            return approval.Approve(BankAccount);
        }
    }
}
