using System.Linq;
using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.UI.Validation
{
    public class LendingConcessionValidator : AbstractValidator<LendingConcession>
    {
        public LendingConcessionValidator()
        {
            RuleFor(x => x.Concession.ConcessionType).NotEmpty();
            RuleFor(x => x.Concession.RiskGroupId).NotEmpty();
            RuleFor(x => x.Concession.SmtDealNumber).NotEmpty();
            RuleFor(x => x.Concession.Motivation).NotEmpty();
            RuleFor(x => x.LendingConcessionDetails).NotEmpty();
            RuleFor(x => x.LendingConcessionDetails.First()).NotEmpty();
            RuleFor(x => x.LendingConcessionDetails.First().ProductTypeId).NotEmpty();
            RuleFor(x => x.LendingConcessionDetails.First().LegalEntityId).NotNull();
        }
    }
}
