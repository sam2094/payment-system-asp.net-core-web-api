namespace Models.Parameters.AuthParams
{
    public class PaymentAuthParam
    {
        public int  AppId  { get; set; }

        public string Ip { get; set;  }

        public string  Nonce { get; set; }

        public string  Body { get; set; }

        public string Signature { get; set; }

        public string MakeSignatureRaw()
        {
            return Nonce + AppId.ToString() + Body; 
        }
    }
}
