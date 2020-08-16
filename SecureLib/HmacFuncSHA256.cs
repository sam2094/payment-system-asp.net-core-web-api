using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureLib
{
    public class HmacFuncSHA256 : IEncryptFunction
    {




        public byte[] Encrypt(byte[] key ,  byte[] data)
        {
            using (HMACSHA256 hasher = new HMACSHA256(key))
            {

                return  hasher.ComputeHash(data);
            }
        }

        public bool Verify(byte[] key , byte [] data, byte[] signature)
        {
            using (HMACSHA256 hasher = new HMACSHA256(key))
            {

                byte[] hash = hasher.ComputeHash(data);

                if (signature.Length != hash.Length) return false;

                return hash.SequenceEqual(signature); 
                
            }
        }
    }
}
