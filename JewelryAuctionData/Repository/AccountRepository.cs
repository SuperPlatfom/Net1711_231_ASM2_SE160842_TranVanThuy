using BadmintonReservationData.Base;
using JewelryAuctionData.Entity;
using Microsoft.EntityFrameworkCore;

namespace JewelryAuctionData.Repository;

public class AccountRepository: GenericRepository<Account>
{
    public AccountRepository(UnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Account?> LoginAsync(string username, string password)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));
    }
}