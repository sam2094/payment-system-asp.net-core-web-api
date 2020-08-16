using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum Roles : int
    {
        [EnumDescription("Super admin")]
        SUPER_ADMIN = 1,
        [EnumDescription("Admin")]
        ADMIN = 2,
        [EnumDescription("Manager")]
        MANAGER = 3,
        [EnumDescription("Operator")]
        OPERATOR = 4
    }
}
