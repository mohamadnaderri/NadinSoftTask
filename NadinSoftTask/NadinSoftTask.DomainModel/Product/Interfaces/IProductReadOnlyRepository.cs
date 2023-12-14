namespace NadinSoftTask.DomainModel.Product.Interfaces
{
    /// <summary>
    /// ریپوزیتوری محصول 
    /// فقط برای خواندن
    /// </summary>
    public interface IProductReadOnlyRepository
    {
        /// <summary>
        /// دریافت محصول با شناسه محصول
        /// </summary>
        /// <param name="id"> شناسه محصول</param>
        /// <returns></returns>
        Task<Product> GetById(Guid id);

        /// <summary>
        /// دریافت لیست محصولات
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAllProducts();
    }
}