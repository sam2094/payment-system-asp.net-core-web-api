using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum GenderTypes : byte
    {
        [EnumDescription("Female")]
        FEMALE = 1,
        [EnumDescription("Male")]
        MALE
    }
}
