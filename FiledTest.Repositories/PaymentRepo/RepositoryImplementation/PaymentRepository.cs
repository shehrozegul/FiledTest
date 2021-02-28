using FiledTest.Data;
using FiledTest.Models.Models;

using FiledTest.Repositories.PaymentRepo.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiledTest.Repositories.PaymentRepo.RepositoryImplementation
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext dbContext) : base(dbContext)
        {

        }
        public override async Task<Payment> GetById(long id)
        {
            return await _dbContext.Set<Payment>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PaymentId == id);
        }
    }
}
