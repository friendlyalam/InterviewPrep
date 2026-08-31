namespace InterviewPrep.LLD.OOPS.Encapsulation
{
    public class BankAccount
    {
        //====================================================
        // Private Fields (Hidden Data)
        //====================================================

        private decimal _balance;

        //====================================================
        // Properties
        //====================================================

        public string AccountNumber { get; }

        public string AccountHolder { get; private set; }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        //====================================================
        // Constructor
        //====================================================

        public BankAccount(string accountNumber,
                           string accountHolder,
                           decimal openingBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Invalid Account Number");

            if (string.IsNullOrWhiteSpace(accountHolder))
                throw new ArgumentException("Invalid Account Holder");

            if (openingBalance < 0)
                throw new ArgumentException("Opening balance cannot be negative.");

            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            _balance = openingBalance;
        }

        //====================================================
        // Business Methods
        //====================================================

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            _balance += amount;

            Console.WriteLine($"£{amount} deposited successfully.");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            if (amount > _balance)
                throw new InvalidOperationException("Insufficient balance.");

            _balance -= amount;

            Console.WriteLine($"£{amount} withdrawn successfully.");
        }

        public void ChangeAccountHolder(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Invalid name.");

            AccountHolder = newName;
        }

        public void DisplayAccount()
        {
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Account No   : {AccountNumber}");
            Console.WriteLine($"Holder       : {AccountHolder}");
            Console.WriteLine($"Balance      : £{Balance}");

            Console.WriteLine("--------------------------------");
        }
    }
}

#region  Program explanation
//Product Company Design Thinking

//A senior interviewer may ask:

//Why is Balance read - only ?

//Because balance should never be changed directly.

//The only valid ways to modify it are:

//Deposit()

//Withdraw()

//Interest()

//Transfer()

//This ensures:

//Validation
//Auditability
//Business rules
//Data integrity
//Why Use Methods Instead of a Public Setter?

//Imagine this design:

//public decimal Balance { get; set; }

//Then anyone can do:

//account.Balance = -5000;
//account.Balance = 100000000;
//account.Balance = 0;

//There is no validation.

//Now consider this design:

//public decimal Balance
//{
//    get { return _balance; }
//}

//public void Deposit(decimal amount)
//{
//    // Validation
//}

//public void Withdraw(decimal amount)
//{
//    // Validation
//}

//Now every update is validated.

//This is real encapsulation.

//SOLID Principle Connection

//This design also supports the Single Responsibility Principle (SRP).

//The BankAccount class is responsible for:

//Maintaining account state.
//Ensuring account data is always valid.
//Protecting its own data.

//It does not handle:

//Email notifications
//Database persistence
//Logging

//Those responsibilities belong to separate classes or services.

//Product Company Interview Questions
//Q1.Why is _balance private?

//To prevent external code from modifying the balance without validation.

//Q2. Why doesn't Balance have a public setter?

//Because balance changes must always follow business rules such as deposits, withdrawals, transfers, or interest calculations.

//Q3. Why use methods instead of exposing the field?

//Methods allow:

//Validation
//Logging
//Auditing
//Security checks
//Business rules

//before changing the object's state.

//Q4. Is using properties alone enough for encapsulation?

//No.

//A property with a public setter like:

//public decimal Balance { get; set; }

//does not truly protect the object.

//Encapsulation is about controlling state changes, not simply wrapping a field in a property.

//Q5. How would you improve this class further in a real banking system?

//Some possible improvements include:

//Add transaction history instead of just changing the balance.
//Raise domain events after deposits or withdrawals.
//Make operations thread-safe if multiple requests can access the same account simultaneously.
//Separate persistence into a repository.
//Add authentication/authorisation before sensitive operations.
//Support transfers between accounts with transactional consistency.

//These kinds of answers show product-company interviewers that you think beyond syntax and understand real-world software design.

//⭐ Interview-Ready Definition

//Encapsulation is the object-oriented principle of protecting an object's internal state by keeping
//its data private and exposing only controlled operations that enforce business rules. This ensures data integrity, improves maintainability, and prevents invalid or unauthorized modifications.
#endregion
