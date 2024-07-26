using JewelryAuctionData;
using JewelryAuctionData.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JewelryAuctionWebAPI.BackgroundService
{
    public class AuctionStatusUpdater : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AuctionStatusUpdater> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Kiểm tra mỗi 5 phút

        public AuctionStatusUpdater(IServiceProvider serviceProvider, ILogger<AuctionStatusUpdater> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateExpiredAuctions();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating auction statuses.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task UpdateExpiredAuctions()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

                var auctionSections = await unitOfWork.AuctionSectionRepository.GetAllAsync();
                var expiredAuctionSections = auctionSections.Where(x => x.EndTime <= DateTime.Now && x.Status != AuctionSessionEnum.Close.ToString()).ToList();

                if (expiredAuctionSections.Any())
                {
                    await unitOfWork.BeginTransactionAsync();

                    try
                    {
                        foreach (var auctionSection in expiredAuctionSections)
                        {
                            auctionSection.Status = AuctionSessionEnum.Close.ToString();
                            unitOfWork.AuctionSectionRepository.Update(auctionSection);
                        }

                        await unitOfWork.CommitTransactionAsync();
                        _logger.LogInformation("Expired auction sections updated successfully.");
                    }
                    catch
                    {
                        unitOfWork.RollbackTransactionAsync();
                        _logger.LogError("Failed to update expired auction sections. Transaction rolled back.");
                        throw;
                    }
                }
            }
        }
    }
}
