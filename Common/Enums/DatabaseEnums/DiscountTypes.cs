using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum DiscountTypes : byte
    {
        [EnumDescription("Percent")]
        PERCENT = 1,
        [EnumDescription("Amount")]
        AMOUNT
    }
}
