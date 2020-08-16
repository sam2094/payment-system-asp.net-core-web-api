using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum CitizenTypes : byte
    {
        [EnumDescription("Azerbaijani")]
        AZERBAIJANI = 1,
        [EnumDescription("Permanent resident")]
        PERMANENT_RESIDENT,
        [EnumDescription("Temporary resident")]
        TEMPORARY_RESIDENT
    }
}
