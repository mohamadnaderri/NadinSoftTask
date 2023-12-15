using FluentValidation;
using MediatR;
using NadinSoftTask.Commands.Extensions;
using NadinSoftTask.Infrastructure;
using System.Text.Json.Serialization;

namespace NadinSoftTask.Commands.Product
{
    /// <summary>
    /// فرمان ویرایش محصول
    /// </summary>
    public class UpdateProductCommand : CommandBase, IRequest<bool>
    {
        [JsonIgnore]
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public Guid Id { get; set; }

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

        public override void Validate() => new UpdateProductCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("نام محصول را وارد نمایید.");
            RuleFor(c => c.ManufacturerPhoneNumber).NotEmpty().WithMessage("تلفن سازنده محصول را وارد نمایید.");
            RuleFor(c => c.ManufacturerEmail).NotEmpty().WithMessage("ایمیل سازنده محصول را وارد نمایید.");
        }
    }
}