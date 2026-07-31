using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Processors;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Factories
{
    public static class PaymentProcessorFactory
    {
        public static IPaymentProcessor Create(PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CreditCard => new CreditCardPaymentProcessor(),

                PaymentMethod.Upi => new UpiPaymentProcessor(),

                PaymentMethod.Wallet => new WalletPaymentProcessor(),

                PaymentMethod.NetBanking => new NetBankingPaymentProcessor(),

                _ => throw new NotSupportedException("Payment method is not supported.")
            };
        }
    }
}

//This is the Factory Method. It decides which concrete object to create while returning the abstraction (IPaymentProcessor).