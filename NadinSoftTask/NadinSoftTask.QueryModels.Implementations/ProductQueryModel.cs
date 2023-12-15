using MediatR;

namespace NadinSoftTask.QueryModels.Implementations
{
    /// <summary>
    /// دریافت اطلاعات محصول
    /// </summary>
    /// <param name="Id">شناسه محصول</param>
    public record GetProductQueryModel(Guid Id) : IRequest<ProductDto>;

    /// <summary>
    /// دریافت لیست محصولات
    /// </summary>
    /// <param name="OperatorName">کاربرایجاد کننده</param>
    public record GetAllProductQueryModel(string? OperatorName) : IRequest<List<ProductDto>>;
}
