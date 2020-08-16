using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum PaymentStates : byte
    {
        [EnumDescription("Payed out")]
        PAYED = 1,
        [EnumDescription("Payment was Canceled")]
        CANCELED = 2,
        [EnumDescription("Money was Reversed back")]
        REVERSED = 3,
        [EnumDescription("Policy Blocked")]
        BLOCKED = 4,
        [EnumDescription("Payment was Rejected")]
        REJECTED = 5,
        [EnumDescription("Payment is in Processing")]
        PENDING = 6,
    }
}
