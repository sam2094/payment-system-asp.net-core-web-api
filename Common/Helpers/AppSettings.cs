namespace Common.Helpers
{
	public class AppSettings
	{
		public string Secret { get; set; }
		public static readonly string JwtScheme = "bearer ";
	}
}
