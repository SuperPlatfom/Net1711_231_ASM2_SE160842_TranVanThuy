using AutoMapper;
using JewelryAuctionBusiness.Dto;
using JewelryAuctionData.Dto;
using JewelryAuctionData.Entity;

namespace JewelryAuctionBusiness.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map AuctionResult to AuctionResultDto and reverse
            CreateMap<AuctionResult, AuctionResultDto>()
                .ForMember(dest => dest.Auction, opt => opt.MapFrom(src => src.Auction))
                .ForMember(dest => dest.Bidder, opt => opt.MapFrom(src => src.Bidder))
                .ReverseMap();

            // Map AuctionSection to AuctionSectionDto and reverse
            CreateMap<AuctionSection, AuctionSectionDto>()
                .ForMember(dest => dest.JewelryDto, opt => opt.MapFrom(src => src.Jewelry))
                .ForMember(dest => dest.BidderDto, opt => opt.MapFrom(src => src.Bidder))
                .ReverseMap()
                .ForMember(dest => dest.Jewelry, opt => opt.MapFrom(src => src.JewelryDto))
                .ForMember(dest => dest.Bidder, opt => opt.MapFrom(src => src.BidderDto));

            // Map Bidder to BidderDto and reverse
            CreateMap<Bidder, BidderDto>()
                .ForMember(dest => dest.CustomerDto, opt => opt.MapFrom(src => src.Customer))
                .ReverseMap();

            // Map Customer to CustomerDto and reverse
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            // Map Jewelry to JewelryDTO and reverse
            CreateMap<Jewelry, JewelryDTO>().ReverseMap();

            // Map Customer to CustomerDTO and reverse
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            // Map Company to CompanyDTO and reverse
            CreateMap<Company, CompanyDTO>()
                .ReverseMap();
            CreateMap<AuctionSection, AuctionSectionUpdateDto>()
                .ReverseMap();
            // Map RequestAuction to RequestAuctionDTO and reverse
            CreateMap<RequestAuction, RequestAuctionDTO>()
                .ForMember(dest => dest.Jewelry, opt => opt.MapFrom(src => src.Jewelry))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ReverseMap();

            // Map RequestAuctionDetail to RequestAuctionDetailsDto and reverse
            CreateMap<RequestAuctionDetail, RequestAuctionDetailsDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(dest => dest.JewelryName, opt => opt.MapFrom(src => src.Jewelry.JewelryName))
                .ReverseMap();

            // Map Payment to PaymentDto and reverse
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.AuctionResultDto, opt => opt.MapFrom(src => src.AuctionResult))
                .ForMember(dest => dest.JewelryDto, opt => opt.MapFrom(src => src.Jewelry))
                .ForMember(dest => dest.CustomerDto, opt => opt.MapFrom(src => src.Customer))
                .ReverseMap();
        }
    }
}
