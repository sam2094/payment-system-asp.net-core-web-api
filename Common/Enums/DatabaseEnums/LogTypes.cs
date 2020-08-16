using Common.Attributes;

namespace Common.Enums.DatabaseEnums
{
    public enum LogTypes : byte
    {
        [EnumDescription("Change Role")]
        CHANGE_ROLE = 1,
        [EnumDescription("Change Number")]
        CHANGE_NUMBER,
        [EnumDescription("Change Pin Code")]
        CHANGE_PIN_CODE,
        [EnumDescription("Send SMS")]
        SEND_SMS,
        [EnumDescription("Sign")]
        SIGN,
        [EnumDescription("Create certificate start")]
        CREATE_CERTIFICATE_START,
        [EnumDescription("Create certificate finish")]
        CREATE_CERTIFICATE_FINISH,
        [EnumDescription("Change Pin Code error")]
        CHANGE_PIN_CODE_ERROR,
        [EnumDescription("Send SMS error")]
        SEND_SMS_ERROR,
        [EnumDescription("Create certificate start error")]
        CREATE_CERTIFICATE_START_ERROR,
        [EnumDescription("Create certificate finish error")]
        CREATE_CERTIFICATE_FINISH_ERROR
    }
}
