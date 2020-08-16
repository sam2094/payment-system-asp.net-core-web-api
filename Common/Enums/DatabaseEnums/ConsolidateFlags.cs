using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum ConsolidateFlags : byte
    {
        [EnumDescription("Getting List of payments with From and To Dates parameters")]
        LIST_OF_PAYMENTS = 1,
        [EnumDescription("Getting Sum of payments with From and To Dates parameters")]
        PAYMENTS_SUM = 2
    }
}
