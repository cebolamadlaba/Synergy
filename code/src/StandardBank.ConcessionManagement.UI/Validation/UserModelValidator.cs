using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface;
namespace StandardBank.ConcessionManagement.UI.Validation
{
    public class UserModelValidator : AbstractValidator<User>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.ANumber).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.CentreId).NotEmpty();
        }
    }
}
