namespace Models.Parameters.User
{
	public class AuthorizationInput
	{
		public int PermissionId { get; set; }
		public int CurrentUserId { get; set; }
		
	}

	public class AuthorizationOutput { };
}
