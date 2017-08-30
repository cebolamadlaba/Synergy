using System.Linq;
using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

namespace StandardBank.ConcessionManagement.UI.Validation
{
    /// <summary>
    /// Cash concession validator
    /// </summary>
    /// <seealso cref="CashConcession" />
    public class CashConcessionValidator : AbstractValidator<CashConcession>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CashConcessionValidator"/> class.
        /// </summary>
        public CashConcessionValidator()
        {
            RuleFor(x => x.Concession.ConcessionType).NotEmpty();
            RuleFor(x => x.Concession.RiskGroupId).NotEmpty();
            RuleFor(x => x.Concession.SmtDealNumber).NotEmpty();
            RuleFor(x => x.Concession.Motivation).NotEmpty();
            RuleFor(x => x.CashConcessionDetails).NotEmpty();
            RuleFor(x => x.CashConcessionDetails.First()).NotEmpty();
            RuleFor(x => x.CashConcessionDetails.First().TableNumberId).NotEmpty();
            RuleFor(x => x.CashConcessionDetails.First().LegalEntityId).NotEmpty();
        }
    }
}
