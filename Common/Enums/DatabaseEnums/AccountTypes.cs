using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum AccountTypes : byte
    {
        [EnumDescription("Invoice account type")]
        INVOICE = 1,
        [EnumDescription("Payment Account Type")]
        PAYMENT = 2
    }
}
