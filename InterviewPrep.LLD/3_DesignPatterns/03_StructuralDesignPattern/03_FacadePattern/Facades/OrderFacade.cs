using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Facades
{
    public sealed class OrderFacade : IOrderFacade
    {
        private readonly IInventoryService _inventoryService;
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;
        private readonly IInvoiceService _invoiceService;
        private readonly INotificationService _notificationService;

        public OrderFacade(
            IInventoryService inventoryService,
            IPaymentService paymentService,
            IShippingService shippingService,
            IInvoiceService invoiceService,
            INotificationService notificationService)
        {
            _inventoryService = inventoryService;
            _paymentService = paymentService;
            _shippingService = shippingService;
            _invoiceService = invoiceService;
            _notificationService = notificationService;
        }

        public OrderResult PlaceOrder(OrderRequest request)
        {
            Console.WriteLine("===== Order Processing Started =====");

            if (!_inventoryService.IsAvailable(
                request.ProductId,
                request.Quantity))
            {
                return new OrderResult
                {
                    Success = false,
                    Message = "Product is out of stock."
                };
            }

            if (!_paymentService.ProcessPayment(request.Amount))
            {
                return new OrderResult
                {
                    Success = false,
                    Message = "Payment failed."
                };
            }

            string trackingNumber =
                _shippingService.CreateShipment(
                    request.DeliveryAddress);

            string invoiceNumber =
                _invoiceService.GenerateInvoice(
                    request.CustomerId);

            _notificationService.SendConfirmation(
                request.Email);

            Console.WriteLine("===== Order Processing Completed =====");

            return new OrderResult
            {
                Success = true,
                OrderNumber = $"ORD-{Guid.NewGuid():N}"[..12].ToUpper(),
                Message =
                    $"Invoice: {invoiceNumber}, Tracking: {trackingNumber}"
            };
        }
    }
}
