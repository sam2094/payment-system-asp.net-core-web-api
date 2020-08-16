using Common.Resources;
using FluentValidation;
using System;

namespace Models.Parameters.Payments
{
    public class CancellPaymentInput
    {
        public long PaymentId { get; set; }
        public decimal Sum { get; set; }
        public string TransId { get; set; }
        public string Description { get; set; }
        public DateTime ExternalDate { get; set; }
    }

    public class CancellPaymentValidator : AbstractValidator<CancellPaymentInput>
    {
        public CancellPaymentValidator()
        {
            RuleFor(t => t.Sum).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.SUM));
            RuleFor(t => t.Description).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.DESCRIPTION));
        }
    }
}
