using Common.Resources;
using FluentValidation;
using Models.Dtos.PaymentDtos;

namespace Models.Parameters.Payments
{

    public class VerifyTokenInput
    {
        public string Token { get; set; }
    }

    public class VerifyTokenOutput
    {
        public VerifyTokenDto VerifyTokenResult { get; set; }
    }

    public class VerifyTokenInputValidator : AbstractValidator<VerifyTokenInput>
    {
        public VerifyTokenInputValidator()
        {
            RuleFor(t => t.Token).NotEmpty().WithMessage(x => string.Format(Resource.NOTEMPTY, Resource.TOKEN));
        }
    }
}
