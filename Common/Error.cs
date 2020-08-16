using Common.Enums.ErrorEnums;

namespace Common
{
	public sealed class Error
	{
		public ErrorHttpStatus StatusCode { get; set; }
		public ErrorCodes ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
