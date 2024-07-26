using BadmintonReservationData.Base;
using JewelryAuctionData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JewelryAuctionData.Repository
{
    public class AuctionSectionRepository : GenericRepository<AuctionSection>
    {
        public AuctionSectionRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<AuctionSection>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.Bidder)
                .Include(x => x.Jewelry)
                .Include(x => x.RequestDetail)
                .ToListAsync();
        }
        public async Task<AuctionSection?> GetByIdAsync(int key)
        {
            return await _dbSet
                .Include(x => x.Bidder)
                .ThenInclude(x => x!.Customer)
                .Include(x => x.Jewelry)
                .Include(x => x.RequestDetail)
                .FirstOrDefaultAsync(c => c.AuctionId == key);
        }
    }
}
