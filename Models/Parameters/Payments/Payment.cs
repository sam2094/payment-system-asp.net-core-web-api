using Common.Resources;
using FluentValidation;
using Models.Dtos.PaymentDtos;
using System;

namespace Models.Parameters.Payments
{
    public class PaymentInput
    {
        public string ExternalTransId { get; set; }
        public long InvoiceId { get; set; }
        public decimal Sum { get; set; }
        public byte CurrencyId { get; set; }
        public DateTime ExternalDate { get; set; }
        public string ExternalPoint { get; set; } 
        public string CheckNumb { get; set; } 
        public string Description { get; set; }
    }

    public class PaymentOutput
    {
        public PaymentDto Payment { get; set; }
    }

    public class PaymentInputValidator : AbstractValidator<PaymentInput>
    {
        public PaymentInputValidator()
        {
            RuleFor(t => t.ExternalTransId).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.EXTERNAL_TRANSACTION_ID));
            RuleFor(t => t.InvoiceId).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.INVOICE_ID));
            RuleFor(t => t.Sum).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.SUM));
            RuleFor(t => t.CurrencyId).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.CURRENCY_ID));
            RuleFor(t => t.ExternalDate).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.EXTERNAL_DATE));
        }
    }
}
