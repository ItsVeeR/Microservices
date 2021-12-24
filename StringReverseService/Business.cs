using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringReverseService
{
    public static class Business
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
