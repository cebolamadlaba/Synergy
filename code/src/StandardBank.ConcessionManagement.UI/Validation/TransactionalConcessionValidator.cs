using System.Linq;
using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.UI.Validation
{
    /// <summary>
    /// Transactional concession validator
    /// </summary>
    /// <seealso cref="TransactionalConcession" />
    public class TransactionalConcessionValidator : AbstractValidator<TransactionalConcession>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalConcessionValidator"/> class.
        /// </summary>
        public TransactionalConcessionValidator()
        {
            RuleFor(x => x.Concession.ConcessionType).NotEmpty();
            RuleFor(x => x.Concession).Must(a => a.RiskGroupId.HasValue || a.LegalEntityId.HasValue);
            RuleFor(x => x.Concession.Motivation).NotEmpty();
            RuleFor(x => x.TransactionalConcessionDetails).NotEmpty();
            RuleFor(x => x.TransactionalConcessionDetails.First()).NotEmpty();
            RuleFor(x => x.TransactionalConcessionDetails.First().TransactionTableNumberId).NotEmpty();
            RuleFor(x => x.TransactionalConcessionDetails.First().LegalEntityId).NotEmpty();
        }
    }
}
