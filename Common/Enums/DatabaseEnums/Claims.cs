using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
	public enum Claims : int
	{
        // User
        [EnumDescription("Can add a new user")]
        CAN_ADD_USER = 1_0_0_0,
        [EnumDescription("Can block a user")]
        CAN_BLOCK_USER = 1_0_0_1,
        [EnumDescription("Can update user information")]
        CAN_UPDATE_USER = 1_0_0_2,
        [EnumDescription("Can get all users")]
        CAN_GET_USERS = 1_0_0_3,
        [EnumDescription("Can set or modify role on user")]
        CAN_SET_OR_MODIFY_ROLE_ON_USER = 1_0_0_4,
        [EnumDescription("Can get user details by id")]
        CAN_GET_USER_DETAILS = 1_0_0_5,
        [EnumDescription("Can get user by username")]
        CAN_GET_USER_BY_USERNAME = 1_0_0_6,
        [EnumDescription("Can update username")]
        CAN_UPDATE_USERNAME = 1_0_0_8,
        [EnumDescription("Can change password")]
        CAN_CHANGE_PASSWORD = 1_0_0_9,

        //Finance
        [EnumDescription("Can get active currencies")]
        CAN_GET_ACTIVE_CURRENCIES = 1_2_0_0,
        [EnumDescription("Can get per trasactions's Min and Max")]
        CAN_GET_PER_TRANSACTION_MIN_MAX = 1_2_0_1,
        [EnumDescription("Can create invoice")]
        CAN_CREATE_INVOICE = 1_2_0_2,
        [EnumDescription("Can get invoices for current mobile user")]
        CAN_GET_INVOICES_FOR_CURRENT_MOBILE_USER = 1_2_0_3,


        //Payment
        [EnumDescription("Can get active currencies")]
        CAN_VERIFY_TOKEN = 1_3_0_0,
       
    }
}
