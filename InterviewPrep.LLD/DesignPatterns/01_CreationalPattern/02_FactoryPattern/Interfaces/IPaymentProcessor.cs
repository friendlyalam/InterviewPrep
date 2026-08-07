using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Interfaces
{
    public interface IPaymentProcessor
    {
        PaymentResponse ProcessPayment(
            PaymentRequests requests);
    }
}

//Every payment gateway eventually performs one task.

//Process Payment

//Internally,

//Credit Card may

//Validate card
//Contact bank
//Deduct amount

//UPI may

//Validate UPI ID
//Contact PSP
//Complete transaction

//Wallet may

//Check wallet balance
//Deduct money

//Different implementations.

//Same contract.