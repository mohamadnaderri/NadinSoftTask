using MediatR;
using NadinSoftTask.Infrastructure;
using System.Text.Json.Serialization;

namespace NadinSoftTask.Commands.Product
{
    /// <summary>
    /// فرمان حذف محصول
    /// </summary>
    public class DeleteProductCommand : CommandBase, IRequest<bool>
    {
        [JsonIgnore]
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public Guid Id { get; set; }

        public override void Validate() { }
    }
}
