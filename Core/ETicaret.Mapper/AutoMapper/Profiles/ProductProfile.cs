using AutoMapper;
using ETicaret.Application.Features.Products.Commands.CreateProduct;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Mapper.AutoMapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommandRequest, ProductEntity>();
        }
    }
}
