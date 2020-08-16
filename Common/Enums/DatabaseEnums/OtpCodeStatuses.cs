using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum OtpCodeStatuses : byte
    {
        [EnumDescription("Active")]
        ACTIVE = 1,
        [EnumDescription("Used")]
        USED
    }
}
