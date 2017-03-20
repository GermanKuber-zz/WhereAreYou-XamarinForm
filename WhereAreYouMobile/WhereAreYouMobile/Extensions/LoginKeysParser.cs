using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WhereAreYouMobile.Extensions
{
   public  static class LoginKeysParser
    {
        public  static string GetKeyValue(this JObject profile, string key)
        {
            var value = profile[key];
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
