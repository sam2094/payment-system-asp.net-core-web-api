using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureLib
{
    public static  class HexHelper
    {
        
        public static string ToHexString(this byte[] bytedata)
        {
            StringBuilder hex = new StringBuilder(bytedata.Length * 2);
            foreach (byte b in bytedata)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


        public static byte[] HexStringToByteArray( this string  hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        

    }
}
