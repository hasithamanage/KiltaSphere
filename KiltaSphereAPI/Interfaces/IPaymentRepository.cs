using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByMemberIdAsync(int memberId);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task AddPaymentAsync(Payment payment);
        Task<bool> SaveChangesAsync();
    }
}