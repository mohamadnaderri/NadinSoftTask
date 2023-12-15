﻿namespace NadinSoftTask.Infrastructure
{
    public class UserInfo
    {
        public Guid TokenId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public string PersonType { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime TokenExpireAt { get; set; }

        public DateTime RefreshExpireAt { get; set; }
    }
}
