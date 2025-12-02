using AutoMapper;
using Core.Models.Location;
using Domain.Entities.Location;

namespace Core.Mappers;

public class CountryMapper : Profile
{
    public CountryMapper()
    {
        CreateMap<CountryEntity, CountryItemModel>();

        CreateMap<CountryCreateModel, CountryEntity>()
            .ForMember(x => x.Image, opt => opt.Ignore()); 

        CreateMap<CountryUpdateModel, CountryEntity>()
            .ForMember(x => x.Image, opt => opt.Ignore());

        CreateMap<CityCreateModel, CityEntity>();

        CreateMap<CityEntity, CityItemModel>()
            .ForMember(dest => dest.CountryName,
                       opt => opt.MapFrom(src => src.Country.Name));
    

        CreateMap<CountryUpdateModel, CountryEntity>()
            .ForMember(x => x.Image, opt => opt.Ignore());
    }
}
