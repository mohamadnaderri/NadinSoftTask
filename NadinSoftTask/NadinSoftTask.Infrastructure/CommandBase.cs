using System.Text.Json.Serialization;

namespace NadinSoftTask.Infrastructure
{
    public abstract class CommandBase
    {
        [JsonIgnore]
        public string? Token { get; set; }

        [JsonIgnore]
        public UserInfo? CommandSender { get; set; }

        public void SetCommandSenderInfo(UserInfo commandSender)
        {
            this.CommandSender = commandSender;
        }

        /// <inheritdoc />
        public abstract void Validate();
    }
}
