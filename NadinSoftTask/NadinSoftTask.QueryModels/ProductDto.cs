namespace NadinSoftTask.QueryModels
{
    public class ProductDto
    {
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreationDate { get; set; }

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
    }
}
