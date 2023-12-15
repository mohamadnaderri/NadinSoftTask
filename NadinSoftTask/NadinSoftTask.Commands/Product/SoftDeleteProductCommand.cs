using MediatR;
using NadinSoftTask.Infrastructure;
using System.Text.Json.Serialization;

namespace NadinSoftTask.Commands.Product
{
    /// <summary>
    /// فرمان حذف منطقی محصول
    /// </summary>
    public class SoftDeleteProductCommand : CommandBase, IRequest<bool>
    {
        [JsonIgnore]
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public Guid Id { get; set; }

        public override void Validate() { }
    }
}
