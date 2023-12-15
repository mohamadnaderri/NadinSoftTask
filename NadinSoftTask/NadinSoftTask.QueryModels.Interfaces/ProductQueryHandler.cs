using AutoMapper;
using MediatR;
using NadinSoftTask.DomainModel.Product.Interfaces;
using NadinSoftTask.QueryModels.Implementations;

namespace NadinSoftTask.QueryHandlers
{
    public class ProductQueryHandler :
    IRequestHandler<GetProductQueryModel, ProductDto>,
    IRequestHandler<GetAllProductQueryModel, List<ProductDto>>
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IProductReadOnlyRepository productReadOnlyRepository, IMapper mapper)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQueryModel request, CancellationToken cancellationToken)
        {
            var products = await _productReadOnlyRepository.GetAll(request.OperatorName);
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<ProductDto> Handle(GetProductQueryModel request, CancellationToken cancellationToken)
        {
            var product = await _productReadOnlyRepository.GetById(request.Id);
            if (product is null)
                throw new Exception("محصول یافت نشد");
            var result = _mapper.Map<ProductDto>(product);

            return result;
        }
    }
}
