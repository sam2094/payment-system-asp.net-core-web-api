using System;

namespace Common.Helpers
{
    public static class TokenGenerator
    {
        public static string CreateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string CreateUniqueNumber() {

            return String.Format("{0:d8}", (DateTime.Now.Ticks / 10) % 1000000000);
        }
    }
}
