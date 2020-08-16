using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum OperationPriceTypes : byte
    {
        [EnumDescription("Sign")]
        SIGN = 1,
        [EnumDescription("Certificate")]
        CERTIFICATE
    }
}
