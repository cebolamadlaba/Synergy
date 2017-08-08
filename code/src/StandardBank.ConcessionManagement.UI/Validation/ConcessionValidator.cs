using FluentValidation;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.UI.Validation
{
    public class ConcessionValidator: AbstractValidator<Concession>
    {
        public ConcessionValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.ConcessionType).NotEmpty();
            RuleFor(x => x.RiskGroupNumber).NotEmpty();
            RuleFor(x => x.SmtDealNumber).NotEmpty();
            RuleFor(x => x.Motivation).NotEmpty();
           
        }
    }
}
