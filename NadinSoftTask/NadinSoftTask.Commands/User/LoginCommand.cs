using FluentValidation;
using NadinSoftTask.Commands.Extensions;
using NadinSoftTask.Infrastructure;

namespace NadinSoftTask.Commands.User
{
    /// <summary>
    /// فرمان لاگین
    /// </summary>
    public class LoginCommand : CommandBase
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        public string Password { get; set; }

        public override void Validate()
        {
            new LoginCommandValidator().Validate(this).RaiseExceptionIfRequired();
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("نام کاربری را وارد نمایید.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("کلمه عبور را وارد نمایید.");
        }
    }
}
