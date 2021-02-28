
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
    public class PaymentStateRepository : GenericRepository<PaymentState>, IPaymentStateRepository
    {
        public PaymentStateRepository(PaymentContext dbContext) : base(dbContext)
        {

        }
        public override async Task<PaymentState> GetById(long id)
        {
            return await _dbContext.Set<PaymentState>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PaymentId == id);
        }
    }
}
