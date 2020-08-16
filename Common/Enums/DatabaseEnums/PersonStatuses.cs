using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum PersonStatuses : byte
    {
        [EnumDescription("Actived")]
        ACTIVE = 1,
        [EnumDescription("Blocked")]
        BLOCKED
    }
}
