using Common.Resources;
using FluentValidation;
using Models.Dtos.PaymentDtos;
using System;
using System.Collections.Generic;

namespace Models.Parameters.Payments
{
    public class ConsolidateInput
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public byte CurrencyId { get; set; }
        public byte FlagId { get; set; }
    }

    public class ConsolidateOutput
    {
        public List<PaymentDto> Payments { get; set; }
        public decimal PaymentsSum { get; set; }
        public long PaymentsCount{ get; set; }
    }

    public class ConsolidateInputValidator : AbstractValidator<ConsolidateInput>
    {
        public ConsolidateInputValidator()
        {
            RuleFor(t => t.DateFrom).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.DATE_FROM));
            RuleFor(t => t.DateTo).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.DATE_TO));
            RuleFor(t => t.CurrencyId).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.CURRENCY_ID));
        }
    }
}
