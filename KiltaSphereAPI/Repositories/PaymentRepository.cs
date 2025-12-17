using KiltaSphereAPI.Data;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KiltaSphereAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByMemberIdAsync(int memberId)
        {
            return await _context.Payments
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.DueDate)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }
    }
}