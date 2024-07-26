using BadmintonReservationData.Base;
using JewelryAuctionData.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class AuctionResultRepository : GenericRepository<AuctionResult>
    {
        public AuctionResultRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<AuctionResult>> GetAllAsync()
        {
            return await _dbSet
                .Include(ar => ar.Bidder)
                    .ThenInclude(b => b!.Customer)
                .Include(ar => ar.Auction)
                    .ThenInclude(a => a!.Jewelry)
                .ToListAsync();
        }

        public async Task<AuctionResult?> GetByIdAsync(int key)
        {
            return await _dbSet
                .Include(ar => ar.Bidder)
                    .ThenInclude(b => b!.Customer)
                .Include(ar => ar.Auction)
                    .ThenInclude(a => a!.Jewelry)
                .FirstOrDefaultAsync(ar => ar.AuctionResultId == key);
        }
    }
}
