using Microsoft.EntityFrameworkCore;
using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.DomainModel.Product.Interfaces;
using NadinSoftTask.Infrastructure.Persistence.Context;

namespace NadinSoftTask.Infrastructure.Persistence.Write.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly DatabaseContext Context;

        public ProductRepository(DatabaseContext context)
        {
            this.Context = context;
        }

        /// <inheritdoc/>
        public void Add(Product product)
        {
            Context.Products.Add(product);
        }

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            var product = Context.Products.FirstOrDefault(q => q.Id == id);
            if (product is null)
                throw new Exception("محصول یافت نشد.");

            Context.Products.Remove(product);
        }

        /// <inheritdoc/>
        public Task<Product?> GetById(Guid id)
        {
            return Context.Products.Where(q => !q.IsDeleted).FirstOrDefaultAsync(q => q.Id == id);
        }

        public IQueryable<Product> AsQuery()
        {
            return Context.Products.AsQueryable();
        }
    }
}
