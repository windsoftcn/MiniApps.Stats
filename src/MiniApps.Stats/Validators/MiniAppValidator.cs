using FluentValidation;
using MiniApps.Stats.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Validators
{
    public class MiniAppValidator : AbstractValidator<MiniApp>
    {
        public MiniAppValidator()
        {
            RuleFor(x => x.AppId).NotEmpty();
            RuleFor(x => x.AppName).NotEmpty();            
        }
    }
}
