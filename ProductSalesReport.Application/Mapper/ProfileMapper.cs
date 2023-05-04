using AutoMapper;

namespace ProductSalesReport.Application.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Domain.Entities.Rate, Dtos.RateDto>()
                .ForMember(d => d.Rate, opt => opt.MapFrom(s => s.RateValue));

            CreateMap<Domain.Entities.Transaction, Dtos.TransactionDto>();
        }
    }
}
