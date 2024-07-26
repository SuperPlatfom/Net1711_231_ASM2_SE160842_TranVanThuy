using AutoMapper;
using JewelryAuctionBusiness.Dto;
using JewelryAuctionData;
using JewelryAuctionData.Enum;

namespace JewelryAuctionBusiness;

public class AuctionResultBusiness
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuctionResultBusiness(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task UpdateExpiredAuctionsAsync()
    {
        var auctionSections = await _unitOfWork.AuctionSectionRepository.GetAllAsync().ConfigureAwait(false);
        var expiredAuctionSections = auctionSections.Where(x => x.EndTime <= DateTime.Now && x.Status != AuctionSessionEnum.Close.ToString()).ToList();

        if (expiredAuctionSections.Any())
        {
            await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                foreach (var auctionSection in expiredAuctionSections)
                {
                    auctionSection.Status = AuctionSessionEnum.Close.ToString();
                    _unitOfWork.AuctionSectionRepository.Update(auctionSection);
                }

                await _unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
    public async Task<IBusinessResult> GetAllAuctionResultsAsync()
    {
        try
        {
            var auctionResults = await _unitOfWork.AuctionResultRepository.GetAllAsync().ConfigureAwait(false);
            var result = _mapper.Map<List<AuctionResultDto>>(auctionResults);

            return new BusinessResult(200, "Successfully retrieved all auction results.", result);
        }
        catch (Exception ex)
        {
            return new BusinessResult(500, "An error occurred while retrieving auction results.");
        }
    }

    public async Task<IBusinessResult> GetAuctionResultByIdAsync(int id)
    {
        try
        {
            var auctionResult = await _unitOfWork.AuctionResultRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (auctionResult == null)
            {
                return new BusinessResult(404, "Auction result not found.");
            }

            var result = _mapper.Map<AuctionResultDto>(auctionResult);
            return new BusinessResult(200, "Successfully retrieved auction result.", result);
        }
        catch (Exception ex)
        {
            return new BusinessResult(500, "An error occurred while retrieving the auction result.");
        }
    }
}