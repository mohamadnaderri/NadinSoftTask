using NadinSoftTask.DomainModel.Product.ValueObjects;
using NadinSoftTask.Infrastructure.Helpers;

namespace NadinSoftTask.DomainModel.Product
{
    /// <summary>
    /// مدل محصول
    /// </summary>
    public class Product
    {
        public Product(string name, DateTime produceDate, string manufacturePhoneNumber, string manufactureEmail, bool isAvailable, OperatorInfo creator)
        {
            ThrowExceptionIfNameIsNullOrEmpty(name);
            ThrowExceptionIfProduceDateIsInvalid(produceDate);
            ThrowExceptionIfManufacturerPhoneNumberIsInvalid(manufacturePhoneNumber);
            ThrowExceptionIfManufacturerEmailIsInvalid(manufactureEmail);
            ThrowExceptionIfIsAvailableIsInvalid(isAvailable);

            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.Name = name;
            this.ProduceDate = produceDate;
            this.ManufacturerPhoneNumber = manufacturePhoneNumber;
            this.ManufacturerEmail = manufactureEmail;
            this.IsAvailable = isAvailable;
            this.Creator = creator;
        }

        /// <summary>
        /// شناسه محصول
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// ایجاد کننده
        /// </summary>
        public OperatorInfo Creator { get; private set; }

        /// <summary>
        /// نام محصول
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// تاریخ تولید
        /// </summary>
        public DateTime ProduceDate { get; private set; }

        /// <summary>
        /// تلفن سازنده
        /// </summary>
        public string ManufacturerPhoneNumber { get; private set; }

        /// <summary>
        /// ایمیل سازنده
        /// </summary>
        public string ManufacturerEmail { get; private set; }

        /// <summary>
        /// آیا موجود است ؟
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// حذف منطقی شده؟
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// حذف منطقی محصول
        /// </summary>
        public void SoftDelete()
        {
            this.IsDeleted = true;
        }

        private void ThrowExceptionIfNameIsNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("نام محصول نمی تواند خالی باشد.");
        }

        private void ThrowExceptionIfProduceDateIsInvalid(DateTime produceDate)
        {
            if (produceDate >= DateTime.Now)
                throw new Exception("تاریخ تولید محصول معتبر نمی باشد.");
        }

        private void ThrowExceptionIfManufacturerPhoneNumberIsInvalid(string manufacturerPhoneNumber)
        {
            if (!manufacturerPhoneNumber.IsValidPhoneNumber())
                throw new Exception("شماره تلفن سازنده معتبر نمی باشد.");
        }

        private void ThrowExceptionIfManufacturerEmailIsInvalid(string manufacturerEmail)
        {
            if (!manufacturerEmail.IsValidEmail())
                throw new Exception("ایمیل سازنده معتبر نمی باشد.");
        }

        private void ThrowExceptionIfIsAvailableIsInvalid(bool isAvailable)
        {
            if (!isAvailable)
                throw new Exception("محصول بدون موجودی امکان اضافه شدن ندارد.");
        }

        /// FOR ORM
        private Product() { }
    }
}