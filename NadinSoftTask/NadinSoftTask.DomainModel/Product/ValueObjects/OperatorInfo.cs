namespace NadinSoftTask.DomainModel.Product.ValueObjects
{
    public record OperatorInfo
    {
        public OperatorInfo(Guid operatorId, string name)
        {
            ThrowExceptionIfNameIsNullOrEmpty(name);
            OperatorId = operatorId;
            Name = name;
        }

        /// <summary>
        /// شناسه شخص
        /// </summary>
        public Guid OperatorId { get; private set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; private set; }

        private void ThrowExceptionIfNameIsNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("نام نمی تواند خالی باشد.");
        }

        /// FOR ORM
        private OperatorInfo() { }
    }
}
