using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetMemberPaymentsAsync(int memberId);
        Task<Payment> CreateInitialFeeAsync(int memberId);
        Task<bool> ProcessPaymentAsync(int paymentId);
    }
}