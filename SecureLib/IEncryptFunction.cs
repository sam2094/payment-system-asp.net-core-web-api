using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureLib
{
    public interface IEncryptFunction
    {
        byte[] Encrypt( byte[] key ,byte[] data);

        bool Verify(byte[] key, byte[] data  , byte[] signature);

    }
}
