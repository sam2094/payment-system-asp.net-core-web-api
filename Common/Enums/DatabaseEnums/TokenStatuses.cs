using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
	public enum TokenStatuses : byte
	{
		[EnumDescription("Active")]
		ACTIVE = 1,
		[EnumDescription("Blocked")]
		BLOCKED
	}
}
