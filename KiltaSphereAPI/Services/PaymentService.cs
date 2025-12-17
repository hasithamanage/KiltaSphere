using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetMemberPaymentsAsync(int memberId)
        {
            return await _paymentRepository.GetPaymentsByMemberIdAsync(memberId);
        }

        public async Task<Payment> CreateInitialFeeAsync(int memberId)
        {
            var initialFee = new Payment
            {
                MemberId = memberId,
                Amount = 50.00m, // Standard Finnish association fee example
                DueDate = DateTime.UtcNow.AddDays(14), // 14 days to pay
                PaymentStatus = "Pending",
                ReferenceNumber = $"REF-{DateTime.UtcNow.Ticks}" // Simple unique reference
            };

            await _paymentRepository.AddPaymentAsync(initialFee);
            await _paymentRepository.SaveChangesAsync();
            return initialFee;
        }

        public async Task<bool> ProcessPaymentAsync(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null) return false;

            payment.PaymentStatus = "Paid";
            payment.PaidDate = DateTime.UtcNow;

            return await _paymentRepository.SaveChangesAsync();
        }
    }
}