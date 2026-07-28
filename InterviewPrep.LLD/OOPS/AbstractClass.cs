namespace InterviewPrep.LLD.OOPS
{
    //=========================================================
    // Interface
    //=========================================================
    public interface INotification
    {
        void SendNotification();
    }

    //=========================================================
    // Abstract Base Class
    //=========================================================
    public abstract class Payment : INotification
    {
        //-----------------------------------------------------
        // Fields
        //-----------------------------------------------------

        private readonly Guid _paymentId;

        //-----------------------------------------------------
        // Properties
        //-----------------------------------------------------

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedOn { get; }

        //-----------------------------------------------------
        // Static Property
        //-----------------------------------------------------

        public static int TotalPayments { get; private set; }

        //-----------------------------------------------------
        // Event
        //-----------------------------------------------------

        public event Action<string> PaymentCompleted;

        //-----------------------------------------------------
        // Constructor
        //-----------------------------------------------------

        protected Payment(decimal amount, string currency)
        {
            _paymentId = Guid.NewGuid();
            Amount = amount;
            Currency = currency;
            CreatedOn = DateTime.Now;

            TotalPayments++;
        }

        //-----------------------------------------------------
        // Concrete Method
        //-----------------------------------------------------

        public void Validate()
        {
            if (Amount <= 0)
                throw new Exception("Invalid Amount");

            Console.WriteLine("Validation Successful");
        }

        //-----------------------------------------------------
        // Concrete Method
        //-----------------------------------------------------

        public void GenerateReceipt()
        {
            Console.WriteLine("------------- Receipt -------------");
            Console.WriteLine($"Payment Id : {_paymentId}");
            Console.WriteLine($"Amount     : {Amount}");
            Console.WriteLine($"Currency   : {Currency}");
            Console.WriteLine($"Date       : {CreatedOn}");
            Console.WriteLine("-----------------------------------");
        }

        //-----------------------------------------------------
        // Protected Method
        //-----------------------------------------------------

        protected void RaisePaymentCompleted(string message)
        {
            PaymentCompleted?.Invoke(message);
        }

        //-----------------------------------------------------
        // Static Method
        //-----------------------------------------------------

        public static void ShowCompanyPolicy()
        {
            Console.WriteLine("Payments are encrypted.");
        }

        //-----------------------------------------------------
        // Abstract Methods
        //-----------------------------------------------------

        public abstract void ProcessPayment();

        public abstract void Refund();

        //-----------------------------------------------------
        // Interface Method
        //-----------------------------------------------------

        public virtual void SendNotification()
        {
            Console.WriteLine("Generic Payment Notification");
        }

        //-----------------------------------------------------
        // Nested Class
        //-----------------------------------------------------

        public class AuditLog
        {
            public void Save()
            {
                Console.WriteLine("Audit Log Saved");
            }
        }
    }

    //=========================================================
    // Child Class
    //=========================================================

    public class CreditCardPayment : Payment
    {
        public string CardNumber { get; set; }

        public CreditCardPayment(decimal amount,
                                 string currency,
                                 string cardNumber)
            : base(amount, currency)
        {
            CardNumber = cardNumber;
        }

        public override void ProcessPayment()
        {
            Console.WriteLine("Credit Card Payment Processed");

            RaisePaymentCompleted("Credit Card Payment Success");
        }

        public override void Refund()
        {
            Console.WriteLine("Refund to Credit Card");
        }

        public override void SendNotification()
        {
            Console.WriteLine("SMS Sent to Card Holder");
        }
    }

    //=========================================================
    // Another Child Class
    //=========================================================

    public class UpiPayment : Payment
    {
        public string UpiId { get; set; }

        public UpiPayment(decimal amount,
                          string currency,
                          string upiId)
            : base(amount, currency)
        {
            UpiId = upiId;
        }

        public override void ProcessPayment()
        {
            Console.WriteLine("UPI Payment Processed");

            RaisePaymentCompleted("UPI Payment Success");
        }

        public override void Refund()
        {
            Console.WriteLine("Refund to UPI");
        }
    }
}
