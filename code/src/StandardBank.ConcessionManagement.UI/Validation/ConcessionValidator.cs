using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.UI.Validation
{
    public class ConcessionValidator: AbstractValidator<LendingConcession>
    {
        public ConcessionValidator()
        {
            RuleFor(x => x.Concession.Type).NotEmpty();
            RuleFor(x => x.Concession.ConcessionType).NotEmpty();
            RuleFor(x => x.Concession.RiskGroupNumber).NotEmpty();
            RuleFor(x => x.Concession.SmtDealNumber).NotEmpty();
            RuleFor(x => x.Concession.Motivation).NotEmpty();
           
        }
    }
}
