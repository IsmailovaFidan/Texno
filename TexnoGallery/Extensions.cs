using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TexnoGallery
{
    public static class Extensions
    {
        public static string HashMe(this string psw)
        {
            byte[] bytepsw = new ASCIIEncoding().GetBytes(psw);
            var getbytepsw = new SHA256Managed().ComputeHash(bytepsw);
            string haspsw = new ASCIIEncoding().GetString(getbytepsw);
            return haspsw;
        }
    }
}