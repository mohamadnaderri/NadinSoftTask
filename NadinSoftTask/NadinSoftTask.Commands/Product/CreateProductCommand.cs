using FluentValidation;
using MediatR;
using NadinSoftTask.Commands.Extensions;
using NadinSoftTask.Infrastructure;

namespace NadinSoftTask.Commands.Product
{
    /// <summary>
    /// فرمان ایجاد محصول
    /// </summary>
    public class CreateProductCommand : CommandBase, IRequest<bool>
    {
        /// <summary>
        /// نام محصول
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// تاریخ تولید
        /// </summary>
        public DateTime ProduceDate { get; set; }

        /// <summary>
        /// تلفن سازنده
        /// </summary>
        public string ManufacturerPhoneNumber { get; set; }

        /// <summary>
        /// ایمیل سازنده
        /// </summary>
        public string ManufacturerEmail { get; set; }

        /// <summary>
        /// آیا موجود است ؟
        /// </summary>
        public bool IsAvailable { get; set; }

        public override void Validate() => new CreateProductCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("نام محصول را وارد نمایید.");
            RuleFor(c => c.ManufacturerPhoneNumber).NotEmpty().WithMessage("تلفن سازنده محصول را وارد نمایید.");
            RuleFor(c => c.ManufacturerEmail).NotEmpty().WithMessage("ایمیل سازنده محصول را وارد نمایید.");
        }
    }
}