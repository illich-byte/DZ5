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
<<<<<<< HEAD
            .ForMember(x => x.Image, opt => opt.Ignore()); 

        CreateMap<CountryUpdateModel, CountryEntity>()
            .ForMember(x => x.Image, opt => opt.Ignore());

        CreateMap<CityCreateModel, CityEntity>();

        CreateMap<CityEntity, CityItemModel>()
            .ForMember(dest => dest.CountryName,
                       opt => opt.MapFrom(src => src.Country.Name));
    }
}
=======
            .ForMember(x=>x.Image, opt=>opt.Ignore());

        CreateMap<CountryUpdateModel, CountryEntity>()
            .ForMember(x => x.Image, opt => opt.Ignore());
    }
}
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
