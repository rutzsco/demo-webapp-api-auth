using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Demo.WebUI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(List<KeyValuePair<string, string>> headers)
        {
            Claims = new List<Claim>();
            Headers = headers;
        }
        public HomeViewModel(IEnumerable<Claim> claims, List<KeyValuePair<string, string>> headers)
        {
            Claims = claims ?? throw new ArgumentNullException(nameof(claims));
            Headers = headers;
        }

        public IEnumerable<Claim> Claims { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }

        
    }
}
