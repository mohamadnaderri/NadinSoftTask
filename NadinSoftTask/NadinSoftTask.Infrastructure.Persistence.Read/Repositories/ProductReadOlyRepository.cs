﻿using Microsoft.EntityFrameworkCore;
using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.DomainModel.Product.Interfaces;
using NadinSoftTask.Infrastructure.Persistence.Context;

namespace NadinSoftTask.Infrastructure.Persistence.Read.Repositories
{
    public class ProductReadOlyRepository : IProductReadOnlyRepository
    {
        protected readonly DatabaseContext Context;

        public ProductReadOlyRepository(DatabaseContext context)
        {
            this.Context = context;
        }

        /// <inheritdoc/>
        public Task<List<Product>?> GetAll(string? operatorName)
        {
            var products = Context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(operatorName))
                products = products.Where(q => q.Creator.Name.Contains(operatorName));

            return products.ToListAsync();
        }

        /// <inheritdoc/>
        public Task<Product?> GetById(Guid id)
        {
            return Context.Products.FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}