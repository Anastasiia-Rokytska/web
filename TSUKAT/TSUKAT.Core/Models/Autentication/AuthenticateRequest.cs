using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.Models.Autentication
{
    public class AuthenticateRequest
    {
        public string AccessToken { get; set; }
        public string RedirectUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
