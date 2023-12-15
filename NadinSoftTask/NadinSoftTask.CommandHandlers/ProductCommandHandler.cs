using AutoMapper;
using MediatR;
using NadinSoftTask.Commands.Product;
using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.DomainModel.Product.Interfaces;
using NadinSoftTask.Infrastructure;

namespace NadinSoftTask.CommandHandlers
{
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, bool>,
        IRequestHandler<UpdateProductCommand, bool>,
        IRequestHandler<DeleteProductCommand, bool>,
        IRequestHandler<SoftDeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command);
            ThrowExceptionIfexistProductWithProduceDateAndManufacturerEmail(command.ProduceDate, command.ManufacturerEmail);
            _productRepository.Add(product);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) != 0;
        }

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await GetProduct(command.Id);
            ThrowExceptionIfexistProductWithProduceDateAndManufacturerEmail(command.ProduceDate, command.ManufacturerEmail, product.Id);
            if (product.Creator.OperatorId != command.CommandSender?.UserId)
                throw new Exception("فقط کاربری که محصول را ایجاد کرده امکان ویرایش محصول را دارد.");

            product.Update(command.Name, command.ProduceDate, command.ManufacturerPhoneNumber, command.ManufacturerEmail, command.IsAvailable);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) != 0;
        }

        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await GetProduct(command.Id);
            if (product.Creator.OperatorId != command.CommandSender?.UserId)
                throw new Exception("فقط کاربری که محصول را ایجاد کرده امکان حذف محصول را دارد.");

            _productRepository.Delete(product.Id);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) != 0;
        }

        public async Task<bool> Handle(SoftDeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await GetProduct(command.Id);
            if (product.Creator.OperatorId != command.CommandSender?.UserId)
                throw new Exception("فقط کاربری که محصول را ایجاد کرده امکان حذف محصول را دارد.");
            product.SoftDelete();
            return await _unitOfWork.SaveChangesAsync(cancellationToken) != 0;
        }

        #region Private Methods
        private void ThrowExceptionIfexistProductWithProduceDateAndManufacturerEmail(DateTime produceDate, string manufacturerEmail, Guid? productId = null)
        {
            var productQuery = _productRepository.AsQuery().Where(q => q.ProduceDate == produceDate && q.ManufacturerEmail == manufacturerEmail);
            if (productId is not null)
                productQuery = productQuery.Where(q => q.Id != productId);

            if (productQuery.Any())
                throw new Exception("محصولی با تاریخ تولید و ایمیل وارد شده موجود می باشد.");
        }

        private async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
                throw new Exception("محصول یافت نشد");
            return product;
        }
        #endregion
    }
}