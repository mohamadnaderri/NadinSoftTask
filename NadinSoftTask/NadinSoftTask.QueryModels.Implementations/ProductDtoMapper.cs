using AutoMapper;
using NadinSoftTask.DomainModel.Product;

namespace NadinSoftTask.QueryModels.Implementations
{
    public class ProductDtoMapper : Profile
    {
        public ProductDtoMapper()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
