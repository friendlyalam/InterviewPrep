using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IInventoryService _inventoryService;

        private readonly IInvoiceService _invoiceService;

        private readonly IEmailService _emailService;

        private readonly IAuditService _auditService;

        public OrderService(
            IOrderRepository orderRepository,
            IInventoryService inventoryService,
            IInvoiceService invoiceService,
            IEmailService emailService,
            IAuditService auditService)
        {
            _orderRepository = orderRepository;

            _inventoryService = inventoryService;

            _invoiceService = invoiceService;

            _emailService = emailService;

            _auditService = auditService;
        }

        public void PlaceOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            Console.WriteLine("========== ORDER PROCESSING ==========");

            _orderRepository.Save(order);

            _inventoryService.UpdateStock(order);

            _invoiceService.GenerateInvoice(order);

            _emailService.SendConfirmation(order);

            _auditService.WriteLog(order);

            Console.WriteLine("Order Process Completed.");
        }
    }
}