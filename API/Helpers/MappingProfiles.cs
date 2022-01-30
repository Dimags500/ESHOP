using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.ProductBrand, y => y.MapFrom(s => s.ProductBrand.Name))
                .ForMember(x => x.ProductType, z => z.MapFrom(s => s.ProductType.Name))
                .ForMember(x => x.PictureUrl , o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
