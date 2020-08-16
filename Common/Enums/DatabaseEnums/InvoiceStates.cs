using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum InvoiceStates : byte
    {
        [EnumDescription("Ready for payment")]
        ACTIVE = 1,
        [EnumDescription("Rejected by creator or  office")]
        DEACTIVE = 2,
        [EnumDescription("Paid out")]
        PAID = 3,
    }
}
