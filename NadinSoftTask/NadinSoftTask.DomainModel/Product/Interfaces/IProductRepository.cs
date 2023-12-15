namespace NadinSoftTask.DomainModel.Product.Interfaces
{
    /// <summary>
    /// ریپوزیتوری محصول
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// دریافت محصول با شناسه محصول
        /// </summary>
        /// <param name="id"> شناسه محصول</param>
        /// <returns></returns>
        Task<Product?> GetById(Guid id);

        /// <summary>
        /// افزودن محصول
        /// </summary>
        void Add(Product product);

        /// <summary>
        /// حذف محصول
        /// </summary>
        void Delete(Guid id);

        IQueryable<Product> AsQuery();
    }
}
