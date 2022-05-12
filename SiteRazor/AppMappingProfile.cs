using AutoMapper;
using DataTransferObject;
using Entities;

namespace SiteRazor;

public class AppMappingProfileForRazor:Profile
{
    public AppMappingProfileForRazor()
    {
        CreateMap<BrandDto, BrandModel>()
            .ReverseMap();
        CreateMap<Brand, BrandDto>()
            .ReverseMap();

        CreateMap<BodyTypeDto, BodyTypeModel>()
            .ReverseMap();
        CreateMap<BodyType, BodyTypeDto>()
            .ReverseMap();  
        
        CreateMap<CarDto, CarModel>()
            .ReverseMap();
        CreateMap<Car, CarDto>()
            .ReverseMap();
    }
}