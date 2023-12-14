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
        Task<Product?> GetById(Guid id);

        /// <summary>
        /// دریافت لیست محصولات
        /// </summary>
        /// <param name="operatorId"> شناسه شخص ایجاد کننده محصول</param>
        /// <param name="operatorName"> نام شخص ایجاد کننده محصول</param>
        /// <returns></returns>
        Task<List<Product>?> GetAll(Guid? operatorId, string? operatorName);
    }
}