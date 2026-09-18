Console Output
Status          : True
Transaction Id  : 8b4ef90b-f70e-40f8-98a0-a73fd5d68a2f
Message         : UPI payment of 2500 processed successfully.
Enterprise Flow
Client
   │
   ▼
CheckoutService
   │
   ▼
PaymentProcessorFactory
   │
   ▼
IPaymentProcessor
   │
   ├────────► CreditCardPaymentProcessor
   ├────────► UpiPaymentProcessor
   ├────────► WalletPaymentProcessor
   └────────► NetBankingPaymentProcessor
   │
   ▼
ProcessPayment()
   │
   ▼
PaymentResponse

